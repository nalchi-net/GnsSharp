// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Call result for <see cref="ISteamUserStats.UploadLeaderboardScore"/> indicating that a leaderboard score has been uploaded.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LeaderboardScoreUploaded_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 6;

    /// <summary>
    /// Was the call successful? Returns <c>1</c> if the call was successful, <c>0</c> on failure.<br/>
    /// The amount of details sent exceeds <see cref="ISteamUserStats.LeaderboardDetailsMax"/>.<br/>
    /// The leaderboard is set to "Trusted" in App Admin on Steamworks website, and will only accept scores sent from the Steam Web API.
    /// </summary>
    public bool Success;

    /// <summary>
    /// Handle to the leaderboard that this score was uploaded to.
    /// </summary>
    public SteamLeaderboard_t SteamLeaderboard;

    /// <summary>
    /// The score that was attempted to set.
    /// </summary>
    public int Score;

    /// <summary>
    /// <c>true</c> if the score on the leaderboard changed, otherwise <c>false</c> if the existing score was better.
    /// </summary>
    public bool ScoreChanged;

    /// <summary>
    /// The new global rank of the user on this leaderboard.
    /// </summary>
    public int GlobalRankNew;

    /// <summary>
    /// The previous global rank of the user on this leaderboard;<br/>
    /// <c>0</c> if the user had no existing entry in the leaderboard.
    /// </summary>
    public int GlobalRankPrevious;

    public static int CallbackParamId => CallbackId;
}
