namespace Hexa.NET.DirectXTex
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

#if !NET5_0_OR_GREATER
    using HexaGen.Runtime;
#endif

    public static class LibraryLoader
    {
        public static nint LoadLibrary()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return LoadLocalLibrary("DirectXTex");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return LoadLocalLibrary("libDirectXTex");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return LoadLocalLibrary("libDirectXTex");
            }
            else
            {
                return LoadLocalLibrary("libDirectXTex");
            }
        }

        public static string GetExtension()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return ".dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return ".dylib";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return ".so";
            }

            return ".so";
        }

        private static nint DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return LoadLocalLibrary("DirectXTex");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return LoadLocalLibrary("libDirectXTex");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return LoadLocalLibrary("libDirectXTex");
            }
            else
            {
                return LoadLocalLibrary("libDirectXTex");
            }
        }

        public static nint LoadLocalLibrary(string libraryName)
        {
            var extension = GetExtension();

            if (!libraryName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
            {
                libraryName += extension;
            }

            var osPlatform = GetOSPlatform();
            var architecture = GetArchitecture();

            var libraryPath = GetNativeAssemblyPath(osPlatform, architecture, libraryName);

            static string GetOSPlatform()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return "win";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return "linux";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "osx";
                }

                throw new ArgumentException("Unsupported OS platform.");
            }

            static string GetArchitecture()
            {
                switch (RuntimeInformation.ProcessArchitecture)
                {
                    case Architecture.X86: return "x86";
                    case Architecture.X64: return "x64";
                    case Architecture.Arm: return "arm";
                    case Architecture.Arm64: return "arm64";
                }

                throw new ArgumentException("Unsupported architecture.");
            }

            static string GetNativeAssemblyPath(string osPlatform, string architecture, string libraryName)
            {
                var assemblyLocation = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

                if (assemblyLocation == null)
                {
                    throw new Exception();
                }

                var paths = new[]
                {
                    Path.Combine(assemblyLocation, libraryName),
                    Path.Combine(assemblyLocation, "runtimes", osPlatform, "native", libraryName),
                    Path.Combine(assemblyLocation, "runtimes", $"{osPlatform}-{architecture}", "native", libraryName),
                };

                foreach (var path in paths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                return libraryName;
            }

            IntPtr handle;

            handle = NativeLibrary.Load(libraryPath);

            if (handle == IntPtr.Zero)
            {
                throw new DllNotFoundException($"Unable to load library '{libraryName}'.");
            }

            return handle;
        }
    }
}