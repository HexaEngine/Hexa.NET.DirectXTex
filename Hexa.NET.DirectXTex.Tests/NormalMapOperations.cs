﻿namespace Hexa.NET.DirectXTex.Tests
{
    public class NormalMapOperations
    {
        [Test]
        public unsafe void ComputeNormalMap()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };
            ScratchImage image = DirectXTex.CreateScratchImage();
            image.Initialize(metadata, CPFlags.None);

            ScratchImage normalMap = DirectXTex.CreateScratchImage();
            normalMap.Initialize(metadata, CPFlags.None);

            DirectXTex.ComputeNormalMap2(image.GetImages(), image.GetImageCount(), image.GetMetadata(), CNMAPFlags.Default, 2, (int)Format.FormatR8G8B8A8Unorm, normalMap);

            image.Release();
            normalMap.Release();
        }
    }
}