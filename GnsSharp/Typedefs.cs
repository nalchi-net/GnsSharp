// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

#pragma warning disable SA1200 // Using directives should be placed correctly

global using AppId_t = uint;

global using PsnIdType = System.UInt64;

/// <summary>
/// RTime32.  Seconds elapsed since Jan 1 1970, i.e. unix timestamp.<br>
/// It's the same as time_t, but it is always 32-bit and unsigned.
/// </summary>
global using RTime32 = System.UInt32;

global using SizeT = System.UIntPtr;

global using SteamNetworkingErrMsg = GnsSharp.Array1024<byte>;

global using SteamNetworkingMicroseconds = System.Int64;
global using SteamNetworkingPOPID = System.UInt32;

#pragma warning restore SA1200
