// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// lobby search distance. Lobby results are sorted from closest to farthest.
/// </summary>
public enum ELobbyDistanceFilter : int
{
    /// <summary>
    /// only lobbies in the same immediate region will be returned
    /// </summary>
    Close,

    /// <summary>
    /// only lobbies in the same region or near by regions
    /// </summary>
    Default,

    /// <summary>
    /// for games that don't have many latency requirements, will return lobbies about half-way around the globe
    /// </summary>
    Far,

    /// <summary>
    /// no filtering, will match lobbies as far as India to NY (not recommended, expect multiple seconds of latency between the clients)
    /// </summary>
    Worldwide,
}
