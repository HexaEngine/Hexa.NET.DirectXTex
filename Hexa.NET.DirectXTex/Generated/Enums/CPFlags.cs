// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using HexaGen.Runtime;
using System.Numerics;
#if !STANDALONE
using Silk.NET.DXGI;
using Silk.NET.Direct2D;
using Silk.NET.Direct3D11;
using Silk.NET.Direct3D12;
#endif

namespace Hexa.NET.DirectXTex
{
	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.Enum, "CP_FLAGS")]
	[Flags]
	public enum CPFlags : int
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_NONE")]
		[NativeName(NativeNameType.Value, "0")]
		None = unchecked(0),

		/// <summary>
		/// Normal operation<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_LEGACY_DWORD")]
		[NativeName(NativeNameType.Value, "1")]
		LegacyDword = unchecked(1),

		/// <summary>
		/// Assume pitch is DWORD aligned instead of BYTE aligned<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_PARAGRAPH")]
		[NativeName(NativeNameType.Value, "2")]
		Paragraph = unchecked(2),

		/// <summary>
		/// Assume pitch is 16-byte aligned instead of BYTE aligned<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_YMM")]
		[NativeName(NativeNameType.Value, "4")]
		Ymm = unchecked(4),

		/// <summary>
		/// Assume pitch is 32-byte aligned instead of BYTE aligned<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_ZMM")]
		[NativeName(NativeNameType.Value, "8")]
		Zmm = unchecked(8),

		/// <summary>
		/// Assume pitch is 64-byte aligned instead of BYTE aligned<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_PAGE4K")]
		[NativeName(NativeNameType.Value, "512")]
		Page4K = unchecked(512),

		/// <summary>
		/// Assume pitch is 4096-byte aligned instead of BYTE aligned<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_BAD_DXTN_TAILS")]
		[NativeName(NativeNameType.Value, "4096")]
		BadDxtnTails = unchecked(4096),

		/// <summary>
		/// BC formats with malformed mipchain blocks smaller than 4x4<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_24BPP")]
		[NativeName(NativeNameType.Value, "65536")]
		Flags24Bpp = unchecked(65536),

		/// <summary>
		/// Override with a legacy 24 bits-per-pixel format size<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_16BPP")]
		[NativeName(NativeNameType.Value, "131072")]
		Flags16Bpp = unchecked(131072),

		/// <summary>
		/// Override with a legacy 16 bits-per-pixel format size<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_8BPP")]
		[NativeName(NativeNameType.Value, "262144")]
		Flags8Bpp = unchecked(262144),
	}
}
