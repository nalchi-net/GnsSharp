// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// List of states a Steam friend can be in.
/// </summary>
public enum EPersonaState : int
{
    /// <summary>
    /// Friend is not currently logged on.
    /// </summary>
    Offline = 0,

    /// <summary>
    /// Friend is logged on.
    /// </summary>
    Online = 1,

    /// <summary>
    /// Friend is logged on, but set to "Do not disturb."
    /// </summary>
    Busy = 2,

    /// <summary>
    /// Auto-away feature.
    /// </summary>
    Away = 3,

    /// <summary>
    /// Auto-away for a long time.
    /// </summary>
    Snooze = 4,

    /// <summary>
    /// Online, trading.
    /// </summary>
    LookingToTrade = 5,

    /// <summary>
    /// Online, wanting to play.
    /// </summary>
    LookingToPlay = 6,

    /// <summary>
    /// Online, but appears offline to friends.<br/>
    /// This status is never published to clients.
    /// </summary>
    Invisible = 7,
}
