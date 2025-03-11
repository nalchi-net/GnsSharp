// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// <para>Describe the status of a particular network resource.</para>
///
/// <para>Negative values indicate a problem.</para>
///
/// <para>
/// In general, we will not automatically retry unless you take some action that<br/>
/// depends on of requests this resource, such as querying the status, attempting<br/>
/// to initiate a connection, receive a connection, etc.  If you do not take any<br/>
/// action at all, we do not automatically retry in the background.
/// </para>
/// </summary>
public enum ESteamNetworkingAvailability : int
{
    /// <summary>
    /// A dependent resource is missing, so this service is unavailable.<br/>
    /// (E.g. we cannot talk to routers because Internet is down or we don't have the network config.)
    /// </summary>
    CannotTry = -102,

    /// <summary>
    /// We have tried for enough time that we would expect to have been successful by now.<br/>
    /// We have never been successful
    /// </summary>
    Failed = -101,

    /// <summary>
    /// We tried and were successful at one time, but now it looks like we have a problem
    /// </summary>
    Previously = -100,

    /// <summary>
    /// We previously failed and are currently retrying
    /// </summary>
    Retrying = -10,

    // Not a problem, but not ready either

    /// <summary>
    /// We don't know because we haven't ever checked/tried
    /// </summary>
    NeverTried = 1,

    /// <summary>
    /// We're waiting on a dependent resource to be acquired.<br/>
    /// (E.g. we cannot obtain a cert until we are logged into Steam.<br/>
    /// We cannot measure latency to relays until we have the network config.)
    /// </summary>
    Waiting = 2,

    /// <summary>
    /// We're actively trying now, but are not yet successful.
    /// </summary>
    Attempting = 3,

    /// <summary>
    /// Resource is online/available
    /// </summary>
    Current = 100,

    /// <summary>
    /// Internal dummy/sentinel, or value is not applicable in this context
    /// </summary>
    Unknown = 0,
}
