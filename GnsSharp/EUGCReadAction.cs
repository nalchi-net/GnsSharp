// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Possible UGC Read Actions used with <see cref="ISteamRemoteStorage.UGCRead"/>.
/// </summary>
public enum EUGCReadAction : int
{
    /// <summary>
    /// Keeps the file handle open unless the last byte is read. You can use this when reading large files (over 100MB) in sequential chunks.<br/>
    /// If the last byte is read, this will behave the same as k_EUGCRead_Close.<br/>
    /// Otherwise, it behaves the same as k_EUGCRead_ContinueReading.<br/>
    /// This value maintains the same behavior as before the EUGCReadAction parameter was introduced.
    /// </summary>
    ContinueReadingUntilFinished = 0,

    /// <summary>
    /// Keeps the file handle open. Use this when using UGCRead to seek to different parts of the file.<br/>
    /// When you are done seeking around the file, make a final call with k_EUGCRead_Close to close it.
    /// </summary>
    ContinueReading = 1,

    /// <summary>
    /// Frees the file handle. Use this when you're done reading the content.<br/>
    /// To read the file from Steam again you will need to call UGCDownload again.
    /// </summary>
    Close = 2,
}
