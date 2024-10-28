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
	[NativeName(NativeNameType.Enum, "TGA_FLAGS")]
	[Flags]
	public enum TGAFlags : int
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_NONE")]
		[NativeName(NativeNameType.Value, "0")]
		None = unchecked(0),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_BGR")]
		[NativeName(NativeNameType.Value, "1")]
		Bgr = unchecked(1),

		/// <summary>
		/// 24bpp files are returned as BGRX; 32bpp files are returned as BGRA<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_ALLOW_ALL_ZERO_ALPHA")]
		[NativeName(NativeNameType.Value, "2")]
		AllowAllZeroAlpha = unchecked(2),

		/// <summary>
		/// If the loaded image has an all zero alpha channel, normally we assume it should be opaque. This flag leaves it alone.<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_IGNORE_SRGB")]
		[NativeName(NativeNameType.Value, "16")]
		IgnoreSrgb = unchecked(16),

		/// <summary>
		/// Ignores sRGB TGA 2.0 metadata if present in the file<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_FORCE_SRGB")]
		[NativeName(NativeNameType.Value, "32")]
		ForceSrgb = unchecked(32),

		/// <summary>
		/// Writes sRGB metadata into the file reguardless of format (TGA 2.0 only)<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_FORCE_LINEAR")]
		[NativeName(NativeNameType.Value, "64")]
		ForceLinear = unchecked(64),

		/// <summary>
		/// Writes linear gamma metadata into the file reguardless of format (TGA 2.0 only)<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_DEFAULT_SRGB")]
		[NativeName(NativeNameType.Value, "128")]
		DefaultSrgb = unchecked(128),
	}
}