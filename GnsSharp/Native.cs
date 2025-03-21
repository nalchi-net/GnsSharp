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
#if GNS_SHARP_PLATFORM_POSIX64 || GNS_SHARP_PLATFORM_POSIX32
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
#elif GNS_SHARP_PLATFORM_POSIX64 || GNS_SHARP_PLATFORM_POSIX32
    public const string GnsLibraryName = "libGameNetworkingSockets";
#endif

#elif GNS_SHARP_STEAMWORKS_SDK // Steamworks SDK

#if GNS_SHARP_PLATFORM_WIN64
    public const string GnsLibraryName = "steam_api64";
#elif GNS_SHARP_PLATFORM_WIN32
    public const string GnsLibraryName = "steam_api";
#elif GNS_SHARP_PLATFORM_POSIX64 || GNS_SHARP_PLATFORM_POSIX32
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
    public static partial bool GameNetworkingSockets_Init(in SteamNetworkingIdentity identity, out SteamNetworkingErrMsg errMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void GameNetworkingSockets_Kill();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void SteamNetworkingSockets_SetCustomMemoryAllocator(FPtrCustomMemoryMalloc malloc, FPtrCustomMemoryFree free, FPtrCustomMemoryRealloc realloc);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamNetworkingSockets_SetLockWaitWarningThreshold(SteamNetworkingMicroseconds usecThreshold);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void SteamNetworkingSockets_SetLockAcquiredCallback(FPtrSteamNetworkingSocketsLockWaitedFor callback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamNetworkingSockets_SetLockAcquiredCallback(FSteamNetworkingSocketsLockWaitedFor callback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void SteamNetworkingSockets_SetLockHeldCallback(FPtrSteamNetworkingSocketsLockWaitedFor callback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamNetworkingSockets_SetLockHeldCallback(FSteamNetworkingSocketsLockWaitedFor callback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void SteamNetworkingSockets_SetServiceThreadInitCallback(FPtrSteamNetworkingSocketsServiceThreadInit callback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamNetworkingSockets_SetServiceThreadInitCallback(FSteamNetworkingSocketsServiceThreadInit callback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingSockets_v009();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingUtils_v003();

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
    public static partial ESteamAPIInitResult SteamAPI_InitFlat(out SteamErrMsg pOutErrMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamAPIInitResult SteamInternal_SteamAPI_Init([MarshalAs(UnmanagedType.LPUTF8Str)] string pszInternalCheckInterfaceVersions, out SteamErrMsg pOutErrMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_Shutdown();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_RestartAppIfNecessary(uint unOwnAppID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ReleaseCurrentThreadMemory();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamAPIInitResult SteamInternal_GameServer_Init_V2(uint unIP, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchVersionString, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszInternalCheckInterfaceVersions, out SteamErrMsg pOutErrMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamGameServer_Shutdown();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamGameServer_BSecure();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamGameServer_GetSteamID();

    /// <summary>
    /// Inform the API that you wish to use manual event dispatch.  This must be called after SteamAPI_Init, but before<br/>
    /// you use any of the other manual dispatch functions below.
    /// </summary>
    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ManualDispatch_Init();

    /// <summary>
    /// Perform certain periodic actions that need to be performed.
    /// </summary>
    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ManualDispatch_RunFrame(HSteamPipe hSteamPipe);

    /// <summary>
    /// Fetch the next pending callback on the given pipe, if any.  If a callback is available, true is returned<br/>
    /// and the structure is populated.  In this case, you MUST call SteamAPI_ManualDispatch_FreeLastCallback<br/>
    /// (after dispatching the callback) before calling SteamAPI_ManualDispatch_GetNextCallback again.
    /// </summary>
    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ManualDispatch_GetNextCallback(HSteamPipe hSteamPipe, out CallbackMsg_t pCallbackMsg);

    /// <summary>
    /// You must call this after dispatching the callback, if SteamAPI_ManualDispatch_GetNextCallback returns true.
    /// </summary>
    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ManualDispatch_FreeLastCallback(HSteamPipe hSteamPipe);

    /// <summary>
    /// Return the call result for the specified call on the specified pipe.  You really should<br/>
    /// only call this in a handler for SteamAPICallCompleted_t callback.
    /// </summary>
    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ManualDispatch_GetAPICallResult(HSteamPipe hSteamPipe, SteamAPICall_t hSteamAPICall, Span<byte> pCallback, int cubCallback, int iCallbackExpected, [MarshalAs(UnmanagedType.I1)] out bool pbFailed);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_RunCallbacks();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamPipe SteamAPI_GetHSteamPipe();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamPipe SteamGameServer_GetHSteamPipe();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingSockets_SteamAPI_v012();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamGameServerNetworkingSockets_SteamAPI_v012();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamNetworkingUtils_SteamAPI_v004();

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
    public static partial void SteamAPI_ISteamNetworkingSockets_GetFakeIP(IntPtr self, int idxFirstPort, out SteamNetworkingFakeIPResult_t pInfo);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamListenSocket SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(IntPtr self, int idxFakePort, int nOptions, ReadOnlySpan<SteamNetworkingConfigValue_t> pOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetRemoteFakeIPForConnection(IntPtr self, HSteamNetConnection hConn, out SteamNetworkingIPAddr pOutAddr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamNetworkingSockets_CreateFakeUDPPort(IntPtr self, int idxFakeServerPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_IsFakeIPv4(IntPtr self, uint nIPv4);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingFakeIPType SteamAPI_ISteamNetworkingUtils_GetIPv4FakeIPType(IntPtr self, uint nIPv4);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingUtils_GetRealIdentityForFakeIP(IntPtr self, in SteamNetworkingIPAddr fakeIP, ref SteamNetworkingIdentity pOutRealIdentity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_FakeIPResult(IntPtr self, FnPtrSteamNetworkingFakeIPResult fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_FakeIPResult(IntPtr self, FnSteamNetworkingFakeIPResult fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionRequest(IntPtr self, FnPtrSteamNetworkingMessagesSessionRequest fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionRequest(IntPtr self, FnSteamNetworkingMessagesSessionRequest fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionFailed(IntPtr self, FnPtrSteamNetworkingMessagesSessionFailed fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionFailed(IntPtr self, FnSteamNetworkingMessagesSessionFailed fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(IntPtr self, in SteamNetworkingIPAddr addr, Span<byte> buf, uint cbBuf, [MarshalAs(UnmanagedType.I1)] bool bWithPort);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(IntPtr self, ref SteamNetworkingIPAddr pAddr, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszStr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingFakeIPType SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType(IntPtr self, in SteamNetworkingIPAddr addr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(IntPtr self, in SteamNetworkingIdentity identity, Span<byte> buf, uint cbBuf);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(IntPtr self, ref SteamNetworkingIdentity pIdentity, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszStr);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamUtils_v010();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamGameServerUtils_v010();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamUtils_GetSecondsSinceAppActive(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamUtils_GetSecondsSinceComputerActive(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EUniverse SteamAPI_ISteamUtils_GetConnectedUniverse(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamUtils_GetServerRealTime(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamUtils_GetIPCountry(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_GetImageSize(IntPtr self, int iImage, out uint pnWidth, out uint pnHeight);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_GetImageRGBA(IntPtr self, int iImage, Span<byte> pubDest, int nDestBufferSize);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial byte SteamAPI_ISteamUtils_GetCurrentBatteryPower(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamUtils_GetAppID(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUtils_SetOverlayNotificationPosition(IntPtr self, ENotificationPosition eNotificationPosition);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_IsAPICallCompleted(IntPtr self, SteamAPICall_t hSteamAPICall, [MarshalAs(UnmanagedType.I1)] out bool pbFailed);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamAPICallFailure SteamAPI_ISteamUtils_GetAPICallFailureReason(IntPtr self, SteamAPICall_t hSteamAPICall);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_GetAPICallResult(IntPtr self, SteamAPICall_t hSteamAPICall, Span<byte> pCallback, int cubCallback, int iCallbackExpected, [MarshalAs(UnmanagedType.I1)] out bool pbFailed);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamUtils_GetIPCCallCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUtils_SetWarningMessageHook(IntPtr self, SteamAPIWarningMessageHook_t pFunction);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_IsOverlayEnabled(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_BOverlayNeedsPresent(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamUtils_CheckFileSignature(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string szFileName);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_ShowGamepadTextInput(IntPtr self, EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchDescription, uint unCharMax, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchExistingText);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamUtils_GetEnteredGamepadTextLength(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_GetEnteredGamepadTextInput(IntPtr self, Span<byte> pchText, uint cchText);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamUtils_GetSteamUILanguage(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_IsSteamRunningInVR(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUtils_SetOverlayNotificationInset(IntPtr self, int nHorizontalInset, int nVerticalInset);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_IsSteamInBigPictureMode(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUtils_StartVRDashboard(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled(IntPtr self, [MarshalAs(UnmanagedType.I1)] bool bEnabled);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_IsSteamChinaLauncher(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_InitFilterText(IntPtr self, uint unFilterOptions);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamUtils_FilterText(IntPtr self, ETextFilteringContext eContext, CSteamID sourceSteamID, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchInputMessage, Span<byte> pchOutFilteredText, uint nByteSizeOutFilteredText);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamIPv6ConnectivityState SteamAPI_ISteamUtils_GetIPv6ConnectivityState(IntPtr self, ESteamIPv6ConnectivityProtocol eProtocol);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_IsSteamRunningOnSteamDeck(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_ShowFloatingGamepadTextInput(IntPtr self, EFloatingGamepadTextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUtils_SetGameLauncherMode(IntPtr self, [MarshalAs(UnmanagedType.I1)] bool bLauncherMode);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_DismissFloatingGamepadTextInput(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUtils_DismissGamepadTextInput(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamMatchmaking_v009();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamMatchmaking_GetFavoriteGameCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_GetFavoriteGame(IntPtr self, int iGame, out AppId_t pnAppID, out uint pnIP, out ushort pnConnPort, out ushort pnQueryPort, out uint punFlags, out RTime32 pRTime32LastPlayedOnServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamMatchmaking_AddFavoriteGame(IntPtr self, AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, RTime32 rTime32LastPlayedOnServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_RemoveFavoriteGame(IntPtr self, AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamMatchmaking_RequestLobbyList(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKeyToMatch, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchValueToMatch, ELobbyComparison eComparisonType);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKeyToMatch, int nValueToMatch, ELobbyComparison eComparisonType);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKeyToMatch, int nValueToBeCloseTo);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(IntPtr self, int nSlotsAvailable);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter(IntPtr self, ELobbyDistanceFilter eLobbyDistanceFilter);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter(IntPtr self, int cMaxResults);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamMatchmaking_GetLobbyByIndex(IntPtr self, int iLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamMatchmaking_CreateLobby(IntPtr self, ELobbyType eLobbyType, int cMaxMembers);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamMatchmaking_JoinLobby(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_LeaveLobby(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_InviteUserToLobby(IntPtr self, CSteamID steamIDLobby, CSteamID steamIDInvitee);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamMatchmaking_GetNumLobbyMembers(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex(IntPtr self, CSteamID steamIDLobby, int iMember);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamMatchmaking_GetLobbyData(IntPtr self, CSteamID steamIDLobby, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKey);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_SetLobbyData(IntPtr self, CSteamID steamIDLobby, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKey, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchValue);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamMatchmaking_GetLobbyDataCount(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex(IntPtr self, CSteamID steamIDLobby, int iLobbyData, Span<byte> pchKey, int cchKeyBufferSize, Span<byte> pchValue, int cchValueBufferSize);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_DeleteLobbyData(IntPtr self, CSteamID steamIDLobby, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKey);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamMatchmaking_GetLobbyMemberData(IntPtr self, CSteamID steamIDLobby, CSteamID steamIDUser, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKey);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_SetLobbyMemberData(IntPtr self, CSteamID steamIDLobby, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKey, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchValue);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_SendLobbyChatMsg(IntPtr self, CSteamID steamIDLobby, ReadOnlySpan<byte> pvMsgBody, int cubMsgBody);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamMatchmaking_GetLobbyChatEntry(IntPtr self, CSteamID steamIDLobby, int iChatID, IntPtr pSteamIDUser, Span<byte> pvData, int cubData, IntPtr peChatEntryType);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_RequestLobbyData(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmaking_SetLobbyGameServer(IntPtr self, CSteamID steamIDLobby, uint unGameServerIP, ushort unGameServerPort, CSteamID steamIDGameServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_GetLobbyGameServer(IntPtr self, CSteamID steamIDLobby, out uint punGameServerIP, out ushort punGameServerPort, out CSteamID psteamIDGameServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit(IntPtr self, CSteamID steamIDLobby, int cMaxMembers);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_SetLobbyType(IntPtr self, CSteamID steamIDLobby, ELobbyType eLobbyType);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_SetLobbyJoinable(IntPtr self, CSteamID steamIDLobby, [MarshalAs(UnmanagedType.I1)] bool bLobbyJoinable);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamMatchmaking_GetLobbyOwner(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_SetLobbyOwner(IntPtr self, CSteamID steamIDLobby, CSteamID steamIDNewOwner);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamMatchmaking_SetLinkedLobby(IntPtr self, CSteamID steamIDLobby, CSteamID steamIDLobbyDependent);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingServerListResponse_ServerResponded(IntPtr self, HServerListRequest hRequest, int iServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingServerListResponse_ServerFailedToRespond(IntPtr self, HServerListRequest hRequest, int iServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingServerListResponse_RefreshComplete(IntPtr self, HServerListRequest hRequest, EMatchMakingServerResponse response);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingPingResponse_ServerResponded(IntPtr self, IntPtr server);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingPingResponse_ServerFailedToRespond(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingPlayersResponse_AddPlayerToList(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchName, int nScore, float flTimePlayed);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingPlayersResponse_PlayersFailedToRespond(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingPlayersResponse_PlayersRefreshComplete(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingRulesResponse_RulesResponded(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchRule, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchValue);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingRulesResponse_RulesFailedToRespond(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamMatchmakingRulesResponse_RulesRefreshComplete(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamFriends_v018();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetPersonaName(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EPersonaState SteamAPI_ISteamFriends_GetPersonaState(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendCount(IntPtr self, EFriendFlags iFriendFlags);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamFriends_GetFriendByIndex(IntPtr self, int iFriend, EFriendFlags iFriendFlags);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EFriendRelationship SteamAPI_ISteamFriends_GetFriendRelationship(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EPersonaState SteamAPI_ISteamFriends_GetFriendPersonaState(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetFriendPersonaName(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_GetFriendGamePlayed(IntPtr self, CSteamID steamIDFriend, out FriendGameInfo_t pFriendGameInfo);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetFriendPersonaNameHistory(IntPtr self, CSteamID steamIDFriend, int iPersonaName);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendSteamLevel(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetPlayerNickname(IntPtr self, CSteamID steamIDPlayer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendsGroupCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial FriendsGroupID_t SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex(IntPtr self, int iFG);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetFriendsGroupName(IntPtr self, FriendsGroupID_t friendsGroupID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendsGroupMembersCount(IntPtr self, FriendsGroupID_t friendsGroupID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_GetFriendsGroupMembersList(IntPtr self, FriendsGroupID_t friendsGroupID, Span<CSteamID> pOutSteamIDMembers, int nMembersCount);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_HasFriend(IntPtr self, CSteamID steamIDFriend, EFriendFlags iFriendFlags);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetClanCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamFriends_GetClanByIndex(IntPtr self, int iClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetClanName(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetClanTag(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_GetClanActivityCounts(IntPtr self, CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamFriends_DownloadClanActivityCounts(IntPtr self, Span<CSteamID> psteamIDClans, int cClansToRequest);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendCountFromSource(IntPtr self, CSteamID steamIDSource);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamFriends_GetFriendFromSourceByIndex(IntPtr self, CSteamID steamIDSource, int iFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_IsUserInSource(IntPtr self, CSteamID steamIDUser, CSteamID steamIDSource);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_SetInGameVoiceSpeaking(IntPtr self, CSteamID steamIDUser, [MarshalAs(UnmanagedType.I1)] bool bSpeaking);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ActivateGameOverlay(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchDialog);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ActivateGameOverlayToUser(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchDialog, CSteamID steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchURL, EActivateGameOverlayToWebPageMode eMode);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ActivateGameOverlayToStore(IntPtr self, AppId_t nAppID, EOverlayToStoreFlag eFlag);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_SetPlayedWith(IntPtr self, CSteamID steamIDUserPlayedWith);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetSmallFriendAvatar(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetMediumFriendAvatar(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetLargeFriendAvatar(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_RequestUserInformation(IntPtr self, CSteamID steamIDUser, [MarshalAs(UnmanagedType.I1)] bool bRequireNameOnly);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamFriends_RequestClanOfficerList(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamFriends_GetClanOwner(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetClanOfficerCount(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamFriends_GetClanOfficerByIndex(IntPtr self, CSteamID steamIDClan, int iOfficer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_SetRichPresence(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKey, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchValue);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ClearRichPresence(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetFriendRichPresence(IntPtr self, CSteamID steamIDFriend, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchKey);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex(IntPtr self, CSteamID steamIDFriend, int iKey);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_RequestFriendRichPresence(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_InviteUserToGame(IntPtr self, CSteamID steamIDFriend, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchConnectString);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetCoplayFriendCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamFriends_GetCoplayFriend(IntPtr self, int iCoplayFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendCoplayTime(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial AppId_t SteamAPI_ISteamFriends_GetFriendCoplayGame(IntPtr self, CSteamID steamIDFriend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamFriends_JoinClanChatRoom(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_LeaveClanChatRoom(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetClanChatMemberCount(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamFriends_GetChatMemberByIndex(IntPtr self, CSteamID steamIDClan, int iUser);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_SendClanChatMessage(IntPtr self, CSteamID steamIDClanChat, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchText);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetClanChatMessage(IntPtr self, CSteamID steamIDClanChat, int iMessage, Span<byte> prgchText, int cchTextMax, out EChatEntryType peChatEntryType, out CSteamID psteamidChatter);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_IsClanChatAdmin(IntPtr self, CSteamID steamIDClanChat, CSteamID steamIDUser);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam(IntPtr self, CSteamID steamIDClanChat);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_OpenClanChatWindowInSteam(IntPtr self, CSteamID steamIDClanChat);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_CloseClanChatWindowInSteam(IntPtr self, CSteamID steamIDClanChat);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_SetListenForFriendsMessages(IntPtr self, [MarshalAs(UnmanagedType.I1)] bool bInterceptEnabled);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_ReplyToFriendMessage(IntPtr self, CSteamID steamIDFriend, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchMsgToSend);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetFriendMessage(IntPtr self, CSteamID steamIDFriend, int iMessageID, Span<byte> pvData, int cubData, out EChatEntryType peChatEntryType);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamFriends_GetFollowerCount(IntPtr self, CSteamID steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamFriends_IsFollowing(IntPtr self, CSteamID steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamFriends_EnumerateFollowingList(IntPtr self, uint unStartIndex);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_IsClanPublic(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_IsClanOfficialGameGroup(IntPtr self, CSteamID steamIDClan);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamFriends_GetNumChatsWithUnreadPriorityMessages(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog(IntPtr self, CSteamID steamIDLobby);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_RegisterProtocolInOverlayBrowser(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchProtocol);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialogConnectString(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchConnectString);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamFriends_RequestEquippedProfileItems(IntPtr self, CSteamID steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamFriends_BHasEquippedProfileItem(IntPtr self, CSteamID steamID, ECommunityProfileItemType itemType);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamFriends_GetProfileItemPropertyString(IntPtr self, CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamFriends_GetProfileItemPropertyUint(IntPtr self, CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamUser_v023();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HSteamUser SteamAPI_ISteamUser_GetHSteamUser(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_BLoggedOn(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_ISteamUser_GetSteamID(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamUser_InitiateGameConnection_DEPRECATED(IntPtr self, Span<byte> pAuthBlob, int cbMaxAuthBlob, CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs(UnmanagedType.I1)] bool bSecure);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUser_TerminateGameConnection_DEPRECATED(IntPtr self, uint unIPServer, ushort usPortServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUser_TrackAppUsageEvent(IntPtr self, CGameID gameID, int eAppUsageEvent, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchExtraInfo);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_GetUserDataFolder(IntPtr self, Span<byte> pchBuffer, int cubBuffer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUser_StartVoiceRecording(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUser_StopVoiceRecording(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EVoiceResult SteamAPI_ISteamUser_GetAvailableVoice(IntPtr self, out uint pcbCompressed, IntPtr pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EVoiceResult SteamAPI_ISteamUser_GetVoice(IntPtr self, [MarshalAs(UnmanagedType.I1)] bool bWantCompressed, Span<byte> pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, [MarshalAs(UnmanagedType.I1)] bool bWantUncompressed_Deprecated, IntPtr
pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, IntPtr nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EVoiceResult SteamAPI_ISteamUser_DecompressVoice(IntPtr self, ReadOnlySpan<byte> pCompressed, uint cbCompressed, Span<byte> pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint SteamAPI_ISteamUser_GetVoiceOptimalSampleRate(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HAuthTicket SteamAPI_ISteamUser_GetAuthSessionTicket(IntPtr self, Span<byte> pTicket, int cbMaxTicket, out uint pcbTicket, in SteamNetworkingIdentity pSteamNetworkingIdentity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HAuthTicket SteamAPI_ISteamUser_GetAuthTicketForWebApi(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchIdentity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial HAuthTicket SteamAPI_ISteamUser_GetAuthTicketForWebApi(IntPtr self, IntPtr pchIdentity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EBeginAuthSessionResult SteamAPI_ISteamUser_BeginAuthSession(IntPtr self, ReadOnlySpan<byte> pAuthTicket, int cbAuthTicket, CSteamID steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUser_EndAuthSession(IntPtr self, CSteamID steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUser_CancelAuthTicket(IntPtr self, HAuthTicket hAuthTicket);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EUserHasLicenseForAppResult SteamAPI_ISteamUser_UserHasLicenseForApp(IntPtr self, CSteamID steamID, AppId_t appID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_BIsBehindNAT(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamUser_AdvertiseGame(IntPtr self, CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamUser_RequestEncryptedAppTicket(IntPtr self, Span<byte> pDataToInclude, int cbDataToInclude);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_GetEncryptedAppTicket(IntPtr self, Span<byte> pTicket, int cbMaxTicket, out uint pcbTicket);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamUser_GetGameBadgeLevel(IntPtr self, int nSeries, [MarshalAs(UnmanagedType.I1)] bool bFoil);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamUser_GetPlayerSteamLevel(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamUser_RequestStoreAuthURL(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchRedirectURL);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_BIsPhoneVerified(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_BIsTwoFactorEnabled(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_BIsPhoneIdentifying(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_BIsPhoneRequiringVerification(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamUser_GetMarketEligibility(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamUser_GetDurationControl(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamUser_BSetDurationControlOnlineState(IntPtr self, EDurationControlOnlineState eNewState);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_SteamRemoteStorage_v016();

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileWrite(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile, ReadOnlySpan<byte> pvData, int cubData);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamRemoteStorage_FileRead(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile, Span<byte> pvData, int cubDataToRead);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamRemoteStorage_FileWriteAsync(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile, ReadOnlySpan<byte> pvData, uint cubData);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamRemoteStorage_FileReadAsync(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile, uint nOffset, uint cubToRead);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete(IntPtr self, SteamAPICall_t hReadCall, Span<byte> pvBuffer, uint cubToRead);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileForget(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileDelete(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamRemoteStorage_FileShare(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_SetSyncPlatforms(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile, ERemoteStoragePlatform eRemoteStoragePlatform);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial UGCFileWriteStreamHandle_t SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk(IntPtr self, UGCFileWriteStreamHandle_t writeHandle, ReadOnlySpan<byte> pvData, int cubData);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileWriteStreamClose(IntPtr self, UGCFileWriteStreamHandle_t writeHandle);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel(IntPtr self, UGCFileWriteStreamHandle_t writeHandle);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FileExists(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_FilePersisted(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamRemoteStorage_GetFileSize(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial long SteamAPI_ISteamRemoteStorage_GetFileTimestamp(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ERemoteStoragePlatform SteamAPI_ISteamRemoteStorage_GetSyncPlatforms(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchFile);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamRemoteStorage_GetFileCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamRemoteStorage_GetFileNameAndSize(IntPtr self, int iFile, out int pnFileSizeInBytes);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_GetQuota(IntPtr self, out ulong pnTotalBytes, out ulong puAvailableBytes);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp(IntPtr self, [MarshalAs(UnmanagedType.I1)] bool bEnabled);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamRemoteStorage_UGCDownload(IntPtr self, UGCHandle_t hContent, uint unPriority);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress(IntPtr self, UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_GetUGCDetails(IntPtr self, UGCHandle_t hContent, out AppId_t pnAppID, Span<IntPtr> ppchName, out int pnFileSizeInBytes, out CSteamID pSteamIDOwner);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamRemoteStorage_UGCRead(IntPtr self, UGCHandle_t hContent, Span<byte> pvData, int cubDataToRead, uint cOffset, EUGCReadAction eAction);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamRemoteStorage_GetCachedUGCCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial UGCHandle_t SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle(IntPtr self, int iCachedContent);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamAPICall_t SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation(IntPtr self, UGCHandle_t hContent, [MarshalAs(UnmanagedType.LPUTF8Str)] string pchLocation, uint unPriority);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamRemoteStorage_GetLocalFileChangeCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamRemoteStorage_GetLocalFileChange(IntPtr self, int iFile, out ERemoteStorageLocalFileChange pEChangeType, out ERemoteStorageFilePathType pEFilePathType);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_BeginFileWriteBatch(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamRemoteStorage_EndFileWriteBatch(IntPtr self);

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
    public static partial void SteamAPI_SteamNetworkingIdentity_SetSteamID(ref SteamNetworkingIdentity self, CSteamID steamID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial CSteamID SteamAPI_SteamNetworkingIdentity_GetSteamID(ref SteamNetworkingIdentity self);

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
    public static partial bool SteamAPI_ISteamNetworkingSockets_CloseConnection(IntPtr self, HSteamNetConnection hPeer, int nReason, [MarshalAs(UnmanagedType.LPUTF8Str)] string? pszDebug, [MarshalAs(UnmanagedType.I1)] bool bEnableLinger);

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
    public static partial EResult SteamAPI_ISteamNetworkingSockets_SendMessageToConnection(IntPtr self, HSteamNetConnection hConn, ReadOnlySpan<byte> pData, uint cbData, ESteamNetworkingSendType nSendFlags, IntPtr pOutMessageNumber);

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
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetConnectionInfo(IntPtr self, HSteamNetConnection hConn, out SteamNetConnectionInfo_t pInfo);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetConnectionRealTimeStatus(IntPtr self, HSteamNetConnection hConn, out SteamNetConnectionRealTimeStatus_t pStatus, int nLanes, Span<SteamNetConnectionRealTimeLaneStatus_t> pLanes);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetConnectionRealTimeStatus(IntPtr self, HSteamNetConnection hConn, IntPtr pStatus, int nLanes, Span<SteamNetConnectionRealTimeLaneStatus_t> pLanes);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingSockets_GetDetailedConnectionStatus(IntPtr self, HSteamNetConnection hConn, Span<byte> pszBuf, int cbBuf);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetListenSocketAddress(IntPtr self, HSteamListenSocket hSocket, out SteamNetworkingIPAddr address);

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
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetIdentity(IntPtr self, out SteamNetworkingIdentity pIdentity);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingAvailability SteamAPI_ISteamNetworkingSockets_InitAuthentication(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingAvailability SteamAPI_ISteamNetworkingSockets_GetAuthenticationStatus(IntPtr self, out SteamNetAuthenticationStatus_t pDetails);

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
    public static partial EResult SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerAddress(IntPtr self, out SteamDatagramHostedAddress pRouting);

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
    public static partial bool SteamAPI_ISteamNetworkingSockets_GetCertificateRequest(IntPtr self, ref int pcbBlob, Span<byte> pBlob, out SteamNetworkingErrMsg errMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingSockets_SetCertificate(IntPtr self, ReadOnlySpan<byte> pCertificate, int cbCertificate, out SteamNetworkingErrMsg errMsg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingSockets_RunCallbacks(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamNetworkingUtils_AllocateMessage(IntPtr self, int cbAllocateBuffer);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingUtils_InitRelayNetworkAccess(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingAvailability SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus(IntPtr self, out SteamRelayNetworkStatus_t pDetails);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingAvailability SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus(IntPtr self, IntPtr pDetails);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial float SteamAPI_ISteamNetworkingUtils_GetLocalPingLocation(IntPtr self, out SteamNetworkPingLocation_t result);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(IntPtr self, in SteamNetworkPingLocation_t location1, in SteamNetworkPingLocation_t location2);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(IntPtr self, in SteamNetworkPingLocation_t remoteLocation);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString(IntPtr self, in SteamNetworkPingLocation_t location, Span<byte> pszBuf, int cchBufSize);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_ParsePingLocationString(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string pszString, out SteamNetworkPingLocation_t result);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_CheckPingDataUpToDate(IntPtr self, float flMaxAgeSeconds);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingUtils_GetPingToDataCenter(IntPtr self, SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingUtils_GetDirectPingToPOP(IntPtr self, SteamNetworkingPOPID popID);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingUtils_GetPOPCount(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int SteamAPI_ISteamNetworkingUtils_GetPOPList(IntPtr self, Span<SteamNetworkingPOPID> list, int nListSz);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SteamNetworkingMicroseconds SteamAPI_ISteamNetworkingUtils_GetLocalTimestamp(IntPtr self);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction(IntPtr self, ESteamNetworkingSocketsDebugOutputType eDetailLevel, FPtrSteamNetworkingSocketsDebugOutput pfnFunc);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction(IntPtr self, ESteamNetworkingSocketsDebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueInt32(IntPtr self, ESteamNetworkingConfigValue eValue, int val);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueFloat(IntPtr self, ESteamNetworkingConfigValue eValue, float val);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueString(IntPtr self, ESteamNetworkingConfigValue eValue, [MarshalAs(UnmanagedType.LPUTF8Str)] string val);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValuePtr(IntPtr self, ESteamNetworkingConfigValue eValue, IntPtr val);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueInt32(IntPtr self, HSteamNetConnection hConn, ESteamNetworkingConfigValue eValue, int val);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueFloat(IntPtr self, HSteamNetConnection hConn, ESteamNetworkingConfigValue eValue, float val);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueString(IntPtr self, HSteamNetConnection hConn, ESteamNetworkingConfigValue eValue, [MarshalAs(UnmanagedType.LPUTF8Str)] string val);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetConnectionStatusChanged(IntPtr self, FnPtrSteamNetConnectionStatusChanged fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetConnectionStatusChanged(IntPtr self, FnSteamNetConnectionStatusChanged fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetAuthenticationStatusChanged(IntPtr self, FnPtrSteamNetAuthenticationStatusChanged fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetAuthenticationStatusChanged(IntPtr self, FnSteamNetAuthenticationStatusChanged fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamRelayNetworkStatusChanged(IntPtr self, FnPtrSteamRelayNetworkStatusChanged fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamRelayNetworkStatusChanged(IntPtr self, FnSteamRelayNetworkStatusChanged fnCallback);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetConfigValue(IntPtr self, ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, ESteamNetworkingConfigDataType eDataType, ReadOnlySpan<byte> pArg);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SteamAPI_ISteamNetworkingUtils_SetConfigValueStruct(IntPtr self, in SteamNetworkingConfigValue_t opt, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingGetConfigValueResult SteamAPI_ISteamNetworkingUtils_GetConfigValue(IntPtr self, ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, out ESteamNetworkingConfigDataType pOutDataType, Span<byte> pResult, ref SizeT cbResult);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr SteamAPI_ISteamNetworkingUtils_GetConfigValueInfo(IntPtr self, ESteamNetworkingConfigValue eValue, out ESteamNetworkingConfigDataType pOutDataType, out ESteamNetworkingConfigScope pOutScope);

    [LibraryImport(GnsLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ESteamNetworkingConfigValue SteamAPI_ISteamNetworkingUtils_IterateGenericEditableConfigValues(IntPtr self, ESteamNetworkingConfigValue eCurrent, [MarshalAs(UnmanagedType.I1)] bool bEnumerateDevVars);
}
