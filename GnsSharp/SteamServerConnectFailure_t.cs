// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a connection attempt has failed.<br/>
/// This will occur periodically if the Steam client is not connected, and has failed when retrying to establish a connection.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamServerConnectFailure_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 2;

    /// <summary>
    /// The reason why the connection failed.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// Is the Steam client still trying to connect to the server?
    /// </summary>
    public bool StillRetrying;

    public static int CallbackParamId => CallbackId;
}
