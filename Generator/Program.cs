namespace Generator
{
    using CppAst;

    internal class Program
    {
        private static int Main(string[] args)
        {
            string headerFile = "DirectXTex/DirectXTex.h";

            var options = new CppParserOptions
            {
                ParseMacros = true,
            };

            options.SystemIncludeFolders.Add("C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\VC\\Tools\\Llvm\\x64\\lib\\clang\\15.0.1\\include");
            options.SystemIncludeFolders.Add("C:\\Dev\\vcpkg\\packages\\directxmath_x64-windows\\include\\directxmath");

            var compilation = CppParser.ParseFile(headerFile, options);

            // Print diagnostic messages
            if (compilation.HasErrors)
            {
                for (int i = 0; i < compilation.Diagnostics.Messages.Count; i++)
                {
                    CppDiagnosticMessage? message = compilation.Diagnostics.Messages[i];
                    if (message.Type == CppLogMessageType.Error)
                    {
                        var currentColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(message);
                        Console.ForegroundColor = currentColor;
                    }
                }

                return 0;
            }

            CsCodeGenerator.Generate(compilation, "../../../../HexaEngine.DirectXTex/Generated");

            return 0;
        }
    }
}