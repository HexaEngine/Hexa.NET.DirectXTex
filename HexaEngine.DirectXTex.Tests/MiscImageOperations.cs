namespace HexaEngine.DirectXTex.Tests
{
    using System.Numerics;

    public unsafe class MiscImageOperations
    {
        [Test]
        public void CopyRectangle()
        {
            TexMetadata metadataSrc = new()
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
            ScratchImage srcImage = DirectXTex.CreateScratchImage();
            DirectXTex.Initialize(srcImage, metadataSrc, CPFlags.None);

            TexMetadata metadataDest = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.FormatR8G8B8A8Unorm,
                Height = 512,
                Width = 512,
                MipLevels = 1,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };
            ScratchImage dstImage = DirectXTex.CreateScratchImage();
            DirectXTex.Initialize(dstImage, metadataDest, CPFlags.None);

            Rect rect = new() { X = 0, Y = 0, W = 64, H = 64 };
            DirectXTex.CopyRectangle(srcImage.GetImage(0, 0, 0), rect, dstImage.GetImage(0, 0, 0), TexFilterFlags.Default, 100, 50);

            srcImage.Release();
            dstImage.Release();
        }

        [Test]
        public void ComputeMSE()
        {
            TexMetadata metadataSrc = new()
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
            ScratchImage srcImage = DirectXTex.CreateScratchImage();
            srcImage.Initialize(metadataSrc, CPFlags.None);

            ScratchImage dstImage = DirectXTex.CreateScratchImage();
            dstImage.Initialize(metadataSrc, CPFlags.None);
            Vector4 mseV;
            float mse;

            DirectXTex.ComputeMSE(srcImage.GetImage(0, 0, 0), dstImage.GetImage(0, 0, 0), &mse, (float*)&mseV, CMSEFlags.Default);

            srcImage.Release();
            dstImage.Release();
        }

        private static Vector4 maxLum = Vector4.Zero;

        private static void EvaluateImageFunc(Vector4* pixels, nuint width, nuint y)
        {
            for (nuint j = 0; j < width; ++j)
            {
                Vector4 s_luminance = new(0.3f, 0.59f, 0.11f, 0.0f);

                Vector4 v = *pixels++;

                v = new(Vector4.Dot(v, s_luminance));

                maxLum = Vector4.Max(v, maxLum);
            }
        }

        [Test]
        public void EvaluateImage()
        {
            TexMetadata metadataSrc = new()
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
            ScratchImage srcImage = DirectXTex.CreateScratchImage();
            srcImage.Initialize(metadataSrc, CPFlags.None);

            EvaluateImageFunc evaluateImageFunc = new((nint)(delegate*<Vector4*, nuint, nuint, void>)&EvaluateImageFunc);
            DirectXTex.EvaluateImage(srcImage.GetImage(0, 0, 0), evaluateImageFunc);
        }

        public static bool NearEqual(Vector4 V1, Vector4 V2, Vector4 Epsilon)
        {
            return ((Math.Abs(V1.X - V2.X) <= Epsilon.X) &&
                     (Math.Abs(V1.Y - V2.Y) <= Epsilon.Y) &&
                     (Math.Abs(V1.Z - V2.Z) <= Epsilon.Z));
        }

        [Test]
        public void TransformImage()
        {
            static void func(Vector4* outPixels, Vector4* inPixels, nuint width, nuint y)
            {
                Vector4 s_chromaKey = new(0.0f, 1.0f, 0.0f, 0.0f);
                Vector4 s_tolerance = new(0.2f, 0.2f, 0.2f, 0.0f);

                for (nuint j = 0; j < width; ++j)
                {
                    Vector4 value = inPixels[j];

                    if (NearEqual(value, s_chromaKey, s_tolerance))
                    {
                        value = Vector4.Zero;
                    }

                    outPixels[j] = value;
                }
            };

            TexMetadata metadataSrc = new()
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
            ScratchImage srcImage = DirectXTex.CreateScratchImage();
            srcImage.Initialize(metadataSrc, CPFlags.None);

            ScratchImage dstImage = DirectXTex.CreateScratchImage();
            dstImage.Initialize(metadataSrc, CPFlags.None);
            TransformImageFunc transformImageFunc = new((nint)(delegate*<Vector4*, Vector4*, nuint, nuint, void>)&func);
            DirectXTex.TransformImage(srcImage.GetImage(0, 0, 0), transformImageFunc, dstImage);
        }
    }
}