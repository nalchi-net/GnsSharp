// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// The display type used by the Steam Community web site to know how to format the leaderboard scores when displayed. You can set the display type when creating a leaderboard with <see cref="ISteamUserStats.FindOrCreateLeaderboard"/> or in the Steamworks partner backend. You can retrieve the display type for a given leaderboard with <see cref="ISteamUserStats.GetLeaderboardDisplayType"/>.
/// </summary>
public enum ELeaderboardDisplayType : int
{
    /// <summary>
    /// This is only ever used when a leaderboard is invalid, you should never set this yourself.
    /// </summary>
    None = 0,

    /// <summary>
    /// The score is just a simple numerical value.
    /// </summary>
    Numeric = 1,

    /// <summary>
    /// The score represents a time, in seconds.
    /// </summary>
    TimeSeconds = 2,

    /// <summary>
    /// The score represents a time, in milliseconds.
    /// </summary>
    TimeMilliSeconds = 3,
}
