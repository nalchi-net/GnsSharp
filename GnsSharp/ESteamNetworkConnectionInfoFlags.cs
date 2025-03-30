// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;

[Flags]
public enum ESteamNetworkConnectionInfoFlags : int
{
    /// <summary>
    /// We don't have a certificate for the remote host.
    /// </summary>
    Unauthenticated = 1,

    /// <summary>
    /// Information is being sent out over a wire unencrypted (by this library)
    /// </summary>
    Unencrypted = 2,

    /// <summary>
    /// Internal loopback buffers.  Won't be true for localhost.  (You can check the address to determine that.)  This implies FastLAN
    /// </summary>
    LoopbackBuffers = 4,

    /// <summary>
    /// The connection is "fast" and "reliable".  Either internal/localhost (check the address to find out), or the peer is on the same LAN.  (Probably.  It's based on the address and the ping time, this is actually hard to determine unambiguously).
    /// </summary>
    Fast = 8,

    /// <summary>
    /// The connection is relayed somehow (SDR or TURN).
    /// </summary>
    Relayed = 16,

    /// <summary>
    /// We're taking advantage of dual-wifi multi-path
    /// </summary>
    DualWifi = 32,
}
