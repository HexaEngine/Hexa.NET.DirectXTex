namespace Hexa.NET.DirectXTex
{
#if STANDALONE
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("dc8e63f3-d12b-4952-b47b-5e45026a862d")]
    public struct ID3D11Resource
    {
        public unsafe void** LpVtbl;

        public unsafe ID3D11Resource(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator ID3D11Resource(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("db6f6ddb-ac77-4e88-8253-819df9bbf140")]
    public struct ID3D11Device
    {
        public unsafe void** LpVtbl;

        public unsafe ID3D11Device(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator ID3D11Device(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("c0bfa96c-e089-44fb-8eaf-26f8796190da")]
    public struct ID3D11DeviceContext
    {
        public unsafe void** LpVtbl;

        public unsafe ID3D11DeviceContext(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator ID3D11DeviceContext(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("b0e06fe0-8192-4e1a-b1ca-36d7414710b2")]
    public struct ID3D11ShaderResourceView
    {
        public unsafe void** LpVtbl;

        public unsafe ID3D11ShaderResourceView(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator ID3D11ShaderResourceView(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("ec5ec8a9-c395-4314-9c77-54d7a935ff70")]
    public struct IWICImagingFactory
    {
        public unsafe void** LpVtbl;

        public unsafe IWICImagingFactory(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator IWICImagingFactory(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("696442be-a72e-4059-bc79-5b5c98040fad")]
    public struct ID3D12Resource
    {
        public unsafe void** LpVtbl;

        public unsafe ID3D12Resource(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator ID3D12Resource(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("189819f1-1db6-4b57-be54-1821339b85f7")]
    public struct ID3D12Device
    {
        public unsafe void** LpVtbl;

        public unsafe ID3D12Device(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator ID3D12Device(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }

    /// <summary>
    /// Dummy implementation.
    /// </summary>
    [Guid("0ec870a6-5d7e-4c22-8cfc-5baae07616ed")]
    public struct ID3D12CommandQueue
    {
        public unsafe void** LpVtbl;

        public unsafe ID3D12CommandQueue(void** lpVtbl = null)
        {
            LpVtbl = lpVtbl;
        }

        public static unsafe implicit operator ID3D12CommandQueue(void** lpVtbl)
        {
            return new(lpVtbl);
        }
    }
#endif
}