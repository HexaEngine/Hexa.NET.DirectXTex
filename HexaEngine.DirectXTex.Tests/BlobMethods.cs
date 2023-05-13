namespace HexaEngine.DirectXTex.Tests
{
    public unsafe class BlobMethods
    {
        [Test]
        public void CreateAndRelease()
        {
            TexBlob blob = new();
            blob.Release();
            if (blob.pBlob != null)
            {
                Assert.Fail("Mem leak");
            }
        }

        [Test]
        public void Init()
        {
            TexBlob blob = new();
            blob.Initialize(256);
            Assert.That(blob.GetBufferSize(), Is.EqualTo(256u));
            blob.Release();
            if (blob.pBlob != null)
            {
                Assert.Fail("Mem leak");
            }
        }

        [Test]
        public void GetBufferPointerAndBufferSizeAndWrite()
        {
            TexBlob blob = new();
            blob.Initialize(256);
            ulong size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(256u));
            void* pointer = blob.GetBufferPointer();
            Span<byte> bytes = new(pointer, (int)size);
            bytes.Fill(1);

            blob.Release();
            if (blob.pBlob != null)
            {
                Assert.Fail("Mem leak");
            }
        }

        [Test]
        public void Resize()
        {
            TexBlob blob = new();
            blob.Initialize(256);
            ulong size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(256u));

            blob.Resize(1024);
            size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(1024u));

            blob.Release();
            if (blob.pBlob != null)
            {
                Assert.Fail("Mem leak");
            }
        }

        [Test]
        public void Trim()
        {
            TexBlob blob = new();
            blob.Initialize(256);
            ulong size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(256u));

            blob.Trim(128);
            size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(128u));

            blob.Release();
            if (blob.pBlob != null)
            {
                Assert.Fail("Mem leak");
            }
        }
    }
}