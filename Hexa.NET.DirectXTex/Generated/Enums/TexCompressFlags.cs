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
	[NativeName(NativeNameType.Enum, "TEX_COMPRESS_FLAGS")]
	[Flags]
	public enum TexCompressFlags : int
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_RGB_DITHER")]
		[NativeName(NativeNameType.Value, "65536")]
		RgbDither = unchecked(65536),

		/// <summary>
		/// Enables dithering RGB colors for BC1-3 compression<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_A_DITHER")]
		[NativeName(NativeNameType.Value, "131072")]
		ADither = unchecked(131072),

		/// <summary>
		/// Enables dithering alpha for BC1-3 compression<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_DITHER")]
		[NativeName(NativeNameType.Value, "196608")]
		Dither = unchecked(196608),

		/// <summary>
		/// Enables both RGB and alpha dithering for BC1-3 compression<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_UNIFORM")]
		[NativeName(NativeNameType.Value, "262144")]
		Uniform = unchecked(262144),

		/// <summary>
		/// Uniform color weighting for BC1-3 compression; by default uses perceptual weighting<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_BC7_USE_3SUBSETS")]
		[NativeName(NativeNameType.Value, "524288")]
		Bc7Use3Subsets = unchecked(524288),

		/// <summary>
		/// Enables exhaustive search for BC7 compress for mode 0 and 2; by default skips trying these modes<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_BC7_QUICK")]
		[NativeName(NativeNameType.Value, "1048576")]
		Bc7Quick = unchecked(1048576),

		/// <summary>
		/// Minimal modes (usually mode 6) for BC7 compression<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_SRGB_IN")]
		[NativeName(NativeNameType.Value, "16777216")]
		SrgbIn = unchecked(16777216),

		/// <summary>
		/// Minimal modes (usually mode 6) for BC7 compression<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_SRGB_OUT")]
		[NativeName(NativeNameType.Value, "33554432")]
		SrgbOut = unchecked(33554432),

		/// <summary>
		/// Minimal modes (usually mode 6) for BC7 compression<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_SRGB")]
		[NativeName(NativeNameType.Value, "50331648")]
		Srgb = unchecked(50331648),

		/// <summary>
		/// if the input format type is IsSRGB(), then SRGB_IN is on by default<br/>
		/// if the output format type is IsSRGB(), then SRGB_OUT is on by default<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_PARALLEL")]
		[NativeName(NativeNameType.Value, "268435456")]
		Parallel = unchecked(268435456),
	}
}
