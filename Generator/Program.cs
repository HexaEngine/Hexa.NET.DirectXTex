namespace Generator
{
    using CppAst;
    using HexaGen;
    using Microsoft.CodeAnalysis;

    internal class Program
    {
        private static int Main(string[] args)
        {
            string headerFile = "DirectXTex/DirectXTex.h";

            CsCodeGeneratorSettings generatorSettings = CsCodeGeneratorSettings.Load("generator.json");

            generatorSettings.SystemIncludeFolders.Add("C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\VC\\Tools\\Llvm\\x64\\lib\\clang\\17\\include");
            generatorSettings.SystemIncludeFolders.Add("C:\\Dev\\vcpkg\\packages\\directxmath_x64-windows\\include\\directxmath");

            CsCodeGenerator generator = new(generatorSettings);
            generator.Generate(headerFile, "../../../../Hexa.NET.DirectXTex/Generated");
            generator.DisplayMessages();

            return 0;
        }
    }
}