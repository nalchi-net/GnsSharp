// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Detail level for diagnostic output callback.<br/>
/// See ISteamNetworkingUtils::SetDebugOutputFunction
/// </summary>
public enum ESteamNetworkingSocketsDebugOutputType : int
{
    None = 0,

    /// <summary>
    /// You used the API incorrectly, or an internal error happened
    /// </summary>
    Bug = 1,

    /// <summary>
    /// Run-time error condition that isn't the result of a bug.<br/>
    /// (E.g. we are offline, cannot bind a port, etc)
    /// </summary>
    Error = 2,

    /// <summary>
    /// Nothing is wrong, but this is an important notification
    /// </summary>
    Important = 3,

    Warning = 4,

    /// <summary>
    /// Recommended amount
    /// </summary>
    Msg = 5,

    /// <summary>
    /// Quite a bit
    /// </summary>
    Verbose = 6,

    /// <summary>
    /// Practically everything
    /// </summary>
    Debug = 7,

    /// <summary>
    /// Wall of text, detailed packet contents breakdown, etc
    /// </summary>
    Everything = 8,
}
