// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Steam API call failure results
/// </summary>
public enum ESteamAPICallFailure : int
{
    /// <summary>
    /// no failure
    /// </summary>
    None = -1,

    /// <summary>
    /// the local Steam process has gone away
    /// </summary>
    SteamGone = 0,

    /// <summary>
    /// <para>
    /// the network connection to Steam has been broken, or was already broken
    /// </para>
    ///
    /// <para>
    /// SteamServersDisconnected_t callback will be sent around the same time<br/>
    /// SteamServersConnected_t will be sent when the client is able to talk to the Steam servers again
    /// </para>
    /// </summary>
    NetworkFailure = 1,

    /// <summary>
    /// the SteamAPICall_t handle passed in no longer exists
    /// </summary>
    InvalidHandle = 2,

    /// <summary>
    /// GetAPICallResult() was called with the wrong callback type for this API call
    /// </summary>
    MismatchedCallback = 3,
}
