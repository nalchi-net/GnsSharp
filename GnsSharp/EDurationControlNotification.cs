// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Describes which notification timer has expired, for steam china duration control feature.<br/>
/// Some of these notifications are deprecated and are no longer sent.
/// </summary>
public enum EDurationControlNotification : int
{
    /// <summary>
    /// Callback is just informing you about progress, no notification to show
    /// </summary>
    None = 0,

    /// <summary>
    /// player has been playing for an hour - game can show something at this time if desired
    /// </summary>
    OneHour = 1,

    /// <summary>
    /// deprecated - "you've been playing for 3 hours; take a break"
    /// </summary>
    ThreeHours = 2,

    /// <summary>
    /// deprecated - "your XP / progress is half normal"
    /// </summary>
    HalfProgress = 3,

    /// <summary>
    /// deprecated - "your XP / progress is zero"
    /// </summary>
    NoProgress = 4,

    /// <summary>
    /// allowed 3h time since 5h gap/break has elapsed, game should exit - steam will terminate the game soon
    /// </summary>
    ExitSoon_3h = 5,

    /// <summary>
    /// allowed 5h time in calendar day has elapsed, game should exit - steam will terminate the game soon
    /// </summary>
    ExitSoon_5h = 6,

    /// <summary>
    /// player has been playing until the locally allowed time of day (10PM) and should exit the game
    /// </summary>
    ExitSoon_Night = 7,
}
