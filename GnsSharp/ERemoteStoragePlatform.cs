// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;

/// <summary>
/// Sync Platforms flags. These can be used with <see cref="ISteamRemoteStorage.SetSyncPlatforms"/> to restrict a file to a specific OS.
/// </summary>
[Flags]
public enum ERemoteStoragePlatform : uint
{
    /// <summary>
    /// This file will not be downloaded on any platform.
    /// </summary>
    None = 0,

    /// <summary>
    /// This file will download on Windows.
    /// </summary>
    Windows = 1 << 0,

    /// <summary>
    /// This file will download on macOS.
    /// </summary>
    OSX = 1 << 1,

    /// <summary>
    /// This file will download on the Playstation 3.
    /// </summary>
    PS3 = 1 << 2,

    /// <summary>
    /// This file will download on SteamOS/Linux.
    /// </summary>
    Linux = 1 << 3,

    /// <summary>
    /// This file will download on the Switch.
    /// </summary>
    Switch = 1 << 4,

    /// <summary>
    /// This file will download on the Android.
    /// </summary>
    Android = 1 << 5,

    /// <summary>
    /// This file will download on the iOS.
    /// </summary>
    IOS = 1 << 6,

    /// <summary>
    /// This file will download on every platform. This is the default.
    /// </summary>
    All = 0xffffffff,
}
