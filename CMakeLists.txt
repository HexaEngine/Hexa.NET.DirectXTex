# Copyright (c) Microsoft Corporation.
# Licensed under the MIT License.

cmake_minimum_required(VERSION 3.20)
if(MSVC)
    set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} /MD")
    set(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} /MDd")
else()
    # Add any Linux-specific flags here if necessary
endif()

set(DIRECTXTEX_VERSION 2.0.4)

project(DirectXTex
  VERSION ${DIRECTXTEX_VERSION}
  DESCRIPTION "DirectX Texture Library"
  HOMEPAGE_URL "https://go.microsoft.com/fwlink/?LinkId=248926"
  LANGUAGES CXX C)

# Options
option(BUILD_DX11 "Build with DirectX11 Runtime support (requires fxc.exe)" ON)
option(BUILD_DX12 "Build with DirectX12 Runtime support" ON)
option(BC_USE_OPENMP "Build with OpenMP support" ON)
option(ENABLE_SPECTRE_MITIGATION "Build using /Qspectre for MSVC" OFF)
option(DISABLE_MSVC_ITERATOR_DEBUGGING "Disable iterator debugging in Debug configurations with the MSVC CRT" OFF)
option(ENABLE_CODE_ANALYSIS "Use Static Code Analysis on build" OFF)
option(USE_PREBUILT_SHADERS "Use externally built HLSL shaders" OFF)
option(NO_WCHAR_T "Use legacy wide-character as unsigned short" OFF)
option(ENABLE_OPENEXR_SUPPORT "Build with OpenEXR support" ON)
option(ENABLE_LIBJPEG_SUPPORT "Build with libjpeg support" ON)
option(ENABLE_LIBPNG_SUPPORT "Build with libpng support" ON)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/lib")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/lib")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin")

include(GNUInstallDirs)

#--- Library
set(LIBRARY_HEADERS
    DirectXTex/DirectXTex/DirectXTex.h
    DirectXTex/DirectXTex/DirectXTex.inl)

set(LIBRARY_SOURCES
    DirectXTex/DirectXTex/BC.h
    DirectXTex/DirectXTex/DDS.h
    DirectXTex/DirectXTex/DirectXTexP.h
    DirectXTex/DirectXTex/filters.h
    DirectXTex/DirectXTex/scoped.h
    DirectXTex/DirectXTex/BC.cpp
    DirectXTex/DirectXTex/BC4BC5.cpp
    DirectXTex/DirectXTex/BC6HBC7.cpp
    DirectXTex/DirectXTex/DirectXTexCompress.cpp
    DirectXTex/DirectXTex/DirectXTexConvert.cpp
    DirectXTex/DirectXTex/DirectXTexDDS.cpp
    DirectXTex/DirectXTex/DirectXTexHDR.cpp
    DirectXTex/DirectXTex/DirectXTexImage.cpp
    DirectXTex/DirectXTex/DirectXTexMipmaps.cpp
    DirectXTex/DirectXTex/DirectXTexMisc.cpp
    DirectXTex/DirectXTex/DirectXTexNormalMaps.cpp
    DirectXTex/DirectXTex/DirectXTexPMAlpha.cpp
    DirectXTex/DirectXTex/DirectXTexResize.cpp
    DirectXTex/DirectXTex/DirectXTexTGA.cpp
    DirectXTex/DirectXTex/DirectXTexUtil.cpp)



if(WIN32)
   list(APPEND LIBRARY_SOURCES
       DirectXTex/DirectXTex/DirectXTexFlipRotate.cpp
       DirectXTex/DirectXTex/DirectXTexWIC.cpp)
endif()

if(BUILD_DX11 AND WIN32)
    list(APPEND LIBRARY_SOURCES
        DirectXTex/DirectXTex/BCDirectCompute.h
        DirectXTex/DirectXTex/BCDirectCompute.cpp
        DirectXTex/DirectXTex/DirectXTexCompressGPU.cpp
        DirectXTex/DirectXTex/DirectXTexD3D11.cpp)
 endif()

if(BUILD_DX12)
   list(APPEND LIBRARY_SOURCES DirectXTex/DirectXTex/DirectXTexD3D12.cpp)

   if(NOT (DEFINED XBOX_CONSOLE_TARGET))
       list(APPEND LIBRARY_SOURCES DirectXTex/DirectXTex/d3dx12.h)
   endif()
endif()

if(ENABLE_OPENEXR_SUPPORT)
   list(APPEND LIBRARY_HEADERS DirectXTex/Auxiliary/DirectXTexEXR.h)
   list(APPEND LIBRARY_SOURCES DirectXTex/Auxiliary/DirectXTexEXR.cpp)
endif()

if(ENABLE_LIBJPEG_SUPPORT)
    if(WIN32)
        message(STATUS "Use of the Windows Imaging Component (WIC) instead of libjpeg is recommended.")
    endif()
    list(APPEND LIBRARY_HEADERS DirectXTex/Auxiliary/DirectXTexJPEG.h)
    list(APPEND LIBRARY_SOURCES DirectXTex/Auxiliary/DirectXTexJPEG.cpp)
endif()

if(ENABLE_LIBPNG_SUPPORT)
    if(WIN32)
        message(STATUS "Use of the Windows Imaging Component (WIC) instead of libpng is recommended.")
    endif()
    list(APPEND LIBRARY_HEADERS DirectXTex/Auxiliary/DirectXTexPNG.h)
    list(APPEND LIBRARY_SOURCES DirectXTex/Auxiliary/DirectXTexPNG.cpp)
endif()

# Shader sources
set(SHADER_SOURCES
    DirectXTex/DirectXTex/Shaders/BC6HEncode.hlsl
    DirectXTex/DirectXTex/Shaders/BC7Encode.hlsl)

if(BUILD_DX11 AND WIN32)
    if(NOT COMPILED_SHADERS)
        if(USE_PREBUILT_SHADERS)
            message(FATAL_ERROR "ERROR: Using prebuilt shaders requires the COMPILED_SHADERS variable is set")
        endif()
        set(COMPILED_SHADERS ${CMAKE_CURRENT_BINARY_DIR}/Shaders/Compiled)
        file(MAKE_DIRECTORY ${COMPILED_SHADERS})
    else()
        file(TO_CMAKE_PATH ${COMPILED_SHADERS} COMPILED_SHADERS)
    endif()

    list(APPEND LIBRARY_SOURCES ${COMPILED_SHADERS}/BC6HEncode_EncodeBlockCS.inc)

    if(NOT USE_PREBUILT_SHADERS)
        add_custom_command(
            OUTPUT "${COMPILED_SHADERS}/BC6HEncode_EncodeBlockCS.inc"
            MAIN_DEPENDENCY "${PROJECT_SOURCE_DIR}/DirectXTex/DirectXTex/Shaders/CompileShaders.cmd"
            DEPENDS ${SHADER_SOURCES}
            COMMENT "Generating HLSL shaders..."
            COMMAND ${CMAKE_COMMAND} -E env CompileShadersOutput="${COMPILED_SHADERS}" CompileShaders.cmd > "${COMPILED_SHADERS}/compileshaders.log"
            WORKING_DIRECTORY "${PROJECT_SOURCE_DIR}/DirectXTex/DirectXTex/Shaders"
            USES_TERMINAL)
    endif()
endif()

# Wrapper
set(WRAPPER_HEADERS DirectXTex.h)
set(WRAPPER_SOURCES DirectXTex.cpp)

add_library(${PROJECT_NAME} SHARED ${LIBRARY_SOURCES} ${LIBRARY_HEADERS} ${WRAPPER_SOURCES} ${WRAPPER_HEADERS})

if(BUILD_DX11 AND WIN32)
   target_include_directories(${PROJECT_NAME} PRIVATE ${COMPILED_SHADERS})
endif()

target_include_directories(${PROJECT_NAME} PUBLIC
  $<BUILD_INTERFACE:${CMAKE_CURRENT_SOURCE_DIR}/DirectXTex/DirectXTex>
  $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>
  ${COMPILED_SHADERS_DIR})

target_compile_features(${PROJECT_NAME} PUBLIC cxx_std_17)

if(BC_USE_OPENMP)
  find_package(OpenMP)
  if(OpenMP_CXX_FOUND)
    target_link_libraries(${PROJECT_NAME} PUBLIC OpenMP::OpenMP_CXX)
  else()
    set(BC_USE_OPENMP OFF)
  endif()
endif()

if(ENABLE_OPENEXR_SUPPORT)
  find_package(OpenEXR REQUIRED)
  target_link_libraries(${PROJECT_NAME} PUBLIC OpenEXR::OpenEXR)
endif()

if(ENABLE_LIBJPEG_SUPPORT)
  find_package(JPEG REQUIRED)
  target_link_libraries(${PROJECT_NAME} PUBLIC JPEG::JPEG)
endif()

if(ENABLE_LIBPNG_SUPPORT)
  find_package(PNG REQUIRED)
  target_link_libraries(${PROJECT_NAME} PUBLIC PNG::PNG)
endif()

if(NOT MINGW)
    target_precompile_headers(${PROJECT_NAME} PRIVATE DirectXTex/DirectXTex/DirectXTexP.h)
endif()

if(MINGW OR (NOT WIN32))
    find_package(directxmath CONFIG REQUIRED)
    find_package(directx-headers CONFIG REQUIRED)
else()
    find_package(directxmath CONFIG QUIET)
    find_package(directx-headers CONFIG QUIET)
endif()

if(directxmath_FOUND)
    message(STATUS "Using DirectXMath package")
    target_link_libraries(${PROJECT_NAME} PUBLIC Microsoft::DirectXMath)
endif()

if(directx-headers_FOUND)
    message(STATUS "Using DirectX-Headers package")
    target_link_libraries(${PROJECT_NAME} PUBLIC Microsoft::DirectX-Headers)
    target_compile_definitions(${PROJECT_NAME} PUBLIC USING_DIRECTX_HEADERS)
endif()

# Compiler and linker settings
if(MSVC)
    target_compile_options(${PROJECT_NAME} PRIVATE /Wall /GR- /Zc:__cplusplus /MT)
    if(NO_WCHAR_T)
      message(STATUS "Using non-native wchar_t as unsigned short")
      target_compile_options(${PROJECT_NAME} PRIVATE "/Zc:wchar_t-")
    endif()
    if(ENABLE_CODE_ANALYSIS)
      target_compile_options(${PROJECT_NAME} PRIVATE /analyze /WX)
    endif()
    if(ENABLE_SPECTRE_MITIGATION
       AND (CMAKE_CXX_COMPILER_VERSION VERSION_GREATER_EQUAL 19.13))
        message(STATUS "Building Spectre-mitigated libraries")
        target_compile_options(${PROJECT_NAME} PRIVATE "/Qspectre")
    endif()
endif()

if(WIN32)
    set(WINVER 0x0A00)
    target_compile_definitions(${PROJECT_NAME} PRIVATE _WIN32_WINNT=${WINVER})
    if(DISABLE_MSVC_ITERATOR_DEBUGGING)
      target_compile_definitions(${PROJECT_NAME} PRIVATE _ITERATOR_DEBUG_LEVEL=0)
    endif()
endif()