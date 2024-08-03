// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HexaGen.Runtime;
using System.Numerics;
using Silk.NET.DXGI;
using Silk.NET.Direct2D;
using Silk.NET.Direct3D11;
using Silk.NET.Direct3D12;

namespace Hexa.NET.DirectXTex
{
	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.Delegate, "SetCustomProps")]
	[return: NativeName(NativeNameType.Type, "void")]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void SetCustomProps([NativeName(NativeNameType.Param, "pBag")] [NativeName(NativeNameType.Type, "IPropertyBag2*")] void* pBag);

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.Delegate, "GetMQR")]
	[return: NativeName(NativeNameType.Type, "void")]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void GetMQR([NativeName(NativeNameType.Param, "qMqr")] [NativeName(NativeNameType.Type, "IWICMetadataQueryReader*")] void* qMqr);

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.Delegate, "EvaluateImageFunc")]
	[return: NativeName(NativeNameType.Type, "void")]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void EvaluateImageFunc([NativeName(NativeNameType.Param, "pixels")] [NativeName(NativeNameType.Type, "const XMVECTOR*")] Vector4* pixels, [NativeName(NativeNameType.Param, "width")] [NativeName(NativeNameType.Type, "size_t")] ulong width, [NativeName(NativeNameType.Param, "y")] [NativeName(NativeNameType.Type, "size_t")] ulong y);

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.Delegate, "TransformImageFunc")]
	[return: NativeName(NativeNameType.Type, "void")]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void TransformImageFunc([NativeName(NativeNameType.Param, "outPixels")] [NativeName(NativeNameType.Type, "XMVECTOR*")] Vector4* outPixels, [NativeName(NativeNameType.Param, "inPixels")] [NativeName(NativeNameType.Type, "const XMVECTOR*")] Vector4* inPixels, [NativeName(NativeNameType.Param, "width")] [NativeName(NativeNameType.Type, "size_t")] ulong width, [NativeName(NativeNameType.Param, "y")] [NativeName(NativeNameType.Type, "size_t")] ulong y);

}
