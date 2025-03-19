// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the user tries to join a lobby from their friends list or from an invite.<br/>
/// The game client should attempt to connect to specified lobby when this is received.<br/>
/// If the game isn't running yet then the game will be automatically launched with the command line parameter <c>+connect_lobby &lt;64-bit lobby Steam ID&gt;</c> instead.
/// </summary>
/// <remarks>
/// NOTE: This callback is made when joining a lobby.<br/>
/// If the user is attempting to join a game but not a lobby, then the callback <see cref="GameRichPresenceJoinRequested_t"/> will be made.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameLobbyJoinRequested_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 33;

    /// <summary>
    /// The Steam ID of the lobby to connect to.
    /// </summary>
    public CSteamID SteamIDLobby;

    /// <summary>
    /// The friend they joined through. This will be invalid if not directly via a friend.
    /// </summary>
    public CSteamID SteamIDFriend;

    public static int CallbackParamId => CallbackId;
}
