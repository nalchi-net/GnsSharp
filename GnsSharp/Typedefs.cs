// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

#pragma warning disable SA1200 // Using directives should be placed correctly

global using AccountID_t = uint;

global using CGameID = ulong;

global using DepotId_t = uint;

global using unsafe FnPtrSteamNetAuthenticationStatusChanged = delegate* unmanaged[Cdecl]<GnsSharp.SteamNetAuthenticationStatus_t*, void>;
global using unsafe FnPtrSteamNetConnectionStatusChanged = delegate* unmanaged[Cdecl]<GnsSharp.SteamNetConnectionStatusChangedCallback_t*, void>;

global using unsafe FnPtrSteamNetworkingFakeIPResult = delegate* unmanaged[Cdecl]<GnsSharp.SteamNetworkingFakeIPResult_t*, void>;

global using unsafe FnPtrSteamNetworkingMessagesSessionFailed = delegate* unmanaged[Cdecl]<GnsSharp.SteamNetworkingMessagesSessionFailed_t*, void>;
global using unsafe FnPtrSteamNetworkingMessagesSessionRequest = delegate* unmanaged[Cdecl]<GnsSharp.SteamNetworkingMessagesSessionRequest_t*, void>;
global using unsafe FnPtrSteamRelayNetworkStatusChanged = delegate* unmanaged[Cdecl]<GnsSharp.SteamRelayNetworkStatus_t*, void>;

global using unsafe FPtrCustomMemoryFree = delegate* unmanaged[Cdecl]<void*, void>;
global using unsafe FPtrCustomMemoryMalloc = delegate* unmanaged[Cdecl]<System.UIntPtr, void*>;
global using unsafe FPtrCustomMemoryRealloc = delegate* unmanaged[Cdecl]<void*, System.UIntPtr, void*>;

global using unsafe FPtrSteamNetworkingSocketsDebugOutput = delegate* unmanaged[Cdecl]<GnsSharp.ESteamNetworkingSocketsDebugOutputType, byte*, void>;

global using unsafe FPtrSteamNetworkingSocketsLockWaitedFor = delegate* unmanaged[Cdecl]<byte*, System.Int64, void>;
global using unsafe FPtrSteamNetworkingSocketsServiceThreadInit = delegate* unmanaged[Cdecl]<void>;

global using HServerListRequest = System.IntPtr;

global using PsnIdType = System.UInt64;

global using unsafe PtrSteamAPIWarningMessageHook_t = delegate* unmanaged[Cdecl]<GnsSharp.ESteamWarningSeverity, byte*, void>;

global using PublishedFileId_t = System.UInt64;

#if GNS_SHARP_PLATFORM_WIN64 || GNS_SHARP_PLATFORM_POSIX64
global using SizeT = System.UInt64;
#elif GNS_SHARP_PLATFORM_WIN32 || GNS_SHARP_PLATFORM_POSIX32
global using SizeT = System.UInt32;
#endif

global using SteamErrMsg = GnsSharp.Array1024<byte>;

global using SteamLeaderboard_t = ulong;
global using SteamLeaderboardEntries_t = ulong;

global using SteamNetworkingErrMsg = GnsSharp.Array1024<byte>;
