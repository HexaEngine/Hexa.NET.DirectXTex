namespace HexaEngine.DirectXTex.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Utils
    {
        public static void ThrowHResult(this int code)
        {
            ResultCode resultCode = (ResultCode)code;
            if (resultCode != ResultCode.S_OK)
            {
                throw new D3D11Exception(resultCode);
            }
        }

        public static unsafe Span<byte> AsBytes(this Blob blob)
        {
            return new(blob.GetBufferPointer(), (int)blob.GetBufferSize());
        }
    }
}