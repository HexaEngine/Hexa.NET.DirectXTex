# Hexa.NET.DirectXTex

Hexa.NET.DirectXTex is a thin .NET wrapper for the DirectXTex library, providing developers with streamlined access to DirectX texture processing functionalities within .NET applications.

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

## Introduction

DirectXTex is a comprehensive library for handling texture processing in DirectX applications. Hexa.NET.DirectXTex aims to bridge the gap between .NET developers and the powerful features of DirectXTex by providing a straightforward and user-friendly thin wrapper.

## Features

- Load and save a variety of texture formats.
- Support for texture compression and decompression.
- Mipmap generation and management.
- Format conversion and resizing.
- Powerful image processing capabilities.
- Supporting .NET 8.0 and .netstandard 2.0/2.1

## Installation

You can install Hexa.NET.DirectXTex via NuGet Package Manager:

```sh
Install-Package Hexa.NET.DirectXTex
```
Or add it to your .csproj file:
```xml
<PackageReference Include="Hexa.NET.DirectXTex" Version="2.0.0" />
```
A standalone version is also available (without depending on Silk)
```xml
<PackageReference Include="Hexa.NET.DirectXTex.Standalone" Version="2.0.0" />
```

## Usage

Here's a quick example of how to use Hexa.NET.DirectXTex to load a texture, generate mipmaps, and save it in a different format:
```cs
    using Hexa.NET.DirectXTex;

    public class Program
    {
        private static unsafe void Main(string[] args)
        {
            ScratchImage image = DirectXTex.CreateScratchImage();
            TexMetadata metadata = default;

            string inputPath = "assets/textures/test.dds";
            DirectXTex.LoadFromDDSFile(inputPath, DDSFlags.None, ref metadata, image);

            ScratchImage mipChain = DirectXTex.CreateScratchImage();
            int mipLevels = 4;
            DirectXTex.GenerateMipMaps2(image.GetImages(), image.GetImageCount(), ref metadata, TexFilterFlags.Default, (ulong)mipLevels, ref mipChain);
            image.Release();

            string outputPath = "test.dds";
            TexMetadata mipChainMetadata = mipChain.GetMetadata();
            DirectXTex.SaveToDDSFile2(mipChain.GetImages(), mipChain.GetImageCount(), ref mipChainMetadata, DDSFlags.None, outputPath);

            mipChain.Release();
        }
    }
```

## API Documentation

The full API documentation is available [here](https://github.com/microsoft/DirectXTex/wiki/DirectXTex). It provides detailed information on all available methods and classes within Hexa.NET.DirectXTex, as it is a thin wrapper around the DirectXTex library.

## Contributing

We welcome contributions from the community! If you'd like to contribute, please follow these steps:

    Fork the repository.
    Create a new branch with a descriptive name.
    Make your changes and commit them with clear and concise messages.
    Push your changes to your fork.
    Submit a pull request.

Please ensure that your code adheres to the project's coding standards and includes appropriate tests.
## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/HexaEngine/Hexa.NET.DirectXTex/blob/master/LICENSE.txt) file for more information.

## Acknowledgements

    The DirectXTex library: DirectXTex
    The .NET community for their continuous support and contributions.
    
Feel free to modify as needed!
