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
	[NativeName(NativeNameType.Typedef, "ScratchImage")]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly partial struct ScratchImage : IEquatable<ScratchImage>
	{
		public ScratchImage(nint handle) { Handle = handle; }
		public nint Handle { get; }
		public bool IsNull => Handle == 0;
		public static ScratchImage Null => new ScratchImage(0);
		public static implicit operator ScratchImage(nint handle) => new ScratchImage(handle);
		public static bool operator ==(ScratchImage left, ScratchImage right) => left.Handle == right.Handle;
		public static bool operator !=(ScratchImage left, ScratchImage right) => left.Handle != right.Handle;
		public static bool operator ==(ScratchImage left, nint right) => left.Handle == right;
		public static bool operator !=(ScratchImage left, nint right) => left.Handle != right;
		public bool Equals(ScratchImage other) => Handle == other.Handle;
		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is ScratchImage handle && Equals(handle);
		/// <inheritdoc/>
		public override int GetHashCode() => Handle.GetHashCode();
		private string DebuggerDisplay => string.Format("ScratchImage [0x{0}]", Handle.ToString("X"));
	}

	/// <summary>
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.Typedef, "Blob")]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly partial struct Blob : IEquatable<Blob>
	{
		public Blob(nint handle) { Handle = handle; }
		public nint Handle { get; }
		public bool IsNull => Handle == 0;
		public static Blob Null => new Blob(0);
		public static implicit operator Blob(nint handle) => new Blob(handle);
		public static bool operator ==(Blob left, Blob right) => left.Handle == right.Handle;
		public static bool operator !=(Blob left, Blob right) => left.Handle != right.Handle;
		public static bool operator ==(Blob left, nint right) => left.Handle == right;
		public static bool operator !=(Blob left, nint right) => left.Handle != right;
		public bool Equals(Blob other) => Handle == other.Handle;
		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is Blob handle && Equals(handle);
		/// <inheritdoc/>
		public override int GetHashCode() => Handle.GetHashCode();
		private string DebuggerDisplay => string.Format("Blob [0x{0}]", Handle.ToString("X"));
	}

}