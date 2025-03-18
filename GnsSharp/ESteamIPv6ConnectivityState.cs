// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// For the above transport protocol, what do we think the local machine's connectivity to the internet over ipv6 is like
/// </summary>
public enum ESteamIPv6ConnectivityState : int
{
    /// <summary>
    /// We haven't run a test yet
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// We have recently been able to make a request on ipv6 for the given protocol
    /// </summary>
    Good = 1,

    /// <summary>
    /// We failed to make a request, either because this machine has no ipv6 address assigned, or it has no upstream connectivity
    /// </summary>
    Bad = 2,
}
