// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// lobby type description
/// </summary>
public enum ELobbyType : int
{
    /// <summary>
    /// only way to join the lobby is to invite to someone else
    /// </summary>
    Private = 0,

    /// <summary>
    /// shows for friends or invitees, but not in lobby list
    /// </summary>
    FriendsOnly = 1,

    /// <summary>
    /// visible for friends and in lobby list
    /// </summary>
    Public = 2,

    /// <summary>
    /// returned by search, but not visible to other friends<br/>
    /// useful if you want a user in two lobbies, for example matching groups together<br/>
    /// a user can be in only one regular lobby, and up to two invisible lobbies
    /// </summary>
    Invisible = 3,

    /// <summary>
    /// private, unique and does not delete when empty - only one of these may exist per unique keypair set<br/>
    /// can only create from webapi
    /// </summary>
    PrivateUnique = 4,
}
