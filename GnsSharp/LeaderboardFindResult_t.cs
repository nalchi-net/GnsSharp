// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Call Result when finding a leaderboard, returned as a result of <see cref="ISteamUserStats.FindOrCreateLeaderboard"/> or <see cref="ISteamUserStats.FindLeaderboard"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LeaderboardFindResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 4;

    /// <summary>
    /// Handle to the leaderboard that was searched for.<br/>
    /// <c>0</c> if no leaderboard was found.
    /// </summary>
    public SteamLeaderboard_t SteamLeaderboard;

    /// <summary>
    /// Was the leaderboard found?<br/>
    /// <c>1</c> if it was, <c>0</c> if it wasn't.
    /// </summary>
    public bool LeaderboardFound;

    public static int CallbackParamId => CallbackId;
}
