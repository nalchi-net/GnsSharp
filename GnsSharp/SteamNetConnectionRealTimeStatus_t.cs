// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Quick connection state, pared down to something you could call<br/>
/// more frequently without it being too big of a perf hit.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamNetConnectionRealTimeStatus_t
{
    /// <summary>
    /// High level state of the connection
    /// </summary>
    public ESteamNetworkingConnectionState State;

    /// <summary>
    /// Current ping (ms)
    /// </summary>
    public int Ping;

    /// <summary>
    /// Connection quality measured locally, 0...1.  (Percentage of packets delivered<br/>
    /// end-to-end in order).
    /// </summary>
    public float ConnectionQualityLocal;

    /// <summary>
    /// Packet delivery success rate as observed from remote host
    /// </summary>
    public float ConnectionQualityRemote;

    /// <summary>
    /// Current data rates from recent history.
    /// </summary>
    public float OutPacketsPerSec;

    /// <summary>
    /// Current data rates from recent history.
    /// </summary>
    public float OutBytesPerSec;

    /// <summary>
    /// Current data rates from recent history.
    /// </summary>
    public float InPacketsPerSec;

    /// <summary>
    /// Current data rates from recent history.
    /// </summary>
    public float InBytesPerSec;

    /// <summary>
    /// Estimate rate that we believe that we can send data to our peer.<br/>
    /// Note that this could be significantly higher than m_flOutBytesPerSec,<br/>
    /// meaning the capacity of the channel is higher than you are sending data.<br/>
    /// (That's OK!)
    /// </summary>
    public int SendRateBytesPerSecond;

    /// <summary>
    /// Number of bytes pending to be sent.  This is data that you have recently<br/>
    /// requested to be sent but has not yet actually been put on the wire.  The<br/>
    /// reliable number ALSO includes data that was previously placed on the wire,<br/>
    /// but has now been scheduled for re-transmission.  Thus, it's possible to<br/>
    /// observe m_cbPendingReliable increasing between two checks, even if no<br/>
    /// calls were made to send reliable data between the checks.  Data that is<br/>
    /// awaiting the Nagle delay will appear in these numbers.
    /// </summary>
    public int PendingUnreliable;

    /// <summary>
    /// Number of bytes pending to be sent.  This is data that you have recently<br/>
    /// requested to be sent but has not yet actually been put on the wire.  The<br/>
    /// reliable number ALSO includes data that was previously placed on the wire,<br/>
    /// but has now been scheduled for re-transmission.  Thus, it's possible to<br/>
    /// observe m_cbPendingReliable increasing between two checks, even if no<br/>
    /// calls were made to send reliable data between the checks.  Data that is<br/>
    /// awaiting the Nagle delay will appear in these numbers.
    /// </summary>
    public int PendingReliable;

    /// <summary>
    /// Number of bytes of reliable data that has been placed the wire, but<br/>
    /// for which we have not yet received an acknowledgment, and thus we may<br/>
    /// have to re-transmit.
    /// </summary>
    public int SentUnackedReliable;

    /// <summary>
    /// <para>
    /// If you queued a message right now, approximately how long would that message<br/>
    /// wait in the queue before we actually started putting its data on the wire in<br/>
    /// a packet?
    /// </para>
    ///
    /// <para>
    /// In general, data that is sent by the application is limited by the bandwidth<br/>
    /// of the channel.  If you send data faster than this, it must be queued and<br/>
    /// put on the wire at a metered rate.  Even sending a small amount of data (e.g.<br/>
    /// a few MTU, say ~3k) will require some of the data to be delayed a bit.
    /// </para>
    ///
    /// <para>
    /// Ignoring multiple lanes, the estimated delay will be approximately equal to
    /// </para>
    ///
    /// <para>
    ///        ( m_cbPendingUnreliable+m_cbPendingReliable ) / m_nSendRateBytesPerSecond
    /// </para>
    ///
    /// <para>
    /// plus or minus one MTU.  It depends on how much time has elapsed since the last<br/>
    /// packet was put on the wire.  For example, the queue might have *just* been emptied,<br/>
    /// and the last packet placed on the wire, and we are exactly up against the send<br/>
    /// rate limit.  In that case we might need to wait for one packet's worth of time to<br/>
    /// elapse before we can send again.  On the other extreme, the queue might have data<br/>
    /// in it waiting for Nagle.  (This will always be less than one packet, because as<br/>
    /// soon as we have a complete packet we would send it.)  In that case, we might be<br/>
    /// ready to send data now, and this value will be 0.
    /// </para>
    ///
    /// <para>
    /// This value is only valid if multiple lanes are not used.  If multiple lanes are<br/>
    /// in use, then the queue time will be different for each lane, and you must use<br/>
    /// the value in SteamNetConnectionRealTimeLaneStatus_t.
    /// </para>
    ///
    /// <para>
    /// Nagle delay is ignored for the purposes of this calculation.
    /// </para>
    /// </summary>
    public SteamNetworkingMicroseconds QueueTime;

    /// <summary>
    /// Internal stuff, room to change API easily
    /// </summary>
    private Array16<uint> reserved;
}
