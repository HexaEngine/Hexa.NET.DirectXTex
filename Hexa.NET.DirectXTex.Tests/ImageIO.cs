namespace Hexa.NET.DirectXTex.Tests
{
    public unsafe class ImageIO : IDisposable
    {
        private const string DDSFilename = "assets/textures/test.dds";
        private const string HDRFilename = "assets/textures/test.hdr";
        private const string TGAFilename = "assets/textures/test.tga";
        private const string WICFilename = "assets/textures/test.png";

        private static byte[] LoadTexture(string path) => File.ReadAllBytes(Path.GetFullPath(path));

        [Test]
        public void LoadAndSaveFromDDSMemory()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            Span<byte> src = LoadTexture(DDSFilename);
            Blob blob = DirectXTex.CreateBlob();

            TexMetadata metadata;
            fixed (byte* srcPtr = src)
            {
                DirectXTex.LoadFromDDSMemory(srcPtr, (nuint)src.Length, DDSFlags.None, &metadata, ref image);
            }

            metadata = image.GetMetadata();
            DirectXTex.SaveToDDSMemory2(image.GetImages(), image.GetImageCount(), ref metadata, DDSFlags.None, ref blob);

            Span<byte> dest = blob.AsBytes();
            Assert.That(src.SequenceEqual(dest), Is.True);

            blob.Release();
            image.Release();
        }

        [Test]
        public void LoadAndSaveFromDDSFile()
        {
            var path = Path.Combine("results", nameof(LoadAndSaveFromDDSFile), "test.dds");
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata;

            DirectXTex.LoadFromDDSFile(DDSFilename, DDSFlags.None, &metadata, ref image);

            metadata = image.GetMetadata();
            DirectXTex.SaveToDDSFile2(image.GetImages(), image.GetImageCount(), ref metadata, DDSFlags.None, path);

            Span<byte> src = LoadTexture(DDSFilename);
            Span<byte> dest = LoadTexture(path);
            Assert.That(src.SequenceEqual(dest), Is.True);

            image.Release();
        }

        [Test]
        public void LoadAndSaveFromHDRMemory()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata;
            Span<byte> src = LoadTexture(HDRFilename);
            Blob blob = DirectXTex.CreateBlob();

            fixed (byte* srcPtr = src)
            {
                DirectXTex.LoadFromHDRMemory(srcPtr, (nuint)src.Length, &metadata, ref image);
            }
            metadata = image.GetMetadata();
            DirectXTex.SaveToHDRMemory(image.GetImages(), ref blob);

            Span<byte> dest = blob.AsBytes();
            Assert.That(src.SequenceEqual(dest), Is.True);

            blob.Release();
            image.Release();
        }

        [Test]
        public void LoadAndSaveFromHDRFile()
        {
            var path = Path.Combine("results", nameof(LoadAndSaveFromHDRFile), "test.hdr");
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata;

            DirectXTex.LoadFromHDRFile(HDRFilename, &metadata, ref image);

            metadata = image.GetMetadata();
            DirectXTex.SaveToHDRFile(image.GetImages(), path);

            Span<byte> src = LoadTexture(HDRFilename);
            Span<byte> dest = LoadTexture(path);
            Assert.That(src.SequenceEqual(dest), Is.True);

            image.Release();
        }

        [Test]
        public void LoadAndSaveFromTGAMemory()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata;
            Span<byte> src = LoadTexture(TGAFilename);
            Blob blob = DirectXTex.CreateBlob();

            fixed (byte* srcPtr = src)
            {
                DirectXTex.LoadFromTGAMemory(srcPtr, (nuint)src.Length, TGAFlags.None, &metadata, ref image);
            }

            metadata = image.GetMetadata();
            DirectXTex.SaveToTGAMemory(image.GetImages(), TGAFlags.None, ref blob, &metadata);

            blob.Release();
            image.Release();
        }

        [Test]
        public void LoadAndSaveFromTGAFile()
        {
            var path = Path.Combine("results", nameof(LoadAndSaveFromTGAFile), "test.tga");
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata;

            DirectXTex.LoadFromTGAFile(TGAFilename, TGAFlags.None, &metadata, ref image);
            TexMetadata metadata1 = image.GetMetadata();

            metadata = image.GetMetadata();
            DirectXTex.SaveToTGAFile(image.GetImages(), 0, path, &metadata1);

            image.Release();
        }

        [Platform("Win")]
        [Test]
        public void LoadAndSaveFromWICMemory()
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata;
            Span<byte> src = LoadTexture(WICFilename);
            Blob blob = DirectXTex.CreateBlob();

            fixed (byte* srcPtr = src)
            {
                DirectXTex.LoadFromWICMemory(srcPtr, (nuint)src.Length, WICFlags.None, &metadata, ref image, default);
            }
            Guid guid = DirectXTex.GetWICCodec(WICCodecs.CodecPng);
            DirectXTex.SaveToWICMemory2(image.GetImages(), image.GetImageCount(), WICFlags.None, guid, ref blob, null, default);

            Span<byte> dest = blob.AsBytes();
            Assert.That(src.SequenceEqual(dest), Is.True);

            blob.Release();
            image.Release();
        }

        [Platform("Win")]
        [Test]
        public void LoadAndSaveFromWICFile()
        {
            var path = Path.Combine("results", nameof(LoadAndSaveFromTGAFile), "test.png");
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            ScratchImage image = DirectXTex.CreateScratchImage();

            TexMetadata metadata;
            Guid guid = DirectXTex.GetWICCodec(WICCodecs.CodecPng);
            DirectXTex.LoadFromWICFile(WICFilename, WICFlags.None, &metadata, ref image, default);
            DirectXTex.SaveToWICFile2(image.GetImages(), image.GetImageCount(), 0, guid, path, null, default);

            Span<byte> src = LoadTexture(WICFilename);
            Span<byte> dest = LoadTexture(path);
            Assert.That(src.SequenceEqual(dest), Is.True);

            image.Release();
        }

        public void Dispose()
        {
            if (Directory.Exists("results"))
                Directory.Delete("results", true);
            GC.SuppressFinalize(this);
        }
    }
}