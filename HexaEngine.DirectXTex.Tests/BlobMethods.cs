namespace HexaEngine.DirectXTex.Tests
{
    public unsafe class BlobMethods
    {
        [Test]
        public void CreateAndRelease()
        {
            Blob blob = DirectXTex.CreateBlob();
            blob.Release();
        }

        [Test]
        public void Init()
        {
            Blob blob = DirectXTex.CreateBlob();
            blob.Initialize(256);
            Assert.That(blob.GetBufferSize(), Is.EqualTo((nuint)256));
            blob.Release();
        }

        [Test]
        public void GetBufferPointerAndBufferSizeAndWrite()
        {
            Blob blob = DirectXTex.CreateBlob();
            blob.Initialize(256);
            ulong size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(256u));
            void* pointer = blob.GetBufferPointer();
            Span<byte> bytes = new(pointer, (int)size);
            bytes.Fill(1);

            blob.Release();
        }

        [Test]
        public void Resize()
        {
            Blob blob = DirectXTex.CreateBlob();
            blob.Initialize(256);
            ulong size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(256u));

            blob.Resize(1024);
            size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(1024u));

            blob.Release();
        }

        [Test]
        public void Trim()
        {
            Blob blob = DirectXTex.CreateBlob();
            blob.Initialize(256);
            ulong size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(256u));

            blob.Trim(128);
            size = blob.GetBufferSize();
            Assert.That(size, Is.EqualTo(128u));

            blob.Release();
        }
    }
}