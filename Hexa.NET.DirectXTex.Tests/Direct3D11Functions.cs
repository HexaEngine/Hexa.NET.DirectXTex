﻿namespace Hexa.NET.DirectXTex.Tests
{
    using Hexa.NET.D3D11;
    using Hexa.NET.D3DCommon;
    using Hexa.NET.DXGI;
    using HexaGen.Runtime.COM;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [Platform("Win")]
    public unsafe class Direct3D11Functions : IDisposable
    {
        private ComPtr<IDXGIFactory2> IDXGIFactory;
        private ComPtr<IDXGIAdapter1> IDXGIAdapter;

        private readonly ComPtr<ID3D11Device> Device;
        private readonly ComPtr<ID3D11DeviceContext> DeviceContext;

        private readonly ComPtr<ID3D11Debug> DebugDevice;

        public Direct3D11Functions()
        {
            DXGI.CreateDXGIFactory2(0, out IDXGIFactory);

            IDXGIAdapter = GetHardwareAdapter();

            FeatureLevel[] levelsArr = new FeatureLevel[]
            {
                FeatureLevel.Level111,
                FeatureLevel.Level110
            };

            CreateDeviceFlag flags = CreateDeviceFlag.BgraSupport;

#if DEBUG
            flags |= CreateDeviceFlag.Debug;
#endif

            ID3D11Device* tempDevice;
            ID3D11DeviceContext* tempContext;

            FeatureLevel level = 0;
            FeatureLevel* levels = (FeatureLevel*)Unsafe.AsPointer(ref levelsArr[0]);

            D3D11.CreateDevice((IDXGIAdapter*)IDXGIAdapter.Handle, DriverType.Unknown, nint.Zero, (uint)flags, levels, (uint)levelsArr.Length, D3D11.D3D11_SDK_VERSION, &tempDevice, &level, &tempContext).ThrowIf();

            tempDevice->QueryInterface(out Device);
            tempContext->QueryInterface(out DeviceContext);

            tempDevice->Release();
            tempContext->Release();

#if DEBUG
            Device.QueryInterface(out DebugDevice);
#endif
        }

        private ComPtr<IDXGIAdapter1> GetHardwareAdapter()
        {
            ComPtr<IDXGIAdapter1> adapter = null;
            ComPtr<IDXGIFactory6> factory6;
            IDXGIFactory.QueryInterface(out factory6);

            if (factory6.Handle != null)
            {
                for (uint adapterIndex = 0;
                    (ResultCode)factory6.EnumAdapterByGpuPreference(adapterIndex, GpuPreference.HighPerformance, out adapter).Value !=
                    ResultCode.DXGI_ERROR_NOT_FOUND;
                    adapterIndex++)
                {
                    AdapterDesc1 desc;
                    adapter.GetDesc1(&desc);
                    if (((AdapterFlag)desc.Flags & AdapterFlag.Software) != AdapterFlag.None)
                    {
                        // Don't select the Basic Render Driver adapter.
                        adapter.Release();
                        continue;
                    }

                    return adapter;
                }

                factory6.Release();
            }

            if (adapter.Handle == null)
            {
                for (uint adapterIndex = 0;
                    (ResultCode)IDXGIFactory.EnumAdapters1(adapterIndex, &adapter.Handle).Value != ResultCode.DXGI_ERROR_NOT_FOUND;
                    adapterIndex++)
                {
                    AdapterDesc1 desc;
                    adapter.GetDesc1(&desc);
                    string name = new(&desc.Description_0);

                    Trace.WriteLine($"Found Adapter {name}");

                    if (((AdapterFlag)desc.Flags & AdapterFlag.Software) != AdapterFlag.None)
                    {
                        // Don't select the Basic Render Driver adapter.
                        adapter.Release();
                        continue;
                    }

                    return adapter;
                }
            }

            return adapter;
        }

        [Test]
        public void IsSupportedTexture()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.R8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };
            Assert.That(DirectXTex.IsSupportedTexture((NET.DirectXTex.ID3D11Device*)Device.Handle, ref metadata), Is.True);
        }

        [Test]
        public void CreateTexture()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.R8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };

            ScratchImage image = DirectXTex.CreateScratchImage();
            DirectXTex.Initialize(image, ref metadata, CPFlags.None);
            ID3D11Resource* resource;
            DirectXTex.CreateTexture((NET.DirectXTex.ID3D11Device*)Device.Handle, image.GetImages(), image.GetImageCount(), ref metadata, (NET.DirectXTex.ID3D11Resource**)&resource);
            if (resource == null)
                Assert.Fail("Fail");
            resource->Release();
        }

        [Test]
        public void CreateShaderResourceView()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.R8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };
            ScratchImage image = DirectXTex.CreateScratchImage();
            DirectXTex.Initialize(image, ref metadata, CPFlags.None);
            ID3D11ShaderResourceView* srv;
            DirectXTex.CreateShaderResourceView((NET.DirectXTex.ID3D11Device*)Device.Handle, image.GetImages(), image.GetImageCount(), ref metadata, (NET.DirectXTex.ID3D11ShaderResourceView**)&srv);
            if (srv == null)
                Assert.Fail("Fail");
            srv->Release();
        }

        [Test]
        public void CreateTextureEx()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.R8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };
            ScratchImage image = DirectXTex.CreateScratchImage();
            DirectXTex.Initialize(image, ref metadata, CPFlags.None);
            ID3D11Resource* resource;
            DirectXTex.CreateTextureEx((NET.DirectXTex.ID3D11Device*)Device.Handle, image.GetImages(), image.GetImageCount(), ref metadata, (int)Usage.Immutable, (uint)BindFlag.ShaderResource, 0, 0, CreateTexFlags.Default, (NET.DirectXTex.ID3D11Resource**)&resource);
            if (resource == null)
                Assert.Fail("Fail");
            resource->Release();
        }

        [Test]
        public void CreateShaderResourceViewEx()
        {
            TexMetadata metadata = new()
            {
                ArraySize = 1,
                Depth = 1,
                Dimension = TexDimension.Texture2D,
                Format = (int)Format.R8G8B8A8Unorm,
                Height = 64,
                Width = 64,
                MipLevels = 4,
                MiscFlags = 0,
                MiscFlags2 = 0,
            };
            ScratchImage image = DirectXTex.CreateScratchImage();
            DirectXTex.Initialize(image, ref metadata, CPFlags.None);
            ID3D11ShaderResourceView* srv;
            DirectXTex.CreateShaderResourceViewEx((NET.DirectXTex.ID3D11Device*)Device.Handle, image.GetImages(), image.GetImageCount(), ref metadata, (int)Usage.Immutable, (uint)BindFlag.ShaderResource, 0, 0, CreateTexFlags.Default, (NET.DirectXTex.ID3D11ShaderResourceView**)&srv);
            if (srv == null)
                Assert.Fail("Fail");
            srv->Release();
        }

        [Test]
        public void CaptureTexture()
        {
            ID3D11Resource* resource;
            Texture2DDesc desc = new(64, 64, 1, 1, Format.R8G8B8A8Unorm, new(1, 0), Usage.Default, 8, 0, 0);
            Device.CreateTexture2D(&desc, (SubresourceData*)null, (ID3D11Texture2D**)&resource);

            ScratchImage image = DirectXTex.CreateScratchImage();
            DirectXTex.CaptureTexture((NET.DirectXTex.ID3D11Device*)Device.Handle, (NET.DirectXTex.ID3D11DeviceContext*)DeviceContext.Handle, (NET.DirectXTex.ID3D11Resource*)resource, ref image);

            resource->Release();

            TexMetadata metadata = DirectXTex.GetMetadata(image);

            Assert.That(metadata.Height, Is.EqualTo((nuint)desc.Height));
            Assert.That(metadata.Width, Is.EqualTo((nuint)desc.Width));
            Assert.That(metadata.ArraySize, Is.EqualTo((nuint)desc.ArraySize));
            Assert.That((Format)metadata.Format, Is.EqualTo(desc.Format));
            Assert.That(metadata.MiscFlags, Is.EqualTo(desc.MiscFlags));
            Assert.That(metadata.MipLevels, Is.EqualTo((nuint)desc.MipLevels));

            image.Release();
        }

        public void Dispose()
        {
            DeviceContext.Dispose();
            Device.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}