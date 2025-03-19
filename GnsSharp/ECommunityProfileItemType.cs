// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Steam Community profile item types
/// </summary>
public enum ECommunityProfileItemType : int
{
    /// <summary>
    /// Animated avatar image (GIF)
    /// </summary>
    AnimatedAvatar = 0,

    /// <summary>
    /// Avatar frame (may or may not be an animated PNG)
    /// </summary>
    AvatarFrame = 1,

    /// <summary>
    /// Special profile modifier item, like Seasonal Profile or Artist Profile
    /// </summary>
    ProfileModifier = 2,

    /// <summary>
    /// Profile background image or movie
    /// </summary>
    ProfileBackground = 3,

    /// <summary>
    /// Background image or movie for the hover flyout for a user
    /// </summary>
    MiniProfileBackground = 4,
}
