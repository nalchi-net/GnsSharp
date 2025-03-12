// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// A struct used to describe our readiness to use the relay network.<br/>
/// To do this we first need to fetch the network configuration,<br/>
/// which describes what POPs are available.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamRelayNetworkStatus_t
{
    public const int Callback = Constants.SteamNetworkingUtilsCallbacks + 1;

    /// <summary>
    /// Summary status.  When this is "current", initialization has<br/>
    /// completed.  Anything else means you are not ready yet, or<br/>
    /// there is a significant problem.
    /// </summary>
    public ESteamNetworkingAvailability Avail;

    /// <summary>
    /// Nonzero if latency measurement is in progress (or pending,<br/>
    /// awaiting a prerequisite).
    /// </summary>
    public int PingMeasurementInProgress;

    /// <summary>
    /// <para>
    /// Status obtaining the network config.  This is a prerequisite<br/>
    /// for relay network access.
    /// </para>
    ///
    /// <para>
    /// Failure to obtain the network config almost always indicates<br/>
    /// a problem with the local internet connection.
    /// </para>
    /// </summary>
    public ESteamNetworkingAvailability AvailNetworkConfig;

    /// <summary>
    /// Current ability to communicate with ANY relay.  Note that<br/>
    /// the complete failure to communicate with any relays almost<br/>
    /// always indicates a problem with the local Internet connection.<br/>
    /// (However, just because you can reach a single relay doesn't<br/>
    /// mean that the local connection is in perfect health.)
    /// </summary>
    public ESteamNetworkingAvailability AvailAnyRelay;

    /// <summary>
    /// Non-localized English language status.  For diagnostic/debugging<br/>
    /// purposes only.
    /// </summary>
    public Array256<byte> DebugMsg;
}
