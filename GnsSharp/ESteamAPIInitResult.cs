// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public enum ESteamAPIInitResult : int
{
    OK = 0,

    /// <summary>
    /// Some other failure
    /// </summary>
    FailedGeneric = 1,

    /// <summary>
    /// We cannot connect to Steam, steam probably isn't running
    /// </summary>
    NoSteamClient = 2,

    /// <summary>
    /// Steam client appears to be out of date
    /// </summary>
    VersionMismatch = 3,
}
