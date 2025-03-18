// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public enum ESteamIPv6ConnectivityProtocol : int
{
    Invalid = 0,

    /// <summary>
    /// because a proxy may make this different than other protocols
    /// </summary>
    HTTP = 1,

    /// <summary>
    /// test UDP connectivity. Uses a port that is commonly needed for other Steam stuff. If UDP works, TCP probably works.
    /// </summary>
    UDP = 2,
}
