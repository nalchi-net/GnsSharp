// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Dispatched when an overlay browser instance is navigated to a protocol/scheme registered by <see cref="ISteamFriends.RegisterProtocolInOverlayBrowser"/>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct OverlayBrowserProtocolNavigation_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 49;

    private Array1024<byte> uri;

    public static int CallbackParamId => CallbackId;

    public readonly string? Uri => Utf8StringHelper.NullTerminatedSpanToString(this.uri);
}
