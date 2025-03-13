// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

#pragma warning disable SA1649 // File name should match first type name

/// <summary>
/// Setup callback for debug output, and the desired verbosity you want.
/// </summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FSteamNetworkingSocketsDebugOutput(ESteamNetworkingSocketsDebugOutputType type, [MarshalAs(UnmanagedType.LPUTF8Str)] string msg);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FSteamNetworkingSocketsLockWaitedFor([MarshalAs(UnmanagedType.LPUTF8Str)] string tags, SteamNetworkingMicroseconds usecWaited);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FSteamNetworkingSocketsServiceThreadInit();

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FnSteamNetConnectionStatusChanged(ref SteamNetConnectionStatusChangedCallback_t callback);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FnSteamNetAuthenticationStatusChanged(ref SteamNetAuthenticationStatus_t callback);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FnSteamRelayNetworkStatusChanged(ref SteamRelayNetworkStatus_t callback);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FnSteamNetworkingMessagesSessionRequest(ref SteamNetworkingMessagesSessionRequest_t callback);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FnSteamNetworkingMessagesSessionFailed(ref SteamNetworkingMessagesSessionFailed_t callback);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FnSteamNetworkingFakeIPResult(ref SteamNetworkingFakeIPResult_t callback);

#pragma warning restore SA1649
