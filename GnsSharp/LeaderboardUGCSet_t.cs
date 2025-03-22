// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Call result for <see cref="ISteamUserStats.AttachLeaderboardUGC"/> indicating that user generated content has been attached to one of the current user's leaderboard entries.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LeaderboardUGCSet_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 11;

    /// <summary>
    /// The result of the operation. Possible Values:<br/>
    /// * <see cref="EResult.OK"/> - The UGC has been successfully attached.<br/>
    /// * <see cref="EResult.Timeout"/> - The upload took too long, the UGC was not submitted.<br/>
    /// * <see cref="EResult.InvalidParam"/> - The handle passed into <see cref="SteamLeaderboard"/> is invalid, the UGC was not submitted.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// Handle to the leaderboard that the UGC was attached to.
    /// </summary>
    public SteamLeaderboard_t SteamLeaderboard;

    public static int CallbackParamId => CallbackId;
}
