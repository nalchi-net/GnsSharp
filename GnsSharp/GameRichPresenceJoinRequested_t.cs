// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the user tries to join a game from their friends list or after a user accepts an invite by a friend with <see cref="ISteamFriends.InviteUserToGame"/>.
/// </summary>
/// <remarks>
/// NOTE: This callback is made when joining a game.<br/>
/// If the user is attempting to join a lobby, then the callback <see cref="GameLobbyJoinRequested_t"/> will be made.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameRichPresenceJoinRequested_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 37;

    public const int MaxRichPresenceValueLength = 256;

    /// <summary>
    /// The friend they joined through.<br/>
    /// This will be invalid if not directly via a friend.
    /// </summary>
    public CSteamID SteamIDFriend;

    private Array256<byte> connect;

    public static int CallbackParamId => CallbackId;

    /// <summary>
    /// The value associated with the "connect" Rich Presence key.
    /// </summary>
    public readonly string? Connect => Utf8StringHelper.NullTerminatedSpanToString(this.connect);
}
