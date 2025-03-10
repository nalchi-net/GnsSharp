// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SizeT = System.UIntPtr;

/// <summary>
/// Native methods to P/Invoke.
/// </summary>
internal static partial class Native
{
    /// <summary>
    /// Platform dependent struct pack size.
    /// </summary>
#if GNS_SHARP_PLATFORM_POSIX
    public const int PackSize = 4;
#elif GNS_SHARP_PLATFORM_WIN64 || GNS_SHARP_PLATFORM_WIN32
    public const int PackSize = 8;
#else
#error "Unknown struct pack size. Define `GNS_SHARP_PLATFORM_*` according to your platform."
#endif

    /// <summary>
    /// Native library name.
    /// </summary>
#if GNS_SHARP_OPENSOURCE_GNS // Open-source version of GameNetworkingSockets

#if GNS_SHARP_PLATFORM_WIN64 || GNS_SHARP_PLATFORM_WIN32
    public const string GnsLibraryName = "GameNetworkingSockets";
#elif GNS_SHARP_PLATFORM_POSIX
    public const string GnsLibraryName = "libGameNetworkingSockets";
#endif

#elif GNS_SHARP_STEAMWORKS_SDK // Steamworks SDK

#if GNS_SHARP_PLATFORM_WIN64
    public const string GnsLibraryName = "steam_api64";
#elif GNS_SHARP_PLATFORM_WIN32
    public const string GnsLibraryName = "steam_api";
#elif GNS_SHARP_PLATFORM_POSIX
    public const string GnsLibraryName = "libsteam_api";
#else
#error "Unknown native library name. Define `GNS_SHARP_PLATFORM_*` according to your platform."
#endif

#else // What? No open-source GNS nor Steamworks SDK?

#error "Unknown underlying GNS library. Define `GNS_SHARP_OPENSOURCE_GNS` if you want to use open source GameNetworkingSockets, or define `GNS_SHARP_STEAMWORKS_SDK` if you want to use Steamworks SDK version of it."

#endif

#if GNS_SHARP_OPENSOURCE_GNS

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GameNetworkingSockets_Init(IntPtr identity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void GameNetworkingSockets_Kill();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_ToString(in SteamNetworkingIdentity self, Span<byte> buf, SizeT len);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_ParseString(ref SteamNetworkingIdentity self, SizeT sizeofIdentity, [MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_ToString(in SteamNetworkingIPAddr self, Span<byte> buf, SizeT len, [MarshalAs(UnmanagedType.I1)] bool withPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamNetworkingIPAddr_ToString(in SteamNetworkingIPAddr pAddr, Span<byte> buf, SizeT cbBuf, [MarshalAs(UnmanagedType.I1)] bool bWithPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamNetworkingIPAddr_ParseString(ref SteamNetworkingIPAddr pAddr, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszStr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingFakeIPType SteamNetworkingIPAddr_GetFakeIPType(in SteamNetworkingIPAddr pAddr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamNetworkingIdentity_ToString(in SteamNetworkingIdentity pIdentity, Span<byte> buf, SizeT cbBuf);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamNetworkingIdentity_ParseString(ref SteamNetworkingIdentity pIdentity, SizeT sizeofIdentity, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszStr);

#elif GNS_SHARP_STEAMWORKS_SDK

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_ToString(ref SteamNetworkingIdentity self, Span<byte> buf, uint len);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_ParseString(ref SteamNetworkingIdentity self, [MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_ToString(ref SteamNetworkingIPAddr self, Span<byte> buf, uint len, [MarshalAs(UnmanagedType.I1)] bool withPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingFakeIPType SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_IsFakeIP(ref SteamNetworkingIPAddr self);

#endif

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_Clear(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_IsInvalid(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_SetSteamID(ref SteamNetworkingIdentity self, ulong steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ulong SteamAPI_SteamNetworkingIdentity_GetSteamID(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref SteamNetworkingIdentity self, ulong steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ulong SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref SteamNetworkingIdentity self, [MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref SteamNetworkingIdentity self, in SteamNetworkingIPAddr addr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingIdentity_GetIPAddr(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_SetGenericString(ref SteamNetworkingIdentity self, [MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericString(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref SteamNetworkingIdentity self, ReadOnlySpan<byte> data, uint len);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericBytes(ref SteamNetworkingIdentity self, ref int len);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_IsEqualTo(in SteamNetworkingIdentity self, in SteamNetworkingIdentity x);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_Clear(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref SteamNetworkingIPAddr self, ReadOnlySpan<byte> ipv6, ushort port);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref SteamNetworkingIPAddr self, uint ip, ushort port);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref SteamNetworkingIPAddr self, ushort port);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_ParseString(ref SteamNetworkingIPAddr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszStr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_IsEqualTo(in SteamNetworkingIPAddr self, in SteamNetworkingIPAddr x);
}
