namespace Generator
{
    using HexaGen;
    using HexaGen.Core;
    using System.Text;

    internal class Program
    {
        private static int Main(string[] args)
        {
            string headerFile = "DirectXTex/DirectXTex.h";

            CsCodeGeneratorConfig generatorSettings = CsCodeGeneratorConfig.Load("generator.json");

            generatorSettings.SystemIncludeFolders.Add("C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\VC\\Tools\\Llvm\\x64\\lib\\clang\\17\\include");
            generatorSettings.SystemIncludeFolders.Add("C:\\Dev\\vcpkg\\packages\\directxmath_x64-windows\\include\\directxmath");

            generatorSettings.HeaderInjector += Injector;

            CsCodeGenerator generator = new(generatorSettings);
            generator.LogToConsole();
            generator.Generate(headerFile, "../../../../Hexa.NET.DirectXTex/Generated");
          

            return 0;
        }

        private static void Injector(ICodeWriter codeWriter, StringBuilder builder)
        {
            if (builder == null)
                return;

            builder.AppendLine("#if !STANDALONE");
            builder.AppendLine("using Silk.NET.DXGI;");
            builder.AppendLine("using Silk.NET.Direct2D;");
            builder.AppendLine("using Silk.NET.Direct3D11;");
            builder.AppendLine("using Silk.NET.Direct3D12;");
            builder.AppendLine("#endif");
        }
    }
}