// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// A game server has been set via <see cref="ISteamMatchmaking.SetLobbyGameServer"/> for all of the members of the lobby to join.<br/>
/// It's up to the individual clients to take action on this;<br/>
/// the typical game behavior is to leave the lobby and connect to the specified game server;<br/>
/// but the lobby may stay open throughout the session if desired.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyGameCreated_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 9;

    /// <summary>
    /// The lobby that set the game server.
    /// </summary>
    public ulong SteamIDLobby;

    /// <summary>
    /// The Steam ID of the game server, if it's set.
    /// </summary>
    public ulong SteamIDGameServer;

    /// <summary>
    /// The IP address of the game server in host order, i.e 127.0.0.1 == 0x7f000001, if it's set.
    /// </summary>
    public uint Ip;

    /// <summary>
    /// The connection port of the game server, in host order, if it's set.
    /// </summary>
    public ushort Port;

    public static int CallbackParamId => CallbackId;
}
