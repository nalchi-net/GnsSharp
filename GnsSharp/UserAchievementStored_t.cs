// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result of a request to store the achievements on the server, or an "indicate progress" call.<br/>
/// If both <see cref="CurProgress"/> and <see cref="MaxProgress"/> are zero, that means the achievement has been fully unlocked.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct UserAchievementStored_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 3;

    /// <summary>
    /// Game ID that this achievement is for.
    /// </summary>
    public ulong GameID;

    /// <summary>
    /// Unused. If this is a "group" achievement.
    /// </summary>
    public bool GroupAchievement;

    /// <summary>
    /// Name of the achievement.
    /// </summary>
    public Array128<byte> AchievementName;

    /// <summary>
    /// Current progress towards the achievement.
    /// </summary>
    public uint CurProgress;

    /// <summary>
    /// The total amount of progress required to unlock.
    /// </summary>
    public uint MaxProgress;

    public static int CallbackParamId => CallbackId;
}
