namespace Hexa.NET.DirectXTex.Tests
{
    public class NormalMapOperations
    {
        [Test]
        public unsafe void ComputeNormalMap()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.R8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };
            ScratchImage image = DirectXTex.CreateScratchImage();
            image.Initialize(ref metadata, CPFlags.None);

            ScratchImage normalMap = DirectXTex.CreateScratchImage();
            normalMap.Initialize(ref metadata, CPFlags.None);

            metadata = image.GetMetadata();
            DirectXTex.ComputeNormalMap2(image.GetImages(), image.GetImageCount(), ref metadata, CNMAPFlags.Default, 2, (int)Format.R8G8B8A8Unorm, ref normalMap);

            image.Release();
            normalMap.Release();
        }
    }
}