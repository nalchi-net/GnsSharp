// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Results returned from <see cref="ISteamUser.BeginAuthSession"/> and <see cref="ISteamGameServer.BeginAuthSession"/>.
/// </summary>
public enum EBeginAuthSessionResult : int
{
    /// <summary>
    /// Ticket is valid for this game and this Steam ID.
    /// </summary>
    OK = 0,                        // Ticket is valid for this game and this steamID.

    /// <summary>
    /// The ticket is invalid.
    /// </summary>
    InvalidTicket = 1,                // Ticket is not valid.

    /// <summary>
    /// A ticket has already been submitted for this Steam ID.
    /// </summary>
    DuplicateRequest = 2,            // A ticket has already been submitted for this steamID

    /// <summary>
    /// Ticket is from an incompatible interface version.
    /// </summary>
    InvalidVersion = 3,            // Ticket is from an incompatible interface version

    /// <summary>
    /// Ticket is not for this game.
    /// </summary>
    GameMismatch = 4,

    /// <summary>
    /// Ticket has expired.
    /// </summary>
    ExpiredTicket = 5,
}
