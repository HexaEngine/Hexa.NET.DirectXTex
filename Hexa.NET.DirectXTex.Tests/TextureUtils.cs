namespace Hexa.NET.DirectXTex.Tests
{
    using Silk.NET.Core.Win32Extras;

    public unsafe class TextureUtils
    {
        [Test]
        public void FlipRotate()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize(ref metadata, CPFlags.None);

            ScratchImage image1 = DirectXTex.CreateScratchImage();
            var meta = image.GetMetadata();
            DirectXTex.FlipRotate2(image.GetImages(), image.GetImageCount(), ref meta, TexFRFlags.Rotate90, ref image1);

            Assert.That(DirectXTex.GetMetadata(image), Is.EqualTo(metadata));
            Assert.That(image1.GetMetadata(), Is.EqualTo(metadata));

            image1.Release();
            image.Release();
        }

        [Test]
        public void Resize()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            TexMetadata metadata3 = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 1024,
                Width = 1024,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize(ref metadata, CPFlags.None);

            ScratchImage image1 = DirectXTex.CreateScratchImage();
            var meta = image.GetMetadata();
            DirectXTex.Resize2(image.GetImages(), image.GetImageCount(), ref meta, 1024, 1024, TexFilterFlags.Default, ref image1);

            Assert.That(DirectXTex.GetMetadata(image), Is.EqualTo(metadata));
            var metadata2 = image1.GetMetadata();
            Assert.That(metadata2, Is.EqualTo(metadata3));

            image1.Release();
            image.Release();
        }

        [Test]
        public void Convert()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            TexMetadata metadata3 = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR16G16B16A16Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize(ref metadata, CPFlags.None);

            ScratchImage image1 = DirectXTex.CreateScratchImage();
            var meta = image.GetMetadata();
            DirectXTex.Convert2(image.GetImages(), image.GetImageCount(), ref meta, (int)Format.FormatR16G16B16A16Unorm, TexFilterFlags.Default, 0.5f, ref image1);

            Assert.That(DirectXTex.GetMetadata(image), Is.EqualTo(metadata));
            var metadata2 = image1.GetMetadata();
            Assert.That(metadata2, Is.EqualTo(metadata3));

            image1.Release();
            image.Release();
        }

        [Test]
        public void ConvertToSinglePlane()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatP010,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            TexMetadata metadata3 = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatY210,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize(ref metadata, CPFlags.None);

            ScratchImage image1 = DirectXTex.CreateScratchImage();
            var meta = image.GetMetadata();
            DirectXTex.ConvertToSinglePlane2(image.GetImages(), image.GetImageCount(), ref meta, ref image1);

            Assert.That(DirectXTex.GetMetadata(image), Is.EqualTo(metadata));
            var metadata2 = image1.GetMetadata();
            Assert.That(metadata2, Is.EqualTo(metadata3));

            image1.Release();
            image.Release();
        }

        [Test]
        public void GenerateMipMaps()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            TexMetadata metadata3 = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            DirectXTex.Initialize(image, ref metadata, CPFlags.None);

            ScratchImage image1 = DirectXTex.CreateScratchImage();
            var meta = image.GetMetadata();
            DirectXTex.GenerateMipMaps2(image.GetImages(), image.GetImageCount(), ref meta, TexFilterFlags.Default, 4, ref image1);

            Assert.That(DirectXTex.GetMetadata(image), Is.EqualTo(metadata));
            var metadata2 = DirectXTex.GetMetadata(image1);
            Assert.That(metadata2, Is.EqualTo(metadata3));

            DirectXTex.ScratchImageRelease(image1);
            image.Release();
        }

        [Test]
        public void GenerateMipMaps3D()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 6,
                Dimension = TexDimension.Texture3D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            TexMetadata metadata3 = new()
            {
                ArraySize = 1,
                Depth = 6,
                Dimension = TexDimension.Texture3D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize(ref metadata, CPFlags.None);

            ScratchImage image1 = DirectXTex.CreateScratchImage();
            DirectXTex.GenerateMipMaps3D2(image.GetImages(), image.GetImageCount(), TexFilterFlags.Default, 4, ref image1);

            Assert.That(DirectXTex.GetMetadata(image), Is.EqualTo(metadata));
            var metadata2 = image1.GetMetadata();
            Assert.That(metadata2, Is.EqualTo(metadata3));

            image1.Release();
            image.Release();
        }

        [Test]
        public void ScaleMipMapsAlphaForCoverage()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 2048,
                Width = 2048,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            ScratchImage mipChain = DirectXTex.CreateScratchImage();
            mipChain.Initialize(ref metadata, CPFlags.None);

            var info = mipChain.GetMetadata();
            ScratchImage coverageMipChain = DirectXTex.CreateScratchImage();
            coverageMipChain.Initialize(ref info, CPFlags.None);

            for (nuint item = 0; item < info.ArraySize; ++item)
            {
                var meta = mipChain.GetMetadata();
                DirectXTex.ScaleMipMapsAlphaForCoverage(mipChain.GetImages(), mipChain.GetImageCount(), ref meta, item, 0.5f, ref coverageMipChain);
            }
            coverageMipChain.Release();
            mipChain.Release();
        }

        [Test]
        public void PremultiplyAlpha()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 2048,
                Width = 2048,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            ScratchImage srcImage = DirectXTex.CreateScratchImage();
            srcImage.Initialize(ref metadata, CPFlags.None);

            ScratchImage destImage = DirectXTex.CreateScratchImage();
            var meta = srcImage.GetMetadata();
            DirectXTex.PremultiplyAlpha2(srcImage.GetImages(), srcImage.GetImageCount(), ref meta, TexPMAlphaFlags.Default, ref destImage);

            srcImage.Release();
            destImage.Release();
        }

        [Test]
        public void Compress()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR32G32B32A32Float,
                Height = 512,
                Width = 512,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            ScratchImage srcImage = DirectXTex.CreateScratchImage();
            srcImage.Initialize(ref metadata, CPFlags.None);

            ScratchImage destImage = DirectXTex.CreateScratchImage();
            DirectXTex.BitsPerPixel((int)Format.FormatBC7Unorm);
            var meta = srcImage.GetMetadata();
            DirectXTex.Compress2(srcImage.GetImages(), srcImage.GetImageCount(), ref meta, (int)Format.FormatBC7Unorm, TexCompressFlags.Bc7Quick | TexCompressFlags.Parallel, 0.5f, ref destImage);

            srcImage.Release();
            destImage.Release();
        }

        [Test]
        public void Decompress()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatBC7Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            ScratchImage srcImage = DirectXTex.CreateScratchImage();
            srcImage.Initialize(ref metadata, CPFlags.None);

            ScratchImage destImage = DirectXTex.CreateScratchImage();
            var meta = srcImage.GetMetadata();
            DirectXTex.Decompress2(srcImage.GetImages(), srcImage.GetImageCount(), ref meta, (int)Format.FormatR8G8B8A8Unorm, ref destImage);

            srcImage.Release();
            destImage.Release();
        }
    }
}