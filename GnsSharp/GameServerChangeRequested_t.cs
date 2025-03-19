// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the user tries to join a different game server from their friends list.<br/>
/// The game client should attempt to connect to the specified server when this is received.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameServerChangeRequested_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 32;

    private Array64<byte> server;
    private Array64<byte> password;

    public static int CallbackParamId => CallbackId;

    /// <summary>
    /// Server address (e.g. <c>"127.0.0.1:27015"</c>, <c>"tf2.valvesoftware.com"</c>)
    /// </summary>
    public readonly string? Server => Utf8StringHelper.NullTerminatedSpanToString(this.server);

    /// <summary>
    /// Server password, if any.
    /// </summary>
    public readonly string? Password => Utf8StringHelper.NullTerminatedSpanToString(this.password);
}
