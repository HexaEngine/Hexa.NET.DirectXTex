﻿namespace Example
{
    using Hexa.NET.DirectXTex;
    using System.Runtime.InteropServices;

    public static class Helper
    {
        public static void ThrowIfFailed(this int hr)
        {
            if (hr != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
        }
    }

    public class Program
    {
        private static unsafe void Main(string[] args)
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = default;

            string inputPath = "assets/textures/test.dds";
            DirectXTex.LoadFromDDSFile(inputPath, DDSFlags.None, ref metadata, ref image).ThrowIfFailed();

            ScratchImage mipChain = DirectXTex.CreateScratchImage();

            int mipLevels = 4;
            DirectXTex.GenerateMipMaps2(image.GetImages(), 1, ref metadata, TexFilterFlags.ForceNonWic, (ulong)mipLevels, ref mipChain).ThrowIfFailed();
            image.Release();

            metadata = mipChain.GetMetadata();

            string outputPath = "test.dds";
            DirectXTex.SaveToDDSFile2(mipChain.GetImages(), mipChain.GetImageCount(), ref metadata, DDSFlags.None, outputPath).ThrowIfFailed();

            mipChain.Release();
        }
    }
}