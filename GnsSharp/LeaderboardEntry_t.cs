// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// A single entry in a leaderboard, as returned by <see cref="ISteamUserStats.GetDownloadedLeaderboardEntry"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LeaderboardEntry_t
{
    /// <summary>
    /// User who this entry belongs to.<br/>
    /// You can use <see cref="ISteamFriends.GetFriendPersonaName"/> and <see cref="ISteamFriends.GetSmallFriendAvatar"/> to get more info.
    /// </summary>
    public CSteamID SteamIDUser;

    /// <summary>
    /// The global rank of this entry ranging from [1..N], where N is the number of users with an entry in the leaderboard.
    /// </summary>
    public int GlobalRank;

    /// <summary>
    /// The raw score as set in the leaderboard.
    /// </summary>
    public int Score;

    /// <summary>
    /// The number of details available for this entry.
    /// </summary>
    public int Details;

    /// <summary>
    /// Handle for the UGC attached to the entry. <see cref="UGCHandle_t.Invalid"/> if there is none.
    /// </summary>
    public UGCHandle_t UGC;
}
