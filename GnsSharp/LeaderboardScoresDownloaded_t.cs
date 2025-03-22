// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Call result for <see cref="ISteamUserStats.DownloadLeaderboardEntries"/> when scores for a leaderboard have been downloaded and are ready to be retrieved.
/// After calling you must use <see cref="ISteamUserStats.GetDownloadedLeaderboardEntry"/> to retrieve the info for each downloaded entry.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LeaderboardScoresDownloaded_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 5;

    /// <summary>
    /// Handle to the leaderboard that these entries belong to.
    /// </summary>
    public SteamLeaderboard_t SteamLeaderboard;

    /// <summary>
    /// The handle to pass into <see cref="ISteamUserStats.GetDownloadedLeaderboardEntry"/>  to retrieve the info for each downloaded entry.
    /// </summary>
    public SteamLeaderboardEntries_t SteamLeaderboardEntries;

    /// <summary>
    /// The number of entries downloaded.
    /// </summary>
    public int EntryCount;

    public static int CallbackParamId => CallbackId;
}
