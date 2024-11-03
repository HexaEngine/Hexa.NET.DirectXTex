namespace Example
{
    using Hexa.NET.DirectXTex;
    using HexaGen.Runtime;

    public class Program
    {
        private static unsafe void Main(string[] args)
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = default;

            string inputPath = "assets/textures/test.png";
            DirectXTex.LoadFromWICFile(inputPath, WICFlags.None, ref metadata, ref image, default).ThrowIf();
            ScratchImage mipChain = DirectXTex.CreateScratchImage();

            int mipLevels = 4;
            DirectXTex.GenerateMipMaps2(image.GetImages(), 1, ref metadata, TexFilterFlags.ForceNonWic, (nuint)mipLevels, ref mipChain).ThrowIf();
            image.Release();

            metadata = mipChain.GetMetadata();

            string outputPath = "test.dds";
            DirectXTex.SaveToDDSFile2(mipChain.GetImages(), mipChain.GetImageCount(), ref metadata, DDSFlags.None, outputPath).ThrowIf();

            mipChain.Release();
        }
    }
}