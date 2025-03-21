// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a connections to the Steam back-end has been established.<br/>
/// This means the Steam client now has a working connection to the Steam servers.<br/>
/// Usually this will have occurred before the game has launched, and should only be seen if the user has dropped connection due to a networking issue or a Steam server update.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamServersConnected_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 1;

    public static int CallbackParamId => CallbackId;
}
