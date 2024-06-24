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
using Silk.NET.Direct2D;
using Silk.NET.Direct3D11;
using Silk.NET.Direct3D12;
using System.Numerics;

namespace Hexa.NET.DirectXTex
{
	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.StructOrClass, "TexMetadata")]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct TexMetadata
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "width")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong Width;

		/// <summary>
		/// Should be 1 for 1D textures<br/>
		/// </summary>
		[NativeName(NativeNameType.Field, "height")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong Height;

		/// <summary>
		/// Should be 1 for 1D or 2D textures<br/>
		/// </summary>
		[NativeName(NativeNameType.Field, "depth")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong Depth;

		/// <summary>
		/// For cubemap, this is a multiple of 6<br/>
		/// </summary>
		[NativeName(NativeNameType.Field, "arraySize")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong ArraySize;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "mipLevels")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong MipLevels;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "miscFlags")]
		[NativeName(NativeNameType.Type, "uint32_t")]
		public uint MiscFlags;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "miscFlags2")]
		[NativeName(NativeNameType.Type, "uint32_t")]
		public uint MiscFlags2;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "format")]
		[NativeName(NativeNameType.Type, "DXGI_FORMAT")]
		public int Format;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "dimension")]
		[NativeName(NativeNameType.Type, "TEX_DIMENSION")]
		public TexDimension Dimension;


		/// <summary>/// To be documented./// </summary>		public unsafe TexMetadata(ulong width = default, ulong height = default, ulong depth = default, ulong arraySize = default, ulong mipLevels = default, uint miscFlags = default, uint miscFlags2 = default, int format = default, TexDimension dimension = default)
		{
			Width = width;
			Height = height;
			Depth = depth;
			ArraySize = arraySize;
			MipLevels = mipLevels;
			MiscFlags = miscFlags;
			MiscFlags2 = miscFlags2;
			Format = format;
			Dimension = dimension;
		}


		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] ulong mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] ulong item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] ulong slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] nuint mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] ulong item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] ulong slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] ulong mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] nuint item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] ulong slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] nuint mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] nuint item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] ulong slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] ulong mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] ulong item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] nuint slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] nuint mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] ulong item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] nuint slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] ulong mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] nuint item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] nuint slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "ComputeIndex")]
		[return: NativeName(NativeNameType.Type, "size_t")]
		public unsafe ulong ComputeIndex([NativeName(NativeNameType.Param, "mip")] [NativeName(NativeNameType.Type, "size_t")] nuint mip, [NativeName(NativeNameType.Param, "item")] [NativeName(NativeNameType.Type, "size_t")] nuint item, [NativeName(NativeNameType.Param, "slice")] [NativeName(NativeNameType.Type, "size_t")] nuint slice)
		{
			ulong ret = DirectXTex.ComputeIndexNative(this, mip, item, slice);
			return ret;
		}

		/// <summary>/// Returns size_t(-1) to indicate an out-of-range error<br/>/// </summary>		[NativeName(NativeNameType.Func, "IsCubemap")]
		[return: NativeName(NativeNameType.Type, "bool")]
		public unsafe bool IsCubemap()
		{
			byte ret = DirectXTex.IsCubemapNative(this);
			return ret != 0;
		}

		/// <summary>/// Helper for miscFlags<br/>/// </summary>		[NativeName(NativeNameType.Func, "IsPMAlpha")]
		[return: NativeName(NativeNameType.Type, "bool")]
		public unsafe bool IsPMAlpha()
		{
			byte ret = DirectXTex.IsPMAlphaNative(this);
			return ret != 0;
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "SetAlphaMode")]
		[return: NativeName(NativeNameType.Type, "void")]
		public unsafe void SetAlphaMode([NativeName(NativeNameType.Param, "mode")] [NativeName(NativeNameType.Type, "TEX_ALPHA_MODE")] TexAlphaMode mode)
		{
			fixed (TexMetadata* @this = &this)
			{
				DirectXTex.SetAlphaModeNative(@this, mode);
			}
		}

		/// <summary>/// To be documented./// </summary>		[NativeName(NativeNameType.Func, "GetAlphaMode")]
		[return: NativeName(NativeNameType.Type, "TEX_ALPHA_MODE")]
		public unsafe TexAlphaMode GetAlphaMode()
		{
			TexAlphaMode ret = DirectXTex.GetAlphaModeNative(this);
			return ret;
		}

		/// <summary>/// Helpers for miscFlags2<br/>/// </summary>		[NativeName(NativeNameType.Func, "IsVolumemap")]
		[return: NativeName(NativeNameType.Type, "bool")]
		public unsafe bool IsVolumemap()
		{
			byte ret = DirectXTex.IsVolumemapNative(this);
			return ret != 0;
		}

	}

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.StructOrClass, "Image")]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct Image
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "width")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong Width;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "height")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong Height;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "format")]
		[NativeName(NativeNameType.Type, "DXGI_FORMAT")]
		public int Format;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "rowPitch")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong RowPitch;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "slicePitch")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong SlicePitch;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "pixels")]
		[NativeName(NativeNameType.Type, "uint8_t*")]
		public unsafe byte* Pixels;


		/// <summary>/// To be documented./// </summary>		public unsafe Image(ulong width = default, ulong height = default, int format = default, ulong rowPitch = default, ulong slicePitch = default, byte* pixels = default)
		{
			Width = width;
			Height = height;
			Format = format;
			RowPitch = rowPitch;
			SlicePitch = slicePitch;
			Pixels = pixels;
		}


	}

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.StructOrClass, "ScratchImageT")]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct ScratchImage
	{


	}

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.StructOrClass, "BlobT")]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct Blob
	{


	}

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.StructOrClass, "Rect")]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct Rect
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "x")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong X;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "y")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong Y;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "w")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong W;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "h")]
		[NativeName(NativeNameType.Type, "size_t")]
		public ulong H;


		/// <summary>/// To be documented./// </summary>		public unsafe Rect(ulong x = default, ulong y = default, ulong w = default, ulong h = default)
		{
			X = x;
			Y = y;
			W = w;
			H = h;
		}


	}

}
