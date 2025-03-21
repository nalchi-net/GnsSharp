// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Sent to your game in response to a steam://gamewebcallback/ command from a user clicking a link in the Steam overlay browser.
/// You can use this to add support for external site signups where you want to pop back into the browser after some web page signup sequence, and optionally get back some detail about that.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameWebCallback_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 64;

    private Array256<byte> url;

    public static int CallbackParamId => CallbackId;

    /// <summary>
    /// The complete url that the user clicked on.
    /// </summary>
    public readonly string? Url => Utf8StringHelper.NullTerminatedSpanToString(this.url);
}
