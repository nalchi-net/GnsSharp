// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// Someone has invited you to join a Lobby.<br/>
/// Normally you don't need to do anything with this, as the Steam UI will also display a '&lt;user&gt; has invited you to the lobby, join?' notification and message.
/// </para>
///
/// <para>
/// If the user outside a game chooses to join, your game will be launched with the parameter <c>+connect_lobby &lt;64-bit lobby id&gt;</c>, or with the callback <see cref="GameLobbyJoinRequested_t"/> if they're already in-game.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyInvite_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 3;

    /// <summary>
    /// Steam ID of the person that sent the invite.
    /// </summary>
    public ulong SteamIDUser;

    /// <summary>
    /// Steam ID of the lobby we're invited to.
    /// </summary>
    public ulong SteamIDLobby;

    /// <summary>
    /// Game ID of the lobby we're invited to.
    /// </summary>
    public ulong GameID;

    public static int CallbackParamId => CallbackId;
}
