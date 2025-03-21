// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Ways in which a local file may be changed by Steam during the application session. See <see cref="ISteamRemoteStorage.GetLocalFileChange"/>.
/// </summary>
public enum ERemoteStorageLocalFileChange : int
{
    /// <summary>
    /// Unused.
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// The file was updated from another device.
    /// </summary>
    FileUpdated = 1,

    /// <summary>
    /// The file was deleted by another device.
    /// </summary>
    FileDeleted = 2,
}
