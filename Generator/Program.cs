using HexaGen;

CsCodeGeneratorConfig config = CsCodeGeneratorConfig.Load("generator.json");

config.SystemIncludeFolders.Add("C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\VC\\Tools\\Llvm\\x64\\lib\\clang\\17\\include");
config.SystemIncludeFolders.Add("C:\\Dev\\vcpkg\\packages\\directxmath_x64-windows\\include\\directxmath");

CsCodeGenerator generator = new(config);
generator.LogToConsole();
generator.Generate("DirectXTex/DirectXTex.h", "../../../../Hexa.NET.DirectXTex/Generated");