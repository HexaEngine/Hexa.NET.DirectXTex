namespace HexaEngine.DirectXTex.Tests
{
    public unsafe class ScratchImageMethods
    {
        [Test]
        public void CreateAndFree()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            image.Release();
        }

        [Test]
        public void Init()
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

            image.Initialize(metadata, CPFlags.None);

            Assert.That(DirectXTex.GetMetadata(image), Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void Init1D()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture1D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 1,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize1D((int)Format.FormatR8G8B8A8Unorm, 64, 1, 4, CPFlags.None);

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void Init2D()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Width = 64,
                Height = 32,
                Depth = 1,
                ArraySize = 4,
                MipLevels = 2,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize2D((int)Format.FormatR8G8B8A8Unorm, 64, 32, 4, 2, CPFlags.None);

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void Init3D()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                Dimension = TexDimension.Texture3D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Width = 64,
                Height = 32,
                Depth = 4,
                ArraySize = 1,
                MipLevels = 2,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize3D((int)Format.FormatR8G8B8A8Unorm, 64, 32, 4, 2, CPFlags.None);

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void InitCube()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Width = 64,
                Height = 32,
                Depth = 1,
                ArraySize = 6,
                MipLevels = 2,
                MiscFlags = (uint)TexMiscFlag.Texturecube,
                MiscFlags2 = 0,
            };

            image.InitializeCube((int)Format.FormatR8G8B8A8Unorm, 64, 32, 1, 2, CPFlags.None);

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void OverrrideFormat()
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

            {
                image.Initialize(metadata, CPFlags.None);
            }

            {
                bool result = image.OverrideFormat((int)Format.FormatB8G8R8A8Unorm);
                if (!result)
                    throw new Exception();
            }

            metadata.Format = (int)Format.FormatB8G8R8A8Unorm;
            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void GetMetadata()
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

            image.Initialize(metadata, CPFlags.None);

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void GetImage()
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

            image.Initialize(metadata, CPFlags.None);

            var img = image.GetImage(0, 0, 0);

            if (img.Width != metadata.Width &&
                img.Height != metadata.Height &&
                img.Format != metadata.Format)
                Trace.Fail("img doesn't match");

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void GetImagesAndGetImageCount()
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

            image.Initialize(metadata, CPFlags.None);

            var imgs = DirectXTex.GetImages(image);

            for (int i = 0; i < (int)DirectXTex.GetImageCount(image); i++)
            {
                var img = imgs[i];
                if (img.Width != metadata.Width &&
                img.Height != metadata.Height &&
                img.Format != metadata.Format)
                    Trace.Fail("img doesn't match");
            }

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void GetPixelsAndGetPixelsSizeAndModify()
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

            image.Initialize(metadata, CPFlags.None);

            var pixels = image.GetPixels();
            var count = image.GetPixelsSize();
            Span<byte> data = new(pixels, (int)count);
            data.Fill(1);

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }

        [Test]
        public void IsAlphaAllOpaque()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR16G16Float,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            image.Initialize(metadata, CPFlags.None);

            // Result should be true because the format contains no alpha information.
            if (!image.IsAlphaAllOpaque())
                throw new();

            var meta = DirectXTex.GetMetadata(image);
            Assert.That(meta, Is.EqualTo(metadata));

            image.Release();
        }
    }
}