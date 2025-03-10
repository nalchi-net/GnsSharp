// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// "Fake IPs" are assigned to hosts, to make it easier to interface with
/// older code that assumed all hosts will have an IPv4 address
/// </summary>
public enum ESteamNetworkingFakeIPType : int
{
    /// <summary>
    /// Error, argument was not even an IP address, etc.
    /// </summary>
    Invalid,

    /// <summary>
    /// Argument was a valid IP, but was not from the reserved "fake" range
    /// </summary>
    NotFake,

    /// <summary>
    /// Globally unique (for a given app) IPv4 address.  Address space managed by Steam
    /// </summary>
    GlobalIPv4,

    /// <summary>
    /// Locally unique IPv4 address.  Address space managed by the local process.  For internal use only; should not be shared!
    /// </summary>
    LocalIPv4,
}
