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
using System.Numerics;

namespace Hexa.NET.DirectXTex
{
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
		public nuint Width;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "height")]
		[NativeName(NativeNameType.Type, "size_t")]
		public nuint Height;

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
		public nuint RowPitch;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "slicePitch")]
		[NativeName(NativeNameType.Type, "size_t")]
		public nuint SlicePitch;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "pixels")]
		[NativeName(NativeNameType.Type, "uint8_t *")]
		public unsafe byte* Pixels;


		/// <summary>
		/// To be documented.
		/// </summary>
		public unsafe Image(nuint width = default, nuint height = default, int format = default, nuint rowPitch = default, nuint slicePitch = default, byte* pixels = default)
		{
			Width = width;
			Height = height;
			Format = format;
			RowPitch = rowPitch;
			SlicePitch = slicePitch;
			Pixels = pixels;
		}


	}

}
