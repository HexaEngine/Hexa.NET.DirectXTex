namespace Hexa.NET.DirectXTex.Tests
{
    public unsafe class TexMetadataMethods
    {
        private TexMetadata texVol = new()
        {
            ArraySize = 1,
            Depth = 6,
            Dimension = TexDimension.Texture3D,
            Format = (int)Format.R8G8B8A8Unorm,
            Height = 64,
            Width = 64,
            MipLevels = 4,
            MiscFlags = 0,
            MiscFlags2 = 0,
        };

        private TexMetadata texCube = new()
        {
            ArraySize = 6,
            Depth = 1,
            Dimension = TexDimension.Texture2D,
            Format = (int)Format.R8G8B8A8Unorm,
            Height = 64,
            Width = 64,
            MipLevels = 4,
            MiscFlags = (uint)TexMiscFlag.Texturecube,
            MiscFlags2 = 0,
        };

        private TexMetadata texArray = new()
        {
            ArraySize = 6,
            Depth = 1,
            Dimension = TexDimension.Texture2D,
            Format = (int)Format.R8G8B8A8Unorm,
            Height = 64,
            Width = 64,
            MipLevels = 4,
            MiscFlags = 0,
            MiscFlags2 = 0,
        };

        private TexMetadata texSingle = new()
        {
            ArraySize = 1,
            Depth = 1,
            Dimension = TexDimension.Texture2D,
            Format = (int)Format.R8G8B8A8Unorm,
            Height = 64,
            Width = 64,
            MipLevels = 4,
            MiscFlags = 0,
            MiscFlags2 = 0,
        };

        private TexMetadata texPMAlpha = new()
        {
            ArraySize = 1,
            Depth = 1,
            Dimension = TexDimension.Texture2D,
            Format = (int)Format.Bc7Unorm,
            Height = 64,
            Width = 64,
            MipLevels = 4,
            MiscFlags = 0,
            MiscFlags2 = 2,
        };

        [Test]
        public void ComputeIndex()
        {
            Assert.That(texArray.ComputeIndex(1, 1, 0), Is.EqualTo((nuint)5));
        }

        [Test]
        public void IsCubemap()
        {
            Assert.That(texVol.IsCubemap(), Is.False);
            Assert.That(texCube.IsCubemap(), Is.True);
            Assert.That(texArray.IsCubemap(), Is.False);
            Assert.That(texSingle.IsCubemap(), Is.False);
            Assert.That(texPMAlpha.IsCubemap(), Is.False);
        }

        [Test]
        public void IsPMAlpha()
        {
            Assert.That(texVol.IsPMAlpha(), Is.False);
            Assert.That(texCube.IsPMAlpha(), Is.False);
            Assert.That(texArray.IsPMAlpha(), Is.False);
            Assert.That(texSingle.IsPMAlpha(), Is.False);
            Assert.That(texPMAlpha.IsPMAlpha(), Is.True);
        }

        [Test]
        public void GetAndSetAlphaMode()
        {
            TexMetadata meta = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.Bc7Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            meta.SetAlphaMode(TexAlphaMode.Premultiplied);
            Assert.That(meta.GetAlphaMode(), Is.EqualTo(TexAlphaMode.Premultiplied));
        }

        [Test]
        public void IsVolumemap()
        {
            Assert.That(texVol.IsVolumemap(), Is.True);
            Assert.That(texCube.IsVolumemap(), Is.False);
            Assert.That(texArray.IsVolumemap(), Is.False);
            Assert.That(texSingle.IsVolumemap(), Is.False);
            Assert.That(texPMAlpha.IsVolumemap(), Is.False);
        }
    }
}