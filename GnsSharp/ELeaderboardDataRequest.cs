// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Type of data request, used when downloading leaderboard entries with <see cref="ISteamUserStats.DownloadLeaderboardEntries"/>.
/// </summary>
public enum ELeaderboardDataRequest : int
{
    /// <summary>
    /// Used to query for a sequential range of leaderboard entries by leaderboard rank.<br/>
    /// The start and end parameters control the requested range.<br/>
    /// For example, you can display the top 10 on a leaderboard for your game by setting start to 1 and end to 10.
    /// </summary>
    Global = 0,

    /// <summary>
    /// Used to retrieve leaderboard entries relative a user's entry.<br/>
    /// The start parameter is the number of entries to retrieve before the current user's entry, and the end parameter is the number of entries after the current user's entry.<br/>
    /// The current user's entry is always included.<br/>
    /// For example, if the current user is #5 on the leaderboard, setting start to -2 and end to 2 will return 5 entries: ranks #3 through #7.<br/>
    /// If there are not enough entries in the leaderboard before or after the user's entry, Steam will adjust the range to try to return the number of entries requested.<br/>
    /// For example, if the user is #1 on the leaderboard and start is set to -2, end is set to 2, Steam will return the first 5 entries in the leaderboard.
    /// </summary>
    GlobalAroundUser = 1,

    /// <summary>
    /// Used to retrieve all leaderboard entries for friends of the current user.<br/>
    /// The start and end parameters are ignored.
    /// </summary>
    Friends = 2,

    /// <summary>
    /// Used internally, do not use with <see cref="ISteamUserStats.DownloadLeaderboardEntries"/>!<br/>
    /// Doing so is undefined behavior.
    /// </summary>
    Users = 3,
}
