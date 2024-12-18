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

namespace Hexa.NET.DirectXTex
{
	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.Enum, "TEX_FILTER_FLAGS")]
	[Flags]
	public enum TexFilterFlags : int
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP_U")]
		[NativeName(NativeNameType.Value, "1")]
		WrapU = unchecked(1),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP_V")]
		[NativeName(NativeNameType.Value, "2")]
		WrapV = unchecked(2),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP_W")]
		[NativeName(NativeNameType.Value, "4")]
		WrapW = unchecked(4),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP")]
		[NativeName(NativeNameType.Value, "7")]
		Wrap = unchecked(7),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR_U")]
		[NativeName(NativeNameType.Value, "16")]
		MirrorU = unchecked(16),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR_V")]
		[NativeName(NativeNameType.Value, "32")]
		MirrorV = unchecked(32),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR_W")]
		[NativeName(NativeNameType.Value, "64")]
		MirrorW = unchecked(64),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR")]
		[NativeName(NativeNameType.Value, "112")]
		Mirror = unchecked(112),

		/// <summary>
		/// Wrap vs. Mirror vs. Clamp filtering options<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SEPARATE_ALPHA")]
		[NativeName(NativeNameType.Value, "256")]
		SeparateAlpha = unchecked(256),

		/// <summary>
		/// Resize color and alpha channel independently<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FLOAT_X2BIAS")]
		[NativeName(NativeNameType.Value, "512")]
		FloatX2Bias = unchecked(512),

		/// <summary>
		/// Enable *2 - 1 conversion cases for unorm<br/>
		/// <<br/>
		/// ->float and positive-only float formats<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_RED")]
		[NativeName(NativeNameType.Value, "4096")]
		RgbCopyRed = unchecked(4096),

		/// <summary>
		/// Enable *2 - 1 conversion cases for unorm<br/>
		/// <<br/>
		/// ->float and positive-only float formats<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_GREEN")]
		[NativeName(NativeNameType.Value, "8192")]
		RgbCopyGreen = unchecked(8192),

		/// <summary>
		/// Enable *2 - 1 conversion cases for unorm<br/>
		/// <<br/>
		/// ->float and positive-only float formats<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_BLUE")]
		[NativeName(NativeNameType.Value, "16384")]
		RgbCopyBlue = unchecked(16384),

		/// <summary>
		/// Enable *2 - 1 conversion cases for unorm<br/>
		/// <<br/>
		/// ->float and positive-only float formats<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_ALPHA")]
		[NativeName(NativeNameType.Value, "32768")]
		RgbCopyAlpha = unchecked(32768),

		/// <summary>
		/// When converting RGB(A) to R, defaults to using grayscale. These flags indicate copying a specific channel instead<br/>
		/// When converting RGB(A) to RG, defaults to copying RED | GREEN. These flags control which channels are selected instead.<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_DITHER")]
		[NativeName(NativeNameType.Value, "65536")]
		Dither = unchecked(65536),

		/// <summary>
		/// Use ordered 4x4 dithering for any required conversions<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_DITHER_DIFFUSION")]
		[NativeName(NativeNameType.Value, "131072")]
		DitherDiffusion = unchecked(131072),

		/// <summary>
		/// Use error-diffusion dithering for any required conversions<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_POINT")]
		[NativeName(NativeNameType.Value, "1048576")]
		Point = unchecked(1048576),

		/// <summary>
		/// Use error-diffusion dithering for any required conversions<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_LINEAR")]
		[NativeName(NativeNameType.Value, "2097152")]
		Linear = unchecked(2097152),

		/// <summary>
		/// Use error-diffusion dithering for any required conversions<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_CUBIC")]
		[NativeName(NativeNameType.Value, "3145728")]
		Cubic = unchecked(3145728),

		/// <summary>
		/// Use error-diffusion dithering for any required conversions<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_BOX")]
		[NativeName(NativeNameType.Value, "4194304")]
		Box = unchecked(4194304),

		/// <summary>
		/// Equiv to Box filtering for mipmap generation<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FANT")]
		[NativeName(NativeNameType.Value, "4194304")]
		Fant = unchecked(4194304),

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_TRIANGLE")]
		[NativeName(NativeNameType.Value, "5242880")]
		Triangle = unchecked(5242880),

		/// <summary>
		/// Filtering mode to use for any required image resizing<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SRGB_IN")]
		[NativeName(NativeNameType.Value, "16777216")]
		SrgbIn = unchecked(16777216),

		/// <summary>
		/// Filtering mode to use for any required image resizing<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SRGB_OUT")]
		[NativeName(NativeNameType.Value, "33554432")]
		SrgbOut = unchecked(33554432),

		/// <summary>
		/// Filtering mode to use for any required image resizing<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SRGB")]
		[NativeName(NativeNameType.Value, "50331648")]
		Srgb = unchecked(50331648),

		/// <summary>
		/// sRGB <br/>
		/// <<br/>
		/// -> RGB for use in conversion operations<br/>
		/// if the input format type is IsSRGB(), then SRGB_IN is on by default<br/>
		/// if the output format type is IsSRGB(), then SRGB_OUT is on by default<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FORCE_NON_WIC")]
		[NativeName(NativeNameType.Value, "268435456")]
		ForceNonWic = unchecked(268435456),

		/// <summary>
		/// Forces use of the non-WIC path when both are an option<br/>
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FORCE_WIC")]
		[NativeName(NativeNameType.Value, "536870912")]
		ForceWic = unchecked(536870912),
	}
}
