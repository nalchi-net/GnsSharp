// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

#pragma warning disable SA1202 // Elements should be ordered by access

/// <summary>
/// Quick status of a particular lane
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamNetConnectionRealTimeLaneStatus_t
{
    /// <summary>
    /// Counters for this particular lane.  See the corresponding variables
    /// in SteamNetConnectionRealTimeStatus_t
    /// </summary>
    public int PendingUnreliable;

    /// <summary>
    /// Counters for this particular lane.  See the corresponding variables
    /// in SteamNetConnectionRealTimeStatus_t
    /// </summary>
    public int PendingReliable;

    /// <summary>
    /// Counters for this particular lane.  See the corresponding variables
    /// in SteamNetConnectionRealTimeStatus_t
    /// </summary>
    public int SentUnackedReliable;

    /// <summary>
    /// Reserved for future use
    /// </summary>
    private int reservePad1;

    /// <summary>
    /// Lane-specific queue time.  This value takes into consideration lane priorities
    /// and weights, and how much data is queued in each lane, and attempts to predict
    /// how any data currently queued will be sent out.
    /// </summary>
    public SteamNetworkingMicroseconds QueueTime;

    /// <summary>
    /// Internal stuff, room to change API easily
    /// </summary>
    private Array10<uint> reserved;
}

#pragma warning restore SA1202 // Elements should be ordered by access
