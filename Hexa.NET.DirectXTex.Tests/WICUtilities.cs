namespace Hexa.NET.DirectXTex.Tests
{
    public unsafe class WICUtilities
    {
        [Test]
        public void GetWICCodec()
        {
            var ptr = DirectXTex.GetWICCodec(WICCodecs.CodecPng);
            if (ptr == null)
            {
                Assert.Fail("Ptr is null");
            }
        }

        [Test]
        public void GetSetWICFactory()
        {
            byte isWIC2;
            var factory = DirectXTex.GetWICFactory(&isWIC2);
            DirectXTex.SetWICFactory(factory);
        }
    }
}