// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

/// <summary>
/// <para>
/// Lower level networking API.
/// </para>
///
/// <para>
/// - Connection-oriented API (like TCP, not UDP).  When sending and receiving<br/>
///   messages, a connection handle is used.  (For a UDP-style interface, where<br/>
///   the peer is identified by their address with each send/recv call, see<br/>
///   ISteamNetworkingMessages.)  The typical pattern is for a "server" to "listen"<br/>
///   on a "listen socket."  A "client" will "connect" to the server, and the<br/>
///   server will "accept" the connection.  If you have a symmetric situation<br/>
///   where either peer may initiate the connection and server/client roles are<br/>
///   not clearly defined, check out k_ESteamNetworkingConfig_SymmetricConnect.<br/>
/// - But unlike TCP, it's message-oriented, not stream-oriented.<br/>
/// - Mix of reliable and unreliable messages<br/>
/// - Fragmentation and reassembly<br/>
/// - Supports connectivity over plain UDP<br/>
/// - Also supports SDR ("Steam Datagram Relay") connections, which are<br/>
///   addressed by the identity of the peer.  There is a "P2P" use case and<br/>
///   a "hosted dedicated server" use case.
/// </para>
///
/// <para>
/// Note that neither of the terms "connection" nor "socket" necessarily correspond<br/>
/// one-to-one with an underlying UDP socket.  An attempt has been made to<br/>
/// keep the semantics as similar to the standard socket model when appropriate,<br/>
/// but some deviations do exist.
/// </para>
///
/// <para>
/// See also: ISteamNetworkingMessages, the UDP-style interface.  This API might be<br/>
/// easier to use, especially when porting existing UDP code.
/// </para>
/// </summary>
public class ISteamNetworkingSockets
{
    private const int MaxUtf8StrBufSize = 1024;

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamNetworkingSockets(bool isGameServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Debug.Assert(!isGameServer, "Open source GNS doesn't have GameServer API");
        this.ptr = Native.SteamAPI_SteamNetworkingSockets_v009();
#elif GNS_SHARP_STEAMWORKS_SDK
        if (isGameServer)
        {
            this.ptr = Native.SteamAPI_SteamGameServerNetworkingSockets_SteamAPI_v012();
        }
        else
        {
            this.ptr = Native.SteamAPI_SteamNetworkingSockets_SteamAPI_v012();
        }
#endif
    }

    public static ISteamNetworkingSockets? User { get; internal set; }

    public static ISteamNetworkingSockets? GameServer { get; internal set; }

    /// <summary>
    /// <para>
    /// Creates a "server" socket that listens for clients to connect to by<br/>
    /// calling ConnectByIPAddress, over ordinary UDP (IPv4 or IPv6)
    /// </para>
    ///
    /// <para>
    /// You must select a specific local port to listen on and set it<br/>
    /// the port field of the local address.
    /// </para>
    ///
    /// <para>
    /// Usually you will set the IP portion of the address to zero (<see cref="SteamNetworkingIPAddr.Clear"/>).<br/>
    /// This means that you will not bind to any particular local interface (i.e. the same<br/>
    /// as <c>INADDR_ANY</c> in plain socket code).  Furthermore, if possible the socket will be bound<br/>
    /// in "dual stack" mode, which means that it can accept both IPv4 and IPv6 client connections.<br/>
    /// If you really do wish to bind a particular interface, then set the local address to the<br/>
    /// appropriate IPv4 or IPv6 IP.
    /// </para>
    ///
    /// <para>
    /// If you need to set any initial config options, pass them here.  See<br/>
    /// SteamNetworkingConfigValue_t for more about why this is preferable to<br/>
    /// setting the options "immediately" after creation.
    /// </para>
    ///
    /// <para>
    /// When a client attempts to connect, a <see cref="SteamNetConnectionStatusChangedCallback_t"/><br/>
    /// will be posted.  The connection will be in the connecting state.
    /// </para>
    /// </summary>
    public HSteamListenSocket CreateListenSocketIP(in SteamNetworkingIPAddr localAddress, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_CreateListenSocketIP(this.ptr, localAddress, options.Length, options);
    }

    /// <summary>
    /// <para>
    /// Creates a connection and begins talking to a "server" over UDP at the<br/>
    /// given IPv4 or IPv6 address.  The remote host must be listening with a<br/>
    /// matching call to CreateListenSocketIP on the specified port.
    /// </para>
    ///
    /// <para>
    /// A SteamNetConnectionStatusChangedCallback_t callback will be triggered when we start<br/>
    /// connecting, and then another one on either timeout or successful connection.
    /// </para>
    ///
    /// <para>
    /// If the server does not have any identity configured, then their network address<br/>
    /// will be the only identity in use.  Or, the network host may provide a platform-specific<br/>
    /// identity with or without a valid certificate to authenticate that identity.  (These<br/>
    /// details will be contained in the SteamNetConnectionStatusChangedCallback_t.)  It's<br/>
    /// up to your application to decide whether to allow the connection.
    /// </para>
    ///
    /// <para>
    /// By default, all connections will get basic encryption sufficient to prevent<br/>
    /// casual eavesdropping.  But note that without certificates (or a shared secret<br/>
    /// distributed through some other out-of-band mechanism), you don't have any<br/>
    /// way of knowing who is actually on the other end, and thus are vulnerable to<br/>
    /// man-in-the-middle attacks.
    /// </para>
    ///
    /// <para>
    /// If you need to set any initial config options, pass them here.  See<br/>
    /// SteamNetworkingConfigValue_t for more about why this is preferable to<br/>
    /// setting the options "immediately" after creation.
    /// </para>
    /// </summary>
    public HSteamNetConnection ConnectByIPAddress(in SteamNetworkingIPAddr address, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ConnectByIPAddress(this.ptr, address, options.Length, options);
    }

    /// <summary>
    /// <para>
    /// Like CreateListenSocketIP, but clients will connect using ConnectP2P.
    /// </para>
    ///
    /// <para>
    /// nLocalVirtualPort specifies how clients can connect to this socket using<br/>
    /// ConnectP2P.  It's very common for applications to only have one listening socket;<br/>
    /// in that case, use zero.  If you need to open multiple listen sockets and have clients<br/>
    /// be able to connect to one or the other, then nLocalVirtualPort should be a small<br/>
    /// integer (&lt;1000) unique to each listen socket you create.
    /// </para>
    ///
    /// <para>
    /// If you use this, you probably want to call ISteamNetworkingUtils::InitRelayNetworkAccess()<br/>
    /// when your app initializes.
    /// </para>
    ///
    /// <para>
    /// If you are listening on a dedicated servers in known data center,<br/>
    /// then you can listen using this function instead of CreateHostedDedicatedServerListenSocket,<br/>
    /// to allow clients to connect without a ticket.  Any user that owns<br/>
    /// the app and is signed into Steam will be able to attempt to connect to<br/>
    /// your server.  Also, a connection attempt may require the client to<br/>
    /// be connected to Steam, which is one more moving part that may fail.  When<br/>
    /// tickets are used, then once a ticket is obtained, a client can connect to<br/>
    /// your server even if they got disconnected from Steam or Steam is offline.
    /// </para>
    ///
    /// <para>
    /// If you need to set any initial config options, pass them here.  See<br/>
    /// SteamNetworkingConfigValue_t for more about why this is preferable to<br/>
    /// setting the options "immediately" after creation.
    /// </para>
    /// </summary>
    public HSteamListenSocket CreateListenSocketP2P(int localVirtualPort, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2P(this.ptr, localVirtualPort, options.Length, options);
    }

    /// <summary>
    /// <para>
    /// Begin connecting to a peer that is identified using a platform-specific identifier.<br/>
    /// This uses the default rendezvous service, which depends on the platform and library<br/>
    /// configuration.  (E.g. on Steam, it goes through the steam backend.)
    /// </para>
    ///
    /// <para>
    /// If you need to set any initial config options, pass them here.  See<br/>
    /// SteamNetworkingConfigValue_t for more about why this is preferable to<br/>
    /// setting the options "immediately" after creation.
    /// </para>
    ///
    /// <para>
    /// To use your own signaling service, see:<br/>
    /// - ConnectP2PCustomSignaling<br/>
    /// - k_ESteamNetworkingConfig_Callback_CreateConnectionSignaling
    /// </para>
    /// </summary>
    public HSteamNetConnection ConnectP2P(in SteamNetworkingIdentity identityRemote, int remoteVirtualPort, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ConnectP2P(this.ptr, identityRemote, remoteVirtualPort, options.Length, options);
    }

    /// <summary>
    /// <para>
    /// Accept an incoming connection that has been received on a listen socket.
    /// </para>
    ///
    /// <para>
    /// When a connection attempt is received (perhaps after a few basic handshake<br/>
    /// packets have been exchanged to prevent trivial spoofing), a connection interface<br/>
    /// object is created in the k_ESteamNetworkingConnectionState_Connecting state<br/>
    /// and a SteamNetConnectionStatusChangedCallback_t is posted.  At this point, your<br/>
    /// application MUST either accept or close the connection.  (It may not ignore it.)<br/>
    /// Accepting the connection will transition it either into the connected state,<br/>
    /// or the finding route state, depending on the connection type.
    /// </para>
    ///
    /// <para>
    /// You should take action within a second or two, because accepting the connection is<br/>
    /// what actually sends the reply notifying the client that they are connected.  If you<br/>
    /// delay taking action, from the client's perspective it is the same as the network<br/>
    /// being unresponsive, and the client may timeout the connection attempt.  In other<br/>
    /// words, the client cannot distinguish between a delay caused by network problems<br/>
    /// and a delay caused by the application.
    /// </para>
    ///
    /// <para>
    /// This means that if your application goes for more than a few seconds without<br/>
    /// processing callbacks (for example, while loading a map), then there is a chance<br/>
    /// that a client may attempt to connect in that interval and fail due to timeout.
    /// </para>
    ///
    /// <para>
    /// If the application does not respond to the connection attempt in a timely manner,<br/>
    /// and we stop receiving communication from the client, the connection attempt will<br/>
    /// be timed out locally, transitioning the connection to the<br/>
    /// k_ESteamNetworkingConnectionState_ProblemDetectedLocally state.  The client may also<br/>
    /// close the connection before it is accepted, and a transition to the<br/>
    /// k_ESteamNetworkingConnectionState_ClosedByPeer is also possible depending the exact<br/>
    /// sequence of events.
    /// </para>
    ///
    /// <para>
    /// Returns k_EResultInvalidParam if the handle is invalid.<br/>
    /// Returns k_EResultInvalidState if the connection is not in the appropriate state.<br/>
    /// (Remember that the connection state could change in between the time that the<br/>
    /// notification being posted to the queue and when it is received by the application.)
    /// </para>
    ///
    /// <para>
    /// A note about connection configuration options.  If you need to set any configuration<br/>
    /// options that are common to all connections accepted through a particular listen<br/>
    /// socket, consider setting the options on the listen socket, since such options are<br/>
    /// inherited automatically.  If you really do need to set options that are connection<br/>
    /// specific, it is safe to set them on the connection before accepting the connection.
    /// </para>
    /// </summary>
    public EResult AcceptConnection(HSteamNetConnection conn)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_AcceptConnection(this.ptr, conn);
    }

    /// <summary>
    /// <para>
    /// Disconnects from the remote host and invalidates the connection handle.<br/>
    /// Any unread data on the connection is discarded.
    /// </para>
    ///
    /// <para>
    /// nReason is an application defined code that will be received on the other<br/>
    /// end and recorded (when possible) in backend analytics.  The value should<br/>
    /// come from a restricted range.  (See `ESteamNetConnectionEnd`.)  If you don't need<br/>
    /// to communicate any information to the remote host, and do not want analytics to<br/>
    /// be able to distinguish "normal" connection terminations from "exceptional" ones,<br/>
    /// You may pass zero, in which case the generic value of<br/>
    /// k_ESteamNetConnectionEnd_App_Generic will be used.
    /// </para>
    ///
    /// <para>
    /// pszDebug is an optional human-readable diagnostic string that will be received<br/>
    /// by the remote host and recorded (when possible) in backend analytics.
    /// </para>
    ///
    /// <para>
    /// If you wish to put the socket into a "linger" state, where an attempt is made to<br/>
    /// flush any remaining sent data, use bEnableLinger=true.  Otherwise reliable data<br/>
    /// is not flushed.
    /// </para>
    ///
    /// <para>
    /// If the connection has already ended and you are just freeing up the<br/>
    /// connection interface, the reason code, debug string, and linger flag are<br/>
    /// ignored.
    /// </para>
    /// </summary>
    public bool CloseConnection(HSteamNetConnection peer, int reason, string? debug, bool enableLinger)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_CloseConnection(this.ptr, peer, reason, debug, enableLinger);
    }

    /// <summary>
    /// Destroy a listen socket.  All the connections that were accepting on the listen<br/>
    /// socket are closed ungracefully.
    /// </summary>
    public bool CloseListenSocket(HSteamListenSocket socket)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_CloseListenSocket(this.ptr, socket);
    }

    /// <summary>
    /// <para>
    /// Set connection user data.  the data is returned in the following places<br/>
    /// - You can query it using GetConnectionUserData.<br/>
    /// - The SteamNetworkingmessage_t structure.<br/>
    /// - The SteamNetConnectionInfo_t structure.<br/>
    /// (Which is a member of SteamNetConnectionStatusChangedCallback_t -- but see WARNINGS below!!!!)
    /// </para>
    ///
    /// <para>
    /// Do you need to set this atomically when the connection is created?<br/>
    /// See k_ESteamNetworkingConfig_ConnectionUserData.
    /// </para>
    ///
    /// <para>
    /// WARNING: Be *very careful* when using the value provided in callbacks structs.<br/>
    /// Callbacks are queued, and the value that you will receive in your<br/>
    /// callback is the userdata that was effective at the time the callback<br/>
    /// was queued.  There are subtle race conditions that can happen if you<br/>
    /// don't understand this!
    /// </para>
    ///
    /// <para>
    /// If any incoming messages for this connection are queued, the userdata<br/>
    /// field is updated, so that when when you receive messages (e.g. with<br/>
    /// ReceiveMessagesOnConnection), they will always have the very latest<br/>
    /// userdata.  So the tricky race conditions that can happen with callbacks<br/>
    /// do not apply to retrieving messages.
    /// </para>
    ///
    /// <para>
    /// Returns false if the handle is invalid.
    /// </para>
    /// </summary>
    public bool SetConnectionUserData(HSteamNetConnection peer, long userData)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_SetConnectionUserData(this.ptr, peer, userData);
    }

    /// <summary>
    /// Fetch connection user data.  Returns -1 if handle is invalid<br/>
    /// or if you haven't set any userdata on the connection.
    /// </summary>
    public long GetConnectionUserData(HSteamNetConnection peer)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetConnectionUserData(this.ptr, peer);
    }

    /// <summary>
    /// Set a name for the connection, used mostly for debugging
    /// </summary>
    public void SetConnectionName(HSteamNetConnection peer, string name)
    {
        Native.SteamAPI_ISteamNetworkingSockets_SetConnectionName(this.ptr, peer, name);
    }

    /// <summary>
    /// Fetch connection name.  Returns false if handle is invalid
    /// </summary>
    public bool GetConnectionName(HSteamNetConnection hPeer, out string name)
    {
        Span<byte> raw = stackalloc byte[MaxUtf8StrBufSize];
        bool success = Native.SteamAPI_ISteamNetworkingSockets_GetConnectionName(this.ptr, hPeer, raw, MaxUtf8StrBufSize);
        name = success ? Utf8StringHelper.NullTerminatedSpanToString(raw) : String.Empty;

        return success;
    }

    /// <summary>
    /// <para>
    /// Send a message to the remote host on the specified connection.
    /// </para>
    ///
    /// <para>
    /// nSendFlags determines the delivery guarantees that will be provided,<br/>
    /// when data should be buffered, etc.  E.g. k_nSteamNetworkingSend_Unreliable
    /// </para>
    ///
    /// <para>
    /// Note that the semantics we use for messages are not precisely<br/>
    /// the same as the semantics of a standard "stream" socket.<br/>
    /// (SOCK_STREAM)  For an ordinary stream socket, the boundaries<br/>
    /// between chunks are not considered relevant, and the sizes of<br/>
    /// the chunks of data written will not necessarily match up to<br/>
    /// the sizes of the chunks that are returned by the reads on<br/>
    /// the other end.  The remote host might read a partial chunk,<br/>
    /// or chunks might be coalesced.  For the message semantics<br/>
    /// used here, however, the sizes WILL match.  Each send call<br/>
    /// will match a successful read call on the remote host<br/>
    /// one-for-one.  If you are porting existing stream-oriented<br/>
    /// code to the semantics of reliable messages, your code should<br/>
    /// work the same, since reliable message semantics are more<br/>
    /// strict than stream semantics.  The only caveat is related to<br/>
    /// performance: there is per-message overhead to retain the<br/>
    /// message sizes, and so if your code sends many small chunks<br/>
    /// of data, performance will suffer. Any code based on stream<br/>
    /// sockets that does not write excessively small chunks will<br/>
    /// work without any changes.
    /// </para>
    ///
    /// <para>
    /// The pOutMessageNumber is an optional pointer to receive the<br/>
    /// message number assigned to the message, if sending was successful.
    /// </para>
    ///
    /// <para>
    /// Returns:<br/>
    /// - k_EResultInvalidParam: invalid connection handle, or the individual message is too big.<br/>
    /// (See k_cbMaxSteamNetworkingSocketsMessageSizeSend)<br/>
    /// - k_EResultInvalidState: connection is in an invalid state<br/>
    /// - k_EResultNoConnection: connection has ended<br/>
    /// - k_EResultIgnored: You used k_nSteamNetworkingSend_NoDelay, and the message was dropped because<br/>
    /// we were not ready to send it.<br/>
    /// - k_EResultLimitExceeded: there was already too much data queued to be sent.<br/>
    /// (See k_ESteamNetworkingConfig_SendBufferSize)
    /// </para>
    /// </summary>
    public EResult SendMessageToConnection(HSteamNetConnection conn, ReadOnlySpan<byte> data, ESteamNetworkingSendType sendFlags, out long outMessageNumber)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_SendMessageToConnection(this.ptr, conn, data, (uint)data.Length, sendFlags, out outMessageNumber);
    }

    /// <summary>
    /// <para>
    /// Send a message to the remote host on the specified connection.
    /// </para>
    ///
    /// <para>
    /// nSendFlags determines the delivery guarantees that will be provided,<br/>
    /// when data should be buffered, etc.  E.g. k_nSteamNetworkingSend_Unreliable
    /// </para>
    ///
    /// <para>
    /// Note that the semantics we use for messages are not precisely<br/>
    /// the same as the semantics of a standard "stream" socket.<br/>
    /// (SOCK_STREAM)  For an ordinary stream socket, the boundaries<br/>
    /// between chunks are not considered relevant, and the sizes of<br/>
    /// the chunks of data written will not necessarily match up to<br/>
    /// the sizes of the chunks that are returned by the reads on<br/>
    /// the other end.  The remote host might read a partial chunk,<br/>
    /// or chunks might be coalesced.  For the message semantics<br/>
    /// used here, however, the sizes WILL match.  Each send call<br/>
    /// will match a successful read call on the remote host<br/>
    /// one-for-one.  If you are porting existing stream-oriented<br/>
    /// code to the semantics of reliable messages, your code should<br/>
    /// work the same, since reliable message semantics are more<br/>
    /// strict than stream semantics.  The only caveat is related to<br/>
    /// performance: there is per-message overhead to retain the<br/>
    /// message sizes, and so if your code sends many small chunks<br/>
    /// of data, performance will suffer. Any code based on stream<br/>
    /// sockets that does not write excessively small chunks will<br/>
    /// work without any changes.
    /// </para>
    ///
    /// <para>
    /// The pOutMessageNumber is an optional pointer to receive the<br/>
    /// message number assigned to the message, if sending was successful.
    /// </para>
    ///
    /// <para>
    /// Returns:<br/>
    /// - k_EResultInvalidParam: invalid connection handle, or the individual message is too big.<br/>
    /// (See k_cbMaxSteamNetworkingSocketsMessageSizeSend)<br/>
    /// - k_EResultInvalidState: connection is in an invalid state<br/>
    /// - k_EResultNoConnection: connection has ended<br/>
    /// - k_EResultIgnored: You used k_nSteamNetworkingSend_NoDelay, and the message was dropped because<br/>
    /// we were not ready to send it.<br/>
    /// - k_EResultLimitExceeded: there was already too much data queued to be sent.<br/>
    /// (See k_ESteamNetworkingConfig_SendBufferSize)
    /// </para>
    /// </summary>
    public EResult SendMessageToConnection(HSteamNetConnection conn, ReadOnlySpan<byte> data, ESteamNetworkingSendType sendFlags)
    {
        return this.SendMessageToConnection(conn, data, sendFlags, out Unsafe.NullRef<long>());
    }

    /// <summary>
    /// <para>
    /// Send one or more messages without copying the message payload.<br/>
    /// This is the most efficient way to send messages. To use this<br/>
    /// function, you must first allocate a message object using<br/>
    /// ISteamNetworkingUtils::AllocateMessage.  (Do not declare one<br/>
    /// on the stack or allocate your own.)
    /// </para>
    ///
    /// <para>
    /// You should fill in the message payload.  You can either let<br/>
    /// it allocate the buffer for you and then fill in the payload,<br/>
    /// or if you already have a buffer allocated, you can just point<br/>
    /// m_pData at your buffer and set the callback to the appropriate function<br/>
    /// to free it.  Note that if you use your own buffer, it MUST remain valid<br/>
    /// until the callback is executed.  And also note that your callback can be<br/>
    /// invoked at any time from any thread (perhaps even before SendMessages<br/>
    /// returns!), so it MUST be fast and threadsafe.
    /// </para>
    ///
    /// <para>
    /// You MUST also fill in:<br/>
    /// - m_conn - the handle of the connection to send the message to<br/>
    /// - m_nFlags - bitmask of k_nSteamNetworkingSend_xxx flags.
    /// </para>
    ///
    /// <para>
    /// All other fields are currently reserved and should not be modified.
    /// </para>
    ///
    /// <para>
    /// The library will take ownership of the message structures.  They may<br/>
    /// be modified or become invalid at any time, so you must not read them<br/>
    /// after passing them to this function.
    /// </para>
    ///
    /// <para>
    /// pOutMessageNumberOrResult is an optional array that will receive,<br/>
    /// for each message, the message number that was assigned to the message<br/>
    /// if sending was successful.  If sending failed, then a negative EResult<br/>
    /// value is placed into the array.  For example, the array will hold<br/>
    /// -k_EResultInvalidState if the connection was in an invalid state.<br/>
    /// See ISteamNetworkingSockets::SendMessageToConnection for possible<br/>
    /// failure codes.
    /// </para>
    /// </summary>
    public void SendMessages(ReadOnlySpan<IntPtr> messages, Span<long> outMessageNumberOrResult)
    {
        Native.SteamAPI_ISteamNetworkingSockets_SendMessages(this.ptr, messages.Length, messages, outMessageNumberOrResult);
    }

    /// <summary>
    /// <para>
    /// Send one or more messages without copying the message payload.<br/>
    /// This is the most efficient way to send messages. To use this<br/>
    /// function, you must first allocate a message object using<br/>
    /// ISteamNetworkingUtils::AllocateMessage.  (Do not declare one<br/>
    /// on the stack or allocate your own.)
    /// </para>
    ///
    /// <para>
    /// You should fill in the message payload.  You can either let<br/>
    /// it allocate the buffer for you and then fill in the payload,<br/>
    /// or if you already have a buffer allocated, you can just point<br/>
    /// m_pData at your buffer and set the callback to the appropriate function<br/>
    /// to free it.  Note that if you use your own buffer, it MUST remain valid<br/>
    /// until the callback is executed.  And also note that your callback can be<br/>
    /// invoked at any time from any thread (perhaps even before SendMessages<br/>
    /// returns!), so it MUST be fast and threadsafe.
    /// </para>
    ///
    /// <para>
    /// You MUST also fill in:<br/>
    /// - m_conn - the handle of the connection to send the message to<br/>
    /// - m_nFlags - bitmask of k_nSteamNetworkingSend_xxx flags.
    /// </para>
    ///
    /// <para>
    /// All other fields are currently reserved and should not be modified.
    /// </para>
    ///
    /// <para>
    /// The library will take ownership of the message structures.  They may<br/>
    /// be modified or become invalid at any time, so you must not read them<br/>
    /// after passing them to this function.
    /// </para>
    ///
    /// <para>
    /// pOutMessageNumberOrResult is an optional array that will receive,<br/>
    /// for each message, the message number that was assigned to the message<br/>
    /// if sending was successful.  If sending failed, then a negative EResult<br/>
    /// value is placed into the array.  For example, the array will hold<br/>
    /// -k_EResultInvalidState if the connection was in an invalid state.<br/>
    /// See ISteamNetworkingSockets::SendMessageToConnection for possible<br/>
    /// failure codes.
    /// </para>
    /// </summary>
    public void SendMessages(ReadOnlySpan<IntPtr> messages)
    {
        this.SendMessages(messages, []);
    }

    /// <summary>
    /// <para>
    /// Flush any messages waiting on the Nagle timer and send them<br/>
    /// at the next transmission opportunity (often that means right now).
    /// </para>
    ///
    /// <para>
    /// If Nagle is enabled (it's on by default) then when calling<br/>
    /// SendMessageToConnection the message will be buffered, up to the Nagle time<br/>
    /// before being sent, to merge small messages into the same packet.<br/>
    /// (See k_ESteamNetworkingConfig_NagleTime)
    /// </para>
    ///
    /// <para>
    /// Returns:<br/>
    /// k_EResultInvalidParam: invalid connection handle<br/>
    /// k_EResultInvalidState: connection is in an invalid state<br/>
    /// k_EResultNoConnection: connection has ended<br/>
    /// k_EResultIgnored: We weren't (yet) connected, so this operation has no effect.
    /// </para>
    /// </summary>
    public EResult FlushMessagesOnConnection(HSteamNetConnection conn)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_FlushMessagesOnConnection(this.ptr, conn);
    }

    /// <summary>
    /// <para>
    /// Fetch the next available message(s) from the connection, if any.<br/>
    /// Returns the number of messages returned into your array, up to nMaxMessages.<br/>
    /// If the connection handle is invalid, -1 is returned.
    /// </para>
    ///
    /// <para>
    /// The order of the messages returned in the array is relevant.<br/>
    /// Reliable messages will be received in the order they were sent (and with the<br/>
    /// same sizes --- see SendMessageToConnection for on this subtle difference from a stream socket).
    /// </para>
    ///
    /// <para>
    /// Unreliable messages may be dropped, or delivered out of order with respect to<br/>
    /// each other or with respect to reliable messages.  The same unreliable message<br/>
    /// may be received multiple times.
    /// </para>
    ///
    /// <para>
    /// If any messages are returned, you MUST call SteamNetworkingMessage_t::Release() on each<br/>
    /// of them free up resources after you are done.  It is safe to keep the object alive for<br/>
    /// a little while (put it into some queue, etc), and you may call Release() from any thread.
    /// </para>
    /// </summary>
    public int ReceiveMessagesOnConnection(HSteamNetConnection conn, Span<IntPtr> outMessages)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnConnection(this.ptr, conn, outMessages, outMessages.Length);
    }

    /// <summary>
    /// Returns basic information about the high-level state of the connection.
    /// </summary>
    public bool GetConnectionInfo(HSteamNetConnection conn, out SteamNetConnectionInfo_t info)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetConnectionInfo(this.ptr, conn, out info);
    }

    /// <summary>
    /// <para>
    /// Returns a small set of information about the real-time state of the connection<br/>
    /// and the queue status of each lane.
    /// </para>
    ///
    /// <para>
    /// - pStatus may be NULL if the information is not desired.  (E.g. you are only interested<br/>
    /// in the lane information.)<br/>
    /// - On entry, nLanes specifies the length of the pLanes array.  This may be 0<br/>
    /// if you do not wish to receive any lane data.  It's OK for this to be smaller than<br/>
    /// the total number of configured lanes.<br/>
    /// - pLanes points to an array that will receive lane-specific info.  It can be NULL<br/>
    /// if this is not needed.
    /// </para>
    ///
    /// <para>
    /// Return value:<br/>
    /// - k_EResultNoConnection - connection handle is invalid or connection has been closed.<br/>
    /// - k_EResultInvalidParam - nLanes is bad
    /// </para>
    /// </summary>
    public EResult GetConnectionRealTimeStatus(HSteamNetConnection conn, out SteamNetConnectionRealTimeStatus_t status, Span<SteamNetConnectionRealTimeLaneStatus_t> lanes)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetConnectionRealTimeStatus(this.ptr, conn, out status, lanes.Length, lanes);
    }

    /// <summary>
    /// <para>
    /// Returns a small set of information about the real-time state of the connection<br/>
    /// and the queue status of each lane.
    /// </para>
    ///
    /// <para>
    /// - pStatus may be NULL if the information is not desired.  (E.g. you are only interested<br/>
    /// in the lane information.)<br/>
    /// - On entry, nLanes specifies the length of the pLanes array.  This may be 0<br/>
    /// if you do not wish to receive any lane data.  It's OK for this to be smaller than<br/>
    /// the total number of configured lanes.<br/>
    /// - pLanes points to an array that will receive lane-specific info.  It can be NULL<br/>
    /// if this is not needed.
    /// </para>
    ///
    /// <para>
    /// Return value:<br/>
    /// - k_EResultNoConnection - connection handle is invalid or connection has been closed.<br/>
    /// - k_EResultInvalidParam - nLanes is bad
    /// </para>
    /// </summary>
    public EResult GetConnectionRealTimeStatus(HSteamNetConnection conn, out SteamNetConnectionRealTimeStatus_t status)
    {
        return this.GetConnectionRealTimeStatus(conn, out status, []);
    }

    /// <summary>
    /// <para>
    /// Returns a small set of information about the real-time state of the connection<br/>
    /// and the queue status of each lane.
    /// </para>
    ///
    /// <para>
    /// - pStatus may be NULL if the information is not desired.  (E.g. you are only interested<br/>
    /// in the lane information.)<br/>
    /// - On entry, nLanes specifies the length of the pLanes array.  This may be 0<br/>
    /// if you do not wish to receive any lane data.  It's OK for this to be smaller than<br/>
    /// the total number of configured lanes.<br/>
    /// - pLanes points to an array that will receive lane-specific info.  It can be NULL<br/>
    /// if this is not needed.
    /// </para>
    ///
    /// <para>
    /// Return value:<br/>
    /// - k_EResultNoConnection - connection handle is invalid or connection has been closed.<br/>
    /// - k_EResultInvalidParam - nLanes is bad
    /// </para>
    /// </summary>
    public EResult GetConnectionRealTimeStatus(HSteamNetConnection conn, Span<SteamNetConnectionRealTimeLaneStatus_t> lanes)
    {
        return this.GetConnectionRealTimeStatus(conn, out Unsafe.NullRef<SteamNetConnectionRealTimeStatus_t>(), lanes);
    }

    /// <summary>
    /// <para>
    /// Returns detailed connection stats in text format.  Useful<br/>
    /// for dumping to a log, etc.
    /// </para>
    ///
    /// <para>
    /// Returns:<br/>
    /// -1 failure (bad connection handle)<br/>
    /// 0 OK, your buffer was filled in and '\0'-terminated<br/>
    /// 0&lt; Your buffer was either nullptr, or it was too small and the text got truncated.<br/>
    /// Try again with a buffer of at least N bytes.
    /// </para>
    /// </summary>
    public int GetDetailedConnectionStatus(HSteamNetConnection conn, out string status)
    {
        int result = -1;
        {
            Span<byte> raw = stackalloc byte[MaxUtf8StrBufSize];
            result = Native.SteamAPI_ISteamNetworkingSockets_GetDetailedConnectionStatus(this.ptr, conn, raw, MaxUtf8StrBufSize);
            if (result < 0)
            {
                status = String.Empty;
                return result;
            }
            else if (result == 0)
            {
                status = Utf8StringHelper.NullTerminatedSpanToString(raw);
                return result;
            }
        }

        // Allocate more space required for the details and retry
        {
            Span<byte> raw = stackalloc byte[result];
            result = Native.SteamAPI_ISteamNetworkingSockets_GetDetailedConnectionStatus(this.ptr, conn, raw, result);
            if (result == 0)
            {
                status = Utf8StringHelper.NullTerminatedSpanToString(raw);
            }
            else
            {
                status = String.Empty;
            }
        }

        return result;
    }

    /// <summary>
    /// <para>
    /// Returns local IP and port that a listen socket created using CreateListenSocketIP is bound to.
    /// </para>
    ///
    /// <para>
    /// An IPv6 address of ::0 means "any IPv4 or IPv6"<br/>
    /// An IPv6 address of ::ffff:0000:0000 means "any IPv4"
    /// </para>
    /// </summary>
    public bool GetListenSocketAddress(HSteamListenSocket socket, out SteamNetworkingIPAddr address)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetListenSocketAddress(this.ptr, socket, out address);
    }

    /// <summary>
    /// <para>
    /// Create a pair of connections that are talking to each other, e.g. a loopback connection.<br/>
    /// This is very useful for testing, or so that your client/server code can work the same<br/>
    /// even when you are running a local "server".
    /// </para>
    ///
    /// <para>
    /// The two connections will immediately be placed into the connected state, and no callbacks<br/>
    /// will be posted immediately.  After this, if you close either connection, the other connection<br/>
    /// will receive a callback, exactly as if they were communicating over the network.  You must<br/>
    /// close *both* sides in order to fully clean up the resources!
    /// </para>
    ///
    /// <para>
    /// By default, internal buffers are used, completely bypassing the network, the chopping up of<br/>
    /// messages into packets, encryption, copying the payload, etc.  This means that loopback<br/>
    /// packets, by default, will not simulate lag or loss.  Passing true for bUseNetworkLoopback will<br/>
    /// cause the socket pair to send packets through the local network loopback device (127.0.0.1)<br/>
    /// on ephemeral ports.  Fake lag and loss are supported in this case, and CPU time is expended<br/>
    /// to encrypt and decrypt.
    /// </para>
    ///
    /// <para>
    /// If you wish to assign a specific identity to either connection, you may pass a particular<br/>
    /// identity.  Otherwise, if you pass nullptr, the respective connection will assume a generic<br/>
    /// "localhost" identity.  If you use real network loopback, this might be translated to the<br/>
    /// actual bound loopback port.  Otherwise, the port will be zero.
    /// </para>
    /// </summary>
    public bool CreateSocketPair(out HSteamNetConnection outConnection1, out HSteamNetConnection outConnection2, bool useNetworkLoopback, in SteamNetworkingIdentity identity1, in SteamNetworkingIdentity identity2)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_CreateSocketPair(this.ptr, out outConnection1, out outConnection2, useNetworkLoopback, in identity1, in identity2);
    }

    /// <summary>
    /// <para>
    /// Create a pair of connections that are talking to each other, e.g. a loopback connection.<br/>
    /// This is very useful for testing, or so that your client/server code can work the same<br/>
    /// even when you are running a local "server".
    /// </para>
    ///
    /// <para>
    /// The two connections will immediately be placed into the connected state, and no callbacks<br/>
    /// will be posted immediately.  After this, if you close either connection, the other connection<br/>
    /// will receive a callback, exactly as if they were communicating over the network.  You must<br/>
    /// close *both* sides in order to fully clean up the resources!
    /// </para>
    ///
    /// <para>
    /// By default, internal buffers are used, completely bypassing the network, the chopping up of<br/>
    /// messages into packets, encryption, copying the payload, etc.  This means that loopback<br/>
    /// packets, by default, will not simulate lag or loss.  Passing true for bUseNetworkLoopback will<br/>
    /// cause the socket pair to send packets through the local network loopback device (127.0.0.1)<br/>
    /// on ephemeral ports.  Fake lag and loss are supported in this case, and CPU time is expended<br/>
    /// to encrypt and decrypt.
    /// </para>
    ///
    /// <para>
    /// If you wish to assign a specific identity to either connection, you may pass a particular<br/>
    /// identity.  Otherwise, if you pass nullptr, the respective connection will assume a generic<br/>
    /// "localhost" identity.  If you use real network loopback, this might be translated to the<br/>
    /// actual bound loopback port.  Otherwise, the port will be zero.
    /// </para>
    /// </summary>
    public bool CreateSocketPair(out HSteamNetConnection outConnection1, out HSteamNetConnection outConnection2, bool useNetworkLoopback, in SteamNetworkingIdentity identity1)
    {
        return this.CreateSocketPair(out outConnection1, out outConnection2, useNetworkLoopback, identity1, in Unsafe.NullRef<SteamNetworkingIdentity>());
    }

    /// <summary>
    /// <para>
    /// Create a pair of connections that are talking to each other, e.g. a loopback connection.<br/>
    /// This is very useful for testing, or so that your client/server code can work the same<br/>
    /// even when you are running a local "server".
    /// </para>
    ///
    /// <para>
    /// The two connections will immediately be placed into the connected state, and no callbacks<br/>
    /// will be posted immediately.  After this, if you close either connection, the other connection<br/>
    /// will receive a callback, exactly as if they were communicating over the network.  You must<br/>
    /// close *both* sides in order to fully clean up the resources!
    /// </para>
    ///
    /// <para>
    /// By default, internal buffers are used, completely bypassing the network, the chopping up of<br/>
    /// messages into packets, encryption, copying the payload, etc.  This means that loopback<br/>
    /// packets, by default, will not simulate lag or loss.  Passing true for bUseNetworkLoopback will<br/>
    /// cause the socket pair to send packets through the local network loopback device (127.0.0.1)<br/>
    /// on ephemeral ports.  Fake lag and loss are supported in this case, and CPU time is expended<br/>
    /// to encrypt and decrypt.
    /// </para>
    ///
    /// <para>
    /// If you wish to assign a specific identity to either connection, you may pass a particular<br/>
    /// identity.  Otherwise, if you pass nullptr, the respective connection will assume a generic<br/>
    /// "localhost" identity.  If you use real network loopback, this might be translated to the<br/>
    /// actual bound loopback port.  Otherwise, the port will be zero.
    /// </para>
    /// </summary>
    public bool CreateSocketPair(out HSteamNetConnection outConnection1, out HSteamNetConnection outConnection2, bool useNetworkLoopback)
    {
        return this.CreateSocketPair(out outConnection1, out outConnection2, useNetworkLoopback, in Unsafe.NullRef<SteamNetworkingIdentity>(), in Unsafe.NullRef<SteamNetworkingIdentity>());
    }

    /// <summary>
    /// <para>
    /// Configure multiple outbound messages streams ("lanes") on a connection, and<br/>
    /// control head-of-line blocking between them.  Messages within a given lane<br/>
    /// are always sent in the order they are queued, but messages from different<br/>
    /// lanes may be sent out of order.  Each lane has its own message number<br/>
    /// sequence.  The first message sent on each lane will be assigned the number 1.
    /// </para>
    ///
    /// <para>
    /// Each lane has a "priority".  Lanes with higher numeric values will only be processed<br/>
    /// when all lanes with lower number values are empty.  The magnitudes of the priority<br/>
    /// values are not relevant, only their sort order.
    /// </para>
    ///
    /// <para>
    /// Each lane also is assigned a weight, which controls the approximate proportion<br/>
    /// of the bandwidth that will be consumed by the lane, relative to other lanes<br/>
    /// of the same priority.  (This is assuming the lane stays busy.  An idle lane<br/>
    /// does not build up "credits" to be be spent once a message is queued.)<br/>
    /// This value is only meaningful as a proportion, relative to other lanes with<br/>
    /// the same priority.  For lanes with different priorities, the strict priority<br/>
    /// order will prevail, and their weights relative to each other are not relevant.<br/>
    /// Thus, if a lane has a unique priority value, the weight value for that lane is<br/>
    /// not relevant.
    /// </para>
    ///
    /// <para>
    /// Example: 3 lanes, with priorities [ 0, 10, 10 ] and weights [ (NA), 20, 5 ].<br/>
    /// Messages sent on the first will always be sent first, before messages in the<br/>
    /// other two lanes.  Its weight value is irrelevant, since there are no other<br/>
    /// lanes with priority=0.  The other two lanes will share bandwidth, with the second<br/>
    /// and third lanes sharing bandwidth using a ratio of approximately 4:1.<br/>
    /// (The weights [ NA, 4, 1 ] would be equivalent.)
    /// </para>
    ///
    /// <para>
    /// Notes:<br/>
    /// - At the time of this writing, some code has performance cost that is linear<br/>
    /// in the number of lanes, so keep the number of lanes to an absolute minimum.<br/>
    /// 3 or so is fine; >8 is a lot.  The max number of lanes on Steam is 255,<br/>
    /// which is a very large number and not recommended!  If you are compiling this<br/>
    /// library from source, see STEAMNETWORKINGSOCKETS_MAX_LANES.)<br/>
    /// - Lane priority values may be any int.  Their absolute value is not relevant,<br/>
    /// only the order matters.<br/>
    /// - Weights must be positive, and due to implementation details, they are restricted<br/>
    /// to 16-bit values.  The absolute magnitudes don't matter, just the proportions.<br/>
    /// - Messages sent on a lane index other than 0 have a small overhead on the wire,<br/>
    /// so for maximum wire efficiency, lane 0 should be the "most common" lane, regardless<br/>
    /// of priorities or weights.<br/>
    /// - A connection has a single lane by default.  Calling this function with<br/>
    /// nNumLanes=1 is legal, but pointless, since the priority and weight values are<br/>
    /// irrelevant in that case.<br/>
    /// - You may reconfigure connection lanes at any time, however reducing the number of<br/>
    /// lanes is not allowed.<br/>
    /// - Reconfiguring lanes might restart any bandwidth sharing balancing.  Usually you<br/>
    /// will call this function once, near the start of the connection, perhaps after<br/>
    /// exchanging a few messages.<br/>
    /// - To assign all lanes the same priority, you may use pLanePriorities=NULL.<br/>
    /// - If you wish all lanes with the same priority to share bandwidth equally (or<br/>
    /// if no two lanes have the same priority value, and thus priority values are<br/>
    /// irrelevant), you may use pLaneWeights=NULL<br/>
    /// - Priorities and weights determine the order that messages are SENT on the wire.<br/>
    /// There are NO GUARANTEES on the order that messages are RECEIVED!  Due to packet<br/>
    /// loss, out-of-order delivery, and subtle details of packet serialization, messages<br/>
    /// might still be received slightly out-of-order!  The *only* strong guarantee is that<br/>
    /// *reliable* messages on the *same lane* will be delivered in the order they are sent.<br/>
    /// - Each host configures the lanes for the packets they send; the lanes for the flow<br/>
    /// in one direction are completely unrelated to the lanes in the opposite direction.
    /// </para>
    ///
    /// <para>
    /// Return value:<br/>
    /// - k_EResultNoConnection - bad hConn<br/>
    /// - k_EResultInvalidParam - Invalid number of lanes, bad weights, or you tried to reduce the number of lanes<br/>
    /// - k_EResultInvalidState - Connection is already dead, etc
    /// </para>
    ///
    /// <para>
    /// See also:<br/>
    /// SteamNetworkingMessage_t::m_idxLane
    /// </para>
    /// </summary>
    public EResult ConfigureConnectionLanes(HSteamNetConnection conn, ReadOnlySpan<int> lanePriorities, ReadOnlySpan<ushort> laneWeights)
    {
        // Both spans are not empty, but lane sizes mismatch?  What number of lanes then?
        if (lanePriorities.Length != 0 && laneWeights.Length != 0 && lanePriorities.Length != laneWeights.Length)
        {
            return EResult.InvalidParam;
        }

        int numLanes = (lanePriorities.Length > laneWeights.Length) ? lanePriorities.Length : laneWeights.Length;

        return Native.SteamAPI_ISteamNetworkingSockets_ConfigureConnectionLanes(this.ptr, conn, numLanes, lanePriorities, laneWeights);
    }

    // Identity and authentication

    /// <summary>
    /// Get the identity assigned to this interface.<br/>
    /// E.g. on Steam, this is the user's SteamID, or for the gameserver interface, the SteamID assigned<br/>
    /// to the gameserver.  Returns false and sets the result to an invalid identity if we don't know<br/>
    /// our identity yet.  (E.g. GameServer has not logged in.  On Steam, the user will know their SteamID<br/>
    /// even if they are not signed into Steam.)
    /// </summary>
    public bool GetIdentity(out SteamNetworkingIdentity identity)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetIdentity(this.ptr, out identity);
    }

    /// <summary>
    /// <para>
    /// Indicate our desire to be ready participate in authenticated communications.<br/>
    /// If we are currently not ready, then steps will be taken to obtain the necessary<br/>
    /// certificates.   (This includes a certificate for us, as well as any CA certificates<br/>
    /// needed to authenticate peers.)
    /// </para>
    ///
    /// <para>
    /// You can call this at program init time if you know that you are going to<br/>
    /// be making authenticated connections, so that we will be ready immediately when<br/>
    /// those connections are attempted.  (Note that essentially all connections require<br/>
    /// authentication, with the exception of ordinary UDP connections with authentication<br/>
    /// disabled using k_ESteamNetworkingConfig_IP_AllowWithoutAuth.)  If you don't call<br/>
    /// this function, we will wait until a feature is utilized that that necessitates<br/>
    /// these resources.
    /// </para>
    ///
    /// <para>
    /// You can also call this function to force a retry, if failure has occurred.<br/>
    /// Once we make an attempt and fail, we will not automatically retry.<br/>
    /// In this respect, the behavior of the system after trying and failing is the same<br/>
    /// as before the first attempt: attempting authenticated communication or calling<br/>
    /// this function will call the system to attempt to acquire the necessary resources.
    /// </para>
    ///
    /// <para>
    /// You can use GetAuthenticationStatus or listen for SteamNetAuthenticationStatus_t<br/>
    /// to monitor the status.
    /// </para>
    ///
    /// <para>
    /// Returns the current value that would be returned from GetAuthenticationStatus.
    /// </para>
    /// </summary>
    public ESteamNetworkingAvailability InitAuthentication()
    {
        return Native.SteamAPI_ISteamNetworkingSockets_InitAuthentication(this.ptr);
    }

    /// <summary>
    /// <para>
    /// Query our readiness to participate in authenticated communications.  A<br/>
    /// SteamNetAuthenticationStatus_t callback is posted any time this status changes,<br/>
    /// but you can use this function to query it at any time.
    /// </para>
    ///
    /// <para>
    /// The value of SteamNetAuthenticationStatus_t::m_eAvail is returned.  If you only<br/>
    /// want this high level status, you can pass NULL for pDetails.  If you want further<br/>
    /// details, pass non-NULL to receive them.
    /// </para>
    /// </summary>
    public ESteamNetworkingAvailability GetAuthenticationStatus(out SteamNetAuthenticationStatus_t details)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetAuthenticationStatus(this.ptr, out details);
    }

    // Poll groups.  A poll group is a set of connections that can be polled efficiently.
    // (In our API, to "poll" a connection means to retrieve all pending messages.  We
    // actually don't have an API to "poll" the connection *state*, like BSD sockets.)

    /// <summary>
    /// <para>
    /// Create a new poll group.
    /// </para>
    ///
    /// <para>
    /// You should destroy the poll group when you are done using DestroyPollGroup
    /// </para>
    /// </summary>
    public HSteamNetPollGroup CreatePollGroup()
    {
        return Native.SteamAPI_ISteamNetworkingSockets_CreatePollGroup(this.ptr);
    }

    /// <summary>
    /// <para>
    /// Destroy a poll group created with CreatePollGroup().
    /// </para>
    ///
    /// <para>
    /// If there are any connections in the poll group, they are removed from the group,<br/>
    /// and left in a state where they are not part of any poll group.<br/>
    /// Returns false if passed an invalid poll group handle.
    /// </para>
    /// </summary>
    public bool DestroyPollGroup(HSteamNetPollGroup pollGroup)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_DestroyPollGroup(this.ptr, pollGroup);
    }

    /// <summary>
    /// <para>
    /// Assign a connection to a poll group.  Note that a connection may only belong to a<br/>
    /// single poll group.  Adding a connection to a poll group implicitly removes it from<br/>
    /// any other poll group it is in.
    /// </para>
    ///
    /// <para>
    /// You can pass k_HSteamNetPollGroup_Invalid to remove a connection from its current<br/>
    /// poll group without adding it to a new poll group.
    /// </para>
    ///
    /// <para>
    /// If there are received messages currently pending on the connection, an attempt<br/>
    /// is made to add them to the queue of messages for the poll group in approximately<br/>
    /// the order that would have applied if the connection was already part of the poll<br/>
    /// group at the time that the messages were received.
    /// </para>
    ///
    /// <para>
    /// Returns false if the connection handle is invalid, or if the poll group handle<br/>
    /// is invalid (and not k_HSteamNetPollGroup_Invalid).
    /// </para>
    /// </summary>
    public bool SetConnectionPollGroup(HSteamNetConnection conn, HSteamNetPollGroup pollGroup)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_SetConnectionPollGroup(this.ptr, conn, pollGroup);
    }

    /// <summary>
    /// <para>
    /// Same as ReceiveMessagesOnConnection, but will return the next messages available<br/>
    /// on any connection in the poll group.  Examine SteamNetworkingMessage_t::m_conn<br/>
    /// to know which connection.  (SteamNetworkingMessage_t::m_nConnUserData might also<br/>
    /// be useful.)
    /// </para>
    ///
    /// <para>
    /// Delivery order of messages among different connections will usually match the<br/>
    /// order that the last packet was received which completed the message.  But this<br/>
    /// is not a strong guarantee, especially for packets received right as a connection<br/>
    /// is being assigned to poll group.
    /// </para>
    ///
    /// <para>
    /// Delivery order of messages on the same connection is well defined and the<br/>
    /// same guarantees are present as mentioned in ReceiveMessagesOnConnection.<br/>
    /// (But the messages are not grouped by connection, so they will not necessarily<br/>
    /// appear consecutively in the list; they may be interleaved with messages for<br/>
    /// other connections.)
    /// </para>
    /// </summary>
    public int ReceiveMessagesOnPollGroup(HSteamNetPollGroup pollGroup, Span<IntPtr> outMessages)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(this.ptr, pollGroup, outMessages, outMessages.Length);
    }

    // Clients connecting to dedicated servers hosted in a data center,
    // using tickets issued by your game coordinator.  If you are not
    // issuing your own tickets to restrict who can attempt to connect
    // to your server, then you won't use these functions.

    /// <summary>
    /// <para>
    /// Call this when you receive a ticket from your backend / matchmaking system.  Puts the<br/>
    /// ticket into a persistent cache, and optionally returns the parsed ticket.
    /// </para>
    ///
    /// <para>
    /// See stamdatagram_ticketgen.h for more details.
    /// </para>
    /// </summary>
    public bool ReceivedRelayAuthTicket(ReadOnlySpan<byte> ticket, out SteamDatagramRelayAuthTicket outParsedTicket)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ReceivedRelayAuthTicket(this.ptr, ticket, ticket.Length, out outParsedTicket);
    }

    /// <summary>
    /// <para>
    /// Search cache for a ticket to talk to the server on the specified virtual port.<br/>
    /// If found, returns the number of seconds until the ticket expires, and optionally<br/>
    /// the complete cracked ticket.  Returns 0 if we don't have a ticket.
    /// </para>
    ///
    /// <para>
    /// Typically this is useful just to confirm that you have a ticket, before you<br/>
    /// call ConnectToHostedDedicatedServer to connect to the server.
    /// </para>
    /// </summary>
    public int FindRelayAuthTicketForServer(in SteamNetworkingIdentity identityGameServer, int remoteVirtualPort, out SteamDatagramRelayAuthTicket outParsedTicket)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_FindRelayAuthTicketForServer(this.ptr, identityGameServer, remoteVirtualPort, out outParsedTicket);
    }

    /// <summary>
    /// <para>
    /// Client call to connect to a server hosted in a Valve data center, on the specified virtual<br/>
    /// port.  You must have placed a ticket for this server into the cache, or else this connect<br/>
    /// attempt will fail!  If you are not issuing your own tickets, then to connect to a dedicated<br/>
    /// server via SDR in auto-ticket mode, use ConnectP2P.  (The server must be configured to allow<br/>
    /// this type of connection by listening using CreateListenSocketP2P.)
    /// </para>
    ///
    /// <para>
    /// You may wonder why tickets are stored in a cache, instead of simply being passed as an argument<br/>
    /// here.  The reason is to make reconnection to a gameserver robust, even if the client computer loses<br/>
    /// connection to Steam or the central backend, or the app is restarted or crashes, etc.
    /// </para>
    ///
    /// <para>
    /// If you use this, you probably want to call ISteamNetworkingUtils::InitRelayNetworkAccess()<br/>
    /// when your app initializes
    /// </para>
    ///
    /// <para>
    /// If you need to set any initial config options, pass them here.  See<br/>
    /// SteamNetworkingConfigValue_t for more about why this is preferable to<br/>
    /// setting the options "immediately" after creation.
    /// </para>
    /// </summary>
    public HSteamNetConnection ConnectToHostedDedicatedServer(in SteamNetworkingIdentity identityTarget, int remoteVirtualPort, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ConnectToHostedDedicatedServer(this.ptr, in identityTarget, remoteVirtualPort, options.Length, options);
    }

    // Servers hosted in data centers known to the Valve relay network

    /// <summary>
    /// <para>
    /// Returns the value of the SDR_LISTEN_PORT environment variable.  This<br/>
    /// is the UDP server your server will be listening on.  This will<br/>
    /// configured automatically for you in production environments.
    /// </para>
    ///
    /// <para>
    /// In development, you'll need to set it yourself.  See<br/>
    /// https://partner.steamgames.com/doc/api/ISteamNetworkingSockets<br/>
    /// for more information on how to configure dev environments.
    /// </para>
    /// </summary>
    public ushort GetHostedDedicatedServerPort()
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPort(this.ptr);
    }

    /// <summary>
    /// Returns 0 if SDR_LISTEN_PORT is not set.  Otherwise, returns the data center the server<br/>
    /// is running in.  This will be k_SteamDatagramPOPID_dev in non-production environment.
    /// </summary>
    public SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(this.ptr);
    }

    /// <summary>
    /// <para>
    /// Return info about the hosted server.  This contains the PoPID of the server,<br/>
    /// and opaque routing information that can be used by the relays to send traffic<br/>
    /// to your server.
    /// </para>
    ///
    /// <para>
    /// You will need to send this information to your backend, and put it in tickets,<br/>
    /// so that the relays will know how to forward traffic from<br/>
    /// clients to your server.  See SteamDatagramRelayAuthTicket for more info.
    /// </para>
    ///
    /// <para>
    /// Also, note that the routing information is contained in SteamDatagramGameCoordinatorServerLogin,<br/>
    /// so if possible, it's preferred to use GetGameCoordinatorServerLogin to send this info<br/>
    /// to your game coordinator service, and also login securely at the same time.
    /// </para>
    ///
    /// <para>
    /// On a successful exit, k_EResultOK is returned
    /// </para>
    ///
    /// <para>
    /// Unsuccessful exit:<br/>
    /// - Something other than k_EResultOK is returned.<br/>
    /// - k_EResultInvalidState: We are not configured to listen for SDR (SDR_LISTEN_SOCKET<br/>
    /// is not set.)<br/>
    /// - k_EResultPending: we do not (yet) have the authentication information needed.<br/>
    /// (See GetAuthenticationStatus.)  If you use environment variables to pre-fetch<br/>
    /// the network config, this data should always be available immediately.<br/>
    /// - A non-localized diagnostic debug message will be placed in m_data that describes<br/>
    /// the cause of the failure.
    /// </para>
    ///
    /// <para>
    /// NOTE: The returned blob is not encrypted.  Send it to your backend, but don't<br/>
    /// directly share it with clients.
    /// </para>
    /// </summary>
    public EResult GetHostedDedicatedServerAddress(out SteamDatagramHostedAddress routing)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerAddress(this.ptr, out routing);
    }

    /// <summary>
    /// <para>
    /// Create a listen socket on the specified virtual port.  The physical UDP port to use<br/>
    /// will be determined by the SDR_LISTEN_PORT environment variable.  If a UDP port is not<br/>
    /// configured, this call will fail.
    /// </para>
    ///
    /// <para>
    /// This call MUST be made through the SteamGameServerNetworkingSockets() interface.
    /// </para>
    ///
    /// <para>
    /// This function should be used when you are using the ticket generator library<br/>
    /// to issue your own tickets.  Clients connecting to the server on this virtual<br/>
    /// port will need a ticket, and they must connect using ConnectToHostedDedicatedServer.
    /// </para>
    ///
    /// <para>
    /// If you need to set any initial config options, pass them here.  See<br/>
    /// SteamNetworkingConfigValue_t for more about why this is preferable to<br/>
    /// setting the options "immediately" after creation.
    /// </para>
    /// </summary>
    public HSteamListenSocket CreateHostedDedicatedServerListenSocket(int localVirtualPort, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(this.ptr, localVirtualPort, options.Length, options);
    }

    /// <summary>
    /// <para>
    /// Generate an authentication blob that can be used to securely login with<br/>
    /// your backend, using SteamDatagram_ParseHostedServerLogin.  (See<br/>
    /// steamdatagram_gamecoordinator.h)
    /// </para>
    ///
    /// <para>
    /// Before calling the function:<br/>
    /// - Populate the app data in pLoginInfo (m_cbAppData and m_appData).  You can leave<br/>
    /// all other fields uninitialized.<br/>
    /// - *pcbSignedBlob contains the size of the buffer at pBlob.  (It should be<br/>
    /// at least k_cbMaxSteamDatagramGameCoordinatorServerLoginSerialized.)
    /// </para>
    ///
    /// <para>
    /// On a successful exit:<br/>
    /// - k_EResultOK is returned<br/>
    /// - All of the remaining fields of pLoginInfo will be filled out.<br/>
    /// - *pcbSignedBlob contains the size of the serialized blob that has been<br/>
    /// placed into pBlob.
    /// </para>
    ///
    /// <para>
    /// Unsuccessful exit:<br/>
    /// - Something other than k_EResultOK is returned.<br/>
    /// - k_EResultNotLoggedOn: you are not logged in (yet)<br/>
    /// - (GnsSharp exclusive) EResult.InvalidParam: size mismatch between signedBlob and blob parameters<br/>
    /// - See GetHostedDedicatedServerAddress for more potential failure return values.<br/>
    /// - A non-localized diagnostic debug message will be placed in pBlob that describes<br/>
    /// the cause of the failure.
    /// </para>
    ///
    /// <para>
    /// This works by signing the contents of the SteamDatagramGameCoordinatorServerLogin<br/>
    /// with the cert that is issued to this server.  In dev environments, it's OK if you do<br/>
    /// not have a cert.  (You will need to enable insecure dev login in SteamDatagram_ParseHostedServerLogin.)<br/>
    /// Otherwise, you will need a signed cert.
    /// </para>
    ///
    /// <para>
    /// NOTE: The routing blob returned here is not encrypted.  Send it to your backend<br/>
    /// and don't share it directly with clients.
    /// </para>
    /// </summary>
    public EResult GetGameCoordinatorServerLogin(ref SteamDatagramGameCoordinatorServerLogin loginInfo, ref int signedBlob, Span<byte> blob)
    {
        if (signedBlob != blob.Length)
        {
            return EResult.InvalidParam;
        }

        return Native.SteamAPI_ISteamNetworkingSockets_GetGameCoordinatorServerLogin(this.ptr, ref loginInfo, ref signedBlob, blob);
    }

    // Relayed connections using custom signaling protocol
    //
    // This is used if you have your own method of sending out-of-band
    // signaling / rendezvous messages through a mutually trusted channel.

    /// <summary>
    /// <para>
    /// Create a P2P "client" connection that does signaling over a custom<br/>
    /// rendezvous/signaling channel.
    /// </para>
    ///
    /// <para>
    /// pSignaling points to a new object that you create just for this connection.<br/>
    /// It must stay valid until Release() is called.  Once you pass the<br/>
    /// object to this function, it assumes ownership.  Release() will be called<br/>
    /// from within the function call if the call fails.  Furthermore, until Release()<br/>
    /// is called, you should be prepared for methods to be invoked on your<br/>
    /// object from any thread!  You need to make sure your object is threadsafe!<br/>
    /// Furthermore, you should make sure that dispatching the methods is done<br/>
    /// as quickly as possible.
    /// </para>
    ///
    /// <para>
    /// This function will immediately construct a connection in the "connecting"<br/>
    /// state.  Soon after (perhaps before this function returns, perhaps in another thread),<br/>
    /// the connection will begin sending signaling messages by calling<br/>
    /// ISteamNetworkingConnectionSignaling::SendSignal.
    /// </para>
    ///
    /// <para>
    /// When the remote peer accepts the connection (See<br/>
    /// ISteamNetworkingSignalingRecvContext::OnConnectRequest),<br/>
    /// it will begin sending signaling messages.  When these messages are received,<br/>
    /// you can pass them to the connection using ReceivedP2PCustomSignal.
    /// </para>
    ///
    /// <para>
    /// If you know the identity of the peer that you expect to be on the other end,<br/>
    /// you can pass their identity to improve debug output or just detect bugs.<br/>
    /// If you don't know their identity yet, you can pass NULL, and their<br/>
    /// identity will be established in the connection handshake.
    /// </para>
    ///
    /// <para>
    /// If you use this, you probably want to call ISteamNetworkingUtils::InitRelayNetworkAccess()<br/>
    /// when your app initializes
    /// </para>
    ///
    /// <para>
    /// If you need to set any initial config options, pass them here.  See<br/>
    /// SteamNetworkingConfigValue_t for more about why this is preferable to<br/>
    /// setting the options "immediately" after creation.
    /// </para>
    /// </summary>
    public HSteamNetConnection ConnectP2PCustomSignaling(IntPtr signaling, in SteamNetworkingIdentity peerIdentity, int remoteVirtualPort, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ConnectP2PCustomSignaling(this.ptr, signaling, in peerIdentity, remoteVirtualPort, options.Length, options);
    }

    /// <summary>
    /// <para>
    /// Called when custom signaling has received a message.  When your<br/>
    /// signaling channel receives a message, it should save off whatever<br/>
    /// routing information was in the envelope into the context object,<br/>
    /// and then pass the payload to this function.
    /// </para>
    ///
    /// <para>
    /// A few different things can happen next, depending on the message:
    /// </para>
    ///
    /// <para>
    /// - If the signal is associated with existing connection, it is dealt<br/>
    /// with immediately.  If any replies need to be sent, they will be<br/>
    /// dispatched using the ISteamNetworkingConnectionSignaling<br/>
    /// associated with the connection.<br/>
    /// - If the message represents a connection request (and the request<br/>
    /// is not redundant for an existing connection), a new connection<br/>
    /// will be created, and ReceivedConnectRequest will be called on your<br/>
    /// context object to determine how to proceed.<br/>
    /// - Otherwise, the message is for a connection that does not<br/>
    /// exist (anymore).  In this case, we *may* call SendRejectionReply<br/>
    /// on your context object.
    /// </para>
    ///
    /// <para>
    /// In any case, we will not save off pContext or access it after this<br/>
    /// function returns.
    /// </para>
    ///
    /// <para>
    /// Returns true if the message was parsed and dispatched without anything<br/>
    /// unusual or suspicious happening.  Returns false if there was some problem<br/>
    /// with the message that prevented ordinary handling.  (Debug output will<br/>
    /// usually have more information.)
    /// </para>
    ///
    /// <para>
    /// If you expect to be using relayed connections, then you probably want<br/>
    /// to call ISteamNetworkingUtils::InitRelayNetworkAccess() when your app initializes
    /// </para>
    /// </summary>
    public bool ReceivedP2PCustomSignal(ReadOnlySpan<byte> msg, IntPtr context)
    {
        return Native.SteamAPI_ISteamNetworkingSockets_ReceivedP2PCustomSignal(this.ptr, msg, msg.Length, context);
    }

    // Certificate provision by the application.  On Steam, we normally handle all this automatically
    // and you will not need to use these advanced functions.

    /// <summary>
    /// <para>
    /// Get blob that describes a certificate request.  You can send this to your game coordinator.<br/>
    /// Upon entry, *pcbBlob should contain the size of the buffer.  On successful exit, it will<br/>
    /// return the number of bytes that were populated.  You can pass pBlob=NULL to query for the required<br/>
    /// size.  (512 bytes is a conservative estimate.)
    /// </para>
    ///
    /// <para>
    /// Pass this blob to your game coordinator and call SteamDatagram_CreateCert.
    /// </para>
    /// </summary>
    public bool GetCertificateRequest(ref int blobSize, Span<byte> blob, out string? errMsg)
    {
        bool result = Native.SteamAPI_ISteamNetworkingSockets_GetCertificateRequest(this.ptr, ref blobSize, blob, out SteamNetworkingErrMsg msg);

        if (!result)
        {
            ReadOnlySpan<byte> span = msg;
            errMsg = Utf8StringHelper.NullTerminatedSpanToString(span);
        }
        else
        {
            errMsg = null;
        }

        return result;
    }

    /// <summary>
    /// Set the certificate.  The certificate blob should be the output of<br/>
    /// SteamDatagram_CreateCert.
    /// </summary>
    public bool SetCertificate(ReadOnlySpan<byte> certificate, out string? errMsg)
    {
        bool result = Native.SteamAPI_ISteamNetworkingSockets_SetCertificate(this.ptr, certificate, certificate.Length, out SteamNetworkingErrMsg msg);

        if (!result)
        {
            ReadOnlySpan<byte> span = msg;
            errMsg = Utf8StringHelper.NullTerminatedSpanToString(span);
        }
        else
        {
            errMsg = null;
        }

        return result;
    }

    /// <summary>
    /// <para>
    /// Reset the identity associated with this instance.<br/>
    /// Any open connections are closed.  Any previous certificates, etc are discarded.<br/>
    /// You can pass a specific identity that you want to use, or you can pass NULL,<br/>
    /// in which case the identity will be invalid until you set it using SetCertificate
    /// </para>
    ///
    /// <para>
    /// NOTE: This function is not actually supported on Steam!  It is included<br/>
    /// for use on other platforms where the active user can sign out and<br/>
    /// a new user can sign in.
    /// </para>
    /// </summary>
    public void ResetIdentity(in SteamNetworkingIdentity identity)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Stand-alone GNS doesn't expose ISteamNetworkingSockets::ResetIdentity()");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamNetworkingSockets_ResetIdentity(this.ptr, in identity);
#endif
    }

    // Misc

    /// <summary>
    /// <para>
    /// Invoke all callback functions queued for this interface.<br/>
    /// See k_ESteamNetworkingConfig_Callback_ConnectionStatusChanged, etc
    /// </para>
    ///
    /// <para>
    /// You don't need to call this if you are using Steam's callback dispatch<br/>
    /// mechanism (SteamAPI_RunCallbacks and SteamGameserver_RunCallbacks).
    /// </para>
    /// </summary>
    public void RunCallbacks()
    {
        Native.SteamAPI_ISteamNetworkingSockets_RunCallbacks(this.ptr);
    }

    // "FakeIP" system.
    //
    // A FakeIP is essentially a temporary, arbitrary identifier that
    // happens to be a valid IPv4 address.  The purpose of this system is to make it
    // easy to integrate with existing code that identifies hosts using IPv4 addresses.
    // The FakeIP address will never actually be used to send or receive any packets
    // on the Internet, it is strictly an identifier.
    //
    // FakeIP addresses are designed to (hopefully) pass through existing code as
    // transparently as possible, while conflicting with "real" addresses that might
    // be in use on networks (both the Internet and LANs) in the same code as little
    // as possible.  At the time this comment is being written, they come from the
    // 169.254.0.0/16 range, and the port number will always be >1024.  HOWEVER,
    // this is subject to change!  Do not make assumptions about these addresses,
    // or your code might break in the future.  In particular, you should use
    // functions such as  ISteamNetworkingUtils::IsFakeIP to determine if an IP
    // address is a "fake" one used by this system.

    /// <summary>
    /// <para>
    /// Begin asynchronous process of allocating a fake IPv4 address that other<br/>
    /// peers can use to contact us via P2P.  IP addresses returned by this<br/>
    /// function are globally unique for a given appid.
    /// </para>
    ///
    /// <para>
    /// nNumPorts is the numbers of ports you wish to reserve.  This is useful<br/>
    /// for the same reason that listening on multiple UDP ports is useful for<br/>
    /// different types of traffic.  Because these allocations come from a global<br/>
    /// namespace, there is a relatively strict limit on the maximum number of<br/>
    /// ports you may request.  (At the time of this writing, the limit is 4.)<br/>
    /// The port assignments are *not* guaranteed to have any particular order<br/>
    /// or relationship!  Do *not* assume they are contiguous, even though that<br/>
    /// may often occur in practice.
    /// </para>
    ///
    /// <para>
    /// Returns false if a request was already in progress, true if a new request<br/>
    /// was started.  A SteamNetworkingFakeIPResult_t will be posted when the request<br/>
    /// completes.
    /// </para>
    ///
    /// <para>
    /// For gameservers, you *must* call this after initializing the SDK but before<br/>
    /// beginning login.  Steam needs to know in advance that FakeIP will be used.<br/>
    /// Everywhere your public IP would normally appear (such as the server browser) will be<br/>
    /// replaced by the FakeIP, and the fake port at index 0.  The request is actually queued<br/>
    /// until the logon completes, so you must not wait until the allocation completes<br/>
    /// before logging in.  Except for trivial failures that can be detected locally<br/>
    /// (e.g. invalid parameter), a SteamNetworkingFakeIPResult_t callback (whether success or<br/>
    /// failure) will not be posted until after we have logged in.  Furthermore, it is assumed<br/>
    /// that FakeIP allocation is essential for your application to function, and so failure<br/>
    /// will not be reported until *several* retries have been attempted.  This process may<br/>
    /// last several minutes.  It is *highly* recommended to treat failure as fatal.
    /// </para>
    ///
    /// <para>
    /// To communicate using a connection-oriented (TCP-style) API:<br/>
    /// - Server creates a listen socket using CreateListenSocketP2PFakeIP<br/>
    /// - Client connects using ConnectByIPAddress, passing in the FakeIP address.<br/>
    /// - The connection will behave mostly like a P2P connection.  The identities<br/>
    /// that appear in SteamNetConnectionInfo_t will be the FakeIP identity until<br/>
    /// we know the real identity.  Then it will be the real identity.  If the<br/>
    /// SteamNetConnectionInfo_t::m_addrRemote is valid, it will be a real IPv4<br/>
    /// address of a NAT-punched connection.  Otherwise, it will not be valid.
    /// </para>
    ///
    /// <para>
    /// To communicate using an ad-hoc sendto/recv from (UDP-style) API,<br/>
    /// use CreateFakeUDPPort.
    /// </para>
    /// </summary>
    public bool BeginAsyncRequestFakeIP(int numPorts)
    {
        // TODO: Maybe change this to C# async-await?
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException($"FakeIP allocation requires Steam");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingSockets_BeginAsyncRequestFakeIP(this.ptr, numPorts);
#endif
    }

    /// <summary>
    /// Return info about the FakeIP and port(s) that we have been assigned,<br/>
    /// if any.  idxFirstPort is currently reserved and must be zero.<br/>
    /// Make sure and check SteamNetworkingFakeIPResult_t::m_eResult
    /// </summary>
    public void GetFakeIP(out SteamNetworkingFakeIPResult_t info)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException($"FakeIP allocation requires Steam");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamNetworkingSockets_GetFakeIP(this.ptr, 0, out info);
#endif
    }

    /// <summary>
    /// <para>
    /// Create a listen socket that will listen for P2P connections sent<br/>
    /// to our FakeIP.  A peer can initiate connections to this listen<br/>
    /// socket by calling ConnectByIPAddress.
    /// </para>
    ///
    /// <para>
    /// idxFakePort refers to the *index* of the fake port requested,<br/>
    /// not the actual port number.  For example, pass 0 to refer to the<br/>
    /// first port in the reservation.  You must call this only after calling<br/>
    /// BeginAsyncRequestFakeIP.  However, you do not need to wait for the<br/>
    /// request to complete before creating the listen socket.
    /// </para>
    /// </summary>
    public HSteamListenSocket CreateListenSocketP2PFakeIP(int idxFakePort, ReadOnlySpan<SteamNetworkingConfigValue_t> options)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException($"FakeIP allocation requires Steam");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(this.ptr, idxFakePort, options.Length, options);
#endif
    }

    /// <summary>
    /// <para>
    /// If the connection was initiated using the "FakeIP" system, then we<br/>
    /// we can get an IP address for the remote host.  If the remote host had<br/>
    /// a global FakeIP at the time the connection was established, this<br/>
    /// function will return that global IP.  Otherwise, a FakeIP that is<br/>
    /// unique locally will be allocated from the local FakeIP address space,<br/>
    /// and that will be returned.
    /// </para>
    ///
    /// <para>
    /// The allocation of local FakeIPs attempts to assign addresses in<br/>
    /// a consistent manner.  If multiple connections are made to the<br/>
    /// same remote host, they *probably* will return the same FakeIP.<br/>
    /// However, since the namespace is limited, this cannot be guaranteed.
    /// </para>
    ///
    /// <para>
    /// On failure, returns:<br/>
    /// - k_EResultInvalidParam: invalid connection handle<br/>
    /// - k_EResultIPNotFound: This connection wasn't made using FakeIP system
    /// </para>
    /// </summary>
    public EResult GetRemoteFakeIPForConnection(HSteamNetConnection conn, out SteamNetworkingIPAddr outAddr)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException($"FakeIP system requires Steam");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingSockets_GetRemoteFakeIPForConnection(this.ptr, conn, out outAddr);
#endif
    }

    /// <summary>
    /// <para>
    /// Get an interface that can be used like a UDP port to send/receive<br/>
    /// datagrams to a FakeIP address.  This is intended to make it easy<br/>
    /// to port existing UDP-based code to take advantage of SDR.
    /// </para>
    ///
    /// <para>
    /// idxFakeServerPort refers to the *index* of the port allocated using<br/>
    /// BeginAsyncRequestFakeIP and is used to create "server" ports.  You may<br/>
    /// call this before the allocation has completed.  However, any attempts<br/>
    /// to send packets will fail until the allocation has succeeded.  When<br/>
    /// the peer receives packets sent from this interface, the from address<br/>
    /// of the packet will be the globally-unique FakeIP.  If you call this<br/>
    /// function multiple times and pass the same (nonnegative) fake port index,<br/>
    /// the same object will be returned, and this object is not reference counted.
    /// </para>
    ///
    /// <para>
    /// To create a "client" port (e.g. the equivalent of an ephemeral UDP port)<br/>
    /// pass -1.  In this case, a distinct object will be returned for each call.<br/>
    /// When the peer receives packets sent from this interface, the peer will<br/>
    /// assign a FakeIP from its own locally-controlled namespace.
    /// </para>
    /// </summary>
    public IntPtr CreateFakeUDPPort(int idxFakeServerPort)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException($"FakeIP system requires Steam");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingSockets_CreateFakeUDPPort(this.ptr, idxFakeServerPort);
#endif
    }
}
