// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called if the client has lost connection to the Steam servers.
/// Real-time services will be disabled until a matching <see cref="SteamServersConnected_t"/> has been posted.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamServersDisconnected_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 3;

    /// <summary>
    /// The reason we were disconnected from Steam.
    /// </summary>
    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
