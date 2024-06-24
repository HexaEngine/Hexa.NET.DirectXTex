namespace Hexa.NET.DirectXTex
{
    public static unsafe partial class DirectXTex
    {
        static DirectXTex()
        {
            LibraryLoader.SetImportResolver();
        }
    }
}