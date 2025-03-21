// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Describes the online/offline play state of a game.<br/>
/// Currently this is only used to support the game being able to prevent steam from exiting the game automatically when the user's playtime is consumed.<br/>
/// If the game calls <see cref="ISteamUser.BSetDurationControlOnlineState"/> (<see cref="OnlineHighPri"/>) then Steam will not force exit the game<br/>
/// - in this case it is up to the game to do so! Steam will continue to send <see cref="DurationControl_t"/> notifications to the game.<br/>
/// If the game later calls <see cref="ISteamUser.BSetDurationControlOnlineState"/> to remove the <see cref="OnlineHighPri"/> state, then Steam will force exit the game soon thereafter.
/// </summary>
public enum EDurationControlOnlineState : int
{
    /// <summary>
    /// nil value
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// currently in offline play - single-player, offline co-op, etc.
    /// </summary>
    Offline = 1,

    /// <summary>
    /// currently in online play
    /// </summary>
    Online = 2,

    /// <summary>
    /// currently in online play and requests not to be interrupted<br/>
    /// (game requests that steam not force exit the game)
    /// </summary>
    OnlineHighPri = 3,
}
