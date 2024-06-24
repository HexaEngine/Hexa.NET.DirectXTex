namespace Example
{
    using Hexa.NET.DirectXTex;

    public class Program
    {
        private static unsafe void Main(string[] args)
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = default;

            string inputPath = "assets/textures/test.dds";
            DirectXTex.LoadFromDDSFile(inputPath, DDSFlags.None, ref metadata, image);

            ScratchImage mipChain = DirectXTex.CreateScratchImage();
            int mipLevels = 4;
            DirectXTex.GenerateMipMaps2(image.GetImages(), image.GetImageCount(), metadata, TexFilterFlags.Default, (ulong)mipLevels, mipChain);
            image.Release();

            string outputPath = "test.dds";
            DirectXTex.SaveToDDSFile2(mipChain.GetImages(), mipChain.GetImageCount(), mipChain.GetMetadata(), DDSFlags.None, outputPath);

            mipChain.Release();
        }
    }
}