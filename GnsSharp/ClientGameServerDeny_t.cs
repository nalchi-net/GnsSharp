// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Sent by the Steam server to the client telling it to disconnect from the specified game server,<br/>
/// which it may be in the process of or already connected to.<br/>
/// The game client should immediately disconnect upon receiving this message.<br/>
/// This can usually occur if the user doesn't have rights to play on the game server.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct ClientGameServerDeny_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 13;

    /// <summary>
    /// The App ID this call is for.<br/>
    /// Verify that it's the same as the current App ID with <see cref="ISteamUtils.GetAppID"/>.
    /// </summary>
    public uint AppID;

    /// <summary>
    /// The IP of the game server that is telling us to disconnect, in host order, i.e 127.0.0.1 == 0x7f000001.
    /// </summary>
    public uint GameServerIP;

    /// <summary>
    /// The port of the game server that is telling us to disconnect, in host order.
    /// </summary>
    public ushort GameServerPort;

    /// <summary>
    /// Is the game server VAC secure (<c>true</c>) or not (<c>false</c>)?
    /// </summary>
    public ushort Secure;

    public uint Reason;

    public static int CallbackParamId => CallbackId;
}
