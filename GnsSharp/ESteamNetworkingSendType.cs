// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;

/// <summary>
/// Flags used to set options for message sending
/// </summary>
[Flags]
public enum ESteamNetworkingSendType : int
{
    /// <summary>
    /// <para>
    /// Send the message unreliably. Can be lost.  Messages *can* be larger than a<br/>
    /// single MTU (UDP packet), but there is no retransmission, so if any piece<br/>
    /// of the message is lost, the entire message will be dropped.
    /// </para>
    ///
    /// <para>
    /// The sending API does have some knowledge of the underlying connection, so<br/>
    /// if there is no NAT-traversal accomplished or there is a recognized adjustment<br/>
    /// happening on the connection, the packet will be batched until the connection<br/>
    /// is open again.
    /// </para>
    /// </summary>
    Unreliable = 0,

    /// <summary>
    /// <para>
    /// Disable Nagle's algorithm.<br/>
    /// By default, Nagle's algorithm is applied to all outbound messages.  This means<br/>
    /// that the message will NOT be sent immediately, in case further messages are<br/>
    /// sent soon after you send this, which can be grouped together.  Any time there<br/>
    /// is enough buffered data to fill a packet, the packets will be pushed out immediately,<br/>
    /// but partially-full packets not be sent until the Nagle timer expires.  See<br/>
    /// <see cref="ISteamNetworkingSockets.FlushMessagesOnConnection"/>, ISteamNetworkingMessages::FlushMessagesToUser
    /// </para>
    ///
    /// <para>
    /// NOTE: Don't just send every message without Nagle because you want packets to get there<br/>
    /// quicker.  Make sure you understand the problem that Nagle is solving before disabling it.<br/>
    /// If you are sending small messages, often many at the same time, then it is very likely that<br/>
    /// it will be more efficient to leave Nagle enabled.  A typical proper use of this flag is<br/>
    /// when you are sending what you know will be the last message sent for a while (e.g. the last<br/>
    /// in the server simulation tick to a particular client), and you use this flag to flush all<br/>
    /// messages.
    /// </para>
    /// </summary>
    NoNagle = 1,

    /// <summary>
    /// Send a message unreliably, bypassing Nagle's algorithm for this message and any messages<br/>
    /// currently pending on the Nagle timer.  This is equivalent to using k_ESteamNetworkingSend_Unreliable<br/>
    /// and then immediately flushing the messages using <see cref="ISteamNetworkingSockets.FlushMessagesOnConnection"/><br/>
    /// or ISteamNetworkingMessages::FlushMessagesToUser.  (But using this flag is more efficient since you<br/>
    /// only make one API call.)
    /// </summary>
    UnreliableNoNagle = Unreliable | NoNagle,

    /// <summary>
    /// If the message cannot be sent very soon (because the connection is still doing some initial<br/>
    /// handshaking, route negotiations, etc), then just drop it.  This is only applicable for unreliable<br/>
    /// messages.  Using this flag on reliable messages is invalid.
    /// </summary>
    NoDelay = 4,

    /// <summary>
    /// <para>
    /// Send an unreliable message, but if it cannot be sent relatively quickly, just drop it instead of queuing it.<br/>
    /// This is useful for messages that are not useful if they are excessively delayed, such as voice data.<br/>
    /// NOTE: The Nagle algorithm is not used, and if the message is not dropped, any messages waiting on the<br/>
    /// Nagle timer are immediately flushed.
    /// </para>
    ///
    /// <para>
    /// A message will be dropped under the following circumstances:<br/>
    /// - the connection is not fully connected.  (E.g. the "Connecting" or "FindingRoute" states)<br/>
    /// - there is a sufficiently large number of messages queued up already such that the current message<br/>
    ///   will not be placed on the wire in the next ~200ms or so.
    /// </para>
    ///
    /// <para>
    /// If a message is dropped for these reasons, <see cref="EResult.Ignored"/> will be returned.
    /// </para>
    /// </summary>
    UnreliableNoDelay = Unreliable | NoDelay | NoNagle,

    /// <summary>
    /// <para>
    /// Reliable message send. Can send up to <see cref="ISteamNetworkingSockets.MaxMessageSizeSend"/> bytes in a single message.<br/>
    /// Does fragmentation/re-assembly of messages under the hood, as well as a sliding window for<br/>
    /// efficient sends of large chunks of data.
    /// </para>
    ///
    /// <para>
    /// The Nagle algorithm is used.  See notes on <see cref="Unreliable"/> for more details.<br/>
    /// See <see cref="ReliableNoNagle"/>, <see cref="ISteamNetworkingSockets.FlushMessagesOnConnection"/>,<br/>
    /// ISteamNetworkingMessages::FlushMessagesToUser
    /// </para>
    /// </summary>
    Reliable = 8,

    /// <summary>
    /// Send a message reliably, but bypass Nagle's algorithm.
    /// </summary>
    ReliableNoNagle = Reliable | NoNagle,

    /// <summary>
    /// <para>
    /// By default, message sending is queued, and the work of encryption and talking to<br/>
    /// the operating system sockets, etc is done on a service thread.  This is usually a<br/>
    /// a performance win when messages are sent from the "main thread".  However, if this<br/>
    /// flag is set, and data is ready to be sent immediately (either from this message<br/>
    /// or earlier queued data), then that work will be done in the current thread, before<br/>
    /// the current call returns.  If data is not ready to be sent (due to rate limiting<br/>
    /// or Nagle), then this flag has no effect.
    /// </para>
    ///
    /// <para>
    /// This is an advanced flag used to control performance at a very low level.  For<br/>
    /// most applications running on modern hardware with more than one CPU core, doing<br/>
    /// the work of sending on a service thread will yield the best performance.  Only<br/>
    /// use this flag if you have a really good reason and understand what you are doing.<br/>
    /// Otherwise you will probably just make performance worse.
    /// </para>
    /// </summary>
    UseCurrentThread = 16,

    /// <summary>
    /// <para>
    /// When sending a message using <see cref="ISteamNetworkingMessages"/>, automatically re-establish<br/>
    /// a broken session, without returning <see cref="EResult.NoConnection"/>.  Without this flag,<br/>
    /// if you attempt to send a message, and the session was proactively closed by the<br/>
    /// peer, or an error occurred that disrupted communications, then you must close the<br/>
    /// session using <see cref="ISteamNetworkingMessages.CloseSessionWithUser"/> before attempting to<br/>
    /// send another message.  (Or you can simply add this flag and retry.)  In this way,<br/>
    /// the disruption cannot go unnoticed, and a more clear order of events can be<br/>
    /// ascertained. This is especially important when reliable messages are used, since<br/>
    /// if the connection is disrupted, some of those messages will not have been delivered,<br/>
    /// and it is in general not possible to know which.  Although a<br/>
    /// <see cref="SteamNetworkingMessagesSessionFailed_t"/> callback will be posted when an error occurs<br/>
    /// to notify you that a failure has happened, callbacks are asynchronous, so it is not<br/>
    /// possible to tell exactly when it happened.  And because the primary purpose of<br/>
    /// <see cref="ISteamNetworkingMessages"/> is to be like UDP, there is no notification when a peer closes<br/>
    /// the session.
    /// </para>
    ///
    /// <para>
    /// If you are not using any reliable messages (e.g. you are using <see cref="ISteamNetworkingMessages"/><br/>
    /// exactly as a transport replacement for UDP-style datagrams only), you may not need to<br/>
    /// know when an underlying connection fails, and so you may not need this notification.
    /// </para>
    /// </summary>
    AutoRestartBrokenSession = 32,
}
