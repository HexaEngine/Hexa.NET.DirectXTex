namespace Example
{
    using Hexa.NET.DirectXTex;
    using HexaGen.Runtime;

    public class Program
    {
        private const string DDSFilename = "assets/textures/test.dds";

        private static byte[] LoadTexture(string path) => File.ReadAllBytes(Path.GetFullPath(path));

        private static unsafe void Main(string[] args)
        {
            var path = Path.Combine("results", "LoadAndSaveFromDDSFile", "test.dds");
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata;

            DirectXTex.LoadFromDDSFile(DDSFilename, DDSFlags.None, &metadata, ref image);

            Console.WriteLine(path);
            metadata = image.GetMetadata();
            DirectXTex.SaveToDDSFile2(image.GetImages(), image.GetImageCount(), ref metadata, DDSFlags.None, path);

            Console.WriteLine(path);

            Span<byte> src = LoadTexture(DDSFilename);
            Span<byte> dest = LoadTexture(path);
        }
    }
}