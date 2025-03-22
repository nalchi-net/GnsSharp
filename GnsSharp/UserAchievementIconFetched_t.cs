// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

#pragma warning disable SA1202 // Elements should be ordered by access

/// <summary>
/// Result of an achievement icon that has been fetched
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct UserAchievementIconFetched_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 9;

    /// <summary>
    /// The Game ID this achievement is for.
    /// </summary>
    public CGameID GameID;

    private Array128<byte> achievementName;

    /// <summary>
    /// Returns whether the icon for the achieved (<c>true</c>) or unachieved (<c>false</c>) version.
    /// </summary>
    public bool Achieved;

    /// <summary>
    /// Handle to the image, which can be used with <see cref="ISteamUtils.GetImageRGBA"/> to get the image data.<br/>
    /// <c>0</c> means no image is set for the achievement.
    /// </summary>
    public int IconHandle;

    public static int CallbackParamId => CallbackId;

    /// <summary>
    /// The name of the achievement that this callback is for.
    /// </summary>
    public readonly string? AchievementName => Utf8StringHelper.NullTerminatedSpanToString(this.achievementName);
}
