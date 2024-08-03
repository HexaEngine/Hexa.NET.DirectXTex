﻿namespace HexaGen
{
    using CppAst;
    using HexaGen.Core;
    using System.IO;

    public partial class CsCodeGenerator
    {
        protected readonly HashSet<string> LibDefinedDelegates = new();

        public readonly HashSet<string> DefinedDelegates = new();

        protected virtual List<string> SetupDelegateUsings()
        {
            List<string> usings = new() { "System", "System.Diagnostics", "System.Runtime.CompilerServices", "System.Runtime.InteropServices", "HexaGen.Runtime" };
            usings.AddRange(settings.Usings);
            return usings;
        }

        protected virtual bool FilterIgnoredType(GenContext context, CppClass cppClass)
        {
            if (settings.AllowedTypes.Count != 0 && !settings.AllowedTypes.Contains(cppClass.Name))
                return true;

            if (settings.IgnoredTypes.Contains(cppClass.Name))
                return true;

            return false;
        }

        protected virtual bool FilterDelegate(GenContext context, ICppMember member)
        {
            if (settings.AllowedDelegates.Count != 0 && !settings.AllowedDelegates.Contains(member.Name))
                return true;
            if (settings.IgnoredDelegates.Contains(member.Name))
                return true;

            if (LibDefinedDelegates.Contains(member.Name))
                return true;

            if (DefinedDelegates.Contains(member.Name))
            {
                LogWarn($"{context.FilePath}: {member.Name} delegate is already defined!");
                return true;
            }

            DefinedDelegates.Add(member.Name);

            return false;
        }

        protected virtual void GenerateDelegates(CppCompilation compilation, string outputPath)
        {
            string filePath = Path.Combine(outputPath, "Delegates.cs");

            // Generate Delegates
            using var writer = new CsCodeWriter(filePath, settings.Namespace, SetupDelegateUsings());

            GenContext context = new(compilation, filePath, writer);

            // Print All classes, structs
            for (int i = 0; i < compilation.Classes.Count; i++)
            {
                CppClass? cppClass = compilation.Classes[i];

                if (FilterIgnoredType(context, cppClass))
                    continue;

                WriteClassDelegates(context, cppClass);
            }

            for (int i = 0; i < compilation.Typedefs.Count; i++)
            {
                CppTypedef typedef = compilation.Typedefs[i];

                if (typedef.ElementType is CppPointerType pointerType && pointerType.ElementType is CppFunctionType functionType)
                {
                    WriteDelegate(context, typedef, functionType);
                }
            }
        }

        protected virtual void WriteClassDelegates(GenContext context, CppClass cppClass, string? csName = null)
        {
            csName ??= settings.GetDelegateName(cppClass.Name);

            if (cppClass.ClassKind == CppClassKind.Class || cppClass.Name.EndsWith("_T") || csName == "void")
            {
                return;
            }

            for (int j = 0; j < cppClass.Classes.Count; j++)
            {
                var subClass = cppClass.Classes[j];
                string csSubName;
                if (string.IsNullOrEmpty(subClass.Name))
                {
                    string label = cppClass.Classes.Count == 1 ? "" : j.ToString();
                    csSubName = csName + "Union" + label;
                }
                else
                {
                    csSubName = settings.GetDelegateName(subClass.Name);
                }

                WriteClassDelegates(context, subClass, csSubName);
            }

            for (int j = 0; j < cppClass.Fields.Count; j++)
            {
                CppField cppField = cppClass.Fields[j];

                if (cppField.Type is CppPointerType cppPointer && cppPointer.IsDelegate(out var functionType))
                {
                    WriteDelegate(context, cppField, functionType);
                }
                else if (cppField.Type is CppTypedef typedef && typedef.ElementType is CppPointerType pointerType && pointerType.ElementType is CppFunctionType cppFunctionType)
                {
                    WriteDelegate(context, cppField, cppFunctionType, false);
                }
            }
        }

        private void WriteDelegate<T>(GenContext context, T field, CppFunctionType functionType, bool isReadOnly = false) where T : class, ICppDeclaration, ICppMember
        {
            if (FilterDelegate(context, field))
            {
                return;
            }

            var writer = context.Writer;
            string csFieldName = settings.GetFieldName(field.Name);
            string fieldPrefix = isReadOnly ? "readonly " : string.Empty;

            writer.WriteLine("#if NET5_0_OR_GREATER");
            WriteFinal(writer, field, functionType, csFieldName, fieldPrefix);
            writer.WriteLine("#else");
            WriteFinal(writer, field, functionType, csFieldName, fieldPrefix, compatibility: true);
            writer.WriteLine("#endif");
            writer.WriteLine();
        }

        private void WriteFinal<T>(ICodeWriter writer, T field, CppFunctionType functionType, string csFieldName, string fieldPrefix, bool compatibility = false) where T : class, ICppDeclaration, ICppMember
        {
            string signature = settings.GetParameterSignature(functionType.Parameters, canUseOut: false, delegateType: true, compatibility: compatibility);
            string returnCsName = settings.GetCsTypeName(functionType.ReturnType, false);
            returnCsName = returnCsName.Replace("bool", settings.GetBoolType());

            if (functionType.ReturnType is CppTypedef typedef && typedef.ElementType.IsDelegate(out var cppFunction) && !returnCsName.Contains('*'))
            {
                if (cppFunction.Parameters.Count == 0)
                {
                    returnCsName = $"delegate*<{settings.GetCsTypeName(cppFunction.ReturnType)}>";
                }
                else
                {
                    returnCsName = $"delegate*<{settings.GetNamelessParameterSignature(cppFunction.Parameters, canUseOut: false, delegateType: true, compatibility)}, {settings.GetCsTypeName(cppFunction.ReturnType)}>";
                }
            }

            if (compatibility && returnCsName.Contains('*'))
            {
                returnCsName = "nint";
            }

            if (settings.TryGetDelegateMapping(csFieldName, out var mapping))
            {
                returnCsName = mapping.ReturnType;
                signature = mapping.Signature;
            }

            string header = $"{returnCsName} {csFieldName}({signature})";

            settings.WriteCsSummary(field.Comment, writer);
            writer.WriteLine($"[UnmanagedFunctionPointer(CallingConvention.{functionType.CallingConvention.GetCallingConvention()})]");
            writer.WriteLine($"public unsafe {fieldPrefix}delegate {header};");
            writer.WriteLine();
        }
    }
}