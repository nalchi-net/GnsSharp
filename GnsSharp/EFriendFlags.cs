// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Flags for enumerating friends list, or quickly checking the relationship between users.
/// </summary>
public enum EFriendFlags : int
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0x00,

    /// <summary>
    /// Users that the current user has blocked from contacting.
    /// </summary>
    Blocked = 0x01,

    /// <summary>
    /// Users that have sent a friend invite to the current user.
    /// </summary>
    FriendshipRequested = 0x02,

    /// <summary>
    /// The current user's "regular" friends.
    /// </summary>
    Immediate = 0x04,

    /// <summary>
    /// Users that are in one of the same (small) Steam groups as the current user.
    /// </summary>
    ClanMember = 0x08,

    /// <summary>
    /// Users that are on the same game server; as set by <see cref="ISteamFriends.SetPlayedWith"/>.
    /// </summary>
    OnGameServer = 0x10,

    // HasPlayedWith= 0x20,// not currently used
    // FriendOfFriend= 0x40, // not currently used

    /// <summary>
    /// Users that the current user has sent friend invites to.
    /// </summary>
    RequestingFriendship = 0x80,

    /// <summary>
    /// Users that are currently sending additional info about themselves after a call to <see cref="ISteamFriends.RequestUserInformation"/>
    /// </summary>
    RequestingInfo = 0x100,

    /// <summary>
    /// Users that the current user has ignored from contacting them.
    /// </summary>
    Ignored = 0x200,

    /// <summary>
    /// Users that have ignored the current user; but the current user still knows about them.
    /// </summary>
    IgnoredFriend = 0x400,

    // Suggested= 0x800,// not used

    /// <summary>
    /// Users in one of the same chats.
    /// </summary>
    ChatMember = 0x1000,

    /// <summary>
    /// Returns all friend flags.
    /// </summary>
    All = 0xFFFF,
}
