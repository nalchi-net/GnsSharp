// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Result values when a client failed to join or has been kicked from a game server.<br/>
/// Obtained from <see cref="GSClientDeny_t"/> and <see cref="GSClientKick_t"/>.
/// </summary>
public enum EDenyReason : int
{
    /// <summary>
    /// Unknown.
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// The client and server are not the same version.
    /// </summary>
    InvalidVersion = 1,

    /// <summary>
    /// Generic.
    /// </summary>
    Generic = 2,

    /// <summary>
    /// The client is not logged on.
    /// </summary>
    NotLoggedOn = 3,

    /// <summary>
    /// The client does not have a license to play this game.
    /// </summary>
    NoLicense = 4,

    /// <summary>
    /// The client is VAC banned.
    /// </summary>
    Cheater = 5,

    /// <summary>
    /// The client is logged in elsewhere.
    /// </summary>
    LoggedInElseWhere = 6,

    UnknownText = 7,
    IncompatibleAnticheat = 8,
    MemoryCorruption = 9,
    IncompatibleSoftware = 10,

    /// <summary>
    /// The server lost connection to steam.
    /// </summary>
    SteamConnectionLost = 11,

    /// <summary>
    /// The server had a general error connecting to Steam.
    /// </summary>
    SteamConnectionError = 12,

    /// <summary>
    /// The server timed out connecting to Steam.
    /// </summary>
    SteamResponseTimedOut = 13,

    /// <summary>
    /// The client has not authed with Steam yet.
    /// </summary>
    SteamValidationStalled = 14,

    /// <summary>
    /// The owner of the shared game has left, called for each guest of the owner.
    /// </summary>
    SteamOwnerLeftGuestUser = 15,
}
