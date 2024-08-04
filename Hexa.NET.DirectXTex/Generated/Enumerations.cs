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
	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "FORMAT_TYPE")]
	public enum FormatType
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "FORMAT_TYPE_TYPELESS")]
		[NativeName(NativeNameType.Value, "0")]
		Typeless = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "FORMAT_TYPE_FLOAT")]
		[NativeName(NativeNameType.Value, "1")]
		Float = unchecked(1),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "FORMAT_TYPE_UNORM")]
		[NativeName(NativeNameType.Value, "2")]
		Unorm = unchecked(2),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "FORMAT_TYPE_SNORM")]
		[NativeName(NativeNameType.Value, "3")]
		Snorm = unchecked(3),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "FORMAT_TYPE_UINT")]
		[NativeName(NativeNameType.Value, "4")]
		Uint = unchecked(4),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "FORMAT_TYPE_SINT")]
		[NativeName(NativeNameType.Value, "5")]
		Sint = unchecked(5),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "CP_FLAGS")]
	public enum CPFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_NONE")]
		[NativeName(NativeNameType.Value, "0")]
		None = unchecked(0),

		/// <summary>		/// Normal operation<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_LEGACY_DWORD")]
		[NativeName(NativeNameType.Value, "1")]
		LegacyDword = unchecked(1),

		/// <summary>		/// Assume pitch is DWORD aligned instead of BYTE aligned<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_PARAGRAPH")]
		[NativeName(NativeNameType.Value, "2")]
		Paragraph = unchecked(2),

		/// <summary>		/// Assume pitch is 16-byte aligned instead of BYTE aligned<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_YMM")]
		[NativeName(NativeNameType.Value, "4")]
		Ymm = unchecked(4),

		/// <summary>		/// Assume pitch is 32-byte aligned instead of BYTE aligned<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_ZMM")]
		[NativeName(NativeNameType.Value, "8")]
		Zmm = unchecked(8),

		/// <summary>		/// Assume pitch is 64-byte aligned instead of BYTE aligned<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_PAGE4K")]
		[NativeName(NativeNameType.Value, "512")]
		Page4K = unchecked(512),

		/// <summary>		/// Assume pitch is 4096-byte aligned instead of BYTE aligned<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_BAD_DXTN_TAILS")]
		[NativeName(NativeNameType.Value, "4096")]
		BadDxtnTails = unchecked(4096),

		/// <summary>		/// BC formats with malformed mipchain blocks smaller than 4x4<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_24BPP")]
		[NativeName(NativeNameType.Value, "65536")]
		Flags24Bpp = unchecked(65536),

		/// <summary>		/// Override with a legacy 24 bits-per-pixel format size<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_16BPP")]
		[NativeName(NativeNameType.Value, "131072")]
		Flags16Bpp = unchecked(131072),

		/// <summary>		/// Override with a legacy 16 bits-per-pixel format size<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CP_FLAGS_8BPP")]
		[NativeName(NativeNameType.Value, "262144")]
		Flags8Bpp = unchecked(262144),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_DIMENSION")]
	public enum TexDimension
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_DIMENSION_TEXTURE1D")]
		[NativeName(NativeNameType.Value, "2")]
		Texture1D = unchecked(2),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_DIMENSION_TEXTURE2D")]
		[NativeName(NativeNameType.Value, "3")]
		Texture2D = unchecked(3),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_DIMENSION_TEXTURE3D")]
		[NativeName(NativeNameType.Value, "4")]
		Texture3D = unchecked(4),

	}

	/// <summary>	/// Subset here matches D3D10_RESOURCE_MISC_FLAG and D3D11_RESOURCE_MISC_FLAG<br/>	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_MISC_FLAG")]
	public enum TexMiscFlag
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_MISC_TEXTURECUBE")]
		[NativeName(NativeNameType.Value, "0x4L")]
		Texturecube = unchecked((int)0x4L),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_MISC_FLAG2")]
	public enum TexMiscFlag2
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_MISC2_ALPHA_MODE_MASK")]
		[NativeName(NativeNameType.Value, "0x7L")]
		Misc2AlphaModeMask = unchecked((int)0x7L),

	}

	/// <summary>	/// Matches DDS_ALPHA_MODE, encoded in MISC_FLAGS2<br/>	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_ALPHA_MODE")]
	public enum TexAlphaMode
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_ALPHA_MODE_UNKNOWN")]
		[NativeName(NativeNameType.Value, "0")]
		Unknown = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_ALPHA_MODE_STRAIGHT")]
		[NativeName(NativeNameType.Value, "1")]
		Straight = unchecked(1),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_ALPHA_MODE_PREMULTIPLIED")]
		[NativeName(NativeNameType.Value, "2")]
		Premultiplied = unchecked(2),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_ALPHA_MODE_OPAQUE")]
		[NativeName(NativeNameType.Value, "3")]
		Opaque = unchecked(3),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_ALPHA_MODE_CUSTOM")]
		[NativeName(NativeNameType.Value, "4")]
		Custom = unchecked(4),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "DDS_FLAGS")]
	public enum DDSFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_NONE")]
		[NativeName(NativeNameType.Value, "0")]
		None = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_LEGACY_DWORD")]
		[NativeName(NativeNameType.Value, "1")]
		LegacyDword = unchecked(1),

		/// <summary>		/// Assume pitch is DWORD aligned instead of BYTE aligned (used by some legacy DDS files)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_NO_LEGACY_EXPANSION")]
		[NativeName(NativeNameType.Value, "2")]
		NoLegacyExpansion = unchecked(2),

		/// <summary>		/// Do not implicitly convert legacy formats that result in larger pixel sizes (24 bpp, 3:3:2, A8L8, A4L4, P8, A8P8)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_NO_R10B10G10A2_FIXUP")]
		[NativeName(NativeNameType.Value, "4")]
		Nor10b10g10a2Fixup = unchecked(4),

		/// <summary>		/// Do not use work-around for long-standing D3DX DDS file format issue which reversed the 10:10:10:2 color order masks<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_FORCE_RGB")]
		[NativeName(NativeNameType.Value, "8")]
		ForceRgb = unchecked(8),

		/// <summary>		/// Convert DXGI 1.1 BGR formats to DXGI_FORMAT_R8G8B8A8_UNORM to avoid use of optional WDDM 1.1 formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_NO_16BPP")]
		[NativeName(NativeNameType.Value, "16")]
		No16Bpp = unchecked(16),

		/// <summary>		/// Conversions avoid use of 565, 5551, and 4444 formats and instead expand to 8888 to avoid use of optional WDDM 1.2 formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_EXPAND_LUMINANCE")]
		[NativeName(NativeNameType.Value, "32")]
		ExpandLuminance = unchecked(32),

		/// <summary>		/// When loading legacy luminance formats expand replicating the color channels rather than leaving them packed (L8, L16, A8L8)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_BAD_DXTN_TAILS")]
		[NativeName(NativeNameType.Value, "64")]
		BadDxtnTails = unchecked(64),

		/// <summary>		/// Some older DXTn DDS files incorrectly handle mipchain tails for blocks smaller than 4x4<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_FORCE_DX10_EXT")]
		[NativeName(NativeNameType.Value, "65536")]
		ForceDx10Ext = unchecked(65536),

		/// <summary>		/// Always use the 'DX10' header extension for DDS writer (i.e. don't try to write DX9 compatible DDS files)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_FORCE_DX10_EXT_MISC2")]
		[NativeName(NativeNameType.Value, "131072")]
		ForceDx10ExtMisc2 = unchecked(131072),

		/// <summary>		/// DDS_FLAGS_FORCE_DX10_EXT including miscFlags2 information (result may not be compatible with D3DX10 or D3DX11)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_FORCE_DX9_LEGACY")]
		[NativeName(NativeNameType.Value, "262144")]
		ForceDx9Legacy = unchecked(262144),

		/// <summary>		/// Force use of legacy header for DDS writer (will fail if unable to write as such)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "DDS_FLAGS_ALLOW_LARGE_FILES")]
		[NativeName(NativeNameType.Value, "16777216")]
		AllowLargeFiles = unchecked(16777216),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "TGA_FLAGS")]
	public enum TGAFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_NONE")]
		[NativeName(NativeNameType.Value, "0")]
		None = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_BGR")]
		[NativeName(NativeNameType.Value, "1")]
		Bgr = unchecked(1),

		/// <summary>		/// 24bpp files are returned as BGRX; 32bpp files are returned as BGRA<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_ALLOW_ALL_ZERO_ALPHA")]
		[NativeName(NativeNameType.Value, "2")]
		AllowAllZeroAlpha = unchecked(2),

		/// <summary>		/// If the loaded image has an all zero alpha channel, normally we assume it should be opaque. This flag leaves it alone.<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_IGNORE_SRGB")]
		[NativeName(NativeNameType.Value, "16")]
		IgnoreSrgb = unchecked(16),

		/// <summary>		/// Ignores sRGB TGA 2.0 metadata if present in the file<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_FORCE_SRGB")]
		[NativeName(NativeNameType.Value, "32")]
		ForceSrgb = unchecked(32),

		/// <summary>		/// Writes sRGB metadata into the file reguardless of format (TGA 2.0 only)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_FORCE_LINEAR")]
		[NativeName(NativeNameType.Value, "64")]
		ForceLinear = unchecked(64),

		/// <summary>		/// Writes linear gamma metadata into the file reguardless of format (TGA 2.0 only)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TGA_FLAGS_DEFAULT_SRGB")]
		[NativeName(NativeNameType.Value, "128")]
		DefaultSrgb = unchecked(128),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "WIC_FLAGS")]
	public enum WICFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_NONE")]
		[NativeName(NativeNameType.Value, "0")]
		None = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_FORCE_RGB")]
		[NativeName(NativeNameType.Value, "1")]
		ForceRgb = unchecked(1),

		/// <summary>		/// Loads DXGI 1.1 BGR formats as DXGI_FORMAT_R8G8B8A8_UNORM to avoid use of optional WDDM 1.1 formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_NO_X2_BIAS")]
		[NativeName(NativeNameType.Value, "2")]
		Nox2Bias = unchecked(2),

		/// <summary>		/// Loads DXGI 1.1 X2 10:10:10:2 format as DXGI_FORMAT_R10G10B10A2_UNORM<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_NO_16BPP")]
		[NativeName(NativeNameType.Value, "4")]
		No16Bpp = unchecked(4),

		/// <summary>		/// Loads 565, 5551, and 4444 formats as 8888 to avoid use of optional WDDM 1.2 formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_ALLOW_MONO")]
		[NativeName(NativeNameType.Value, "8")]
		AllowMono = unchecked(8),

		/// <summary>		/// Loads 1-bit monochrome (black <br/>		/// &<br/>		/// white) as R1_UNORM rather than 8-bit grayscale<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_ALL_FRAMES")]
		[NativeName(NativeNameType.Value, "16")]
		AllFrames = unchecked(16),

		/// <summary>		/// Loads all images in a multi-frame file, converting/resizing to match the first frame as needed, defaults to 0th frame otherwise<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_IGNORE_SRGB")]
		[NativeName(NativeNameType.Value, "32")]
		IgnoreSrgb = unchecked(32),

		/// <summary>		/// Ignores sRGB metadata if present in the file<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_FORCE_SRGB")]
		[NativeName(NativeNameType.Value, "64")]
		ForceSrgb = unchecked(64),

		/// <summary>		/// Writes sRGB metadata into the file reguardless of format<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_FORCE_LINEAR")]
		[NativeName(NativeNameType.Value, "128")]
		ForceLinear = unchecked(128),

		/// <summary>		/// Writes linear gamma metadata into the file reguardless of format<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_DEFAULT_SRGB")]
		[NativeName(NativeNameType.Value, "256")]
		DefaultSrgb = unchecked(256),

		/// <summary>		/// If no colorspace is specified, assume sRGB<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_DITHER")]
		[NativeName(NativeNameType.Value, "65536")]
		Dither = unchecked(65536),

		/// <summary>		/// Use ordered 4x4 dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_DITHER_DIFFUSION")]
		[NativeName(NativeNameType.Value, "131072")]
		DitherDiffusion = unchecked(131072),

		/// <summary>		/// Use error-diffusion dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_FILTER_POINT")]
		[NativeName(NativeNameType.Value, "1048576")]
		FilterPoint = unchecked(1048576),

		/// <summary>		/// Use error-diffusion dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_FILTER_LINEAR")]
		[NativeName(NativeNameType.Value, "2097152")]
		FilterLinear = unchecked(2097152),

		/// <summary>		/// Use error-diffusion dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_FILTER_CUBIC")]
		[NativeName(NativeNameType.Value, "3145728")]
		FilterCubic = unchecked(3145728),

		/// <summary>		/// Combination of Linear and Box filter<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_FLAGS_FILTER_FANT")]
		[NativeName(NativeNameType.Value, "4194304")]
		FilterFant = unchecked(4194304),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_FR_FLAGS")]
	public enum TexFRFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FR_ROTATE0")]
		[NativeName(NativeNameType.Value, "0")]
		Rotate0 = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FR_ROTATE90")]
		[NativeName(NativeNameType.Value, "1")]
		Rotate90 = unchecked(1),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FR_ROTATE180")]
		[NativeName(NativeNameType.Value, "2")]
		Rotate180 = unchecked(2),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FR_ROTATE270")]
		[NativeName(NativeNameType.Value, "3")]
		Rotate270 = unchecked(3),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FR_FLIP_HORIZONTAL")]
		[NativeName(NativeNameType.Value, "8")]
		FlipHorizontal = unchecked(8),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FR_FLIP_VERTICAL")]
		[NativeName(NativeNameType.Value, "16")]
		FlipVertical = unchecked(16),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_FILTER_FLAGS")]
	public enum TexFilterFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP_U")]
		[NativeName(NativeNameType.Value, "1")]
		Wrapu = unchecked(1),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP_V")]
		[NativeName(NativeNameType.Value, "2")]
		Wrapv = unchecked(2),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP_W")]
		[NativeName(NativeNameType.Value, "4")]
		Wrapw = unchecked(4),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_WRAP")]
		[NativeName(NativeNameType.Value, "7")]
		Wrap = unchecked(7),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR_U")]
		[NativeName(NativeNameType.Value, "16")]
		Mirroru = unchecked(16),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR_V")]
		[NativeName(NativeNameType.Value, "32")]
		Mirrorv = unchecked(32),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR_W")]
		[NativeName(NativeNameType.Value, "64")]
		Mirrorw = unchecked(64),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_MIRROR")]
		[NativeName(NativeNameType.Value, "112")]
		Mirror = unchecked(112),

		/// <summary>		/// Wrap vs. Mirror vs. Clamp filtering options<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SEPARATE_ALPHA")]
		[NativeName(NativeNameType.Value, "256")]
		SeparateAlpha = unchecked(256),

		/// <summary>		/// Resize color and alpha channel independently<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FLOAT_X2BIAS")]
		[NativeName(NativeNameType.Value, "512")]
		Floatx2Bias = unchecked(512),

		/// <summary>		/// Enable *2 - 1 conversion cases for unorm<br/>		/// <<br/>		/// ->float and positive-only float formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_RED")]
		[NativeName(NativeNameType.Value, "4096")]
		RgbCopyRed = unchecked(4096),

		/// <summary>		/// Enable *2 - 1 conversion cases for unorm<br/>		/// <<br/>		/// ->float and positive-only float formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_GREEN")]
		[NativeName(NativeNameType.Value, "8192")]
		RgbCopyGreen = unchecked(8192),

		/// <summary>		/// Enable *2 - 1 conversion cases for unorm<br/>		/// <<br/>		/// ->float and positive-only float formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_BLUE")]
		[NativeName(NativeNameType.Value, "16384")]
		RgbCopyBlue = unchecked(16384),

		/// <summary>		/// Enable *2 - 1 conversion cases for unorm<br/>		/// <<br/>		/// ->float and positive-only float formats<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_RGB_COPY_ALPHA")]
		[NativeName(NativeNameType.Value, "32768")]
		RgbCopyAlpha = unchecked(32768),

		/// <summary>		/// When converting RGB(A) to R, defaults to using grayscale. These flags indicate copying a specific channel instead<br/>		/// When converting RGB(A) to RG, defaults to copying RED | GREEN. These flags control which channels are selected instead.<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_DITHER")]
		[NativeName(NativeNameType.Value, "65536")]
		Dither = unchecked(65536),

		/// <summary>		/// Use ordered 4x4 dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_DITHER_DIFFUSION")]
		[NativeName(NativeNameType.Value, "131072")]
		DitherDiffusion = unchecked(131072),

		/// <summary>		/// Use error-diffusion dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_POINT")]
		[NativeName(NativeNameType.Value, "1048576")]
		Point = unchecked(1048576),

		/// <summary>		/// Use error-diffusion dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_LINEAR")]
		[NativeName(NativeNameType.Value, "2097152")]
		Linear = unchecked(2097152),

		/// <summary>		/// Use error-diffusion dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_CUBIC")]
		[NativeName(NativeNameType.Value, "3145728")]
		Cubic = unchecked(3145728),

		/// <summary>		/// Use error-diffusion dithering for any required conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_BOX")]
		[NativeName(NativeNameType.Value, "4194304")]
		Box = unchecked(4194304),

		/// <summary>		/// Equiv to Box filtering for mipmap generation<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FANT")]
		[NativeName(NativeNameType.Value, "4194304")]
		Fant = unchecked(4194304),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_TRIANGLE")]
		[NativeName(NativeNameType.Value, "5242880")]
		Triangle = unchecked(5242880),

		/// <summary>		/// Filtering mode to use for any required image resizing<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SRGB_IN")]
		[NativeName(NativeNameType.Value, "16777216")]
		SrgbIn = unchecked(16777216),

		/// <summary>		/// Filtering mode to use for any required image resizing<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SRGB_OUT")]
		[NativeName(NativeNameType.Value, "33554432")]
		SrgbOut = unchecked(33554432),

		/// <summary>		/// Filtering mode to use for any required image resizing<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_SRGB")]
		[NativeName(NativeNameType.Value, "50331648")]
		Srgb = unchecked(50331648),

		/// <summary>		/// sRGB <br/>		/// <<br/>		/// -> RGB for use in conversion operations<br/>		/// if the input format type is IsSRGB(), then SRGB_IN is on by default<br/>		/// if the output format type is IsSRGB(), then SRGB_OUT is on by default<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FORCE_NON_WIC")]
		[NativeName(NativeNameType.Value, "268435456")]
		ForceNonWic = unchecked(268435456),

		/// <summary>		/// Forces use of the non-WIC path when both are an option<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_FILTER_FORCE_WIC")]
		[NativeName(NativeNameType.Value, "536870912")]
		ForceWic = unchecked(536870912),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_PMALPHA_FLAGS")]
	public enum TexPMAlphaFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_PMALPHA_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_PMALPHA_IGNORE_SRGB")]
		[NativeName(NativeNameType.Value, "1")]
		IgnoreSrgb = unchecked(1),

		/// <summary>		/// ignores sRGB colorspace conversions<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_PMALPHA_REVERSE")]
		[NativeName(NativeNameType.Value, "2")]
		Reverse = unchecked(2),

		/// <summary>		/// converts from premultiplied alpha back to straight alpha<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_PMALPHA_SRGB_IN")]
		[NativeName(NativeNameType.Value, "16777216")]
		SrgbIn = unchecked(16777216),

		/// <summary>		/// converts from premultiplied alpha back to straight alpha<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_PMALPHA_SRGB_OUT")]
		[NativeName(NativeNameType.Value, "33554432")]
		SrgbOut = unchecked(33554432),

		/// <summary>		/// converts from premultiplied alpha back to straight alpha<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_PMALPHA_SRGB")]
		[NativeName(NativeNameType.Value, "50331648")]
		Srgb = unchecked(50331648),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "TEX_COMPRESS_FLAGS")]
	public enum TexCompressFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_RGB_DITHER")]
		[NativeName(NativeNameType.Value, "65536")]
		RgbDither = unchecked(65536),

		/// <summary>		/// Enables dithering RGB colors for BC1-3 compression<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_A_DITHER")]
		[NativeName(NativeNameType.Value, "131072")]
		CompressaDither = unchecked(131072),

		/// <summary>		/// Enables dithering alpha for BC1-3 compression<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_DITHER")]
		[NativeName(NativeNameType.Value, "196608")]
		Dither = unchecked(196608),

		/// <summary>		/// Enables both RGB and alpha dithering for BC1-3 compression<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_UNIFORM")]
		[NativeName(NativeNameType.Value, "262144")]
		Uniform = unchecked(262144),

		/// <summary>		/// Uniform color weighting for BC1-3 compression; by default uses perceptual weighting<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_BC7_USE_3SUBSETS")]
		[NativeName(NativeNameType.Value, "524288")]
		Bc7Use3Subsets = unchecked(524288),

		/// <summary>		/// Enables exhaustive search for BC7 compress for mode 0 and 2; by default skips trying these modes<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_BC7_QUICK")]
		[NativeName(NativeNameType.Value, "1048576")]
		Bc7Quick = unchecked(1048576),

		/// <summary>		/// Minimal modes (usually mode 6) for BC7 compression<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_SRGB_IN")]
		[NativeName(NativeNameType.Value, "16777216")]
		SrgbIn = unchecked(16777216),

		/// <summary>		/// Minimal modes (usually mode 6) for BC7 compression<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_SRGB_OUT")]
		[NativeName(NativeNameType.Value, "33554432")]
		SrgbOut = unchecked(33554432),

		/// <summary>		/// Minimal modes (usually mode 6) for BC7 compression<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_SRGB")]
		[NativeName(NativeNameType.Value, "50331648")]
		Srgb = unchecked(50331648),

		/// <summary>		/// if the input format type is IsSRGB(), then SRGB_IN is on by default<br/>		/// if the output format type is IsSRGB(), then SRGB_OUT is on by default<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "TEX_COMPRESS_PARALLEL")]
		[NativeName(NativeNameType.Value, "268435456")]
		Parallel = unchecked(268435456),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "CNMAP_FLAGS")]
	public enum CNMAPFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_CHANNEL_RED")]
		[NativeName(NativeNameType.Value, "1")]
		ChannelRed = unchecked(1),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_CHANNEL_GREEN")]
		[NativeName(NativeNameType.Value, "2")]
		ChannelGreen = unchecked(2),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_CHANNEL_BLUE")]
		[NativeName(NativeNameType.Value, "3")]
		ChannelBlue = unchecked(3),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_CHANNEL_ALPHA")]
		[NativeName(NativeNameType.Value, "4")]
		ChannelAlpha = unchecked(4),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_CHANNEL_LUMINANCE")]
		[NativeName(NativeNameType.Value, "5")]
		ChannelLuminance = unchecked(5),

		/// <summary>		/// Channel selection when evaluting color value for height<br/>		/// Luminance is a combination of red, green, and blue<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_MIRROR_U")]
		[NativeName(NativeNameType.Value, "4096")]
		Mirroru = unchecked(4096),

		/// <summary>		/// Channel selection when evaluting color value for height<br/>		/// Luminance is a combination of red, green, and blue<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_MIRROR_V")]
		[NativeName(NativeNameType.Value, "8192")]
		Mirrorv = unchecked(8192),

		/// <summary>		/// Channel selection when evaluting color value for height<br/>		/// Luminance is a combination of red, green, and blue<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_MIRROR")]
		[NativeName(NativeNameType.Value, "12288")]
		Mirror = unchecked(12288),

		/// <summary>		/// Use mirror semantics for scanline references (defaults to wrap)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_INVERT_SIGN")]
		[NativeName(NativeNameType.Value, "16384")]
		InvertSign = unchecked(16384),

		/// <summary>		/// Inverts normal sign<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CNMAP_COMPUTE_OCCLUSION")]
		[NativeName(NativeNameType.Value, "32768")]
		ComputeOcclusion = unchecked(32768),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "CMSE_FLAGS")]
	public enum CMSEFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IMAGE1_SRGB")]
		[NativeName(NativeNameType.Value, "1")]
		Image1Srgb = unchecked(1),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IMAGE2_SRGB")]
		[NativeName(NativeNameType.Value, "2")]
		Image2Srgb = unchecked(2),

		/// <summary>		/// Indicates that image needs gamma correction before comparision<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IGNORE_RED")]
		[NativeName(NativeNameType.Value, "16")]
		IgnoreRed = unchecked(16),

		/// <summary>		/// Indicates that image needs gamma correction before comparision<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IGNORE_GREEN")]
		[NativeName(NativeNameType.Value, "32")]
		IgnoreGreen = unchecked(32),

		/// <summary>		/// Indicates that image needs gamma correction before comparision<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IGNORE_BLUE")]
		[NativeName(NativeNameType.Value, "64")]
		IgnoreBlue = unchecked(64),

		/// <summary>		/// Indicates that image needs gamma correction before comparision<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IGNORE_ALPHA")]
		[NativeName(NativeNameType.Value, "128")]
		IgnoreAlpha = unchecked(128),

		/// <summary>		/// Ignore the channel when computing MSE<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IMAGE1_X2_BIAS")]
		[NativeName(NativeNameType.Value, "256")]
		Image1X2Bias = unchecked(256),

		/// <summary>		/// Ignore the channel when computing MSE<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "CMSE_IMAGE2_X2_BIAS")]
		[NativeName(NativeNameType.Value, "512")]
		Image2X2Bias = unchecked(512),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "WICCodecs")]
	public enum WICCodecs
	{
		/// <summary>		/// Windows Bitmap (.bmp)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_BMP")]
		[NativeName(NativeNameType.Value, "1")]
		CodecBmp = unchecked(1),

		/// <summary>		/// Joint Photographic Experts Group (.jpg, .jpeg)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_JPEG")]
		[NativeName(NativeNameType.Value, "2")]
		CodecJpeg = unchecked(2),

		/// <summary>		/// Portable Network Graphics (.png)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_PNG")]
		[NativeName(NativeNameType.Value, "3")]
		CodecPng = unchecked(3),

		/// <summary>		/// Tagged Image File Format  (.tif, .tiff)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_TIFF")]
		[NativeName(NativeNameType.Value, "4")]
		CodecTiff = unchecked(4),

		/// <summary>		/// Graphics Interchange Format  (.gif)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_GIF")]
		[NativeName(NativeNameType.Value, "5")]
		CodecGif = unchecked(5),

		/// <summary>		/// Windows Media Photo / HD Photo / JPEG XR (.hdp, .jxr, .wdp)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_WMP")]
		[NativeName(NativeNameType.Value, "6")]
		CodecWmp = unchecked(6),

		/// <summary>		/// Windows Icon (.ico)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_ICO")]
		[NativeName(NativeNameType.Value, "7")]
		CodecIco = unchecked(7),

		/// <summary>		/// High Efficiency Image File (.heif, .heic)<br/>		/// </summary>		[NativeName(NativeNameType.EnumItem, "WIC_CODEC_HEIF")]
		[NativeName(NativeNameType.Value, "8")]
		CodecHeif = unchecked(8),

	}

	/// <summary>	/// To be documented.	/// </summary>	[NativeName(NativeNameType.Enum, "CREATETEX_FLAGS")]
	public enum CreateTexFlags
	{
		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CREATETEX_DEFAULT")]
		[NativeName(NativeNameType.Value, "0")]
		Default = unchecked(0),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CREATETEX_FORCE_SRGB")]
		[NativeName(NativeNameType.Value, "1")]
		ForceSrgb = unchecked(1),

		/// <summary>		/// To be documented.		/// </summary>		[NativeName(NativeNameType.EnumItem, "CREATETEX_IGNORE_SRGB")]
		[NativeName(NativeNameType.Value, "2")]
		IgnoreSrgb = unchecked(2),

	}

}
