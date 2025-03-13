// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

global using AppId_t = uint;

global using unsafe FnPtrSteamNetAuthenticationStatusChanged = delegate* unmanaged[Cdecl]<ref GnsSharp.SteamNetAuthenticationStatus_t, void>;
global using unsafe FnPtrSteamNetConnectionStatusChanged = delegate* unmanaged[Cdecl]<ref GnsSharp.SteamNetConnectionStatusChangedCallback_t, void>;

global using unsafe FnPtrSteamNetworkingFakeIPResult = delegate* unmanaged[Cdecl]<ref GnsSharp.SteamNetworkingFakeIPResult_t, void>;

global using unsafe FnPtrSteamNetworkingMessagesSessionFailed = delegate* unmanaged[Cdecl]<ref GnsSharp.SteamNetworkingMessagesSessionFailed_t, void>;
global using unsafe FnPtrSteamNetworkingMessagesSessionRequest = delegate* unmanaged[Cdecl]<ref GnsSharp.SteamNetworkingMessagesSessionRequest_t, void>;
global using unsafe FnPtrSteamRelayNetworkStatusChanged = delegate* unmanaged[Cdecl]<ref GnsSharp.SteamRelayNetworkStatus_t, void>;

global using unsafe FPtrSteamNetworkingSocketsDebugOutput = delegate* unmanaged[Cdecl]<GnsSharp.ESteamNetworkingSocketsDebugOutputType, byte*, void>;

/// <summary>
/// handle to a communication pipe to the Steam client
/// </summary>
global using HSteamPipe = System.Int32;

/// <summary>
/// handle to single instance of a steam user
/// </summary>
global using HSteamUser = System.Int32;

global using PsnIdType = System.UInt64;

/// <summary>
/// RTime32.  Seconds elapsed since Jan 1 1970, i.e. unix timestamp.<br>
/// It's the same as time_t, but it is always 32-bit and unsigned.
/// </summary>
global using RTime32 = System.UInt32;

global using SizeT = System.UIntPtr;

global using SteamAPICall_t = System.UInt64;

global using SteamErrMsg = GnsSharp.Array1024<byte>;
global using SteamNetworkingErrMsg = GnsSharp.Array1024<byte>;

global using SteamNetworkingMicroseconds = System.Int64;
global using SteamNetworkingPOPID = System.UInt32;
