namespace Hexa.NET.DirectXTex
{
    public static unsafe partial class DirectXTex
    {
        static DirectXTex()
        {
            InitApi();
        }

        public static nint GetLibraryName()
        {
            return LibraryLoader.LoadLibrary();
        }
    }
}