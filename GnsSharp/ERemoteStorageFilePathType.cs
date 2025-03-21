// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// For APIs that may return file paths in different forms. See <see cref="ISteamRemoteStorage.GetLocalFileChange"/>.
/// </summary>
public enum ERemoteStorageFilePathType : int
{
    /// <summary>
    /// Unused.
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// An absolute disk path is provided. This type of path is used for files managed via AutoCloud.<br/>
    /// The file is directly accessed by the game and this is the full path
    /// </summary>
    Absolute = 1,

    /// <summary>
    /// An <see cref="ISteamRemoteStorage"/> API relative path is provided.<br/>
    /// This type of path is used for files managed via the <see cref="ISteamRemoteStorage"/> API methods (<see cref="ISteamRemoteStorage.FileWrite"/>, <see cref="ISteamRemoteStorage.FileRead"/>, etc).
    /// </summary>
    APIFilename = 2,
}
