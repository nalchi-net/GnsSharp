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

    public Array1024<byte> Uri;

    public static int CallbackParamId => CallbackId;
}
