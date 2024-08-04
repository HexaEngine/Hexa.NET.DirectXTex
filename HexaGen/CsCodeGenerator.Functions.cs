﻿namespace HexaGen
{
    using CppAst;
    using Generator;
    using HexaGen.Core;
    using HexaGen.Core.CSharp;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public enum WriteFunctionFlags
    {
        None = 0,
        UseHandle = 1,
        UseThis = 2,
        Extension = 4,
        COM = 8,
    }

    public partial class CsCodeGenerator
    {
        protected readonly HashSet<string> LibDefinedFunctions = new();
        public readonly HashSet<string> DefinedFunctions = new();
        protected readonly HashSet<string> DefinedVariationsFunctions = new();
        protected readonly HashSet<string> OutReturnFunctions = new();

        protected virtual List<string> SetupFunctionUsings()
        {
            List<string> usings = new() { "System", "System.Runtime.CompilerServices", "System.Runtime.InteropServices", "HexaGen.Runtime" };
            usings.AddRange(settings.Usings);
            return usings;
        }

        protected virtual bool FilterFunctionIgnored(GenContext context, CppFunction cppFunction)
        {
            if (!cppFunction.IsPublicExport())
            {
                return true;
            }

            if (cppFunction.Flags == CppFunctionFlags.Inline)
            {
                return true;
            }

            if (settings.AllowedFunctions.Count != 0 && !settings.AllowedFunctions.Contains(cppFunction.Name))
            {
                return true;
            }

            if (settings.IgnoredFunctions.Contains(cppFunction.Name))
            {
                return true;
            }

            return false;
        }

        protected virtual bool FilterNativeFunction(GenContext context, CppFunction cppFunction, string header)
        {
            if (LibDefinedFunctions.Contains(header))
            {
                return true;
            }

            if (DefinedFunctions.Contains(header))
            {
                LogWarn($"{context.FilePath}: function {cppFunction}, C#: {header} is already defined!");
                return true;
            }

            DefinedFunctions.Add(header);

            return false;
        }

        protected virtual bool FilterFunction(GenContext context, HashSet<string> definedFunctions, string header)
        {
            if (definedFunctions.Contains(header))
            {
                LogWarn($"{context.FilePath}: {header} function is already defined!");
                return true;
            }
            definedFunctions.Add(header);
            return false;
        }

        protected virtual void GenerateFunctions(CppCompilation compilation, string outputPath)
        {
            string filePath = Path.Combine(outputPath, "Functions.cs");
            DefinedVariationsFunctions.Clear();

            // Generate Functions
            using var writer = new CsSplitCodeWriter(filePath, settings.Namespace, SetupFunctionUsings(), settings.HeaderInjectorCallback);
            GenContext context = new(compilation, filePath, writer);

            VTableBuilder vTableBuilder = new();
            using (writer.PushBlock($"public unsafe partial class {settings.ApiName}"))
            {
                writer.WriteLine($"internal const string LibName = \"{settings.LibName}\";\n");
                List<CsFunction> functions = new();
                for (int i = 0; i < compilation.Functions.Count; i++)
                {
                    CppFunction? cppFunction = compilation.Functions[i];
                    if (FilterFunctionIgnored(context, cppFunction))
                    {
                        continue;
                    }

                    string? csName = settings.GetPrettyFunctionName(cppFunction.Name);
                    string returnCsName = settings.GetCsTypeName(cppFunction.ReturnType, false);
                    CppPrimitiveKind returnKind = cppFunction.ReturnType.GetPrimitiveKind();

                    bool boolReturn = returnCsName == "bool";
                    bool canUseOut = OutReturnFunctions.Contains(cppFunction.Name);
                    var argumentsString = settings.GetParameterSignature(cppFunction.Parameters, canUseOut, settings.GenerateMetadata);
                    var headerId = $"{csName}({settings.GetParameterSignature(cppFunction.Parameters, canUseOut, false, false)})";
                    var header = $"{returnCsName} {csName}Native({argumentsString})";

                    if (FilterNativeFunction(context, cppFunction, headerId))
                    {
                        continue;
                    }

                    settings.WriteCsSummary(cppFunction.Comment, writer);
                    if (settings.GenerateMetadata)
                    {
                        writer.WriteLine($"[NativeName(NativeNameType.Func, \"{cppFunction.Name}\")]");
                        writer.WriteLine($"[return: NativeName(NativeNameType.Type, \"{cppFunction.ReturnType.GetDisplayName()}\")]");
                    }

                    string? modifierString = null;

                    switch (settings.ImportType)
                    {
                        case ImportType.DllImport:
                            writer.WriteLine($"[LibraryImport(LibName, EntryPoint = \"{cppFunction.Name}\")]");
                            writer.WriteLine($"[UnmanagedCallConv(CallConvs = new Type[] {{typeof({cppFunction.CallingConvention.GetCallingConventionLibrary()})}})]");
                            modifierString = "internal static partial";
                            break;

                        case ImportType.LibraryImport:
                            writer.WriteLine($"[DllImport(LibName, CallingConvention = CallingConvention.{cppFunction.CallingConvention.GetCallingConvention()}, EntryPoint = \"{cppFunction.Name}\")]");
                            modifierString = "internal static extern";
                            break;

                        case ImportType.VTable:
                            if (boolReturn)
                            {
                                writer.BeginBlock($"internal static byte {csName}Native({argumentsString})");
                            }
                            else
                            {
                                writer.BeginBlock($"internal static {header}");
                            }

                            string returnType = settings.GetCsTypeName(cppFunction.ReturnType);
                            if (returnType == "bool")
                            {
                                returnType = settings.GetBoolType();
                            }
                            string delegateType;
                            if (cppFunction.Parameters.Count == 0)
                            {
                                delegateType = $"delegate* unmanaged[{cppFunction.CallingConvention.GetCallingConventionDelegate()}]<{returnType}>";
                            }
                            else
                            {
                                delegateType = $"delegate* unmanaged[{cppFunction.CallingConvention.GetCallingConventionDelegate()}]<{settings.GetNamelessParameterSignature(cppFunction.Parameters, false, true)}, {returnType}>";
                            }

                            writer.WriteLine("#if NET5_0_OR_GREATER");
                            // isolates the argument names
                            string argumentNames = WriteFunctionMarshalling(cppFunction.Parameters);

                            int vTableIndex = vTableBuilder.Add(cppFunction.Name);

                            if (returnCsName == "void")
                            {
                                writer.WriteLine($"(({delegateType})vt[{vTableIndex}])({argumentNames});");
                            }
                            else
                            {
                                writer.WriteLine($"return (({delegateType})vt[{vTableIndex}])({argumentNames});");
                            }

                            writer.WriteLine("#else");

                            string returnTypeOld = settings.GetCsTypeName(cppFunction.ReturnType);
                            if (returnTypeOld == "bool")
                            {
                                returnTypeOld = settings.GetBoolType();
                            }
                            if (returnTypeOld.Contains('*'))
                            {
                                returnTypeOld = "nint";
                            }
                            string delegateTypeOld;
                            if (cppFunction.Parameters.Count == 0)
                            {
                                delegateTypeOld = $"delegate* unmanaged[{cppFunction.CallingConvention.GetCallingConventionDelegate()}]<{returnTypeOld}>";
                            }
                            else
                            {
                                delegateTypeOld = $"delegate* unmanaged[{cppFunction.CallingConvention.GetCallingConventionDelegate()}]<{settings.GetNamelessParameterSignature(cppFunction.Parameters, false, true, compatibility: true)}, {returnTypeOld}>";
                            }

                            string argumentNamesOld = WriteFunctionMarshalling(cppFunction.Parameters, compatibility: true);

                            if (returnCsName == "void")
                            {
                                writer.WriteLine($"(({delegateTypeOld})vt[{vTableIndex}])({argumentNamesOld});");
                            }
                            else
                            {
                                writer.WriteLine($"return ({returnType})(({delegateTypeOld})vt[{vTableIndex}])({argumentNamesOld});");
                            }

                            writer.WriteLine("#endif");

                            writer.EndBlock();
                            break;
                    }

                    if (modifierString != null)
                    {
                        if (boolReturn)
                        {
                            writer.WriteLine($"{modifierString} {settings.GetBoolType()} {csName}Native({argumentsString});");
                        }
                        else
                        {
                            writer.WriteLine($"{modifierString} {header};");
                        }
                    }
                    writer.WriteLine();

                    var function = CreateCsFunction(cppFunction, csName, functions, out var overload);
                    overload.Modifiers.Add("public");
                    overload.Modifiers.Add("static");
                    funcGen.GenerateVariations(cppFunction.Parameters, overload, false);
                    WriteFunctions(context, DefinedVariationsFunctions, function, overload, WriteFunctionFlags.None, "public static");
                }
            }

            if (settings.UseVTable)
            {
                var initString = vTableBuilder.Finish(out var count);
                string filePathVT = Path.Combine(outputPath, "Functions.VT.cs");
                using var writerVt = new CsCodeWriter(filePathVT, settings.Namespace, SetupFunctionUsings(), settings.HeaderInjectorCallback);
                using (writerVt.PushBlock($"public unsafe partial class {settings.ApiName}"))
                {
                    writerVt.WriteLine("internal static VTable vt;");
                    writerVt.WriteLine();
                    using (writerVt.PushBlock("public static void InitApi()"))
                    {
                        writerVt.WriteLine($"vt = new VTable(GetLibraryName(), {count});");
                        writerVt.WriteLines(initString);
                    }
                    writerVt.WriteLine();
                    using (writerVt.PushBlock("public static void FreeApi()"))
                    {
                        writerVt.WriteLine("vt.Free();");
                    }
                }
            }
        }

        protected virtual void WriteFunctions(GenContext context, HashSet<string> definedFunctions, CsFunction csFunction, CsFunctionOverload overload, WriteFunctionFlags flags, params string[] modifiers)
        {
            for (int j = 0; j < overload.Variations.Count; j++)
            {
                WriteFunction(context, definedFunctions, csFunction, overload, overload.Variations[j], flags, modifiers);
            }
        }

        protected virtual string BuildFunctionSignature(CsFunctionVariation variation, bool useAttributes, bool useNames, WriteFunctionFlags flags)
        {
            int offset = flags == WriteFunctionFlags.None ? 0 : 1;
            StringBuilder sb = new();
            bool isFirst = true;

            if (flags == WriteFunctionFlags.Extension)
            {
                isFirst = false;
                var first = variation.Parameters[0];
                sb.Append($"this {first.Type} {(useNames ? first.Name : string.Empty)}");
            }

            for (int i = offset; i < variation.Parameters.Count; i++)
            {
                var param = variation.Parameters[i];

                if (param.DefaultValue != null)
                    continue;

                if (!isFirst)
                    sb.Append(", ");

                sb.Append($"{(useAttributes ? string.Join(" ", param.Attributes) : string.Empty)} {param.Type} {(useNames ? param.Name : string.Empty)}");
                isFirst = false;
            }

            return sb.ToString();
        }

        protected virtual string BuildFunctionHeaderId(CsFunctionVariation variation, WriteFunctionFlags flags)
        {
            string signature = BuildFunctionSignature(variation, false, false, flags);
            return $"{variation.Name}({signature})";
        }

        protected virtual string BuildFunctionHeader(CsFunctionVariation variation, CsType csReturnType, WriteFunctionFlags flags, bool generateMetadata)
        {
            string signature = BuildFunctionSignature(variation, generateMetadata, true, flags);
            return $"{csReturnType.Name} {variation.Name}({signature})";
        }

        protected virtual void WriteFunction(GenContext context, HashSet<string> definedFunctions, CsFunction function, CsFunctionOverload overload, CsFunctionVariation variation, WriteFunctionFlags flags, params string[] modifiers)
        {
            var writer = context.Writer;
            CsType csReturnType = variation.ReturnType;
            PrepareArgs(variation, csReturnType);

            string header = BuildFunctionHeader(variation, csReturnType, flags, settings.GenerateMetadata);
            string id = BuildFunctionHeaderId(variation, flags);

            if (FilterFunction(context, definedFunctions, id))
            {
                return;
            }

            ClassifyParameters(overload, variation, csReturnType, out bool firstParamReturn, out int offset, out bool hasManaged);

            LogInfo("defined function " + header);

            writer.WriteLines(overload.Comment);
            if (settings.GenerateMetadata)
            {
                writer.WriteLines(overload.Attributes);
            }
            using (writer.PushBlock($"{string.Join(" ", modifiers)} {header}"))
            {
                StringBuilder sb = new();

                if (!firstParamReturn && (!csReturnType.IsVoid || csReturnType.IsVoid && csReturnType.IsPointer))
                {
                    if (csReturnType.IsBool && !csReturnType.IsPointer && !hasManaged)
                    {
                        sb.Append($"{settings.GetBoolType()} ret = ");
                    }
                    else
                    {
                        sb.Append($"{csReturnType.Name} ret = ");
                    }
                }

                if (csReturnType.IsString)
                {
                    WriteStringConvertToManaged(sb, variation.ReturnType);
                }

                if (flags != WriteFunctionFlags.None)
                {
                    sb.Append($"{settings.ApiName}.");
                }

                if (hasManaged)
                {
                    sb.Append($"{overload.Name}(");
                }
                else if (firstParamReturn)
                {
                    sb.Append($"{overload.Name}Native(&ret" + (overload.Parameters.Count > 1 ? ", " : ""));
                }
                else
                {
                    sb.Append($"{overload.Name}Native(");
                }

                Stack<(string, CsParameterInfo, string)> strings = new();
                Stack<string> stringArrays = new();
                int stringCounter = 0;
                int blockCounter = 0;

                for (int i = 0; i < overload.Parameters.Count - offset; i++)
                {
                    var cppParameter = overload.Parameters[i + offset];
                    var paramFlags = ParameterFlags.None;

                    if (variation.TryGetParameter(cppParameter.Name, out var param))
                    {
                        paramFlags = param.Flags;
                        cppParameter = param;
                    }

                    if (flags.HasFlag(WriteFunctionFlags.UseHandle) && i == 0)
                    {
                        sb.Append("Handle");
                    }
                    else if (flags.HasFlag(WriteFunctionFlags.UseThis) && i == 0 && overload.Parameters[i].Type.IsPointer)
                    {
                        writer.BeginBlock($"fixed ({overload.Parameters[i].Type.Name} @this = &this)");
                        sb.Append("@this");
                        blockCounter++;
                    }
                    else if (flags.HasFlag(WriteFunctionFlags.UseThis) && i == 0)
                    {
                        sb.Append("this");
                    }
                    else if (paramFlags.HasFlag(ParameterFlags.Default))
                    {
                        var rootParam = overload.Parameters[i + offset];
                        var paramCsDefault = cppParameter.DefaultValue;
                        if (cppParameter.Type.IsString || paramCsDefault.StartsWith("\"") && paramCsDefault.EndsWith("\""))
                        {
                            sb.Append($"(string){paramCsDefault}");
                        }
                        else if (cppParameter.Type.IsBool && !cppParameter.Type.IsPointer && !cppParameter.Type.IsArray)
                        {
                            sb.Append($"({settings.GetBoolType()})({paramCsDefault})");
                        }
                        else if (rootParam.Type.IsEnum || cppParameter.Type.IsPrimitive || cppParameter.Type.IsPointer || cppParameter.Type.IsArray)
                        {
                            sb.Append($"({rootParam.Type.Name})({paramCsDefault})");
                        }
                        else
                        {
                            sb.Append($"{paramCsDefault}");
                        }
                    }
                    else if (paramFlags.HasFlag(ParameterFlags.String))
                    {
                        if (paramFlags.HasFlag(ParameterFlags.Array))
                        {
                            WriteStringArrayConvertToUnmanaged(writer, cppParameter.Type, cppParameter.Name, stringArrays.Count);
                            sb.Append($"pStrArray{stringArrays.Count}");
                            stringArrays.Push(cppParameter.Name);
                        }
                        else
                        {
                            if (paramFlags.HasFlag(ParameterFlags.Ref))
                            {
                                strings.Push((cppParameter.Name, cppParameter, $"pStr{stringCounter}"));
                            }

                            WriteStringConvertToUnmanaged(writer, cppParameter.Type, cppParameter.Name, stringCounter);
                            sb.Append($"pStr{stringCounter}");
                            stringCounter++;
                        }
                    }
                    else if (paramFlags.HasFlag(ParameterFlags.Ref))
                    {
                        writer.BeginBlock($"fixed ({cppParameter.Type.CleanName}* p{cppParameter.CleanName} = &{cppParameter.Name})");
                        sb.Append($"({overload.Parameters[i + offset].Type.Name})p{cppParameter.CleanName}");
                        blockCounter++;
                    }
                    else if (paramFlags.HasFlag(ParameterFlags.Array))
                    {
                        writer.BeginBlock($"fixed ({cppParameter.Type.CleanName}* p{cppParameter.CleanName} = {cppParameter.Name})");
                        sb.Append($"({overload.Parameters[i + offset].Type.Name})p{cppParameter.CleanName}");
                        blockCounter++;
                    }
                    else if (paramFlags.HasFlag(ParameterFlags.Bool) && !paramFlags.HasFlag(ParameterFlags.Ref) && !paramFlags.HasFlag(ParameterFlags.Pointer))
                    {
                        sb.Append($"{cppParameter.Name} ? ({settings.GetBoolType()})1 : ({settings.GetBoolType()})0");
                    }
                    else
                    {
                        sb.Append(cppParameter.Name);
                    }

                    if (i != overload.Parameters.Count - 1 - offset)
                    {
                        sb.Append(", ");
                    }
                }

                if (csReturnType.IsString)
                {
                    sb.Append("));");
                }
                else
                {
                    sb.Append(");");
                }

                if (firstParamReturn)
                {
                    writer.WriteLine($"{csReturnType.Name} ret;");
                }
                writer.WriteLine(sb.ToString());

                while (strings.TryPop(out var stackItem))
                {
                    WriteStringConvertToManaged(writer, stackItem.Item2.Type, stackItem.Item1, stackItem.Item3);
                }

                while (stringArrays.TryPop(out var arrayName))
                {
                    WriteFreeUnmanagedStringArray(writer, arrayName, stringArrays.Count);
                }

                while (stringCounter > 0)
                {
                    stringCounter--;
                    WriteFreeString(writer, stringCounter);
                }

                if (firstParamReturn || !csReturnType.IsVoid || csReturnType.IsVoid && csReturnType.IsPointer)
                {
                    if (csReturnType.IsBool && !csReturnType.IsPointer && !hasManaged)
                    {
                        writer.WriteLine("return ret != 0;");
                    }
                    else
                    {
                        writer.WriteLine("return ret;");
                    }
                }

                while (blockCounter > 0)
                {
                    blockCounter--;
                    writer.EndBlock();
                }
            }

            writer.WriteLine();
        }

        protected static void ClassifyParameters(CsFunctionOverload overload, CsFunctionVariation variation, CsType csReturnType, out bool firstParamReturn, out int offset, out bool hasManaged)
        {
            firstParamReturn = false;
            if (!csReturnType.IsString && csReturnType.Name != overload.ReturnType.Name)
            {
                firstParamReturn = true;
            }

            offset = firstParamReturn ? 1 : 0;
            hasManaged = false;
            for (int j = 0; j < variation.Parameters.Count - offset; j++)
            {
                var cppParameter = variation.Parameters[j + offset];

                if (cppParameter.DefaultValue == null)
                {
                    continue;
                }

                var paramCsDefault = cppParameter.DefaultValue;
                if (cppParameter.Type.IsString || paramCsDefault.StartsWith("\"") && paramCsDefault.EndsWith("\""))
                {
                    hasManaged = true;
                }
            }
        }

        protected static void WriteStringConvertToManaged(StringBuilder sb, CppType type)
        {
            CppPrimitiveKind primitiveKind = type.GetPrimitiveKind();
            if (primitiveKind == CppPrimitiveKind.Char)
            {
                sb.Append("Utils.DecodeStringUTF8(");
            }
            else if (primitiveKind == CppPrimitiveKind.WChar)
            {
                sb.Append("Utils.DecodeStringUTF16(");
            }
            else
            {
                throw new NotSupportedException($"String type ({primitiveKind}) is not supported");
            }
        }

        protected static void WriteStringConvertToManaged(StringBuilder sb, CsType type)
        {
            if (type.StringType == CsStringType.StringUTF8)
            {
                sb.Append("Utils.DecodeStringUTF8(");
            }
            else if (type.StringType == CsStringType.StringUTF16)
            {
                sb.Append("Utils.DecodeStringUTF16(");
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        protected static void WriteStringConvertToManaged(ICodeWriter writer, CppType type, string variable, string pointer)
        {
            CppPrimitiveKind primitiveKind = type.GetPrimitiveKind();
            if (primitiveKind == CppPrimitiveKind.Char)
            {
                writer.WriteLine($"{variable} = Marshal.DecodeStringUTF8({pointer});");
            }
            else if (primitiveKind == CppPrimitiveKind.WChar)
            {
                writer.WriteLine($"{variable} = Marshal.DecodeStringUTF16({pointer});");
            }
            else
            {
                throw new NotSupportedException($"String type ({primitiveKind}) is not supported");
            }
        }

        protected static void WriteStringConvertToManaged(ICodeWriter writer, CsType type, string variable, string pointer)
        {
            if (type.StringType == CsStringType.StringUTF8)
            {
                writer.WriteLine($"{variable} = Utils.DecodeStringUTF8({pointer});");
            }
            else if (type.StringType == CsStringType.StringUTF16)
            {
                writer.WriteLine($"{variable} = Utils.DecodeStringUTF16({pointer});");
            }
            else
            {
                throw new NotSupportedException($"String type ({type.StringType}) is not supported");
            }
        }

        protected static void WriteStringConvertToUnmanaged(ICodeWriter writer, CppType type, string name, int i)
        {
            CppPrimitiveKind primitiveKind = type.GetPrimitiveKind();
            if (primitiveKind == CppPrimitiveKind.Char)
            {
                writer.WriteLine($"byte* pStr{i} = null;");
                writer.WriteLine($"int pStrSize{i} = 0;");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    writer.WriteLine($"pStrSize{i} = Utils.GetByteCountUTF8({name});");
                    using (writer.PushBlock($"if (pStrSize{i} >= Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStr{i} = Utils.Alloc<byte>(pStrSize{i} + 1);");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrStack{i} = stackalloc byte[pStrSize{i} + 1];");
                        writer.WriteLine($"pStr{i} = pStrStack{i};");
                    }
                    writer.WriteLine($"int pStrOffset{i} = Utils.EncodeStringUTF8({name}, pStr{i}, pStrSize{i});");
                    writer.WriteLine($"pStr{i}[pStrOffset{i}] = 0;");
                }
            }
            else if (primitiveKind == CppPrimitiveKind.WChar)
            {
                writer.WriteLine($"char* pStr{i} = null;");
                writer.WriteLine($"int pStrSize{i} = 0;");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    writer.WriteLine($"pStrSize{i} = Utils.GetByteCountUTF16({name});");
                    using (writer.PushBlock($"if (pStrSize{i} >= Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStr{i} = (char*)Utils.Alloc<byte>(pStrSize{i} + 2);");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrStack{i} = stackalloc byte[pStrSize{i} + 2];");
                        writer.WriteLine($"pStr{i} = (char*)pStrStack{i};");
                    }
                    writer.WriteLine($"int pStrOffset{i} = Utils.EncodeStringUTF16({name}, pStr{i}, pStrSize{i}) / 2;");
                    writer.WriteLine($"pStr{i}[pStrOffset{i}] = '\\0';");
                }
            }
            else
            {
                throw new NotSupportedException($"String type ({primitiveKind}) is not supported");
            }
        }

        protected static void WriteStringConvertToUnmanaged(ICodeWriter writer, CsType type, string name, int i)
        {
            if (type.StringType == CsStringType.StringUTF8)
            {
                writer.WriteLine($"byte* pStr{i} = null;");
                writer.WriteLine($"int pStrSize{i} = 0;");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    writer.WriteLine($"pStrSize{i} = Utils.GetByteCountUTF8({name});");
                    using (writer.PushBlock($"if (pStrSize{i} >= Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStr{i} = Utils.Alloc<byte>(pStrSize{i} + 1);");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrStack{i} = stackalloc byte[pStrSize{i} + 1];");
                        writer.WriteLine($"pStr{i} = pStrStack{i};");
                    }
                    writer.WriteLine($"int pStrOffset{i} = Utils.EncodeStringUTF8({name}, pStr{i}, pStrSize{i});");
                    writer.WriteLine($"pStr{i}[pStrOffset{i}] = 0;");
                }
            }
            else if (type.StringType == CsStringType.StringUTF16)
            {
                writer.WriteLine($"char* pStr{i} = null;");
                writer.WriteLine($"int pStrSize{i} = 0;");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    writer.WriteLine($"pStrSize{i} = Utils.GetByteCountUTF16({name});");
                    using (writer.PushBlock($"if (pStrSize{i} >= Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStr{i} = (char*)Utils.Alloc<byte>(pStrSize{i} + 2);");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrStack{i} = stackalloc byte[pStrSize{i} + 2];");
                        writer.WriteLine($"pStr{i} = (char*)pStrStack{i};");
                    }
                    writer.WriteLine($"int pStrOffset{i} = Utils.EncodeStringUTF16({name}, pStr{i}, pStrSize{i}) / 2;");
                    writer.WriteLine($"pStr{i}[pStrOffset{i}] = '\\0';");
                }
            }
            else
            {
                throw new NotSupportedException($"String type ({type.StringType}) is not supported");
            }
        }

        protected static void WriteFreeString(ICodeWriter writer, int i)
        {
            using (writer.PushBlock($"if (pStrSize{i} >= Utils.MaxStackallocSize)"))
            {
                writer.WriteLine($"Utils.Free(pStr{i});");
            }
        }

        protected static void WriteStringArrayConvertToUnmanaged(ICodeWriter writer, CppType type, string name, int i)
        {
            CppPrimitiveKind primitiveKind = type.GetPrimitiveKind();
            if (primitiveKind == CppPrimitiveKind.Char)
            {
                writer.WriteLine($"byte** pStrArray{i} = null;");
                writer.WriteLine($"int pStrArraySize{i} = Utils.GetByteCountArray({name});");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    using (writer.PushBlock($"if (pStrArraySize{i} > Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStrArray{i} = (byte**)Utils.Alloc<byte>(pStrArraySize{i});");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrArrayStack{i} = stackalloc byte[pStrArraySize{i}];");
                        writer.WriteLine($"pStrArray{i} = (byte**)pStrArrayStack{i};");
                    }
                }
                using (writer.PushBlock($"for (int i = 0; i < {name}.Length; i++)"))
                {
                    writer.WriteLine($"pStrArray{i}[i] = (byte*)Utils.StringToUTF8Ptr({name}[i]);");
                }
            }
            else if (primitiveKind == CppPrimitiveKind.WChar)
            {
                writer.WriteLine($"char** pStrArray{i} = null;");
                writer.WriteLine($"int pStrArraySize{i} = Utils.GetByteCountArray({name});");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    using (writer.PushBlock($"if (pStrArraySize{i} > Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStrArray{i} = (char**)Utils.Alloc<byte>(pStrArraySize{i});");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrArrayStack{i} = stackalloc byte[pStrArraySize{i}];");
                        writer.WriteLine($"pStrArray{i} = (char**)pStrArrayStack{i};");
                    }
                }
                using (writer.PushBlock($"for (int i = 0; i < {name}.Length; i++)"))
                {
                    writer.WriteLine($"pStrArray{i}[i] = (char*)Utils.StringToUTF16Ptr({name}[i]);");
                }
            }
            else
            {
                throw new NotSupportedException($"String type ({primitiveKind}) is not supported");
            }
        }

        protected static void WriteStringArrayConvertToUnmanaged(ICodeWriter writer, CsType type, string name, int i)
        {
            if (type.StringType == CsStringType.StringUTF8)
            {
                writer.WriteLine($"byte** pStrArray{i} = null;");
                writer.WriteLine($"int pStrArraySize{i} = Utils.GetByteCountArray({name});");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    using (writer.PushBlock($"if (pStrArraySize{i} > Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStrArray{i} = (byte**)Utils.Alloc<byte>(pStrArraySize{i});");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrArrayStack{i} = stackalloc byte[pStrArraySize{i}];");
                        writer.WriteLine($"pStrArray{i} = (byte**)pStrArrayStack{i};");
                    }
                }
                using (writer.PushBlock($"for (int i = 0; i < {name}.Length; i++)"))
                {
                    writer.WriteLine($"pStrArray{i}[i] = (byte*)Utils.StringToUTF8Ptr({name}[i]);");
                }
            }
            else if (type.StringType == CsStringType.StringUTF16)
            {
                writer.WriteLine($"char** pStrArray{i} = null;");
                writer.WriteLine($"int pStrArraySize{i} = Utils.GetByteCountArray({name});");
                using (writer.PushBlock($"if ({name} != null)"))
                {
                    using (writer.PushBlock($"if (pStrArraySize{i} > Utils.MaxStackallocSize)"))
                    {
                        writer.WriteLine($"pStrArray{i} = (char**)Utils.Alloc<byte>(pStrArraySize{i});");
                    }
                    using (writer.PushBlock("else"))
                    {
                        writer.WriteLine($"byte* pStrArrayStack{i} = stackalloc byte[pStrArraySize{i}];");
                        writer.WriteLine($"pStrArray{i} = (char**)pStrArrayStack{i};");
                    }
                }
                using (writer.PushBlock($"for (int i = 0; i < {name}.Length; i++)"))
                {
                    writer.WriteLine($"pStrArray{i}[i] = (char*)Utils.StringToUTF16Ptr({name}[i]);");
                }
            }
            else
            {
                throw new NotSupportedException($"String type ({type.StringType}) is not supported");
            }
        }

        protected static void WriteFreeUnmanagedStringArray(ICodeWriter writer, string name, int i)
        {
            using (writer.PushBlock($"for (int i = 0; i < {name}.Length; i++)"))
            {
                writer.WriteLine($"Utils.Free(pStrArray{i}[i]);");
            }
            using (writer.PushBlock($"if (pStrArraySize{i} >= Utils.MaxStackallocSize)"))
            {
                writer.WriteLine($"Utils.Free(pStrArray{i});");
            }
        }

        protected string WriteFunctionMarshalling(IList<CppParameter> parameters, bool compatibility = false)
        {
            var argumentBuilder = new StringBuilder();
            int index = 0;

            for (int i = 0; i < parameters.Count; i++)
            {
                CppParameter cppParameter = parameters[i];
                string direction = string.Empty;
                var paramCsTypeName = settings.GetCsTypeName(cppParameter.Type, false);
                var paramCsName = settings.GetParameterName(i, cppParameter.Name);

                CppType ptrType = cppParameter.Type;
                int depth = 0;
                if (cppParameter.Type.IsPointer(ref depth, out var pointerType))
                {
                    ptrType = pointerType;
                }

                if (cppParameter.Type is CppQualifiedType qualifiedType)
                {
                    ptrType = qualifiedType.ElementType;
                }

                if (ptrType is CppTypedef typedef && typedef.ElementType.IsDelegate(out var cppFunction))
                {
                    if (cppFunction.Parameters.Count == 0)
                    {
                        paramCsTypeName = $"delegate*<{settings.GetCsTypeName(cppFunction.ReturnType)}>";
                    }
                    else
                    {
                        paramCsTypeName = $"delegate*<{settings.GetNamelessParameterSignature(cppFunction.Parameters, false, true)}, {settings.GetCsTypeName(cppFunction.ReturnType)}>";
                    }

                    while (depth-- > 0)
                    {
                        paramCsTypeName += "*";
                    }

                    if (compatibility && paramCsTypeName.Contains('*'))
                    {
                        paramCsTypeName = "nint";
                    }

                    argumentBuilder.Append($"({paramCsTypeName})Utils.GetFunctionPointerForDelegate({paramCsName})");
                }
                else
                {
                    if (compatibility && paramCsTypeName.Contains('*'))
                    {
                        argumentBuilder.Append($"(nint){paramCsName}");
                    }
                    else
                    {
                        argumentBuilder.Append(paramCsName);
                    }
                }

                if (index < parameters.Count - 1)
                {
                    argumentBuilder.Append(", ");
                }

                index++;
            }

            return argumentBuilder.ToString();
        }
    }
}