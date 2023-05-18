namespace Generator
{
    using CppAst;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial class CsCodeGenerator
    {
        private static void GenerateExtensions(CppCompilation compilation, string outputPath)
        {
            // Generate Functions
            using var writer = new CodeWriter(Path.Combine(outputPath, "Extensions.cs"),
                "System",
                "System.Runtime.CompilerServices",
                "System.Runtime.InteropServices",
                "Silk.NET.Direct3D12",
                "Silk.NET.Direct3D11"
                );
            using (writer.PushBlock($"public static unsafe class Extensions"))
            {
                for (int i = 0; i < compilation.Typedefs.Count; i++)
                {
                    CppTypedef typedef = compilation.Typedefs[i];
                    if (typedef.ElementType is not CppPointerType)
                    {
                        continue;
                    }

                    var csHandleName = GetCsCleanName(typedef.Name);

                    for (int j = 0; j < compilation.Functions.Count; j++)
                    {
                        var cppFunction = compilation.Functions[j];

                        if (cppFunction.Parameters.Count == 0 || cppFunction.Parameters[0].Type.TypeKind == CppTypeKind.Pointer)
                            continue;

                        if (cppFunction.Parameters[0].Type.GetDisplayName().Contains(typedef.GetDisplayName()))
                        {
                            var csFunctionName = GetCsCleanName(cppFunction.Name);
                            bool canUseOut = s_outReturnFunctions.Contains(cppFunction.Name);
                            var argumentsString = GetParameterSignature(cppFunction, canUseOut);
                            var sigs = GetVariantParameterSignatures(cppFunction.Parameters, argumentsString, canUseOut);
                            sigs.Add(argumentsString);

                            WriteExtensions(writer, cppFunction, csHandleName, csFunctionName, sigs);
                        }
                    }
                }
            }
        }

        private static void WriteExtensions(CodeWriter writer, CppFunction cppFunction, string handle, string command, List<string> signatures)
        {
            bool voidReturn = IsVoid(cppFunction.ReturnType);
            bool stringReturn = IsString(cppFunction.ReturnType);
            string returnCsName = GetCsTypeName(cppFunction.ReturnType, false);

            for (int i = 0; i < signatures.Count; i++)
            {
                string signature = "this " + signatures[i];

                if (stringReturn)
                    WriteExtensionMethod(writer, cppFunction, handle, command, voidReturn, true, "string", signature);

                WriteExtensionMethod(writer, cppFunction, handle, command, voidReturn, false, returnCsName, signature);
            }
        }

        private static void WriteExtensionMethod(CodeWriter writer, CppFunction cppFunction, string handle, string command, bool voidReturn, bool stringReturn, string returnCsName, string signature)
        {
            string[] paramList = signature.Split(',', StringSplitOptions.RemoveEmptyEntries);

            WriteCsSummary(cppFunction.Comment, writer);
            string header;

            if (stringReturn)
            {
                header = $"public static string {command.Replace(handle, string.Empty)}S({signature})";
            }
            else
            {
                header = $"public static {returnCsName} {command.Replace(handle, string.Empty)}({signature})";
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
                for (int j = 0; j < cppFunction.Parameters.Count; j++)
                {
                    var isRef = paramList[j].Contains("ref");
                    var isStr = paramList[j].Contains("string");
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
    }
}