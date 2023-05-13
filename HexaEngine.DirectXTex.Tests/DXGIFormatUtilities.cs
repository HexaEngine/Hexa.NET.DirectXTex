namespace HexaEngine.DirectXTex.Tests
{
    using Silk.NET.DXGI;

    public unsafe class DXGIFormatUtilities
    {
        [Test]
        public void IsValid()
        {
            if (!DirectXTex.IsValid(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Should be valid");
            if (DirectXTex.IsValid((Silk.NET.DXGI.Format)(-1561658)))
                Trace.Fail("Should be invalid");
        }

        [Test]
        public void IsCompressed()
        {
            if (!DirectXTex.IsCompressed(Format.FormatBC7Unorm))
                Trace.Fail("Should be compressed");
            if (DirectXTex.IsCompressed(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Shouldn't be compressed");
        }

        [Test]
        public void IsPacked()
        {
            if (!DirectXTex.IsPacked(Format.FormatR8G8B8G8Unorm))
                Trace.Fail("Should be packed");
            if (DirectXTex.IsPacked(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Shouldn't be packed");
        }

        [Test]
        public void IsVideo()
        {
            if (!DirectXTex.IsVideo(Format.FormatNV12))
                Trace.Fail("Should be video");
            if (DirectXTex.IsVideo(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Shouldn't be video");
        }

        [Test]
        public void IsPlanar()
        {
            if (!DirectXTex.IsPlanar(Format.FormatP010))
                Trace.Fail("Should be planar");
            if (DirectXTex.IsPlanar(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Shouldn't be planar");
        }

        [Test]
        public void IsPalettized()
        {
            if (!DirectXTex.IsPalettized(Format.FormatAI44))
                Trace.Fail("Should be palettized");
            if (DirectXTex.IsPalettized(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Shouldn't be palettized");
        }

        [Test]
        public void IsDepthStencil()
        {
            if (!DirectXTex.IsDepthStencil(Format.FormatD24UnormS8Uint))
                Trace.Fail("Should be depthStencil");
            if (DirectXTex.IsDepthStencil(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Shouldn't be depthStencil");
        }

        [Test]
        public void IsSRGB()
        {
            if (!DirectXTex.IsSRGB(Format.FormatB8G8R8A8UnormSrgb))
                Trace.Fail("Should be SRGB");
            if (DirectXTex.IsSRGB(Format.FormatR16G16B16A16Uint))
                Trace.Fail("Shouldn't be SRGB");
        }

        [Test]
        public void IsTypeless()
        {
            if (!DirectXTex.IsTypeless(Format.FormatB8G8R8A8Typeless))
                Trace.Fail("Should be partial Typeless");
            if (!DirectXTex.IsTypeless(Format.FormatR32FloatX8X24Typeless))
                Trace.Fail("Should be partial Typeless");
            if (DirectXTex.IsTypeless(Format.FormatBC1Unorm))
                Trace.Fail("Shouldn't be partial Typeless");

            if (!DirectXTex.IsTypeless(Format.FormatB8G8R8A8Typeless, false))
                Trace.Fail("Should be Typeless");
            if (DirectXTex.IsTypeless(Format.FormatX32TypelessG8X24Uint, false))
                Trace.Fail("Shouldn't be Typeless");
        }

        [Test]
        public void HasAlpha()
        {
            if (!DirectXTex.HasAlpha(Format.FormatB8G8R8A8UnormSrgb))
                Trace.Fail("Should have alpha");
            if (DirectXTex.HasAlpha(Format.FormatG8R8G8B8Unorm))
                Trace.Fail("Shouldn't have alpha");
        }

        [Test]
        public void BitsPerPixel()
        {
            if (DirectXTex.BitsPerPixel(Format.FormatR8G8B8A8Uint) != 32)
                Trace.Fail("Should have 32Bits");
            if (DirectXTex.BitsPerPixel(Format.FormatR32G32B32A32Uint) == 32)
                Trace.Fail("Shouldn't have 32Bits");
        }

        [Test]
        public void BitsPerColor()
        {
            if (DirectXTex.BitsPerColor(Format.FormatR8G8B8A8Uint) != 8)
                Trace.Fail("Should have 8Bits per color");
            if (DirectXTex.BitsPerColor(Format.FormatR32G32B32A32Uint) == 8)
                Trace.Fail("Shouldn't have 8Bits per color");
        }

        [Test]
        public void FormatDataType()
        {
            if (DirectXTex.FormatDataType(Format.FormatA8Unorm) != FormatType.UNorm)
                Trace.Fail("Should be unorm");
            if (DirectXTex.FormatDataType(Format.FormatR32G32B32A32Uint) == FormatType.Float)
                Trace.Fail("Shouldn't be float");
        }

        [Test]
        public void ComputePitch()
        {
            uint width = 64;
            uint height = 64;
            ulong rowPitch = 0;
            ulong slicePitch = 0;
            HResult result = DirectXTex.ComputePitch(Format.FormatR8G8B8A8Uint, width, height, &rowPitch, &slicePitch, CPFlags.None);
            if (!result.IsSuccess)
                result.Throw();

            ulong rowPitch2 = width * 4;
            ulong slicePitch2 = rowPitch2 * height;

            Assert.That(rowPitch, Is.EqualTo(rowPitch2));
            Assert.That(slicePitch, Is.EqualTo(slicePitch2));
        }

        [Test]
        public void ComputeScanlines()
        {
            uint height = 64;
            var result = DirectXTex.ComputeScanlines(Format.FormatR8G8B8A8Uint, height);
            ulong expected = 64;
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void MakeSRGB()
        {
            var result = DirectXTex.MakeSRGB(Format.FormatBC1Unorm);
            Assert.That(result, Is.EqualTo(Format.FormatBC1UnormSrgb));
        }

        [Test]
        public void MakeTypeless()
        {
            var result = DirectXTex.MakeTypeless(Format.FormatBC1Unorm);
            Assert.That(result, Is.EqualTo(Format.FormatBC1Typeless));
        }

        [Test]
        public void MakeTypelessUNORM()
        {
            var result = DirectXTex.MakeTypelessUNORM(Format.FormatBC1Typeless);
            Assert.That(result, Is.EqualTo(Format.FormatBC1Unorm));
        }

        [Test]
        public void MakeTypelessFLOAT()
        {
            var result = DirectXTex.MakeTypelessFLOAT(Format.FormatR32G32B32A32Typeless);
            Assert.That(result, Is.EqualTo(Format.FormatR32G32B32A32Float));
        }
    }
}