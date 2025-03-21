// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a user has responded to a microtransaction authorization request.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct MicroTxnAuthorizationResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 52;

    /// <summary>
    /// App ID for this microtransaction
    /// </summary>
    public uint AppID;

    /// <summary>
    /// Order ID provided for the microtransaction.
    /// </summary>
    public ulong OrderID;

    /// <summary>
    /// Did the user authorize the transaction (<c>1</c>) or not (<c>0</c>)?
    /// </summary>
    public bool Authorized;

    public static int CallbackParamId => CallbackId;
}
