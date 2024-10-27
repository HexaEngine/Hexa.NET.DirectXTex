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
	/// Subset here matches D3D10_RESOURCE_MISC_FLAG and D3D11_RESOURCE_MISC_FLAG<br/>
	/// </summary>
	[NativeName(NativeNameType.Enum, "TEX_MISC_FLAG")]
	[Flags]
	public enum TexMiscFlag : int
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.EnumItem, "TEX_MISC_TEXTURECUBE")]
		[NativeName(NativeNameType.Value, "0x4L")]
		Texturecube = unchecked((int)0x4L),
	}
}
