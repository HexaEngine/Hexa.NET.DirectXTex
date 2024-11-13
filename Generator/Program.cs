using HexaGen;

BatchGenerator batch = new();
batch.Start()
    .Setup<CsCodeGenerator>("generator.json")
    .AlterConfig(c =>
    {
        c.SystemIncludeFolders.Add("C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\VC\\Tools\\Llvm\\x64\\lib\\clang\\18\\include");
        c.SystemIncludeFolders.Add("C:\\Dev\\vcpkg\\packages\\directxmath_x64-windows\\include\\directxmath");
    })
    .Generate("DirectXTex/DirectXTex.h", "../../../../Hexa.NET.DirectXTex/Generated")
    .Finish();