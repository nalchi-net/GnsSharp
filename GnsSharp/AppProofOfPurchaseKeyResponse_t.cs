// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Only used internally in Steam.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct AppProofOfPurchaseKeyResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamAppsCallbacks + 21;

    public EResult Result;
    public uint AppID;
    public uint KeyLength;
    public Array240<byte> Key;

    public static int CallbackParamId => CallbackId;
}
