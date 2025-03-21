// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// describes XP / progress restrictions to apply for games with duration control /<br/>
/// anti-indulgence enabled for minor Steam China users.
/// </summary>
public enum EDurationControlProgress : int
{
    /// <summary>
    /// Full progress
    /// </summary>
    Full = 0,

    /// <summary>
    /// deprecated - XP or persistent rewards should be halved
    /// </summary>
    Half = 1,

    /// <summary>
    /// deprecated - XP or persistent rewards should be stopped
    /// </summary>
    None = 2,

    /// <summary>
    /// allowed 3h time since 5h gap/break has elapsed, game should exit - steam will terminate the game soon
    /// </summary>
    ExitSoon_3h = 3,

    /// <summary>
    /// allowed 5h time in calendar day has elapsed, game should exit - steam will terminate the game soon
    /// </summary>
    ExitSoon_5h = 4,

    /// <summary>
    /// game running after day period, game should exit - steam will terminate the game soon
    /// </summary>
    ExitSoon_Night = 5,
}
