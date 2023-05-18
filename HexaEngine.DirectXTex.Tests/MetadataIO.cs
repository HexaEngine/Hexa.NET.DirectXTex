namespace HexaEngine.DirectXTex.Tests
{
    public unsafe class MetadataIO
    {
        private const string DDSFilename = "assets\\textures\\test.dds";
        private const string HDRFilename = "assets\\textures\\test.hdr";
        private const string TGAFilename = "assets\\textures\\test.tga";
        private const string WICFilename = "assets\\textures\\test.png";

        private static byte[] LoadTexture(string path) => File.ReadAllBytes(path);

        [Test]
        public void GetMetadataFromDDSMemoryAndFile()
        {
            TexMetadata metadata1;
            TexMetadata metadata2;

            Span<byte> bytes = LoadTexture(DDSFilename);
            fixed (byte* ptr = bytes)
            {
                DirectXTex.GetMetadataFromDDSMemory(ptr, (nuint)bytes.Length, DDSFlags.None, &metadata1);
            }

            DirectXTex.GetMetadataFromDDSFile(DDSFilename, DDSFlags.None, &metadata2);

            Assert.That(metadata2, Is.EqualTo(metadata1));
        }

        [Test]
        public void GetMetadataFromHDRMemoryAndFile()
        {
            TexMetadata metadata1;
            TexMetadata metadata2;

            Span<byte> bytes = LoadTexture(HDRFilename);
            fixed (byte* ptr = bytes)
            {
                DirectXTex.GetMetadataFromHDRMemory(ptr, (nuint)bytes.Length, &metadata1);
            }

            DirectXTex.GetMetadataFromHDRFile(HDRFilename, &metadata2);

            Assert.That(metadata2, Is.EqualTo(metadata1));
        }

        [Test]
        public void GetMetadataFromTGAMemoryAndFile()
        {
            TexMetadata metadata1;
            TexMetadata metadata2;

            Span<byte> bytes = LoadTexture(TGAFilename);
            fixed (byte* ptr = bytes)
            {
                DirectXTex.GetMetadataFromTGAMemory(ptr, (nuint)bytes.Length, TGAFlags.None, &metadata1);
            }

            DirectXTex.GetMetadataFromTGAFile(TGAFilename, TGAFlags.None, &metadata2);

            Assert.That(metadata2, Is.EqualTo(metadata1));
        }

        [Test]
        public void GetMetadataFromWICMemoryAndFile()
        {
            TexMetadata metadata1;
            TexMetadata metadata2;

            Span<byte> bytes = LoadTexture(WICFilename);
            fixed (byte* ptr = bytes)
            {
                DirectXTex.GetMetadataFromWICMemory(ptr, (nuint)bytes.Length, WICFlags.None, &metadata1);
            }

            DirectXTex.GetMetadataFromWICFile(WICFilename, WICFlags.None, &metadata2);

            Assert.That(metadata2, Is.EqualTo(metadata1));
        }
    }
}