// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// The sort method used to set whether a higher or lower score is better.
/// You can set the sort method when creating a leaderboard with <see cref="ISteamUserStats.FindOrCreateLeaderboard"/> or in App Admin on the Steamworks website.
/// You can retrieve the sort method for a given leaderboard with <see cref="ISteamUserStats.GetLeaderboardSortMethod"/>.
/// </summary>
public enum ELeaderboardSortMethod : int
{
    /// <summary>
    /// Only ever used when a leaderboard is invalid, you should never set this yourself.
    /// </summary>
    None = 0,

    /// <summary>
    /// The top-score is the lowest number.
    /// </summary>
    Ascending = 1,

    /// <summary>
    /// The top-score is the highest number.
    /// </summary>
    Descending = 2,
}
