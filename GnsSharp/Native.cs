// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

#if GNS_SHARP_OPENSOURCE_GNS // Open-source GNS exclusive API

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GameNetworkingSockets_Init(in SteamNetworkingIdentity identity, ref SteamNetworkingErrMsg errMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void GameNetworkingSockets_Kill();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingSockets_v009();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_ToString(in SteamNetworkingIdentity self, Span<byte> buf, SizeT cbBuf);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_ParseString(ref SteamNetworkingIdentity self, SizeT sizeofIdentity, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszStr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_ToString(in SteamNetworkingIPAddr self, Span<byte> buf, SizeT cbBuf, [MarshalAs(UnmanagedType.I1)] bool bWithPort);

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

#elif GNS_SHARP_STEAMWORKS_SDK // Steamworks SDK exclusive API

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingSockets_SteamAPI_v012();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamGameServerNetworkingSockets_SteamAPI_v012();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIdentity_ToString(ref SteamNetworkingIdentity self, Span<byte> buf, uint cbBuf);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_ParseString(ref SteamNetworkingIdentity self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszStr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_ToString(ref SteamNetworkingIPAddr self, Span<byte> buf, uint cbBuf, [MarshalAs(UnmanagedType.I1)] bool bWithPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingFakeIPType SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_IsFakeIP(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingSockets_ResetIdentity(IntPtr self, in SteamNetworkingIdentity pIdentity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_BeginAsyncRequestFakeIP(IntPtr self, int nNumPorts);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingSockets_GetFakeIP(IntPtr self, int idxFirstPort, ref SteamNetworkingFakeIPResult_t pInfo);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamListenSocket SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(IntPtr self, int idxFakePort, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetRemoteFakeIPForConnection(IntPtr self, HSteamNetConnection hConn, out SteamNetworkingIPAddr pOutAddr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamNetworkingSockets_CreateFakeUDPPort(IntPtr self, int idxFakeServerPort);

#endif // Common API

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
    public static partial bool SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref SteamNetworkingIdentity self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszString);

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
    public static partial bool SteamAPI_SteamNetworkingIdentity_SetGenericString(ref SteamNetworkingIdentity self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszString);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericString(ref SteamNetworkingIdentity self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref SteamNetworkingIdentity self, ReadOnlySpan<byte> data, uint cbLen);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericBytes(ref SteamNetworkingIdentity self, ref int cbLen);

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
    public static partial void SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref SteamNetworkingIPAddr self, ReadOnlySpan<byte> ipv6, ushort nPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref SteamNetworkingIPAddr self, uint nIP, ushort nPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref SteamNetworkingIPAddr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref SteamNetworkingIPAddr self, ushort nPort);

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

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_SteamNetworkingMessage_t_Release(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamListenSocket SteamAPI_ISteamNetworkingSockets_CreateListenSocketIP(IntPtr self, in SteamNetworkingIPAddr localAddress, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamNetConnection SteamAPI_ISteamNetworkingSockets_ConnectByIPAddress(IntPtr self, in SteamNetworkingIPAddr address, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamListenSocket SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2P(IntPtr self, int nLocalVirtualPort, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamNetConnection SteamAPI_ISteamNetworkingSockets_ConnectP2P(IntPtr self, in SteamNetworkingIdentity identityRemote, int nRemoteVirtualPort, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_AcceptConnection(IntPtr self, HSteamNetConnection hConn);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_CloseConnection(IntPtr self, HSteamNetConnection hPeer, int nReason, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszDebug, [MarshalAs(UnmanagedType.I1)] bool bEnableLinger);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_CloseListenSocket(IntPtr self, HSteamListenSocket hSocket);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_SetConnectionUserData(IntPtr self, HSteamNetConnection hPeer, long nUserData);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial long SteamAPI_ISteamNetworkingSockets_GetConnectionUserData(IntPtr self, HSteamNetConnection hPeer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingSockets_SetConnectionName(IntPtr self, HSteamNetConnection hPeer, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszName);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetConnectionName(IntPtr self, HSteamNetConnection hPeer, Span<byte> pszName, int nMaxLen);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_SendMessageToConnection(IntPtr self, HSteamNetConnection hConn, ReadOnlySpan<byte> pData, uint cbData, ESteamNetworkingSendType nSendFlags, out long pOutMessageNumber);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingSockets_SendMessages(IntPtr self, int nMessages, ReadOnlySpan<IntPtr> pMessages, Span<long> pOutMessageNumberOrResult);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_FlushMessagesOnConnection(IntPtr self, HSteamNetConnection hConn);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnConnection(IntPtr self, HSteamNetConnection hConn, Span<IntPtr> ppOutMessages, int nMaxMessages);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetConnectionInfo(IntPtr self, HSteamNetConnection hConn, ref SteamNetConnectionInfo_t pInfo);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetConnectionRealTimeStatus(IntPtr self, HSteamNetConnection hConn, ref SteamNetConnectionRealTimeStatus_t pStatus, int nLanes, Span<SteamNetConnectionRealTimeLaneStatus_t> pLanes);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingSockets_GetDetailedConnectionStatus(IntPtr self, HSteamNetConnection hConn, Span<byte> pszBuf, int cbBuf);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetListenSocketAddress(IntPtr self, HSteamListenSocket hSocket, ref SteamNetworkingIPAddr address);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_CreateSocketPair(IntPtr self, out HSteamNetConnection pOutConnection1, out HSteamNetConnection pOutConnection2, [MarshalAs(UnmanagedType.I1)] bool bUseNetworkLoopback, in SteamNetworkingIdentity pIdentity1, in SteamNetworkingIdentity pIdentity2);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_ConfigureConnectionLanes(IntPtr self, HSteamNetConnection hConn, int nNumLanes, ReadOnlySpan<int> pLanePriorities, ReadOnlySpan<ushort> pLaneWeights);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetIdentity(IntPtr self, ref SteamNetworkingIdentity pIdentity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingAvailability SteamAPI_ISteamNetworkingSockets_InitAuthentication(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingAvailability SteamAPI_ISteamNetworkingSockets_GetAuthenticationStatus(IntPtr self, ref SteamNetAuthenticationStatus_t pDetails);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamNetPollGroup SteamAPI_ISteamNetworkingSockets_CreatePollGroup(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_DestroyPollGroup(IntPtr self, HSteamNetPollGroup hPollGroup);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_SetConnectionPollGroup(IntPtr self, HSteamNetConnection hConn, HSteamNetPollGroup hPollGroup);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(IntPtr self, HSteamNetPollGroup hPollGroup, Span<IntPtr> ppOutMessages, int nMaxMessages);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_ReceivedRelayAuthTicket(IntPtr self, ReadOnlySpan<byte> pvTicket, int cbTicket, out SteamDatagramRelayAuthTicket pOutParsedTicket);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingSockets_FindRelayAuthTicketForServer(IntPtr self, in SteamNetworkingIdentity identityGameServer, int nRemoteVirtualPort, out SteamDatagramRelayAuthTicket pOutParsedTicket);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamNetConnection SteamAPI_ISteamNetworkingSockets_ConnectToHostedDedicatedServer(IntPtr self, in SteamNetworkingIdentity identityTarget, int nRemoteVirtualPort, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ushort SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPort(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamNetworkingPOPID SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerAddress(IntPtr self, ref SteamDatagramHostedAddress pRouting);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamListenSocket SteamAPI_ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(IntPtr self, int nLocalVirtualPort, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetGameCoordinatorServerLogin(IntPtr self, ref SteamDatagramGameCoordinatorServerLogin pLoginInfo, ref int pcbSignedBlob, Span<byte> pBlob);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamNetConnection SteamAPI_ISteamNetworkingSockets_ConnectP2PCustomSignaling(IntPtr self, IntPtr pSignaling, in SteamNetworkingIdentity pPeerIdentity, int nRemoteVirtualPort, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_ReceivedP2PCustomSignal(IntPtr self, ReadOnlySpan<byte> pMsg, int cbMsg, IntPtr pContext);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetCertificateRequest(IntPtr self, ref int pcbBlob, Span<byte> pBlob, ref SteamNetworkingErrMsg errMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_SetCertificate(IntPtr self, ReadOnlySpan<byte> pCertificate, int cbCertificate, ref SteamNetworkingErrMsg errMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingSockets_RunCallbacks(IntPtr self);
}
