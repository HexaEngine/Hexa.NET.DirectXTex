#pragma once
#pragma comment(lib, "d3d12.lib")

#ifdef _WIN32
#include <d3d12.h>
#include <d3d11_1.h>
#include <dxgiformat.h>
#include <wincodec.h>
#endif

#include <stdint.h>
#include <DirectXMath.h>

#if defined(_MSC_VER)
//  Microsoft
#define API __declspec(dllexport)
#define IMPORT __declspec(dllimport)
#elif defined(__GNUC__)
//  GCC
#define API __attribute__((visibility("default")))
#define IMPORT
#else
//  do nothing and hope for the best?
#define API
#define IMPORT
#pragma warning Unknown dynamic link import/export semantics.
#endif

#ifdef __cplusplus
extern "C"
{
#endif

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
	typedef	enum
	{
		TEX_MISC_TEXTURECUBE = 0x4L,
	} TEX_MISC_FLAG;

	typedef enum
	{
		TEX_MISC2_ALPHA_MODE_MASK = 0x7L,
	} TEX_MISC_FLAG2;

	// Matches DDS_ALPHA_MODE, encoded in MISC_FLAGS2
	typedef	enum
	{
		TEX_ALPHA_MODE_UNKNOWN = 0,
		TEX_ALPHA_MODE_STRAIGHT = 1,
		TEX_ALPHA_MODE_PREMULTIPLIED = 2,
		TEX_ALPHA_MODE_OPAQUE = 3,
		TEX_ALPHA_MODE_CUSTOM = 4,
	} TEX_ALPHA_MODE;

	typedef	struct
	{
		size_t          width;
		size_t          height;     // Should be 1 for 1D textures
		size_t          depth;      // Should be 1 for 1D or 2D textures
		size_t          arraySize;  // For cubemap, this is a multiple of 6
		size_t          mipLevels;
		uint32_t        miscFlags;
		uint32_t        miscFlags2;
		DXGI_FORMAT     format;
		TEX_DIMENSION   dimension;
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

	typedef	enum
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
		size_t      width;
		size_t      height;
		DXGI_FORMAT format;
		size_t      rowPitch;
		size_t      slicePitch;
		uint8_t* pixels;
	} Image;

	typedef struct ScratchImageT* ScratchImage;
	typedef struct BlobT* Blob;

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

	typedef	enum
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

	typedef	enum
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

	typedef	enum
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

	typedef	enum
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

	typedef	enum
	{
		WIC_CODEC_BMP = 1,          // Windows Bitmap (.bmp)
		WIC_CODEC_JPEG,             // Joint Photographic Experts Group (.jpg, .jpeg)
		WIC_CODEC_PNG,              // Portable Network Graphics (.png)
		WIC_CODEC_TIFF,             // Tagged Image File Format  (.tif, .tiff)
		WIC_CODEC_GIF,              // Graphics Interchange Format  (.gif)
		WIC_CODEC_WMP,              // Windows Media Photo / HD Photo / JPEG XR (.hdp, .jxr, .wdp)
		WIC_CODEC_ICO,              // Windows Icon (.ico)
		WIC_CODEC_HEIF,             // High Efficiency Image File (.heif, .heic)
	} WICCodecs;

	typedef enum
	{
		CREATETEX_DEFAULT = 0,
		CREATETEX_FORCE_SRGB = 0x1,
		CREATETEX_IGNORE_SRGB = 0x2,
	} CREATETEX_FLAGS;

#pragma region DXGI Format Utilities

	API bool IsValid(DXGI_FORMAT fmt) noexcept;
	API bool IsCompressed(DXGI_FORMAT fmt) noexcept;
	API bool IsPacked(DXGI_FORMAT fmt) noexcept;
	API bool IsVideo(DXGI_FORMAT fmt) noexcept;
	API bool IsPlanar(DXGI_FORMAT fmt) noexcept;
	API bool IsPalettized(DXGI_FORMAT fmt) noexcept;
	API bool IsDepthStencil(DXGI_FORMAT fmt) noexcept;
	API bool IsSRGB(DXGI_FORMAT fmt) noexcept;
	API bool IsTypeless(DXGI_FORMAT fmt, bool partialTypeless) noexcept;

	API bool HasAlpha(DXGI_FORMAT fmt) noexcept;

	API size_t BitsPerPixel(DXGI_FORMAT fmt) noexcept;

	API size_t BitsPerColor(DXGI_FORMAT fmt) noexcept;

	API FORMAT_TYPE FormatDataType(DXGI_FORMAT fmt) noexcept;
	API HRESULT ComputePitch(DXGI_FORMAT fmt, size_t width, size_t height, size_t* rowPitch, size_t* slicePitch, CP_FLAGS flags) noexcept;

	API size_t ComputeScanlines(DXGI_FORMAT fmt, size_t height) noexcept;

	API DXGI_FORMAT MakeSRGB(DXGI_FORMAT fmt) noexcept;
	API DXGI_FORMAT MakeTypeless(DXGI_FORMAT fmt) noexcept;
	API DXGI_FORMAT MakeTypelessUNORM(DXGI_FORMAT fmt) noexcept;
	API DXGI_FORMAT MakeTypelessFLOAT(DXGI_FORMAT fmt) noexcept;

#pragma endregion

#pragma region MetadataIO

	API HRESULT GetMetadataFromDDSMemory(
		const void* pSource, size_t size,
		DDS_FLAGS flags,
		TexMetadata* metadata) noexcept;
	API HRESULT GetMetadataFromDDSFile(
		const wchar_t* szFile,
		DDS_FLAGS flags,
		TexMetadata* metadata) noexcept;

	API HRESULT GetMetadataFromHDRMemory(
		const void* pSource, size_t size,
		TexMetadata* metadata) noexcept;
	API HRESULT GetMetadataFromHDRFile(
		const wchar_t* szFile,
		TexMetadata* metadata) noexcept;

	API HRESULT GetMetadataFromTGAMemory(
		const void* pSource, size_t size,
		TGA_FLAGS flags,
		TexMetadata* metadata) noexcept;
	API HRESULT GetMetadataFromTGAFile(
		const wchar_t* szFile,
		TGA_FLAGS flags,
		TexMetadata* metadata) noexcept;

#ifdef _WIN32
	API HRESULT GetMetadataFromWICMemory(
		const void* pSource, size_t size,
		WIC_FLAGS flags,
		TexMetadata* metadata);

	API HRESULT GetMetadataFromWICFile(
		const wchar_t* szFile,
		WIC_FLAGS flags,
		TexMetadata* metadata);
#endif

	// Compatability helpers
	API HRESULT GetMetadataFromTGAMemory2(
		const void* pSource, size_t size,
		TexMetadata* metadata) noexcept;
	API HRESULT GetMetadataFromTGAFile2(
		const wchar_t* szFile,
		TexMetadata* metadata) noexcept;

#pragma endregion

#pragma region TexMetadata Methods

	API size_t ComputeIndex(TexMetadata metadata, size_t mip, size_t item, size_t slice) noexcept;
	// Returns size_t(-1) to indicate an out-of-range error

	API bool IsCubemap(TexMetadata metadata) noexcept;
	// Helper for miscFlags

	API bool IsPMAlpha(TexMetadata metadata) noexcept;
	API void SetAlphaMode(TexMetadata* metadata, TEX_ALPHA_MODE mode) noexcept;
	API TEX_ALPHA_MODE GetAlphaMode(TexMetadata metadata) noexcept;
	// Helpers for miscFlags2

	API bool IsVolumemap(TexMetadata metadata) noexcept;
	// Helper for dimension

#pragma endregion

#pragma region ScratchImage Methods

	API ScratchImage CreateScratchImage();

	API HRESULT Initialize(ScratchImage img, TexMetadata mdata, CP_FLAGS flags) noexcept;

	API HRESULT Initialize1D(ScratchImage img, DXGI_FORMAT fmt, size_t length, size_t arraySize, size_t mipLevels, CP_FLAGS flags) noexcept;
	API HRESULT Initialize2D(ScratchImage img, DXGI_FORMAT fmt, size_t width, size_t height, size_t arraySize, size_t mipLevels, CP_FLAGS flags) noexcept;
	API HRESULT Initialize3D(ScratchImage img, DXGI_FORMAT fmt, size_t width, size_t height, size_t depth, size_t mipLevels, CP_FLAGS flags) noexcept;
	API HRESULT InitializeCube(ScratchImage img, DXGI_FORMAT fmt, size_t width, size_t height, size_t nCubes, size_t mipLevels, CP_FLAGS flags) noexcept;

	API HRESULT InitializeFromImage(ScratchImage img, const Image srcImage, bool allow1D = false, CP_FLAGS flags = CP_FLAGS_NONE) noexcept;
	API HRESULT InitializeArrayFromImages(ScratchImage img, const Image* images, size_t nImages, bool allow1D = false, CP_FLAGS flags = CP_FLAGS_NONE) noexcept;
	API HRESULT InitializeCubeFromImages(ScratchImage img, const Image* images, size_t nImages, CP_FLAGS flags = CP_FLAGS_NONE) noexcept;
	API HRESULT Initialize3DFromImages(ScratchImage img, const Image* images, size_t depth, CP_FLAGS flags = CP_FLAGS_NONE) noexcept;

	API void ScratchImageRelease(ScratchImage img) noexcept;

	API bool OverrideFormat(ScratchImage img, DXGI_FORMAT f) noexcept;

	API const TexMetadata GetMetadata(ScratchImage img) noexcept;
	API const Image GetImage(ScratchImage img, size_t mip, size_t item, size_t slice) noexcept;

	API const Image* GetImages(ScratchImage img) noexcept;
	API size_t GetImageCount(ScratchImage img) noexcept;

	API uint8_t* GetPixels(ScratchImage img) noexcept;
	API size_t GetPixelsSize(ScratchImage img) noexcept;

	API bool IsAlphaAllOpaque(ScratchImage img);

#pragma endregion

#pragma region BlobMethods

	API Blob CreateBlob();

	API HRESULT BlobInitialize(Blob blob, size_t size) noexcept;

	API void BlobRelease(Blob blob) noexcept;

	API void* BlobGetBufferPointer(Blob blob) noexcept;
	API size_t BlobGetBufferSize(Blob blob) noexcept;

	API HRESULT BlobResize(Blob blob, size_t size) noexcept;
	// Reallocate for a new size

	API HRESULT BlobTrim(Blob blob, size_t size) noexcept;
	// Shorten size without reallocation

#pragma endregion

#pragma region ImageIO

	// DDS operations
	API HRESULT LoadFromDDSMemory(const void* pSource, size_t size, DDS_FLAGS flags, TexMetadata* metadata, ScratchImage image) noexcept;
	API HRESULT LoadFromDDSFile(const wchar_t* szFile, DDS_FLAGS flags, TexMetadata* metadata, ScratchImage image) noexcept;

	API HRESULT SaveToDDSMemory(Image image, DDS_FLAGS flags, Blob blob) noexcept;
	API HRESULT SaveToDDSMemory2(const Image* images, size_t nimages, TexMetadata metadata, DDS_FLAGS flags, Blob blob) noexcept;

	API HRESULT SaveToDDSFile(Image image, DDS_FLAGS flags, const wchar_t* szFile) noexcept;
	API HRESULT SaveToDDSFile2(const Image* images, size_t nimages, TexMetadata metadata, DDS_FLAGS flags, const wchar_t* szFile) noexcept;

	// HDR operations
	API HRESULT LoadFromHDRMemory(const void* pSource, size_t size, TexMetadata* metadata, ScratchImage image) noexcept;
	API HRESULT LoadFromHDRFile(const wchar_t* szFile, TexMetadata* metadata, ScratchImage image) noexcept;

	API HRESULT SaveToHDRMemory(Image image, Blob blob) noexcept;
	API HRESULT SaveToHDRFile(Image image, const wchar_t* szFile) noexcept;

	// TGA operations
	API HRESULT LoadFromTGAMemory(const void* pSource, size_t size, TGA_FLAGS flags, TexMetadata* metadata, ScratchImage image) noexcept;
	API HRESULT LoadFromTGAFile(const wchar_t* szFile, TGA_FLAGS flags, TexMetadata* metadata, ScratchImage image) noexcept;

	API HRESULT SaveToTGAMemory(Image image, TGA_FLAGS flags, Blob blob, const TexMetadata* metadata = nullptr) noexcept;
	API HRESULT SaveToTGAFile(Image image, TGA_FLAGS flags, const wchar_t* szFile, const TexMetadata* metadata = nullptr) noexcept;

	// WIC operations
#ifdef _WIN32

	API HRESULT LoadFromWICMemory(const void* pSource, size_t size, WIC_FLAGS flags, TexMetadata* metadata, ScratchImage image, GetMQR getMQR = nullptr);
	API HRESULT LoadFromWICFile(const wchar_t* szFile, WIC_FLAGS flags, TexMetadata* metadata, ScratchImage image, GetMQR getMQR = nullptr);

	API HRESULT SaveToWICMemory(Image image, WIC_FLAGS flags, GUID guidContainerFormat, Blob blob, const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);
	API HRESULT SaveToWICMemory2(const Image* images, size_t nimages, WIC_FLAGS flags, GUID guidContainerFormat, Blob blob, const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);

	API HRESULT SaveToWICFile(Image image, WIC_FLAGS flags, GUID guidContainerFormat, const wchar_t* szFile, const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);
	API HRESULT SaveToWICFile2(const Image* images, size_t nimages, WIC_FLAGS flags, GUID guidContainerFormat, const wchar_t* szFile, const GUID* targetFormat = nullptr, SetCustomProps customProps = nullptr);
#endif // _WIN32

	// Compatability helpers
	API HRESULT LoadFromTGAMemory2(const void* pSource, size_t size, TexMetadata* metadata, ScratchImage image) noexcept;
	API HRESULT LoadFromTGAFile2(const wchar_t* szFile, TexMetadata* metadata, ScratchImage image) noexcept;

	API HRESULT SaveToTGAMemory2(Image image, Blob blob, const TexMetadata* metadata = nullptr) noexcept;
	API HRESULT SaveToTGAFile2(Image image, const wchar_t* szFile, const TexMetadata* metadata = nullptr) noexcept;

#pragma endregion

#pragma region Texture conversion, resizing, mipmap generation, and block compression

#ifdef _WIN32
	API HRESULT FlipRotate(Image srcImage, TEX_FR_FLAGS flags, ScratchImage image) noexcept;
	API HRESULT FlipRotate2(const Image* srcImages, size_t nimages, TexMetadata metadata, TEX_FR_FLAGS flags, ScratchImage result) noexcept;
	// Flip and/or rotate image
#endif

	API HRESULT Resize(Image srcImage, size_t width, size_t height, TEX_FILTER_FLAGS filter, ScratchImage image) noexcept;
	API HRESULT Resize2(const Image* srcImages, size_t nimages, TexMetadata metadata, size_t width, size_t height, TEX_FILTER_FLAGS filter, ScratchImage result) noexcept;

	API HRESULT Convert(Image srcImage, DXGI_FORMAT format, TEX_FILTER_FLAGS filter, float threshold, ScratchImage image) noexcept;
	API HRESULT Convert2(const Image* srcImages, size_t nimages, TexMetadata metadata, DXGI_FORMAT format, TEX_FILTER_FLAGS filter, float threshold, ScratchImage result) noexcept;
	// Convert the image to a new format

	API HRESULT ConvertToSinglePlane(Image srcImage, ScratchImage image) noexcept;
	API HRESULT ConvertToSinglePlane2(const Image* srcImages, size_t nimages, TexMetadata metadata, ScratchImage image) noexcept;
	// Converts the image from a planar format to an equivalent non-planar format

	API HRESULT GenerateMipMaps(Image baseImage, TEX_FILTER_FLAGS filter, size_t levels, ScratchImage mipChain, bool allow1D = false) noexcept;
	API HRESULT GenerateMipMaps2(const Image* srcImages, size_t nimages, TexMetadata metadata, TEX_FILTER_FLAGS filter, size_t levels, ScratchImage mipChain);
	// levels of '0' indicates a full mipchain, otherwise is generates that number of total levels (including the source base image)
	// Defaults to Fant filtering which is equivalent to a box filter

	API HRESULT GenerateMipMaps3D(const Image* baseImages, size_t depth, TEX_FILTER_FLAGS filter, size_t levels, ScratchImage mipChain) noexcept;
	API HRESULT GenerateMipMaps3D2(const Image* srcImages, size_t nimages, TEX_FILTER_FLAGS filter, size_t levels, ScratchImage mipChain);
	// levels of '0' indicates a full mipchain, otherwise is generates that number of total levels (including the source base image)
	// Defaults to Fant filtering which is equivalent to a box filter

	API HRESULT ScaleMipMapsAlphaForCoverage(const Image* srcImages, size_t nimages, TexMetadata metadata, size_t item, float alphaReference, ScratchImage mipChain) noexcept;

	API HRESULT PremultiplyAlpha(Image srcImage, TEX_PMALPHA_FLAGS flags, ScratchImage image) noexcept;
	API HRESULT PremultiplyAlpha2(const Image* srcImages, size_t nimages, TexMetadata metadata, TEX_PMALPHA_FLAGS flags, ScratchImage result) noexcept;

	API HRESULT Compress(Image srcImage, DXGI_FORMAT format, TEX_COMPRESS_FLAGS compress, float threshold, ScratchImage cImage) noexcept;
	API HRESULT Compress2(const Image* srcImages, size_t nimages, TexMetadata metadata, DXGI_FORMAT format, TEX_COMPRESS_FLAGS compress, float threshold, ScratchImage cImages) noexcept;
	// Note that threshold is only used by BC1. TEX_THRESHOLD_DEFAULT is a typical value to use

#if defined(__d3d11_h__) || defined(__d3d11_x_h__)
	API HRESULT Compress3(ID3D11Device* pDevice, Image srcImage, DXGI_FORMAT format, TEX_COMPRESS_FLAGS compress, float alphaWeight, ScratchImage image) noexcept;
	API HRESULT Compress4(ID3D11Device* pDevice, const Image* srcImages, size_t nimages, TexMetadata metadata, DXGI_FORMAT format, TEX_COMPRESS_FLAGS compress, float alphaWeight, ScratchImage cImages) noexcept;
	// DirectCompute-based compression (alphaWeight is only used by BC7. 1.0 is the typical value to use)
#endif

	API HRESULT Decompress(Image cImage, DXGI_FORMAT format, ScratchImage image) noexcept;
	API HRESULT Decompress2(const Image* cImages, size_t nimages, TexMetadata metadata, DXGI_FORMAT format, ScratchImage images) noexcept;

#pragma endregion

#pragma region Normal map operations

	API HRESULT ComputeNormalMap(Image srcImage, CNMAP_FLAGS flags, float amplitude, DXGI_FORMAT format, ScratchImage normalMap) noexcept;
	API HRESULT ComputeNormalMap2(const Image* srcImages, size_t nimages, TexMetadata metadata, CNMAP_FLAGS flags, float amplitude, DXGI_FORMAT format, ScratchImage normalMaps) noexcept;

#pragma endregion

#pragma region Misc image operations

	API HRESULT CopyRectangle(Image srcImage, Rect srcRect, Image dstImage, TEX_FILTER_FLAGS filter, size_t xOffset, size_t yOffset) noexcept;

	API HRESULT ComputeMSE(Image image1, Image image2, float* mse, float* mseV, CMSE_FLAGS flags = CMSE_DEFAULT) noexcept;

	API HRESULT EvaluateImage(Image image, EvaluateImageFunc pixelFunc);
	API HRESULT EvaluateImage2(const Image* images, size_t nimages, TexMetadata metadata, EvaluateImageFunc pixelFunc);

	API HRESULT TransformImage(Image image, TransformImageFunc pixelFunc, ScratchImage result);
	API HRESULT TransformImage2(const Image* srcImages, size_t nimages, TexMetadata metadata, TransformImageFunc pixelFunc, ScratchImage result);

#pragma endregion

#pragma region WIC utility code

#ifdef _WIN32
	API GUID GetWICCodec(WICCodecs codec) noexcept;

	API IWICImagingFactory* GetWICFactory(bool* iswic2) noexcept;
	API void SetWICFactory(IWICImagingFactory* pWIC) noexcept;
#endif

#pragma endregion

#pragma region DDS helper functions

	API HRESULT EncodeDDSHeader(TexMetadata metadata, DDS_FLAGS flags, void* pDestination, size_t maxsize, size_t* required) noexcept;

#pragma endregion

#pragma region Direct3D 11 functions

#if defined(__d3d11_h__) || defined(__d3d11_x_h__)
	API bool IsSupportedTexture(ID3D11Device* pDevice, TexMetadata metadata) noexcept;

	API HRESULT CreateTexture(ID3D11Device* pDevice, const Image* srcImages, size_t nimages, TexMetadata metadata, ID3D11Resource** ppResource) noexcept;

	API HRESULT CreateShaderResourceView(ID3D11Device* pDevice, const Image* srcImages, size_t nimages, TexMetadata metadata, ID3D11ShaderResourceView** ppSRV) noexcept;

	API HRESULT CreateTextureEx(ID3D11Device* pDevice, const Image* srcImages, size_t nimages, TexMetadata metadata, D3D11_USAGE usage, unsigned int bindFlags, unsigned int cpuAccessFlags, unsigned int miscFlags, CREATETEX_FLAGS createFlags, ID3D11Resource** ppResource) noexcept;

	API HRESULT CreateTextureEx2(ID3D11Device* pDevice, ScratchImage img, uint32_t usage, uint32_t bindFlags, uint32_t cpuAccessFlags, uint32_t miscFlags, CREATETEX_FLAGS createFlags, ID3D11Resource** ppResource) noexcept;

	API HRESULT CreateShaderResourceViewEx(ID3D11Device* pDevice, const Image* srcImages, size_t nimages, TexMetadata metadata, D3D11_USAGE usage, unsigned int bindFlags, unsigned int cpuAccessFlags, unsigned int miscFlags, CREATETEX_FLAGS createFlags, ID3D11ShaderResourceView** ppSRV) noexcept;

	API HRESULT CaptureTexture(ID3D11Device* pDevice, ID3D11DeviceContext* pContext, ID3D11Resource* pSource, ScratchImage result) noexcept;
#endif

#pragma endregion

#pragma region Direct3D 12 functions

#if defined(__d3d12_h__) || defined(__d3d12_x_h__) || defined(__XBOX_D3D12_X__)
	API bool IsSupportedTextureD3D12(ID3D12Device* pDevice, TexMetadata metadata) noexcept;

	API HRESULT CreateTextureD3D12(ID3D12Device* pDevice, TexMetadata metadata, ID3D12Resource** ppResource) noexcept;

	API HRESULT CreateTextureExD3D12(ID3D12Device* pDevice, TexMetadata metadata, D3D12_RESOURCE_FLAGS resFlags, CREATETEX_FLAGS createFlags, ID3D12Resource** ppResource) noexcept;

	API HRESULT PrepareUpload(ID3D12Device* pDevice, const Image* srcImages, size_t nimages, TexMetadata metadata, void** subresources, size_t* nSubresources);

	API HRESULT CaptureTextureD3D12(ID3D12CommandQueue* pCommandQueue, ID3D12Resource* pSource, bool isCubeMap, ScratchImage result, D3D12_RESOURCE_STATES beforeState = D3D12_RESOURCE_STATE_RENDER_TARGET, D3D12_RESOURCE_STATES afterState = D3D12_RESOURCE_STATE_RENDER_TARGET) noexcept;
#endif

#pragma endregion

#ifdef __cplusplus
}
#endif