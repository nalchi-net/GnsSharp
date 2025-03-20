// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Callback return values for the <see cref="ValidateAuthTicketResponse_t"/> callback which is posted as a response to <see cref="ISteamUser.BeginAuthSession"/> and <see cref="ISteamGameServer.BeginAuthSession"/>.
/// </summary>
public enum EAuthSessionResponse : int
{
    /// <summary>
    /// Steam has verified the user is online, the ticket is valid and ticket has not been reused.
    /// </summary>
    OK = 0,

    /// <summary>
    /// The user in question is not connected to steam.
    /// </summary>
    UserNotConnectedToSteam = 1,

    /// <summary>
    /// The user doesn't have a license for this App ID or the ticket has expired.
    /// </summary>
    NoLicenseOrExpired = 2,

    /// <summary>
    /// The user is VAC banned for this game.
    /// </summary>
    VACBanned = 3,

    /// <summary>
    /// The user account has logged in elsewhere and the session containing the game instance has been disconnected.
    /// </summary>
    LoggedInElseWhere = 4,

    /// <summary>
    /// VAC has been unable to perform anti-cheat checks on this user.
    /// </summary>
    VACCheckTimedOut = 5,

    /// <summary>
    /// The ticket has been canceled by the issuer.
    /// </summary>
    AuthTicketCanceled = 6,

    /// <summary>
    /// This ticket has already been used, it is not valid.
    /// </summary>
    AuthTicketInvalidAlreadyUsed = 7,

    /// <summary>
    /// This ticket is not from a user instance currently connected to steam.
    /// </summary>
    AuthTicketInvalid = 8,

    /// <summary>
    /// The user is banned for this game. The ban came via the web api and not VAC
    /// </summary>
    PublisherIssuedBan = 9,

    /// <summary>
    /// The network identity in the ticket does not match the server authenticating the ticket.
    /// </summary>
    AuthTicketNetworkIdentityFailure = 10,
}
