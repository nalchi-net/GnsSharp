// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Properties on a Steam Community profile item.<br/>
/// See <see cref="ISteamFriends.GetProfileItemPropertyString"/> and <see cref="ISteamFriends.GetProfileItemPropertyUint"/>.
/// </summary>
public enum ECommunityProfileItemProperty : int
{
    /// <summary>
    /// URL to the small or animated version of the image
    /// </summary>
    ImageSmall = 0,

    /// <summary>
    /// URL to the large or static version of the image
    /// </summary>
    ImageLarge = 1,

    /// <summary>
    /// Internal name entered on the partner site (for debugging)
    /// </summary>
    InternalName = 2,

    /// <summary>
    /// Localized name of the item
    /// </summary>
    Title = 3,

    /// <summary>
    /// Localized description of the item
    /// </summary>
    Description = 4,

    /// <summary>
    /// AppID of the item (unsigned integer)
    /// </summary>
    AppID = 5,

    /// <summary>
    /// Type id of the item, unique to the appid (unsigned integer)
    /// </summary>
    TypeID = 6,

    /// <summary>
    /// "Class" or type of item (internal value, unsigned integer)
    /// </summary>
    Class = 7,

    /// <summary>
    /// URL to the webm video file
    /// </summary>
    MovieWebM = 8,

    /// <summary>
    /// URL to the mp4 video file
    /// </summary>
    MovieMP4 = 9,

    /// <summary>
    /// URL to the small webm video file
    /// </summary>
    MovieWebMSmall = 10,

    /// <summary>
    /// URL to the small mp4 video file
    /// </summary>
    MovieMP4Small = 11,
}
