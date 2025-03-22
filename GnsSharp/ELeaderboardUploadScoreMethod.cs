// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public enum ELeaderboardUploadScoreMethod : int
{
    None = 0,

    /// <summary>
    /// Leaderboard will keep user's best score
    /// </summary>
    KeepBest = 1,

    /// <summary>
    /// Leaderboard will always replace score with specified
    /// </summary>
    ForceUpdate = 2,
}
