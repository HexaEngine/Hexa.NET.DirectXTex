namespace Hexa.NET.DirectXTex
{
    using System.Runtime.InteropServices;

    public static unsafe partial class DirectXTex
    {
        static DirectXTex()
        {
            InitApi();
        }

        public static string GetLibraryName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "DirectXTex";
            }
            return "libDirectXTex";
        }
    }
}