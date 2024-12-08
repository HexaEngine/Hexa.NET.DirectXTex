namespace Hexa.NET.DirectXTex.Tests
{
    public unsafe class DXGIFormatUtilities
    {
        [Test]
        public void IsValid()
        {
            if (!DirectXTex.IsValid((int)Format.R16G16B16A16Uint))
                Trace.Fail("Should be valid");
            if (DirectXTex.IsValid((int)(Format)(-1561658)))
                Trace.Fail("Should be invalid");
        }

        [Test]
        public void IsCompressed()
        {
            if (!DirectXTex.IsCompressed((int)Format.Bc7Unorm))
                Trace.Fail("Should be compressed");
            if (DirectXTex.IsCompressed((int)Format.R16G16B16A16Uint))
                Trace.Fail("Shouldn't be compressed");
        }

        [Test]
        public void IsPacked()
        {
            if (!DirectXTex.IsPacked((int)Format.R8G8B8G8Unorm))
                Trace.Fail("Should be packed");
            if (DirectXTex.IsPacked((int)Format.R16G16B16A16Uint))
                Trace.Fail("Shouldn't be packed");
        }

        [Test]
        public void IsVideo()
        {
            if (!DirectXTex.IsVideo((int)Format.Nv12))
                Trace.Fail("Should be video");
            if (DirectXTex.IsVideo((int)Format.R16G16B16A16Uint))
                Trace.Fail("Shouldn't be video");
        }

        [Test]
        public void IsPlanar()
        {
            if (!DirectXTex.IsPlanar((int)Format.P010))
                Trace.Fail("Should be planar");
            if (DirectXTex.IsPlanar((int)Format.R16G16B16A16Uint))
                Trace.Fail("Shouldn't be planar");
        }

        [Test]
        public void IsPalettized()
        {
            if (!DirectXTex.IsPalettized((int)Format.Ai44))
                Trace.Fail("Should be palettized");
            if (DirectXTex.IsPalettized((int)Format.R16G16B16A16Uint))
                Trace.Fail("Shouldn't be palettized");
        }

        [Test]
        public void IsDepthStencil()
        {
            if (!DirectXTex.IsDepthStencil((int)Format.D24UnormS8Uint))
                Trace.Fail("Should be depthStencil");
            if (DirectXTex.IsDepthStencil((int)Format.R16G16B16A16Uint))
                Trace.Fail("Shouldn't be depthStencil");
        }

        [Test]
        public void IsSRGB()
        {
            if (!DirectXTex.IsSRGB((int)Format.B8G8R8A8UnormSrgb))
                Trace.Fail("Should be SRGB");
            if (DirectXTex.IsSRGB((int)Format.R16G16B16A16Uint))
                Trace.Fail("Shouldn't be SRGB");
        }

        [Test]
        public void IsTypeless()
        {
            if (!DirectXTex.IsTypeless((int)Format.B8G8R8A8Typeless, true))
                Trace.Fail("Should be partial Typeless");
            if (!DirectXTex.IsTypeless((int)Format.R32FloatX8X24Typeless, true))
                Trace.Fail("Should be partial Typeless");
            if (DirectXTex.IsTypeless((int)Format.Bc1Unorm, true))
                Trace.Fail("Shouldn't be partial Typeless");

            if (!DirectXTex.IsTypeless((int)Format.B8G8R8A8Typeless, false))
                Trace.Fail("Should be Typeless");
            if (DirectXTex.IsTypeless((int)Format.X32TypelessG8X24Uint, false))
                Trace.Fail("Shouldn't be Typeless");
        }

        [Test]
        public void HasAlpha()
        {
            if (!DirectXTex.HasAlpha((int)Format.B8G8R8A8UnormSrgb))
                Trace.Fail("Should have alpha");
            if (DirectXTex.HasAlpha((int)Format.G8R8G8B8Unorm))
                Trace.Fail("Shouldn't have alpha");
        }

        [Test]
        public void BitsPerPixel()
        {
            if (DirectXTex.BitsPerPixel((int)Format.R8G8B8A8Uint) != 32)
                Trace.Fail("Should have 32Bits");
            if (DirectXTex.BitsPerPixel((int)Format.R32G32B32A32Uint) == 32)
                Trace.Fail("Shouldn't have 32Bits");
        }

        [Test]
        public void BitsPerColor()
        {
            if (DirectXTex.BitsPerColor((int)Format.R8G8B8A8Uint) != 8)
                Trace.Fail("Should have 8Bits per color");
            if (DirectXTex.BitsPerColor((int)Format.R32G32B32A32Uint) == 8)
                Trace.Fail("Shouldn't have 8Bits per color");
        }

        [Test]
        public void FormatDataType()
        {
            if (DirectXTex.FormatDataType((int)Format.A8Unorm) != FormatType.Unorm)
                Trace.Fail("Should be unorm");
            if (DirectXTex.FormatDataType((int)Format.R32G32B32A32Uint) == FormatType.Float)
                Trace.Fail("Shouldn't be float");
        }

        [Test]
        public void ComputePitch()
        {
            uint width = 64;
            uint height = 64;
            nuint rowPitch = 0;
            nuint slicePitch = 0;
            DirectXTex.ComputePitch((int)Format.R8G8B8A8Uint, width, height, &rowPitch, &slicePitch, CPFlags.None).ThrowIf();

            nuint rowPitch2 = width * 4;
            nuint slicePitch2 = rowPitch2 * height;

            Assert.That(rowPitch, Is.EqualTo(rowPitch2));
            Assert.That(slicePitch, Is.EqualTo(slicePitch2));
        }

        [Test]
        public void ComputeScanlines()
        {
            uint height = 64;
            var result = DirectXTex.ComputeScanlines((int)Format.R8G8B8A8Uint, height);
            nuint expected = 64;
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void MakeSRGB()
        {
            var result = DirectXTex.MakeSRGB((int)Format.Bc1Unorm);
            Assert.That((Format)result, Is.EqualTo(Format.Bc1UnormSrgb));
        }

        [Test]
        public void MakeTypeless()
        {
            var result = DirectXTex.MakeTypeless((int)Format.Bc1Unorm);
            Assert.That((Format)result, Is.EqualTo(Format.Bc1Typeless));
        }

        [Test]
        public void MakeTypelessUNORM()
        {
            var result = DirectXTex.MakeTypelessUNORM((int)Format.Bc1Typeless);
            Assert.That((Format)result, Is.EqualTo(Format.Bc1Unorm));
        }

        [Test]
        public void MakeTypelessFLOAT()
        {
            var result = DirectXTex.MakeTypelessFLOAT((int)Format.R32G32B32A32Typeless);
            Assert.That((Format)result, Is.EqualTo(Format.R32G32B32A32Float));
        }
    }
}