namespace Generator
{
    using ClangSharp;
    using CppAst;
    using System.IO;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;

    public static partial class CsCodeGenerator
    {
        private static void GenerateStructAndUnions(CppCompilation compilation, string outputPath)
        {
            // Generate Structures
            using var writer = new CodeWriter(Path.Combine(outputPath, "Structures.cs"),
                "System",
                "System.Runtime.InteropServices"
                );

            // Print All classes, structs
            foreach (CppClass? cppClass in compilation.Classes)
            {
                if (cppClass.ClassKind == CppClassKind.Class ||
                    cppClass.SizeOf == 0 ||
                    cppClass.Name.EndsWith("_T"))
                {
                    continue;
                }

                bool isUnion = cppClass.ClassKind == CppClassKind.Union;

                string csName = GetCsCleanName(cppClass.Name);
                if (isUnion)
                {
                    writer.WriteLine("[StructLayout(LayoutKind.Explicit)]");
                }
                else
                {
                    writer.WriteLine("[StructLayout(LayoutKind.Sequential)]");
                }

                bool isReadOnly = false;
                string modifier = "partial";

                WriteCsSummary(cppClass.Comment, writer);
                using (writer.PushBlock($"public {modifier} struct {csName}"))
                {
                    if (CsCodeGeneratorSettings.Default.GenerateSizeOfStructs && cppClass.SizeOf > 0)
                    {
                        writer.WriteLine("/// <summary>");
                        writer.WriteLine($"/// The size of the <see cref=\"{csName}\"/> type, in bytes.");
                        writer.WriteLine("/// </summary>");
                        writer.WriteLine($"public static readonly int SizeInBytes = {cppClass.SizeOf};");
                        writer.WriteLine();
                    }

                    foreach (CppField cppField in cppClass.Fields)
                    {
                        WriteField(writer, cppField, isUnion, isReadOnly);
                    }

                    if (CsCodeGeneratorSettings.Default.KnownStructMethods.TryGetValue(cppClass.Name, out var functions))
                    {
                        writer.WriteLine();

                        for (int i = 0; i < functions.Count; i++)
                        {
                            CppFunction cppFunction = FindFunction(compilation, functions[i]);
                            var csFunctionName = GetCsCleanName(cppFunction.Name);
                            bool canUseOut = s_outReturnFunctions.Contains(cppFunction.Name);
                            var argumentsString = GetStructParameterSignature(cppClass, cppFunction.Parameters, canUseOut);
                            var sigs = GetStructVariantParameterSignatures(cppClass, cppFunction.Parameters, argumentsString, canUseOut);
                            sigs.Add(argumentsString);

                            WriteStructMethods(writer, cppFunction, cppClass, csName, csFunctionName, sigs);
                        }
                    }
                }

                writer.WriteLine();
            }
        }

        private static void WriteField(CodeWriter writer, CppField field, bool isUnion = false, bool isReadOnly = false)
        {
            string csFieldName = NormalizeFieldName(field.Name);
            WriteCsSummary(field.Comment, writer);
            if (isUnion)
            {
                writer.WriteLine("[FieldOffset(0)]");
            }

            if (field.Type is CppArrayType arrayType)
            {
                bool canUseFixed = false;
                if (arrayType.ElementType is CppPrimitiveType)
                {
                    canUseFixed = true;
                }
                else if (arrayType.ElementType is CppTypedef typedef
                    && typedef.ElementType is CppPrimitiveType)
                {
                    canUseFixed = true;
                }

                if (canUseFixed)
                {
                    string csFieldType = GetCsTypeName(arrayType.ElementType, false);
                    writer.WriteLine($"public unsafe fixed {csFieldType} {csFieldName}[{arrayType.Size}];");
                }
                else
                {
                    string unsafePrefix = string.Empty;
                    string csFieldType = GetCsTypeName(arrayType.ElementType, false);
                    if (csFieldType.EndsWith('*'))
                    {
                        unsafePrefix = "unsafe ";
                    }

                    for (int i = 0; i < arrayType.Size; i++)
                    {
                        writer.WriteLine($"public {unsafePrefix}{csFieldType} {csFieldName}_{i};");
                    }
                }
            }
            else
            {
                // VkAllocationCallbacks members
                if (field.Type is CppTypedef typedef &&
                    typedef.ElementType is CppPointerType pointerType &&
                    pointerType.ElementType is CppFunctionType functionType)
                {
                    StringBuilder builder = new();
                    foreach (CppParameter parameter in functionType.Parameters)
                    {
                        string paramCsType = GetCsTypeName(parameter.Type, false);
                        // Otherwise we get interop issues with non blittable types

                        builder.Append(paramCsType).Append(", ");
                    }

                    string returnCsName = GetCsTypeName(functionType.ReturnType, false);

                    builder.Append(returnCsName);

                    return;
                }

                string csFieldType = GetCsTypeName(field.Type, false);

                string fieldPrefix = isReadOnly ? "readonly " : string.Empty;
                if (csFieldType.EndsWith('*'))
                {
                    fieldPrefix += "unsafe ";
                }

                writer.WriteLine($"public {fieldPrefix}{csFieldType} {csFieldName};");
            }
        }

        private static void WriteStructMethods(CodeWriter writer, CppFunction cppFunction, CppClass cppClass, string structName, string command, List<string> signatures)
        {
            bool thisRef = false;
            if (cppFunction.Parameters.Count > 0 && IsPointerOf(cppClass, cppFunction.Parameters[0].Type))
            {
                thisRef = true;
            }
            bool thisUse = false;
            if (cppFunction.Parameters.Count > 0 && IsType(cppClass, cppFunction.Parameters[0].Type))
            {
                thisUse = true;
            }

            bool voidReturn = IsVoid(cppFunction.ReturnType);
            bool stringReturn = IsString(cppFunction.ReturnType);
            string returnCsName = GetCsTypeName(cppFunction.ReturnType, false);

            for (int i = 0; i < signatures.Count; i++)
            {
                string signature = signatures[i];

                if (stringReturn)
                    WriteStructMethod(writer, cppFunction, structName, command, thisRef, thisUse, voidReturn, true, "string", signature);

                WriteStructMethod(writer, cppFunction, structName, command, thisRef, thisUse, voidReturn, false, returnCsName, signature);
            }
        }

        private static void WriteStructMethod(CodeWriter writer, CppFunction cppFunction, string structName, string command, bool thisRef, bool thisUse, bool voidReturn, bool stringReturn, string returnCsName, string signature)
        {
            string[] paramList = signature.Split(',', StringSplitOptions.RemoveEmptyEntries);

            WriteCsSummary(cppFunction.Comment, writer);
            string header;

            if (stringReturn)
            {
                header = $"public unsafe string {command.Replace(structName, string.Empty)}S({signature})";
            }
            else
            {
                header = $"public unsafe {returnCsName} {command.Replace(structName, string.Empty)}({signature})";
            }

            using (writer.PushBlock(header))
            {
                StringBuilder sb = new();
                if (!voidReturn)
                {
                    sb.Append($"{returnCsName} ret = ");
                }

                if (stringReturn)
                {
                    WriteStringConvertToManaged(sb, cppFunction.ReturnType);
                }

                sb.Append($"{CsCodeGeneratorSettings.Default.ApiName}.{command}(");
                int strings = 0;
                int stacks = 0;
                int index = 0;
                for (int j = 0; j < cppFunction.Parameters.Count; j++)
                {
                    if (thisUse && j == 0)
                    {
                        sb.Append("this");
                    }
                    else if (thisRef && j == 0)
                    {
                        writer.BeginBlock($"fixed ({structName}* @this = &this)");
                        sb.Append("@this");
                        stacks++;
                    }
                    else
                    {
                        var isRef = paramList[index].Contains("ref");
                        var isStr = paramList[index].Contains("string");
                        var cppParameter = cppFunction.Parameters[j];
                        var paramCsTypeName = GetCsTypeName(cppParameter.Type, false);
                        var paramCsName = GetParameterName(cppParameter.Type, cppParameter.Name);
                        if (isRef)
                        {
                            writer.BeginBlock($"fixed ({paramCsTypeName} p{paramCsName} = &{paramCsName})");
                            sb.Append($"p{paramCsName}");
                            stacks++;
                        }
                        else if (isStr)
                        {
                            WriteStringConvertToUnmanaged(writer, cppParameter.Type, paramCsName, strings);
                            sb.Append($"pStr{strings}");
                            strings++;
                        }
                        else
                        {
                            sb.Append(paramCsName);
                        }
                        index++;
                    }

                    if (j != cppFunction.Parameters.Count - 1)
                        sb.Append(", ");
                }

                if (stringReturn)
                    sb.Append("));");
                else
                    sb.Append(");");

                writer.WriteLine(sb.ToString());

                while (strings > 0)
                {
                    strings--;
                    writer.WriteLine($"Marshal.FreeHGlobal((nint)pStr{strings});");
                }

                if (!voidReturn)
                    writer.WriteLine("return ret;");

                while (stacks > 0)
                {
                    stacks--;
                    writer.EndBlock();
                }
            }

            writer.WriteLine();
        }

        private static string GetStructParameterSignature(CppClass cppClass, IList<CppParameter> parameters, bool canUseOut)
        {
            var argumentBuilder = new StringBuilder();
            int index = 0;

            bool thisRef = false;
            if (parameters.Count > 0)
            {
                thisRef = IsPointerOf(cppClass, parameters[0].Type) || IsType(cppClass, parameters[0].Type);
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                CppParameter cppParameter = parameters[i];
                if (thisRef && i == 0)
                {
                    index++;
                    continue;
                }

                string direction = string.Empty;
                var paramCsTypeName = GetCsTypeName(cppParameter.Type, false);
                var paramCsName = GetParameterName(cppParameter.Type, cppParameter.Name);

                if (canUseOut && CanBeUsedAsOutput(cppParameter.Type, out CppTypeDeclaration? cppTypeDeclaration))
                {
                    argumentBuilder.Append("out ");
                    paramCsTypeName = GetCsTypeName(cppTypeDeclaration, false);
                }

                argumentBuilder.Append(paramCsTypeName).Append(' ').Append(paramCsName);
                if (index < parameters.Count - 1)
                {
                    argumentBuilder.Append(", ");
                }

                index++;
            }

            return argumentBuilder.ToString();
        }

        private static List<string> GetStructVariantParameterSignatures(CppClass cppClass, IList<CppParameter> parameters, string originalSig, bool canUseOut)
        {
            List<string> result = new();
            StringBuilder argumentBuilder = new();

            bool thisRef = false;
            if (parameters.Count > 0)
            {
                thisRef = IsPointerOf(cppClass, parameters[0].Type) || IsType(cppClass, parameters[0].Type);
            }

            for (long ix = 0; ix < Math.Pow(2, parameters.Count); ix++)
            {
                int index = 0;
                for (int j = 0; j < parameters.Count; j++)
                {
                    if (thisRef && j == 0)
                    {
                        index++;
                        continue;
                    }

                    var bit = (ix & (1 << j - 64)) != 0;
                    CppParameter cppParameter = parameters[j];
                    string paramCsTypeName;
                    if (bit)
                        paramCsTypeName = GetCsWrapperTypeName(cppParameter.Type, false);
                    else
                        paramCsTypeName = GetCsTypeName(cppParameter.Type, false);

                    var paramCsName = GetParameterName(cppParameter.Type, cppParameter.Name);

                    if (canUseOut && CanBeUsedAsOutput(cppParameter.Type, out CppTypeDeclaration? cppTypeDeclaration))
                    {
                        argumentBuilder.Append("out ");
                        paramCsTypeName = GetCsWrapperTypeName(cppTypeDeclaration, false);
                    }

                    argumentBuilder.Append(paramCsTypeName).Append(' ').Append(paramCsName);
                    if (index < parameters.Count - 1)
                    {
                        argumentBuilder.Append(", ");
                    }

                    index++;
                }
                string sig = argumentBuilder.ToString();
                if (!result.Contains(sig) && sig != originalSig)
                {
                    result.Add(sig);
                    Console.WriteLine(sig);
                }

                argumentBuilder.Clear();

                index = 0;
                for (int j = 0; j < parameters.Count; j++)
                {
                    if (thisRef && j == 0)
                    {
                        index++;
                        continue;
                    }

                    var bit = (ix & (1 << j - 64)) != 0;
                    CppParameter cppParameter = parameters[j];

                    string paramCsTypeName;
                    if (bit)
                        paramCsTypeName = IsString(cppParameter.Type) ? "string" : GetCsWrapperTypeName(cppParameter.Type, false);
                    else
                        paramCsTypeName = GetCsTypeName(cppParameter.Type, false);

                    var paramCsName = GetParameterName(cppParameter.Type, cppParameter.Name);

                    if (canUseOut && CanBeUsedAsOutput(cppParameter.Type, out CppTypeDeclaration? cppTypeDeclaration))
                    {
                        argumentBuilder.Append("out ");
                        paramCsTypeName = GetCsWrapperTypeName(cppTypeDeclaration, false);
                    }

                    argumentBuilder.Append(paramCsTypeName).Append(' ').Append(paramCsName);
                    if (index < parameters.Count - 1)
                    {
                        argumentBuilder.Append(", ");
                    }

                    index++;
                }
                sig = argumentBuilder.ToString();
                if (!result.Contains(sig) && sig != originalSig)
                {
                    result.Add(sig);
                    Console.WriteLine(sig);
                }

                argumentBuilder.Clear();
            }

            return result;
        }

        public static bool IsPointerOf(CppType type, CppType pointer)
        {
            if (pointer is CppPointerType pointerType)
            {
                return pointerType.ElementType.GetDisplayName() == type.GetDisplayName();
            }
            return false;
        }

        public static bool IsType(CppType a, CppType b)
        {
            return a.GetDisplayName() == b.GetDisplayName();
        }

        private static string NormalizeFieldName(string name)
        {
            var parts = name.Split('_', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new();
            for (int i = 0; i < parts.Length; i++)
            {
                sb.Append(char.ToUpper(parts[i][0]));
                sb.Append(parts[i][1..]);
            }
            name = sb.ToString();
            if (CsCodeGeneratorSettings.Default.Keywords.Contains(name))
                return "@" + name;

            return name;
        }
    }
}