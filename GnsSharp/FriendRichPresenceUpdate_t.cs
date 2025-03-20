// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when Rich Presence data has been updated for a user,<br/>
/// this can happen automatically when friends in the same game update their rich presence, or after a call to <see cref="ISteamFriends.RequestFriendRichPresence"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct FriendRichPresenceUpdate_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 36;

    /// <summary>
    /// The Steam ID of the user who's rich presence has changed.
    /// </summary>
    public CSteamID SteamIDFriend;

    /// <summary>
    /// The App ID of the game. This should always be the current game.
    /// </summary>
    public AppId_t AppID;

    public static int CallbackParamId => CallbackId;
}
