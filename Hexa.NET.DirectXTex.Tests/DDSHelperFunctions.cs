namespace Hexa.NET.DirectXTex.Tests
{
    public unsafe class DDSHelperFunctions
    {
        [Test]
        public void EncodeDDSHeader()
        {
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

            byte[] data = new byte[8192];
            ulong required;
            fixed (byte* ptr = data)
            {
                DirectXTex.EncodeDDSHeader(ref metadata, DDSFlags.None, ptr, 8192, &required);
            }
        }
    }
}