// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HexaGen.Runtime;
using System.Numerics;

namespace Hexa.NET.DirectXTex
{
	public unsafe partial class DirectXTex
	{

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CreateTextureExD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CreateTextureExD3D12([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "resFlags")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_FLAGS")] int resFlags, [NativeName(NativeNameType.Param, "createFlags")] [NativeName(NativeNameType.Type, "CREATETEX_FLAGS")] CreateTexFlags createFlags, [NativeName(NativeNameType.Param, "ppResource")] [NativeName(NativeNameType.Type, "ID3D12Resource * *")] ref ID3D12Resource* ppResource)
		{
			fixed (ID3D12Resource** pppResource = &ppResource)
			{
				HResult ret = CreateTextureExD3D12Native(pDevice, metadata, resFlags, createFlags, (ID3D12Resource**)pppResource);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CreateTextureExD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CreateTextureExD3D12([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "resFlags")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_FLAGS")] int resFlags, [NativeName(NativeNameType.Param, "createFlags")] [NativeName(NativeNameType.Type, "CREATETEX_FLAGS")] CreateTexFlags createFlags, [NativeName(NativeNameType.Param, "ppResource")] [NativeName(NativeNameType.Type, "ID3D12Resource * *")] ref ID3D12Resource* ppResource)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (ID3D12Resource** pppResource = &ppResource)
				{
					HResult ret = CreateTextureExD3D12Native((ID3D12Device*)ppDevice, metadata, resFlags, createFlags, (ID3D12Resource**)pppResource);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CreateTextureExD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CreateTextureExD3D12([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "resFlags")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_FLAGS")] int resFlags, [NativeName(NativeNameType.Param, "createFlags")] [NativeName(NativeNameType.Type, "CREATETEX_FLAGS")] CreateTexFlags createFlags, [NativeName(NativeNameType.Param, "ppResource")] [NativeName(NativeNameType.Type, "ID3D12Resource * *")] ref ID3D12Resource* ppResource)
		{
			fixed (TexMetadata* pmetadata = &metadata)
			{
				fixed (ID3D12Resource** pppResource = &ppResource)
				{
					HResult ret = CreateTextureExD3D12Native(pDevice, (TexMetadata*)pmetadata, resFlags, createFlags, (ID3D12Resource**)pppResource);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CreateTextureExD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CreateTextureExD3D12([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "resFlags")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_FLAGS")] int resFlags, [NativeName(NativeNameType.Param, "createFlags")] [NativeName(NativeNameType.Type, "CREATETEX_FLAGS")] CreateTexFlags createFlags, [NativeName(NativeNameType.Param, "ppResource")] [NativeName(NativeNameType.Type, "ID3D12Resource * *")] ref ID3D12Resource* ppResource)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (TexMetadata* pmetadata = &metadata)
				{
					fixed (ID3D12Resource** pppResource = &ppResource)
					{
						HResult ret = CreateTextureExD3D12Native((ID3D12Device*)ppDevice, (TexMetadata*)pmetadata, resFlags, createFlags, (ID3D12Resource**)pppResource);
						return ret;
					}
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HResult PrepareUploadNative([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			#if NET5_0_OR_GREATER
			return ((delegate* unmanaged[Cdecl]<ID3D12Device*, Image*, nuint, TexMetadata*, void**, nuint*, HResult>)funcTable[137])(pDevice, srcImages, nimages, metadata, subresources, nSubresources);
			#else
			return (HResult)((delegate* unmanaged[Cdecl]<nint, nint, nuint, nint, nint, nint, HResult>)funcTable[137])((nint)pDevice, (nint)srcImages, nimages, (nint)metadata, (nint)subresources, (nint)nSubresources);
			#endif
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			HResult ret = PrepareUploadNative(pDevice, srcImages, nimages, metadata, subresources, nSubresources);
			return ret;
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, srcImages, nimages, metadata, subresources, nSubresources);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			fixed (Image* psrcImages = &srcImages)
			{
				HResult ret = PrepareUploadNative(pDevice, (Image*)psrcImages, nimages, metadata, subresources, nSubresources);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (Image* psrcImages = &srcImages)
				{
					HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, (Image*)psrcImages, nimages, metadata, subresources, nSubresources);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			fixed (TexMetadata* pmetadata = &metadata)
			{
				HResult ret = PrepareUploadNative(pDevice, srcImages, nimages, (TexMetadata*)pmetadata, subresources, nSubresources);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (TexMetadata* pmetadata = &metadata)
				{
					HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, srcImages, nimages, (TexMetadata*)pmetadata, subresources, nSubresources);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			fixed (Image* psrcImages = &srcImages)
			{
				fixed (TexMetadata* pmetadata = &metadata)
				{
					HResult ret = PrepareUploadNative(pDevice, (Image*)psrcImages, nimages, (TexMetadata*)pmetadata, subresources, nSubresources);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] nuint* nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (Image* psrcImages = &srcImages)
				{
					fixed (TexMetadata* pmetadata = &metadata)
					{
						HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, (Image*)psrcImages, nimages, (TexMetadata*)pmetadata, subresources, nSubresources);
						return ret;
					}
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (nuint* pnSubresources = &nSubresources)
			{
				HResult ret = PrepareUploadNative(pDevice, srcImages, nimages, metadata, subresources, (nuint*)pnSubresources);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (nuint* pnSubresources = &nSubresources)
				{
					HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, srcImages, nimages, metadata, subresources, (nuint*)pnSubresources);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (Image* psrcImages = &srcImages)
			{
				fixed (nuint* pnSubresources = &nSubresources)
				{
					HResult ret = PrepareUploadNative(pDevice, (Image*)psrcImages, nimages, metadata, subresources, (nuint*)pnSubresources);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] TexMetadata* metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (Image* psrcImages = &srcImages)
				{
					fixed (nuint* pnSubresources = &nSubresources)
					{
						HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, (Image*)psrcImages, nimages, metadata, subresources, (nuint*)pnSubresources);
						return ret;
					}
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (TexMetadata* pmetadata = &metadata)
			{
				fixed (nuint* pnSubresources = &nSubresources)
				{
					HResult ret = PrepareUploadNative(pDevice, srcImages, nimages, (TexMetadata*)pmetadata, subresources, (nuint*)pnSubresources);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] Image* srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (TexMetadata* pmetadata = &metadata)
				{
					fixed (nuint* pnSubresources = &nSubresources)
					{
						HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, srcImages, nimages, (TexMetadata*)pmetadata, subresources, (nuint*)pnSubresources);
						return ret;
					}
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ID3D12Device* pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (Image* psrcImages = &srcImages)
			{
				fixed (TexMetadata* pmetadata = &metadata)
				{
					fixed (nuint* pnSubresources = &nSubresources)
					{
						HResult ret = PrepareUploadNative(pDevice, (Image*)psrcImages, nimages, (TexMetadata*)pmetadata, subresources, (nuint*)pnSubresources);
						return ret;
					}
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "PrepareUpload")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult PrepareUpload([NativeName(NativeNameType.Param, "pDevice")] [NativeName(NativeNameType.Type, "ID3D12Device *")] ref ID3D12Device pDevice, [NativeName(NativeNameType.Param, "srcImages")] [NativeName(NativeNameType.Type, "Image const *")] ref Image srcImages, [NativeName(NativeNameType.Param, "nimages")] [NativeName(NativeNameType.Type, "size_t")] nuint nimages, [NativeName(NativeNameType.Param, "metadata")] [NativeName(NativeNameType.Type, "TexMetadata const&")] ref TexMetadata metadata, [NativeName(NativeNameType.Param, "subresources")] [NativeName(NativeNameType.Type, "void * *")] void** subresources, [NativeName(NativeNameType.Param, "nSubresources")] [NativeName(NativeNameType.Type, "size_t *")] ref nuint nSubresources)
		{
			fixed (ID3D12Device* ppDevice = &pDevice)
			{
				fixed (Image* psrcImages = &srcImages)
				{
					fixed (TexMetadata* pmetadata = &metadata)
					{
						fixed (nuint* pnSubresources = &nSubresources)
						{
							HResult ret = PrepareUploadNative((ID3D12Device*)ppDevice, (Image*)psrcImages, nimages, (TexMetadata*)pmetadata, subresources, (nuint*)pnSubresources);
							return ret;
						}
					}
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HResult CaptureTextureD3D12Native([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ID3D12CommandQueue* pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ID3D12Resource* pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] byte isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ScratchImage* result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			#if NET5_0_OR_GREATER
			return ((delegate* unmanaged[Cdecl]<ID3D12CommandQueue*, ID3D12Resource*, byte, ScratchImage*, int, int, HResult>)funcTable[138])(pCommandQueue, pSource, isCubeMap, result, beforeState, afterState);
			#else
			return (HResult)((delegate* unmanaged[Cdecl]<nint, nint, byte, nint, int, int, HResult>)funcTable[138])((nint)pCommandQueue, (nint)pSource, isCubeMap, (nint)result, beforeState, afterState);
			#endif
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ID3D12CommandQueue* pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ID3D12Resource* pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ScratchImage* result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			HResult ret = CaptureTextureD3D12Native(pCommandQueue, pSource, isCubeMap ? (byte)1 : (byte)0, result, beforeState, afterState);
			return ret;
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ref ID3D12CommandQueue pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ID3D12Resource* pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ScratchImage* result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			fixed (ID3D12CommandQueue* ppCommandQueue = &pCommandQueue)
			{
				HResult ret = CaptureTextureD3D12Native((ID3D12CommandQueue*)ppCommandQueue, pSource, isCubeMap ? (byte)1 : (byte)0, result, beforeState, afterState);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ID3D12CommandQueue* pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ref ID3D12Resource pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ScratchImage* result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			fixed (ID3D12Resource* ppSource = &pSource)
			{
				HResult ret = CaptureTextureD3D12Native(pCommandQueue, (ID3D12Resource*)ppSource, isCubeMap ? (byte)1 : (byte)0, result, beforeState, afterState);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ref ID3D12CommandQueue pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ref ID3D12Resource pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ScratchImage* result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			fixed (ID3D12CommandQueue* ppCommandQueue = &pCommandQueue)
			{
				fixed (ID3D12Resource* ppSource = &pSource)
				{
					HResult ret = CaptureTextureD3D12Native((ID3D12CommandQueue*)ppCommandQueue, (ID3D12Resource*)ppSource, isCubeMap ? (byte)1 : (byte)0, result, beforeState, afterState);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ID3D12CommandQueue* pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ID3D12Resource* pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ref ScratchImage result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			fixed (ScratchImage* presult = &result)
			{
				HResult ret = CaptureTextureD3D12Native(pCommandQueue, pSource, isCubeMap ? (byte)1 : (byte)0, (ScratchImage*)presult, beforeState, afterState);
				return ret;
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ref ID3D12CommandQueue pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ID3D12Resource* pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ref ScratchImage result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			fixed (ID3D12CommandQueue* ppCommandQueue = &pCommandQueue)
			{
				fixed (ScratchImage* presult = &result)
				{
					HResult ret = CaptureTextureD3D12Native((ID3D12CommandQueue*)ppCommandQueue, pSource, isCubeMap ? (byte)1 : (byte)0, (ScratchImage*)presult, beforeState, afterState);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ID3D12CommandQueue* pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ref ID3D12Resource pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ref ScratchImage result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			fixed (ID3D12Resource* ppSource = &pSource)
			{
				fixed (ScratchImage* presult = &result)
				{
					HResult ret = CaptureTextureD3D12Native(pCommandQueue, (ID3D12Resource*)ppSource, isCubeMap ? (byte)1 : (byte)0, (ScratchImage*)presult, beforeState, afterState);
					return ret;
				}
			}
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Func, "CaptureTextureD3D12")]
		[return: NativeName(NativeNameType.Type, "HRESULT")]
		public static HResult CaptureTextureD3D12([NativeName(NativeNameType.Param, "pCommandQueue")] [NativeName(NativeNameType.Type, "ID3D12CommandQueue *")] ref ID3D12CommandQueue pCommandQueue, [NativeName(NativeNameType.Param, "pSource")] [NativeName(NativeNameType.Type, "ID3D12Resource *")] ref ID3D12Resource pSource, [NativeName(NativeNameType.Param, "isCubeMap")] [NativeName(NativeNameType.Type, "bool")] bool isCubeMap, [NativeName(NativeNameType.Param, "result")] [NativeName(NativeNameType.Type, "ScratchImageT *")] ref ScratchImage result, [NativeName(NativeNameType.Param, "beforeState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int beforeState, [NativeName(NativeNameType.Param, "afterState")] [NativeName(NativeNameType.Type, "D3D12_RESOURCE_STATES")] int afterState)
		{
			fixed (ID3D12CommandQueue* ppCommandQueue = &pCommandQueue)
			{
				fixed (ID3D12Resource* ppSource = &pSource)
				{
					fixed (ScratchImage* presult = &result)
					{
						HResult ret = CaptureTextureD3D12Native((ID3D12CommandQueue*)ppCommandQueue, (ID3D12Resource*)ppSource, isCubeMap ? (byte)1 : (byte)0, (ScratchImage*)presult, beforeState, afterState);
						return ret;
					}
				}
			}
		}

	}
}
