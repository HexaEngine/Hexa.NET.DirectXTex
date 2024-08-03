#pragma once
#pragma comment(lib, "d3d12.lib")

#define D3DX12_NO_STATE_OBJECT_HELPERS
#define D3DX12_NO_CHECK_FEATURE_SUPPORT_CLASS
#ifdef _GAMING_XBOX_SCARLETT
#include <d3dx12_xs.h>
#elif (defined(_XBOX_ONE) && defined(_TITLE)) || defined(_GAMING_XBOX)
#include "d3dx12_x.h"
#elif !defined(_WIN32) || defined(USING_DIRECTX_HEADERS)
#include "directx/d3dx12.h"
#include "dxguids/dxguids.h"
#include "directx/dxgiformat.h"
#else
#include "d3d12.h"
#include "dxgiformat.h"
#endif

#ifdef _WIN32
#include <d3d11_1.h>
#endif

#define DEFINE_STRUCTS_AND_ENUMS

#ifdef DEFINE_STRUCTS_AND_ENUMS

#include <DirectXMath.h>

struct IWICImagingFactory;
struct IWICMetadataQueryReader;

typedef struct ScratchImage* ScratchImageT;
typedef struct Blob* BlobT;

#ifdef _WIN32
typedef void(__cdecl* SetCustomProps)(IPropertyBag2* pBag);
typedef void(__cdecl* GetMQR)(IWICMetadataQueryReader* qMqr);
#endif
typedef void(__cdecl* EvaluateImageFunc)(const DirectX::XMVECTOR* pixels, size_t width, size_t y);
typedef void(__cdecl* TransformImageFunc)(DirectX::XMVECTOR* outPixels, const DirectX::XMVECTOR* inPixels, size_t width, size_t y);

typedef enum
{
	FORMAT_TYPE_TYPELESS,
	FORMAT_TYPE_FLOAT,
	FORMAT_TYPE_UNORM,
	FORMAT_TYPE_SNORM,
	FORMAT_TYPE_UINT,
	FORMAT_TYPE_SINT,
} FORMAT_TYPE;

typedef enum
{
	CP_FLAGS_NONE = 0x0,
	// Normal operation

	CP_FLAGS_LEGACY_DWORD = 0x1,
	// Assume pitch is DWORD aligned instead of BYTE aligned

	CP_FLAGS_PARAGRAPH = 0x2,
	// Assume pitch is 16-byte aligned instead of BYTE aligned

	CP_FLAGS_YMM = 0x4,
	// Assume pitch is 32-byte aligned instead of BYTE aligned

	CP_FLAGS_ZMM = 0x8,
	// Assume pitch is 64-byte aligned instead of BYTE aligned

	CP_FLAGS_PAGE4K = 0x200,
	// Assume pitch is 4096-byte aligned instead of BYTE aligned

	CP_FLAGS_BAD_DXTN_TAILS = 0x1000,
	// BC formats with malformed mipchain blocks smaller than 4x4

	CP_FLAGS_24BPP = 0x10000,
	// Override with a legacy 24 bits-per-pixel format size

	CP_FLAGS_16BPP = 0x20000,
	// Override with a legacy 16 bits-per-pixel format size

	CP_FLAGS_8BPP = 0x40000,
	// Override with a legacy 8 bits-per-pixel format size
} CP_FLAGS;

typedef enum
{
	TEX_DIMENSION_TEXTURE1D = 2,
	TEX_DIMENSION_TEXTURE2D = 3,
	TEX_DIMENSION_TEXTURE3D = 4,
} TEX_DIMENSION;

// Subset here matches D3D10_RESOURCE_MISC_FLAG and D3D11_RESOURCE_MISC_FLAG
typedef enum
{
	TEX_MISC_TEXTURECUBE = 0x4L,
} TEX_MISC_FLAG;

typedef enum
{
	TEX_MISC2_ALPHA_MODE_MASK = 0x7L,
} TEX_MISC_FLAG2;

// Matches DDS_ALPHA_MODE, encoded in MISC_FLAGS2
typedef enum
{
	TEX_ALPHA_MODE_UNKNOWN = 0,
	TEX_ALPHA_MODE_STRAIGHT = 1,
	TEX_ALPHA_MODE_PREMULTIPLIED = 2,
	TEX_ALPHA_MODE_OPAQUE = 3,
	TEX_ALPHA_MODE_CUSTOM = 4,
} TEX_ALPHA_MODE;

typedef struct
{
	size_t width;
	size_t height;	  // Should be 1 for 1D textures
	size_t depth;	  // Should be 1 for 1D or 2D textures
	size_t arraySize; // For cubemap, this is a multiple of 6
	size_t mipLevels;
	uint32_t miscFlags;
	uint32_t miscFlags2;
	DXGI_FORMAT format;
	TEX_DIMENSION dimension;
} TexMetadata;

typedef enum
{
	DDS_FLAGS_NONE = 0x0,

	DDS_FLAGS_LEGACY_DWORD = 0x1,
	// Assume pitch is DWORD aligned instead of BYTE aligned (used by some legacy DDS files)

	DDS_FLAGS_NO_LEGACY_EXPANSION = 0x2,
	// Do not implicitly convert legacy formats that result in larger pixel sizes (24 bpp, 3:3:2, A8L8, A4L4, P8, A8P8)

	DDS_FLAGS_NO_R10B10G10A2_FIXUP = 0x4,
	// Do not use work-around for long-standing D3DX DDS file format issue which reversed the 10:10:10:2 color order masks

	DDS_FLAGS_FORCE_RGB = 0x8,
	// Convert DXGI 1.1 BGR formats to DXGI_FORMAT_R8G8B8A8_UNORM to avoid use of optional WDDM 1.1 formats

	DDS_FLAGS_NO_16BPP = 0x10,
	// Conversions avoid use of 565, 5551, and 4444 formats and instead expand to 8888 to avoid use of optional WDDM 1.2 formats

	DDS_FLAGS_EXPAND_LUMINANCE = 0x20,
	// When loading legacy luminance formats expand replicating the color channels rather than leaving them packed (L8, L16, A8L8)

	DDS_FLAGS_BAD_DXTN_TAILS = 0x40,
	// Some older DXTn DDS files incorrectly handle mipchain tails for blocks smaller than 4x4

	DDS_FLAGS_FORCE_DX10_EXT = 0x10000,
	// Always use the 'DX10' header extension for DDS writer (i.e. don't try to write DX9 compatible DDS files)

	DDS_FLAGS_FORCE_DX10_EXT_MISC2 = 0x20000,
	// DDS_FLAGS_FORCE_DX10_EXT including miscFlags2 information (result may not be compatible with D3DX10 or D3DX11)

	DDS_FLAGS_FORCE_DX9_LEGACY = 0x40000,
	// Force use of legacy header for DDS writer (will fail if unable to write as such)

	DDS_FLAGS_ALLOW_LARGE_FILES = 0x1000000,
	// Enables the loader to read large dimension .dds files (i.e. greater than known hardware requirements)
} DDS_FLAGS;

typedef enum
{
	TGA_FLAGS_NONE = 0x0,

	TGA_FLAGS_BGR = 0x1,
	// 24bpp files are returned as BGRX; 32bpp files are returned as BGRA

	TGA_FLAGS_ALLOW_ALL_ZERO_ALPHA = 0x2,
	// If the loaded image has an all zero alpha channel, normally we assume it should be opaque. This flag leaves it alone.

	TGA_FLAGS_IGNORE_SRGB = 0x10,
	// Ignores sRGB TGA 2.0 metadata if present in the file

	TGA_FLAGS_FORCE_SRGB = 0x20,
	// Writes sRGB metadata into the file reguardless of format (TGA 2.0 only)

	TGA_FLAGS_FORCE_LINEAR = 0x40,
	// Writes linear gamma metadata into the file reguardless of format (TGA 2.0 only)

	TGA_FLAGS_DEFAULT_SRGB = 0x80,
	// If no colorspace is specified in TGA 2.0 metadata, assume sRGB
} TGA_FLAGS;

typedef enum
{
	WIC_FLAGS_NONE = 0x0,

	WIC_FLAGS_FORCE_RGB = 0x1,
	// Loads DXGI 1.1 BGR formats as DXGI_FORMAT_R8G8B8A8_UNORM to avoid use of optional WDDM 1.1 formats

	WIC_FLAGS_NO_X2_BIAS = 0x2,
	// Loads DXGI 1.1 X2 10:10:10:2 format as DXGI_FORMAT_R10G10B10A2_UNORM

	WIC_FLAGS_NO_16BPP = 0x4,
	// Loads 565, 5551, and 4444 formats as 8888 to avoid use of optional WDDM 1.2 formats

	WIC_FLAGS_ALLOW_MONO = 0x8,
	// Loads 1-bit monochrome (black & white) as R1_UNORM rather than 8-bit grayscale

	WIC_FLAGS_ALL_FRAMES = 0x10,
	// Loads all images in a multi-frame file, converting/resizing to match the first frame as needed, defaults to 0th frame otherwise

	WIC_FLAGS_IGNORE_SRGB = 0x20,
	// Ignores sRGB metadata if present in the file

	WIC_FLAGS_FORCE_SRGB = 0x40,
	// Writes sRGB metadata into the file reguardless of format

	WIC_FLAGS_FORCE_LINEAR = 0x80,
	// Writes linear gamma metadata into the file reguardless of format

	WIC_FLAGS_DEFAULT_SRGB = 0x100,
	// If no colorspace is specified, assume sRGB

	WIC_FLAGS_DITHER = 0x10000,
	// Use ordered 4x4 dithering for any required conversions

	WIC_FLAGS_DITHER_DIFFUSION = 0x20000,
	// Use error-diffusion dithering for any required conversions

	WIC_FLAGS_FILTER_POINT = 0x100000,
	WIC_FLAGS_FILTER_LINEAR = 0x200000,
	WIC_FLAGS_FILTER_CUBIC = 0x300000,
	WIC_FLAGS_FILTER_FANT = 0x400000, // Combination of Linear and Box filter
	// Filtering mode to use for any required image resizing (only needed when loading arrays of differently sized images; defaults to Fant)
} WIC_FLAGS;

typedef struct
{
	size_t width;
	size_t height;
	DXGI_FORMAT format;
	size_t rowPitch;
	size_t slicePitch;
	uint8_t* pixels;
} Image;

typedef enum
{
	TEX_FR_ROTATE0 = 0x0,
	TEX_FR_ROTATE90 = 0x1,
	TEX_FR_ROTATE180 = 0x2,
	TEX_FR_ROTATE270 = 0x3,
	TEX_FR_FLIP_HORIZONTAL = 0x08,
	TEX_FR_FLIP_VERTICAL = 0x10,
} TEX_FR_FLAGS;

typedef enum
{
	TEX_FILTER_DEFAULT = 0,

	TEX_FILTER_WRAP_U = 0x1,
	TEX_FILTER_WRAP_V = 0x2,
	TEX_FILTER_WRAP_W = 0x4,
	TEX_FILTER_WRAP = (TEX_FILTER_WRAP_U | TEX_FILTER_WRAP_V | TEX_FILTER_WRAP_W),
	TEX_FILTER_MIRROR_U = 0x10,
	TEX_FILTER_MIRROR_V = 0x20,
	TEX_FILTER_MIRROR_W = 0x40,
	TEX_FILTER_MIRROR = (TEX_FILTER_MIRROR_U | TEX_FILTER_MIRROR_V | TEX_FILTER_MIRROR_W),
	// Wrap vs. Mirror vs. Clamp filtering options

	TEX_FILTER_SEPARATE_ALPHA = 0x100,
	// Resize color and alpha channel independently

	TEX_FILTER_FLOAT_X2BIAS = 0x200,
	// Enable *2 - 1 conversion cases for unorm<->float and positive-only float formats

	TEX_FILTER_RGB_COPY_RED = 0x1000,
	TEX_FILTER_RGB_COPY_GREEN = 0x2000,
	TEX_FILTER_RGB_COPY_BLUE = 0x4000,
	TEX_FILTER_RGB_COPY_ALPHA = 0x8000,
	// When converting RGB(A) to R, defaults to using grayscale. These flags indicate copying a specific channel instead
	// When converting RGB(A) to RG, defaults to copying RED | GREEN. These flags control which channels are selected instead.

	TEX_FILTER_DITHER = 0x10000,
	// Use ordered 4x4 dithering for any required conversions
	TEX_FILTER_DITHER_DIFFUSION = 0x20000,
	// Use error-diffusion dithering for any required conversions

	TEX_FILTER_POINT = 0x100000,
	TEX_FILTER_LINEAR = 0x200000,
	TEX_FILTER_CUBIC = 0x300000,
	TEX_FILTER_BOX = 0x400000,
	TEX_FILTER_FANT = 0x400000, // Equiv to Box filtering for mipmap generation
	TEX_FILTER_TRIANGLE = 0x500000,
	// Filtering mode to use for any required image resizing

	TEX_FILTER_SRGB_IN = 0x1000000,
	TEX_FILTER_SRGB_OUT = 0x2000000,
	TEX_FILTER_SRGB = (TEX_FILTER_SRGB_IN | TEX_FILTER_SRGB_OUT),
	// sRGB <-> RGB for use in conversion operations
	// if the input format type is IsSRGB(), then SRGB_IN is on by default
	// if the output format type is IsSRGB(), then SRGB_OUT is on by default

	TEX_FILTER_FORCE_NON_WIC = 0x10000000,
	// Forces use of the non-WIC path when both are an option

	TEX_FILTER_FORCE_WIC = 0x20000000,
	// Forces use of the WIC path even when logic would have picked a non-WIC path when both are an option
} TEX_FILTER_FLAGS;

typedef enum
{
	TEX_PMALPHA_DEFAULT = 0,

	TEX_PMALPHA_IGNORE_SRGB = 0x1,
	// ignores sRGB colorspace conversions

	TEX_PMALPHA_REVERSE = 0x2,
	// converts from premultiplied alpha back to straight alpha

	TEX_PMALPHA_SRGB_IN = 0x1000000,
	TEX_PMALPHA_SRGB_OUT = 0x2000000,
	TEX_PMALPHA_SRGB = (TEX_PMALPHA_SRGB_IN | TEX_PMALPHA_SRGB_OUT),
	// if the input format type is IsSRGB(), then SRGB_IN is on by default
	// if the output format type is IsSRGB(), then SRGB_OUT is on by default
} TEX_PMALPHA_FLAGS;

typedef enum
{
	TEX_COMPRESS_DEFAULT = 0,

	TEX_COMPRESS_RGB_DITHER = 0x10000,
	// Enables dithering RGB colors for BC1-3 compression

	TEX_COMPRESS_A_DITHER = 0x20000,
	// Enables dithering alpha for BC1-3 compression

	TEX_COMPRESS_DITHER = 0x30000,
	// Enables both RGB and alpha dithering for BC1-3 compression

	TEX_COMPRESS_UNIFORM = 0x40000,
	// Uniform color weighting for BC1-3 compression; by default uses perceptual weighting

	TEX_COMPRESS_BC7_USE_3SUBSETS = 0x80000,
	// Enables exhaustive search for BC7 compress for mode 0 and 2; by default skips trying these modes

	TEX_COMPRESS_BC7_QUICK = 0x100000,
	// Minimal modes (usually mode 6) for BC7 compression

	TEX_COMPRESS_SRGB_IN = 0x1000000,
	TEX_COMPRESS_SRGB_OUT = 0x2000000,
	TEX_COMPRESS_SRGB = (TEX_COMPRESS_SRGB_IN | TEX_COMPRESS_SRGB_OUT),
	// if the input format type is IsSRGB(), then SRGB_IN is on by default
	// if the output format type is IsSRGB(), then SRGB_OUT is on by default

	TEX_COMPRESS_PARALLEL = 0x10000000,
	// Compress is free to use multithreading to improve performance (by default it does not use multithreading)
} TEX_COMPRESS_FLAGS;

typedef enum
{
	CNMAP_DEFAULT = 0,

	CNMAP_CHANNEL_RED = 0x1,
	CNMAP_CHANNEL_GREEN = 0x2,
	CNMAP_CHANNEL_BLUE = 0x3,
	CNMAP_CHANNEL_ALPHA = 0x4,
	CNMAP_CHANNEL_LUMINANCE = 0x5,
	// Channel selection when evaluting color value for height
	// Luminance is a combination of red, green, and blue

	CNMAP_MIRROR_U = 0x1000,
	CNMAP_MIRROR_V = 0x2000,
	CNMAP_MIRROR = 0x3000,
	// Use mirror semantics for scanline references (defaults to wrap)

	CNMAP_INVERT_SIGN = 0x4000,
	// Inverts normal sign

	CNMAP_COMPUTE_OCCLUSION = 0x8000,
	// Computes a crude occlusion term stored in the alpha channel
} CNMAP_FLAGS;

typedef struct
{
	size_t x;
	size_t y;
	size_t w;
	size_t h;
} Rect;

typedef enum
{
	CMSE_DEFAULT = 0,

	CMSE_IMAGE1_SRGB = 0x1,
	CMSE_IMAGE2_SRGB = 0x2,
	// Indicates that image needs gamma correction before comparision

	CMSE_IGNORE_RED = 0x10,
	CMSE_IGNORE_GREEN = 0x20,
	CMSE_IGNORE_BLUE = 0x40,
	CMSE_IGNORE_ALPHA = 0x80,
	// Ignore the channel when computing MSE

	CMSE_IMAGE1_X2_BIAS = 0x100,
	CMSE_IMAGE2_X2_BIAS = 0x200,
	// Indicates that image should be scaled and biased before comparison (i.e. UNORM -> SNORM)
} CMSE_FLAGS;

typedef enum
{
	WIC_CODEC_BMP = 1, // Windows Bitmap (.bmp)
	WIC_CODEC_JPEG,	   // Joint Photographic Experts Group (.jpg, .jpeg)
	WIC_CODEC_PNG,	   // Portable Network Graphics (.png)
	WIC_CODEC_TIFF,	   // Tagged Image File Format  (.tif, .tiff)
	WIC_CODEC_GIF,	   // Graphics Interchange Format  (.gif)
	WIC_CODEC_WMP,	   // Windows Media Photo / HD Photo / JPEG XR (.hdp, .jxr, .wdp)
	WIC_CODEC_ICO,	   // Windows Icon (.ico)
	WIC_CODEC_HEIF,	   // High Efficiency Image File (.heif, .heic)
} WICCodecs;

typedef enum
{
	CREATETEX_DEFAULT = 0,
	CREATETEX_FORCE_SRGB = 0x1,
	CREATETEX_IGNORE_SRGB = 0x2,
} CREATETEX_FLAGS;

namespace DirectX
{
#ifdef _WIN32
	typedef void(__cdecl* SetCustomProps)(IPropertyBag2* pBag);
	typedef void(__cdecl* GetMQR)(IWICMetadataQueryReader* qMqr);
#endif
	typedef void(__cdecl* EvaluateImageFunc)(const DirectX::XMVECTOR* pixels, size_t width, size_t y);
	typedef void(__cdecl* TransformImageFunc)(DirectX::XMVECTOR* outPixels, const DirectX::XMVECTOR* inPixels, size_t width, size_t y);

	typedef enum
	{
		FORMAT_TYPE_TYPELESS,
		FORMAT_TYPE_FLOAT,
		FORMAT_TYPE_UNORM,
		FORMAT_TYPE_SNORM,
		FORMAT_TYPE_UINT,
		FORMAT_TYPE_SINT,
	} FORMAT_TYPE;

	typedef enum
	{
		CP_FLAGS_NONE = 0x0,
		// Normal operation

		CP_FLAGS_LEGACY_DWORD = 0x1,
		// Assume pitch is DWORD aligned instead of BYTE aligned

		CP_FLAGS_PARAGRAPH = 0x2,
		// Assume pitch is 16-byte aligned instead of BYTE aligned

		CP_FLAGS_YMM = 0x4,
		// Assume pitch is 32-byte aligned instead of BYTE aligned

		CP_FLAGS_ZMM = 0x8,
		// Assume pitch is 64-byte aligned instead of BYTE aligned

		CP_FLAGS_PAGE4K = 0x200,
		// Assume pitch is 4096-byte aligned instead of BYTE aligned

		CP_FLAGS_BAD_DXTN_TAILS = 0x1000,
		// BC formats with malformed mipchain blocks smaller than 4x4

		CP_FLAGS_24BPP = 0x10000,
		// Override with a legacy 24 bits-per-pixel format size

		CP_FLAGS_16BPP = 0x20000,
		// Override with a legacy 16 bits-per-pixel format size

		CP_FLAGS_8BPP = 0x40000,
		// Override with a legacy 8 bits-per-pixel format size
	} CP_FLAGS;

	typedef enum
	{
		TEX_DIMENSION_TEXTURE1D = 2,
		TEX_DIMENSION_TEXTURE2D = 3,
		TEX_DIMENSION_TEXTURE3D = 4,
	} TEX_DIMENSION;

	// Subset here matches D3D10_RESOURCE_MISC_FLAG and D3D11_RESOURCE_MISC_FLAG
	typedef enum
	{
		TEX_MISC_TEXTURECUBE = 0x4L,
	} TEX_MISC_FLAG;

	typedef enum
	{
		TEX_MISC2_ALPHA_MODE_MASK = 0x7L,
	} TEX_MISC_FLAG2;

	// Matches DDS_ALPHA_MODE, encoded in MISC_FLAGS2
	typedef enum
	{
		TEX_ALPHA_MODE_UNKNOWN = 0,
		TEX_ALPHA_MODE_STRAIGHT = 1,
		TEX_ALPHA_MODE_PREMULTIPLIED = 2,
		TEX_ALPHA_MODE_OPAQUE = 3,
		TEX_ALPHA_MODE_CUSTOM = 4,
	} TEX_ALPHA_MODE;

	typedef struct
	{
		size_t width;
		size_t height;	  // Should be 1 for 1D textures
		size_t depth;	  // Should be 1 for 1D or 2D textures
		size_t arraySize; // For cubemap, this is a multiple of 6
		size_t mipLevels;
		uint32_t miscFlags;
		uint32_t miscFlags2;
		DXGI_FORMAT format;
		TEX_DIMENSION dimension;
	} TexMetadata;

	typedef enum
	{
		DDS_FLAGS_NONE = 0x0,

		DDS_FLAGS_LEGACY_DWORD = 0x1,
		// Assume pitch is DWORD aligned instead of BYTE aligned (used by some legacy DDS files)

		DDS_FLAGS_NO_LEGACY_EXPANSION = 0x2,
		// Do not implicitly convert legacy formats that result in larger pixel sizes (24 bpp, 3:3:2, A8L8, A4L4, P8, A8P8)

		DDS_FLAGS_NO_R10B10G10A2_FIXUP = 0x4,
		// Do not use work-around for long-standing D3DX DDS file format issue which reversed the 10:10:10:2 color order masks

		DDS_FLAGS_FORCE_RGB = 0x8,
		// Convert DXGI 1.1 BGR formats to DXGI_FORMAT_R8G8B8A8_UNORM to avoid use of optional WDDM 1.1 formats

		DDS_FLAGS_NO_16BPP = 0x10,
		// Conversions avoid use of 565, 5551, and 4444 formats and instead expand to 8888 to avoid use of optional WDDM 1.2 formats

		DDS_FLAGS_EXPAND_LUMINANCE = 0x20,
		// When loading legacy luminance formats expand replicating the color channels rather than leaving them packed (L8, L16, A8L8)

		DDS_FLAGS_BAD_DXTN_TAILS = 0x40,
		// Some older DXTn DDS files incorrectly handle mipchain tails for blocks smaller than 4x4

		DDS_FLAGS_FORCE_DX10_EXT = 0x10000,
		// Always use the 'DX10' header extension for DDS writer (i.e. don't try to write DX9 compatible DDS files)

		DDS_FLAGS_FORCE_DX10_EXT_MISC2 = 0x20000,
		// DDS_FLAGS_FORCE_DX10_EXT including miscFlags2 information (result may not be compatible with D3DX10 or D3DX11)

		DDS_FLAGS_FORCE_DX9_LEGACY = 0x40000,
		// Force use of legacy header for DDS writer (will fail if unable to write as such)

		DDS_FLAGS_ALLOW_LARGE_FILES = 0x1000000,
		// Enables the loader to read large dimension .dds files (i.e. greater than known hardware requirements)
	} DDS_FLAGS;

	typedef enum
	{
		TGA_FLAGS_NONE = 0x0,

		TGA_FLAGS_BGR = 0x1,
		// 24bpp files are returned as BGRX; 32bpp files are returned as BGRA

		TGA_FLAGS_ALLOW_ALL_ZERO_ALPHA = 0x2,
		// If the loaded image has an all zero alpha channel, normally we assume it should be opaque. This flag leaves it alone.

		TGA_FLAGS_IGNORE_SRGB = 0x10,
		// Ignores sRGB TGA 2.0 metadata if present in the file

		TGA_FLAGS_FORCE_SRGB = 0x20,
		// Writes sRGB metadata into the file reguardless of format (TGA 2.0 only)

		TGA_FLAGS_FORCE_LINEAR = 0x40,
		// Writes linear gamma metadata into the file reguardless of format (TGA 2.0 only)

		TGA_FLAGS_DEFAULT_SRGB = 0x80,
		// If no colorspace is specified in TGA 2.0 metadata, assume sRGB
	} TGA_FLAGS;

	typedef enum
	{
		WIC_FLAGS_NONE = 0x0,

		WIC_FLAGS_FORCE_RGB = 0x1,
		// Loads DXGI 1.1 BGR formats as DXGI_FORMAT_R8G8B8A8_UNORM to avoid use of optional WDDM 1.1 formats

		WIC_FLAGS_NO_X2_BIAS = 0x2,
		// Loads DXGI 1.1 X2 10:10:10:2 format as DXGI_FORMAT_R10G10B10A2_UNORM

		WIC_FLAGS_NO_16BPP = 0x4,
		// Loads 565, 5551, and 4444 formats as 8888 to avoid use of optional WDDM 1.2 formats

		WIC_FLAGS_ALLOW_MONO = 0x8,
		// Loads 1-bit monochrome (black & white) as R1_UNORM rather than 8-bit grayscale

		WIC_FLAGS_ALL_FRAMES = 0x10,
		// Loads all images in a multi-frame file, converting/resizing to match the first frame as needed, defaults to 0th frame otherwise

		WIC_FLAGS_IGNORE_SRGB = 0x20,
		// Ignores sRGB metadata if present in the file

		WIC_FLAGS_FORCE_SRGB = 0x40,
		// Writes sRGB metadata into the file reguardless of format

		WIC_FLAGS_FORCE_LINEAR = 0x80,
		// Writes linear gamma metadata into the file reguardless of format

		WIC_FLAGS_DEFAULT_SRGB = 0x100,
		// If no colorspace is specified, assume sRGB

		WIC_FLAGS_DITHER = 0x10000,
		// Use ordered 4x4 dithering for any required conversions

		WIC_FLAGS_DITHER_DIFFUSION = 0x20000,
		// Use error-diffusion dithering for any required conversions

		WIC_FLAGS_FILTER_POINT = 0x100000,
		WIC_FLAGS_FILTER_LINEAR = 0x200000,
		WIC_FLAGS_FILTER_CUBIC = 0x300000,
		WIC_FLAGS_FILTER_FANT = 0x400000, // Combination of Linear and Box filter
		// Filtering mode to use for any required image resizing (only needed when loading arrays of differently sized images; defaults to Fant)
	} WIC_FLAGS;

	typedef struct
	{
		size_t width;
		size_t height;
		DXGI_FORMAT format;
		size_t rowPitch;
		size_t slicePitch;
		uint8_t* pixels;
	} Image;

	typedef enum
	{
		TEX_FR_ROTATE0 = 0x0,
		TEX_FR_ROTATE90 = 0x1,
		TEX_FR_ROTATE180 = 0x2,
		TEX_FR_ROTATE270 = 0x3,
		TEX_FR_FLIP_HORIZONTAL = 0x08,
		TEX_FR_FLIP_VERTICAL = 0x10,
	} TEX_FR_FLAGS;

	typedef enum
	{
		TEX_FILTER_DEFAULT = 0,

		TEX_FILTER_WRAP_U = 0x1,
		TEX_FILTER_WRAP_V = 0x2,
		TEX_FILTER_WRAP_W = 0x4,
		TEX_FILTER_WRAP = (TEX_FILTER_WRAP_U | TEX_FILTER_WRAP_V | TEX_FILTER_WRAP_W),
		TEX_FILTER_MIRROR_U = 0x10,
		TEX_FILTER_MIRROR_V = 0x20,
		TEX_FILTER_MIRROR_W = 0x40,
		TEX_FILTER_MIRROR = (TEX_FILTER_MIRROR_U | TEX_FILTER_MIRROR_V | TEX_FILTER_MIRROR_W),
		// Wrap vs. Mirror vs. Clamp filtering options

		TEX_FILTER_SEPARATE_ALPHA = 0x100,
		// Resize color and alpha channel independently

		TEX_FILTER_FLOAT_X2BIAS = 0x200,
		// Enable *2 - 1 conversion cases for unorm<->float and positive-only float formats

		TEX_FILTER_RGB_COPY_RED = 0x1000,
		TEX_FILTER_RGB_COPY_GREEN = 0x2000,
		TEX_FILTER_RGB_COPY_BLUE = 0x4000,
		TEX_FILTER_RGB_COPY_ALPHA = 0x8000,
		// When converting RGB(A) to R, defaults to using grayscale. These flags indicate copying a specific channel instead
		// When converting RGB(A) to RG, defaults to copying RED | GREEN. These flags control which channels are selected instead.

		TEX_FILTER_DITHER = 0x10000,
		// Use ordered 4x4 dithering for any required conversions
		TEX_FILTER_DITHER_DIFFUSION = 0x20000,
		// Use error-diffusion dithering for any required conversions

		TEX_FILTER_POINT = 0x100000,
		TEX_FILTER_LINEAR = 0x200000,
		TEX_FILTER_CUBIC = 0x300000,
		TEX_FILTER_BOX = 0x400000,
		TEX_FILTER_FANT = 0x400000, // Equiv to Box filtering for mipmap generation
		TEX_FILTER_TRIANGLE = 0x500000,
		// Filtering mode to use for any required image resizing

		TEX_FILTER_SRGB_IN = 0x1000000,
		TEX_FILTER_SRGB_OUT = 0x2000000,
		TEX_FILTER_SRGB = (TEX_FILTER_SRGB_IN | TEX_FILTER_SRGB_OUT),
		// sRGB <-> RGB for use in conversion operations
		// if the input format type is IsSRGB(), then SRGB_IN is on by default
		// if the output format type is IsSRGB(), then SRGB_OUT is on by default

		TEX_FILTER_FORCE_NON_WIC = 0x10000000,
		// Forces use of the non-WIC path when both are an option

		TEX_FILTER_FORCE_WIC = 0x20000000,
		// Forces use of the WIC path even when logic would have picked a non-WIC path when both are an option
	} TEX_FILTER_FLAGS;

	typedef enum
	{
		TEX_PMALPHA_DEFAULT = 0,

		TEX_PMALPHA_IGNORE_SRGB = 0x1,
		// ignores sRGB colorspace conversions

		TEX_PMALPHA_REVERSE = 0x2,
		// converts from premultiplied alpha back to straight alpha

		TEX_PMALPHA_SRGB_IN = 0x1000000,
		TEX_PMALPHA_SRGB_OUT = 0x2000000,
		TEX_PMALPHA_SRGB = (TEX_PMALPHA_SRGB_IN | TEX_PMALPHA_SRGB_OUT),
		// if the input format type is IsSRGB(), then SRGB_IN is on by default
		// if the output format type is IsSRGB(), then SRGB_OUT is on by default
	} TEX_PMALPHA_FLAGS;

	typedef enum
	{
		TEX_COMPRESS_DEFAULT = 0,

		TEX_COMPRESS_RGB_DITHER = 0x10000,
		// Enables dithering RGB colors for BC1-3 compression

		TEX_COMPRESS_A_DITHER = 0x20000,
		// Enables dithering alpha for BC1-3 compression

		TEX_COMPRESS_DITHER = 0x30000,
		// Enables both RGB and alpha dithering for BC1-3 compression

		TEX_COMPRESS_UNIFORM = 0x40000,
		// Uniform color weighting for BC1-3 compression; by default uses perceptual weighting

		TEX_COMPRESS_BC7_USE_3SUBSETS = 0x80000,
		// Enables exhaustive search for BC7 compress for mode 0 and 2; by default skips trying these modes

		TEX_COMPRESS_BC7_QUICK = 0x100000,
		// Minimal modes (usually mode 6) for BC7 compression

		TEX_COMPRESS_SRGB_IN = 0x1000000,
		TEX_COMPRESS_SRGB_OUT = 0x2000000,
		TEX_COMPRESS_SRGB = (TEX_COMPRESS_SRGB_IN | TEX_COMPRESS_SRGB_OUT),
		// if the input format type is IsSRGB(), then SRGB_IN is on by default
		// if the output format type is IsSRGB(), then SRGB_OUT is on by default

		TEX_COMPRESS_PARALLEL = 0x10000000,
		// Compress is free to use multithreading to improve performance (by default it does not use multithreading)
	} TEX_COMPRESS_FLAGS;

	typedef enum
	{
		CNMAP_DEFAULT = 0,

		CNMAP_CHANNEL_RED = 0x1,
		CNMAP_CHANNEL_GREEN = 0x2,
		CNMAP_CHANNEL_BLUE = 0x3,
		CNMAP_CHANNEL_ALPHA = 0x4,
		CNMAP_CHANNEL_LUMINANCE = 0x5,
		// Channel selection when evaluting color value for height
		// Luminance is a combination of red, green, and blue

		CNMAP_MIRROR_U = 0x1000,
		CNMAP_MIRROR_V = 0x2000,
		CNMAP_MIRROR = 0x3000,
		// Use mirror semantics for scanline references (defaults to wrap)

		CNMAP_INVERT_SIGN = 0x4000,
		// Inverts normal sign

		CNMAP_COMPUTE_OCCLUSION = 0x8000,
		// Computes a crude occlusion term stored in the alpha channel
	} CNMAP_FLAGS;

	typedef struct
	{
		size_t x;
		size_t y;
		size_t w;
		size_t h;
	} Rect;

	typedef enum
	{
		CMSE_DEFAULT = 0,

		CMSE_IMAGE1_SRGB = 0x1,
		CMSE_IMAGE2_SRGB = 0x2,
		// Indicates that image needs gamma correction before comparision

		CMSE_IGNORE_RED = 0x10,
		CMSE_IGNORE_GREEN = 0x20,
		CMSE_IGNORE_BLUE = 0x40,
		CMSE_IGNORE_ALPHA = 0x80,
		// Ignore the channel when computing MSE

		CMSE_IMAGE1_X2_BIAS = 0x100,
		CMSE_IMAGE2_X2_BIAS = 0x200,
		// Indicates that image should be scaled and biased before comparison (i.e. UNORM -> SNORM)
	} CMSE_FLAGS;

	typedef enum
	{
		WIC_CODEC_BMP = 1, // Windows Bitmap (.bmp)
		WIC_CODEC_JPEG,	   // Joint Photographic Experts Group (.jpg, .jpeg)
		WIC_CODEC_PNG,	   // Portable Network Graphics (.png)
		WIC_CODEC_TIFF,	   // Tagged Image File Format  (.tif, .tiff)
		WIC_CODEC_GIF,	   // Graphics Interchange Format  (.gif)
		WIC_CODEC_WMP,	   // Windows Media Photo / HD Photo / JPEG XR (.hdp, .jxr, .wdp)
		WIC_CODEC_ICO,	   // Windows Icon (.ico)
		WIC_CODEC_HEIF,	   // High Efficiency Image File (.heif, .heic)
	} WICCodecs;

	typedef enum
	{
		CREATETEX_DEFAULT = 0,
		CREATETEX_FORCE_SRGB = 0x1,
		CREATETEX_IGNORE_SRGB = 0x2,
	} CREATETEX_FLAGS;
} // namespace DirectX

#else

#include "DirectXTex/DirectXTex/DirectXTex.h"

typedef struct
{
	DirectX::ScratchImage* image;
} ScratchImageT;

typedef struct
{
	DirectX::Blob* blob;
} BlobT;

#endif

#if defined(_WIN32) || defined(__CYGWIN__)
#define API __declspec(dllexport)
#elif defined(__linux__) || defined(__APPLE__)
#define API __attribute__((visibility("default")))
#else
#define API
#pragma warning Unknown dynamic link import / export semantics.
#endif

#ifdef __cplusplus
extern "C"
{
#endif

#ifdef _WIN32
	typedef void(__cdecl* SetCustomProps)(IPropertyBag2* pBag);
	typedef void(__cdecl* GetMQR)(IWICMetadataQueryReader* qMqr);
#endif
	typedef void(__cdecl* EvaluateImageFunc)(_In_reads_(width) const DirectX::XMVECTOR* pixels, size_t width, size_t y);
	typedef void(__cdecl* TransformImageFunc)(_Out_writes_(width) DirectX::XMVECTOR* outPixels, _In_reads_(width) const DirectX::XMVECTOR* inPixels, size_t width, size_t y);

#pragma region IPropertyBag2 methods

#ifdef _WIN32
	API HRESULT Read(IPropertyBag2* p, _In_ ULONG cProperties, __RPC__in_ecount_full(cProperties) PROPBAG2* pPropBag, __RPC__in_opt IErrorLog* pErrLog, __RPC__out_ecount_full(cProperties) VARIANT* pvarValue, __RPC__inout_ecount_full_opt(cProperties) HRESULT* phrError);

	API HRESULT Write(IPropertyBag2* p, _In_ ULONG cProperties, __RPC__in_ecount_full(cProperties) PROPBAG2* pPropBag, __RPC__in_ecount_full(cProperties) VARIANT* pvarValue);

	API HRESULT CountProperties(IPropertyBag2* p, __RPC__out ULONG* pcProperties);

	API HRESULT GetPropertyInfo(IPropertyBag2* p, _In_ ULONG iProperty, _In_ ULONG cProperties, __RPC__out_ecount_full(cProperties) PROPBAG2* pPropBag, __RPC__out ULONG* pcProperties);

	API HRESULT LoadObject(IPropertyBag2* p, __RPC__in LPCOLESTR pstrName, _In_ DWORD dwHint, __RPC__in_opt IUnknown* pUnkObject, __RPC__in_opt IErrorLog* pErrLog);
#endif

#pragma endregion

#pragma region IWICMetadataQueryReader methods

#ifdef _WIN32
	API HRESULT GetContainerFormat(IWICMetadataQueryReader* p, __RPC__out GUID* pguidContainerFormat);

	API HRESULT GetLocation(IWICMetadataQueryReader* p, _In_ UINT cchMaxLength, __RPC__inout_ecount_full_opt(cchMaxLength) WCHAR* wzNamespace, __RPC__out UINT* pcchActualLength);

	API HRESULT GetMetadataByName(IWICMetadataQueryReader* p, __RPC__in LPCWSTR wzName, __RPC__inout_opt PROPVARIANT* pvarValue);

	API HRESULT GetEnumerator(IWICMetadataQueryReader* p, __RPC__deref_out_opt IEnumString** ppIEnumString);
#endif

#pragma endregion

#pragma region DXGI Format Utilities

	API bool IsValid(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsCompressed(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsPacked(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsVideo(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsPlanar(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsPalettized(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsDepthStencil(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsSRGB(_In_ DXGI_FORMAT fmt) noexcept;
	API bool IsTypeless(_In_ DXGI_FORMAT fmt, _In_ bool partialTypeless) noexcept;

	API bool HasAlpha(_In_ DXGI_FORMAT fmt) noexcept;

	API size_t BitsPerPixel(_In_ DXGI_FORMAT fmt) noexcept;

	API size_t BitsPerColor(_In_ DXGI_FORMAT fmt) noexcept;

	API DirectX::FORMAT_TYPE FormatDataType(_In_ DXGI_FORMAT fmt) noexcept;
	API HRESULT ComputePitch(_In_ DXGI_FORMAT fmt, _In_ size_t width, _In_ size_t height, _Out_ size_t& rowPitch, _Out_ size_t& slicePitch, _In_ DirectX::CP_FLAGS flags) noexcept;

	API size_t ComputeScanlines(_In_ DXGI_FORMAT fmt, _In_ size_t height) noexcept;

	API DXGI_FORMAT MakeSRGB(_In_ DXGI_FORMAT fmt) noexcept;
	API DXGI_FORMAT MakeTypeless(_In_ DXGI_FORMAT fmt) noexcept;
	API DXGI_FORMAT MakeTypelessUNORM(_In_ DXGI_FORMAT fmt) noexcept;
	API DXGI_FORMAT MakeTypelessFLOAT(_In_ DXGI_FORMAT fmt) noexcept;

#pragma endregion

#pragma region MetadataIO

	API HRESULT GetMetadataFromDDSMemory(
		_In_reads_bytes_(size) const void* pSource, _In_ size_t size,
		_In_ DirectX::DDS_FLAGS flags,
		_Out_ DirectX::TexMetadata& metadata) noexcept;
	API HRESULT GetMetadataFromDDSFile(
		_In_z_ const wchar_t* szFile,
		_In_ DirectX::DDS_FLAGS flags,
		_Out_ DirectX::TexMetadata& metadata) noexcept;

	API HRESULT GetMetadataFromHDRMemory(
		_In_reads_bytes_(size) const void* pSource, _In_ size_t size,
		_Out_ DirectX::TexMetadata& metadata) noexcept;
	API HRESULT GetMetadataFromHDRFile(
		_In_z_ const wchar_t* szFile,
		_Out_ DirectX::TexMetadata& metadata) noexcept;

	API HRESULT GetMetadataFromTGAMemory(
		_In_reads_bytes_(size) const void* pSource, _In_ size_t size,
		_In_ DirectX::TGA_FLAGS flags,
		_Out_ DirectX::TexMetadata& metadata) noexcept;
	API HRESULT GetMetadataFromTGAFile(
		_In_z_ const wchar_t* szFile,
		_In_ DirectX::TGA_FLAGS flags,
		_Out_ DirectX::TexMetadata& metadata) noexcept;

#ifdef _WIN32
	API HRESULT GetMetadataFromWICMemory(
		_In_reads_bytes_(size) const void* pSource, _In_ size_t size,
		_In_ DirectX::WIC_FLAGS flags,
		_Out_ DirectX::TexMetadata& metadata);

	API HRESULT GetMetadataFromWICFile(
		_In_z_ const wchar_t* szFile,
		_In_ DirectX::WIC_FLAGS flags,
		_Out_ DirectX::TexMetadata& metadata);
#endif

	// Compatability helpers
	API HRESULT GetMetadataFromTGAMemory2(
		_In_reads_bytes_(size) const void* pSource, _In_ size_t size,
		_Out_ DirectX::TexMetadata& metadata) noexcept;
	API HRESULT GetMetadataFromTGAFile2(
		_In_z_ const wchar_t* szFile,
		_Out_ DirectX::TexMetadata& metadata) noexcept;

#pragma endregion

#pragma region TexMetadata Methods

	API size_t ComputeIndex(DirectX::TexMetadata* metadata, _In_ size_t mip, _In_ size_t item, _In_ size_t slice) noexcept;
	// Returns size_t(-1) to indicate an out-of-range error

	API bool IsCubemap(DirectX::TexMetadata* metadata) noexcept;
	// Helper for miscFlags

	API bool IsPMAlpha(DirectX::TexMetadata* metadata) noexcept;
	API void SetAlphaMode(DirectX::TexMetadata* metadata, DirectX::TEX_ALPHA_MODE mode) noexcept;
	API DirectX::TEX_ALPHA_MODE GetAlphaMode(DirectX::TexMetadata* metadata) noexcept;
	// Helpers for miscFlags2

	API bool IsVolumemap(DirectX::TexMetadata* metadata) noexcept;
	// Helper for dimension

#pragma endregion

#pragma region ScratchImage Methods

	API ScratchImageT CreateScratchImage();

	API HRESULT Initialize(ScratchImageT img, _In_ const DirectX::TexMetadata& mdata, _In_ DirectX::CP_FLAGS flags) noexcept;

	API HRESULT Initialize1D(ScratchImageT img, _In_ DXGI_FORMAT fmt, _In_ size_t length, _In_ size_t arraySize, _In_ size_t mipLevels, _In_ DirectX::CP_FLAGS flags) noexcept;
	API HRESULT Initialize2D(ScratchImageT img, _In_ DXGI_FORMAT fmt, _In_ size_t width, _In_ size_t height, _In_ size_t arraySize, _In_ size_t mipLevels, _In_ DirectX::CP_FLAGS flags) noexcept;
	API HRESULT Initialize3D(ScratchImageT img, _In_ DXGI_FORMAT fmt, _In_ size_t width, _In_ size_t height, _In_ size_t depth, _In_ size_t mipLevels, _In_ DirectX::CP_FLAGS flags) noexcept;
	API HRESULT InitializeCube(ScratchImageT img, _In_ DXGI_FORMAT fmt, _In_ size_t width, _In_ size_t height, _In_ size_t nCubes, _In_ size_t mipLevels, _In_ DirectX::CP_FLAGS flags) noexcept;

	API HRESULT InitializeFromImage(ScratchImageT img, _In_ const DirectX::Image srcImage, _In_ bool allow1D = false, _In_ DirectX::CP_FLAGS flags = DirectX::CP_FLAGS_NONE) noexcept;
	API HRESULT InitializeArrayFromImages(ScratchImageT img, _In_reads_(nImages) const DirectX::Image* images, _In_ size_t nImages, _In_ bool allow1D = false, _In_ DirectX::CP_FLAGS flags = DirectX::CP_FLAGS_NONE) noexcept;
	API HRESULT InitializeCubeFromImages(ScratchImageT img, _In_reads_(nImages) const DirectX::Image* images, _In_ size_t nImages, _In_ DirectX::CP_FLAGS flags = DirectX::CP_FLAGS_NONE) noexcept;
	API HRESULT Initialize3DFromImages(ScratchImageT img, _In_reads_(depth) const DirectX::Image* images, _In_ size_t depth, _In_ DirectX::CP_FLAGS flags = DirectX::CP_FLAGS_NONE) noexcept;

	API void ScratchImageRelease(ScratchImageT img) noexcept;

	API bool OverrideFormat(ScratchImageT img, _In_ DXGI_FORMAT f) noexcept;

	API const DirectX::TexMetadata GetMetadata(ScratchImageT img) noexcept;
	API const DirectX::Image* GetImage(ScratchImageT img, _In_ size_t mip, _In_ size_t item, _In_ size_t slice) noexcept;

	API const DirectX::Image* GetImages(ScratchImageT img) noexcept;
	API size_t GetImageCount(ScratchImageT img) noexcept;

	API uint8_t* GetPixels(ScratchImageT img) noexcept;
	API size_t GetPixelsSize(ScratchImageT img) noexcept;

	API bool IsAlphaAllOpaque(ScratchImageT img);

#pragma endregion

#pragma region BlobMethods

	API BlobT CreateBlob();

	API HRESULT BlobInitialize(BlobT blob, _In_ size_t size) noexcept;

	API void BlobRelease(BlobT blob) noexcept;

	API void* BlobGetBufferPointer(BlobT blob) noexcept;
	API size_t BlobGetBufferSize(BlobT blob) noexcept;

	API HRESULT BlobResize(BlobT blob, size_t size) noexcept;
	// Reallocate for a new size

	API HRESULT BlobTrim(BlobT blob, size_t size) noexcept;
	// Shorten size without reallocation

#pragma endregion

#pragma region ImageIO

	// DDS operations
	API HRESULT LoadFromDDSMemory(_In_reads_bytes_(size) const void* pSource, _In_ size_t size, _In_ DirectX::DDS_FLAGS flags, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;
	API HRESULT LoadFromDDSFile(_In_z_ const wchar_t* szFile, _In_ DirectX::DDS_FLAGS flags, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;

	API HRESULT SaveToDDSMemory(_In_ const DirectX::Image& image, _In_ DirectX::DDS_FLAGS flags, _Out_ BlobT* blob) noexcept;
	API HRESULT SaveToDDSMemory2(_In_reads_(nimages) const DirectX::Image* images, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DirectX::DDS_FLAGS flags, _Out_ BlobT* blob) noexcept;

	API HRESULT SaveToDDSFile(_In_ const DirectX::Image& image, _In_ DirectX::DDS_FLAGS flags, _In_z_ const wchar_t* szFile) noexcept;
	API HRESULT SaveToDDSFile2(_In_reads_(nimages) const DirectX::Image* images, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DirectX::DDS_FLAGS flags, _In_z_ const wchar_t* szFile) noexcept;

	// HDR operations
	API HRESULT LoadFromHDRMemory(_In_reads_bytes_(size) const void* pSource, _In_ size_t size, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;
	API HRESULT LoadFromHDRFile(_In_z_ const wchar_t* szFile, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;

	API HRESULT SaveToHDRMemory(_In_ const DirectX::Image& image, _Out_ BlobT* blob) noexcept;
	API HRESULT SaveToHDRFile(_In_ const DirectX::Image& image, _In_z_ const wchar_t* szFile) noexcept;

	// TGA operations
	API HRESULT LoadFromTGAMemory(_In_reads_bytes_(size) const void* pSource, _In_ size_t size, _In_ DirectX::TGA_FLAGS flags, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;
	API HRESULT LoadFromTGAFile(_In_z_ const wchar_t* szFile, _In_ DirectX::TGA_FLAGS flags, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;

	API HRESULT SaveToTGAMemory(_In_ const DirectX::Image& image, _In_ DirectX::TGA_FLAGS flags, _Out_ BlobT* blob, _In_opt_ const DirectX::TexMetadata* metadata = nullptr) noexcept;
	API HRESULT SaveToTGAFile(_In_ const DirectX::Image& image, _In_ DirectX::TGA_FLAGS flags, _In_z_ const wchar_t* szFile, _In_opt_ const DirectX::TexMetadata* metadata = nullptr) noexcept;

	// WIC operations
#ifdef _WIN32

	API HRESULT LoadFromWICMemory(_In_reads_bytes_(size) const void* pSource, _In_ size_t size, _In_ DirectX::WIC_FLAGS flags, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image, GetMQR getMQR = nullptr);
	API HRESULT LoadFromWICFile(_In_z_ const wchar_t* szFile, _In_ DirectX::WIC_FLAGS flags, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image, GetMQR getMQR = nullptr);

	API HRESULT SaveToWICMemory(_In_ const DirectX::Image& image, _In_ DirectX::WIC_FLAGS flags, _In_ GUID guidContainerFormat, _Out_ BlobT* blob, _In_opt_ const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);
	API HRESULT SaveToWICMemory2(_In_count_(nimages) const DirectX::Image* images, _In_ size_t nimages, _In_ DirectX::WIC_FLAGS flags, _In_ GUID guidContainerFormat, _Out_ BlobT* blob, _In_opt_ const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);

	API HRESULT SaveToWICFile(_In_ const DirectX::Image& image, _In_ DirectX::WIC_FLAGS flags, _In_ GUID guidContainerFormat, _In_z_ const wchar_t* szFile, _In_opt_ const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);
	API HRESULT SaveToWICFile2(_In_count_(nimages) const DirectX::Image* images, _In_ size_t nimages, _In_ DirectX::WIC_FLAGS flags, _In_ GUID guidContainerFormat, _In_z_ const wchar_t* szFile, _In_opt_ const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);
#endif // _WIN32

	// Compatability helpers
	API HRESULT LoadFromTGAMemory2(_In_reads_bytes_(size) const void* pSource, _In_ size_t size, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;
	API HRESULT LoadFromTGAFile2(_In_z_ const wchar_t* szFile, _Out_opt_ DirectX::TexMetadata* metadata, ScratchImageT* image) noexcept;

	API HRESULT SaveToTGAMemory2(_In_ const DirectX::Image& image, _Out_ BlobT* blob, _In_opt_ const DirectX::TexMetadata* metadata = nullptr) noexcept;
	API HRESULT SaveToTGAFile2(_In_ const DirectX::Image& image, _In_z_ const wchar_t* szFile, _In_opt_ const DirectX::TexMetadata* metadata = nullptr) noexcept;

#pragma endregion

#pragma region Texture conversion, resizing, mipmap generation, and block compression

#ifdef _WIN32
	API HRESULT FlipRotate(_In_ const DirectX::Image& srcImage, _In_ DirectX::TEX_FR_FLAGS flags, _Out_ ScratchImageT* image) noexcept;
	API HRESULT FlipRotate2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DirectX::TEX_FR_FLAGS flags, _Out_ ScratchImageT* result) noexcept;
	// Flip and/or rotate image
#endif

	API HRESULT Resize(_In_ const DirectX::Image& srcImage, _In_ size_t width, _In_ size_t height, _In_ DirectX::TEX_FILTER_FLAGS filter, _Out_ ScratchImageT* image) noexcept;
	API HRESULT Resize2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ size_t width, _In_ size_t height, _In_ DirectX::TEX_FILTER_FLAGS filter, _Out_ ScratchImageT* result) noexcept;

	API HRESULT Convert(_In_ const DirectX::Image& srcImage, _In_ DXGI_FORMAT format, _In_ DirectX::TEX_FILTER_FLAGS filter, _In_ float threshold, _Out_ ScratchImageT* image) noexcept;
	API HRESULT Convert2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DXGI_FORMAT format, _In_ DirectX::TEX_FILTER_FLAGS filter, _In_ float threshold, _Out_ ScratchImageT* result) noexcept;
	// Convert the image to a new format

	API HRESULT ConvertToSinglePlane(_In_ const DirectX::Image& srcImage, _Out_ ScratchImageT* image) noexcept;
	API HRESULT ConvertToSinglePlane2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _Out_ ScratchImageT* image) noexcept;
	// Converts the image from a planar format to an equivalent non-planar format

	API HRESULT GenerateMipMaps(_In_ const DirectX::Image& baseImage, _In_ DirectX::TEX_FILTER_FLAGS filter, _In_ size_t levels, _Inout_ ScratchImageT* mipChain, _In_ bool allow1D = false) noexcept;
	API HRESULT GenerateMipMaps2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DirectX::TEX_FILTER_FLAGS filter, _In_ size_t levels, _Inout_ ScratchImageT* mipChain);
	// levels of '0' indicates a full mipchain, otherwise is generates that number of total levels (including the source base image)
	// Defaults to Fant filtering which is equivalent to a box filter

	API HRESULT GenerateMipMaps3D(_In_reads_(depth) const DirectX::Image* baseImages, _In_ size_t depth, _In_ DirectX::TEX_FILTER_FLAGS filter, _In_ size_t levels, _Out_ ScratchImageT* mipChain) noexcept;
	API HRESULT GenerateMipMaps3D2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ DirectX::TEX_FILTER_FLAGS filter, _In_ size_t levels, _Out_ ScratchImageT* mipChain);
	// levels of '0' indicates a full mipchain, otherwise is generates that number of total levels (including the source base image)
	// Defaults to Fant filtering which is equivalent to a box filter

	API HRESULT ScaleMipMapsAlphaForCoverage(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ size_t item, _In_ float alphaReference, _Inout_ ScratchImageT* mipChain) noexcept;

	API HRESULT PremultiplyAlpha(_In_ const DirectX::Image& srcImage, _In_ DirectX::TEX_PMALPHA_FLAGS flags, _Out_ ScratchImageT* image) noexcept;
	API HRESULT PremultiplyAlpha2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DirectX::TEX_PMALPHA_FLAGS flags, _Out_ ScratchImageT* result) noexcept;

	API HRESULT Compress(_In_ const DirectX::Image& srcImage, _In_ DXGI_FORMAT format, _In_ DirectX::TEX_COMPRESS_FLAGS compress, _In_ float threshold, _Out_ ScratchImageT* cImage) noexcept;
	API HRESULT Compress2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DXGI_FORMAT format, _In_ DirectX::TEX_COMPRESS_FLAGS compress, _In_ float threshold, _Out_ ScratchImageT* cImages) noexcept;
	// Note that threshold is only used by BC1. TEX_THRESHOLD_DEFAULT is a typical value to use

#if defined(__d3d11_h__) || defined(__d3d11_x_h__)
	API HRESULT Compress3(_In_ ID3D11Device* pDevice, _In_ const DirectX::Image& srcImage, _In_ DXGI_FORMAT format, _In_ DirectX::TEX_COMPRESS_FLAGS compress, _In_ float alphaWeight, _Out_ ScratchImageT* image) noexcept;
	API HRESULT Compress4(_In_ ID3D11Device* pDevice, _In_ const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DXGI_FORMAT format, _In_ DirectX::TEX_COMPRESS_FLAGS compress, _In_ float alphaWeight, _Out_ ScratchImageT* cImages) noexcept;
	// DirectCompute-based compression (alphaWeight is only used by BC7. 1.0 is the typical value to use)
#endif

	API HRESULT Decompress(_In_ const DirectX::Image& cImage, _In_ DXGI_FORMAT format, _Out_ ScratchImageT* image) noexcept;
	API HRESULT Decompress2(_In_reads_(nimages) const DirectX::Image* cImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DXGI_FORMAT format, _Out_ ScratchImageT* images) noexcept;

#pragma endregion

#pragma region Normal map operations

	API HRESULT ComputeNormalMap(_In_ const DirectX::Image& srcImage, _In_ DirectX::CNMAP_FLAGS flags, _In_ float amplitude, _In_ DXGI_FORMAT format, _Out_ ScratchImageT* normalMap) noexcept;
	API HRESULT ComputeNormalMap2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ DirectX::CNMAP_FLAGS flags, _In_ float amplitude, _In_ DXGI_FORMAT format, _Out_ ScratchImageT* normalMaps) noexcept;

#pragma endregion

#pragma region Misc image operations

	API HRESULT CopyRectangle(_In_ const DirectX::Image& srcImage, _In_ const DirectX::Rect& srcRect, _In_ const DirectX::Image& dstImage, _In_ DirectX::TEX_FILTER_FLAGS filter, _In_ size_t xOffset, _In_ size_t yOffset) noexcept;

	API HRESULT ComputeMSE(_In_ const DirectX::Image& image1, _In_ const DirectX::Image& image2, _Out_ float& mse, _Out_writes_opt_(4) float* mseV, _In_ DirectX::CMSE_FLAGS flags = DirectX::CMSE_DEFAULT) noexcept;

	API HRESULT EvaluateImage(_In_ const DirectX::Image& image, _In_ EvaluateImageFunc pixelFunc);
	API HRESULT EvaluateImage2(_In_reads_(nimages) const DirectX::Image* images, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ EvaluateImageFunc pixelFunc);

	API HRESULT TransformImage(_In_ const DirectX::Image& image, _In_ TransformImageFunc pixelFunc, ScratchImageT* result);
	API HRESULT TransformImage2(_In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ TransformImageFunc pixelFunc, ScratchImageT* result);

#pragma endregion

#pragma region WIC utility code

#ifdef _WIN32
	API GUID GetWICCodec(_In_ DirectX::WICCodecs codec) noexcept;

	API IWICImagingFactory* GetWICFactory(bool& iswic2) noexcept;
	API void SetWICFactory(_In_opt_ IWICImagingFactory* pWIC) noexcept;
#endif

#pragma endregion

#pragma region DDS helper functions

	API HRESULT EncodeDDSHeader(_In_ const DirectX::TexMetadata& metadata, DirectX::DDS_FLAGS flags, _Out_writes_bytes_to_opt_(maxsize, required) void* pDestination, _In_ size_t maxsize, _Out_ size_t& required) noexcept;

#pragma endregion

#pragma region Direct3D 11 functions

#if defined(__d3d11_h__) || defined(__d3d11_x_h__)
	API bool IsSupportedTexture(_In_ ID3D11Device* pDevice, _In_ const DirectX::TexMetadata& metadata) noexcept;

	API HRESULT CreateTexture(_In_ ID3D11Device* pDevice, _In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _Outptr_ ID3D11Resource** ppResource) noexcept;

	API HRESULT CreateShaderResourceView(_In_ ID3D11Device* pDevice, _In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _Outptr_ ID3D11ShaderResourceView** ppSRV) noexcept;

	API HRESULT CreateTextureEx(_In_ ID3D11Device* pDevice, _In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ D3D11_USAGE usage, _In_ unsigned int bindFlags, _In_ unsigned int cpuAccessFlags, _In_ unsigned int miscFlags, _In_ DirectX::CREATETEX_FLAGS createFlags, _Outptr_ ID3D11Resource** ppResource) noexcept;

	API HRESULT CreateTextureEx2(_In_ ID3D11Device* pDevice, _In_ ScratchImageT img, _In_ uint32_t usage, _In_ uint32_t bindFlags, _In_ uint32_t cpuAccessFlags, _In_ uint32_t miscFlags, _In_ DirectX::CREATETEX_FLAGS createFlags, _Outptr_ ID3D11Resource** ppResource) noexcept;

	API HRESULT CreateShaderResourceViewEx(_In_ ID3D11Device* pDevice, _In_reads_(nimages) const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, _In_ D3D11_USAGE usage, _In_ unsigned int bindFlags, _In_ unsigned int cpuAccessFlags, _In_ unsigned int miscFlags, _In_ DirectX::CREATETEX_FLAGS createFlags, _Outptr_ ID3D11ShaderResourceView** ppSRV) noexcept;

	API HRESULT CaptureTexture(_In_ ID3D11Device* pDevice, _In_ ID3D11DeviceContext* pContext, _In_ ID3D11Resource* pSource, _Out_ ScratchImageT* result) noexcept;
#endif

#pragma endregion

#pragma region Direct3D 12 functions

#if defined(__d3d12_h__) || defined(__d3d12_x_h__) || defined(__XBOX_D3D12_X__)
	API bool IsSupportedTextureD3D12(_In_ ID3D12Device* pDevice, _In_ const DirectX::TexMetadata& metadata) noexcept;

	API HRESULT CreateTextureD3D12(_In_ ID3D12Device* pDevice, _In_ const DirectX::TexMetadata& metadata, _Outptr_ ID3D12Resource** ppResource) noexcept;

	API HRESULT CreateTextureExD3D12(_In_ ID3D12Device* pDevice, _In_ const DirectX::TexMetadata& metadata, _In_ D3D12_RESOURCE_FLAGS resFlags, _In_ DirectX::CREATETEX_FLAGS createFlags, _Outptr_ ID3D12Resource** ppResource) noexcept;

	API HRESULT PrepareUpload(_In_ ID3D12Device* pDevice, _In_ const DirectX::Image* srcImages, _In_ size_t nimages, _In_ const DirectX::TexMetadata& metadata, void** subresources, size_t* nSubresources);

	API HRESULT CaptureTextureD3D12(_In_ ID3D12CommandQueue* pCommandQueue, _In_ ID3D12Resource* pSource, _In_ bool isCubeMap, _Out_ ScratchImageT* result, _In_ D3D12_RESOURCE_STATES beforeState = D3D12_RESOURCE_STATE_RENDER_TARGET, _In_ D3D12_RESOURCE_STATES afterState = D3D12_RESOURCE_STATE_RENDER_TARGET) noexcept;
#endif

#pragma endregion

#ifdef __cplusplus
}
#endif
