// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Configuration options
/// </summary>
public enum ESteamNetworkingConfigValue : int
{
    Invalid = 0,

    // Connection options

    /// <summary>
    /// [connection int32] Timeout value (in ms) to use when first connecting
    /// </summary>
    TimeoutInitial = 24,

    /// <summary>
    /// [connection int32] Timeout value (in ms) to use after connection is established
    /// </summary>
    TimeoutConnected = 25,

    /// <summary>
    /// [connection int32] Upper limit of buffered pending bytes to be sent,<br/>
    /// if this is reached SendMessage will return <c>k_EResultLimitExceeded</c><br/>
    /// Default is 512k (524288 bytes)
    /// </summary>
    SendBufferSize = 9,

    /// <summary>
    /// <para>
    /// [connection int32] Upper limit on total size (in bytes) of received messages<br/>
    /// that will be buffered waiting to be processed by the application.  If this limit<br/>
    /// is exceeded, packets will be dropped.  This is to protect us from a malicious<br/>
    /// peer flooding us with messages faster than we can process them.
    /// </para>
    ///
    /// <para>
    /// This must be bigger than RecvMaxMessageSize
    /// </para>
    /// </summary>
    RecvBufferSize = 47,

    /// <summary>
    /// [connection int32] Upper limit on the number of received messages that will<br/>
    /// that will be buffered waiting to be processed by the application.  If this limit<br/>
    /// is exceeded, packets will be dropped.  This is to protect us from a malicious<br/>
    /// peer flooding us with messages faster than we can pull them off the wire.
    /// </summary>
    RecvBufferMessages = 48,

    /// <summary>
    /// <para>
    /// [connection int32] Maximum message size that we are willing to receive.<br/>
    /// if a client attempts to send us a message larger than this, the connection<br/>
    /// will be immediately closed.
    /// </para>
    ///
    /// <para>
    /// Default is 512k (524288 bytes).  Note that the peer needs to be able to<br/>
    /// send a message this big.  (See <c>k_cbMaxSteamNetworkingSocketsMessageSizeSend</c>.)
    /// </para>
    /// </summary>
    RecvMaxMessageSize = 49,

    /// <summary>
    /// <para>
    /// [connection int32] Max number of message segments that can be received<br/>
    /// in a single UDP packet.  While decoding a packet, if the number of segments<br/>
    /// exceeds this, we will abort further packet processing.
    /// </para>
    ///
    /// <para>
    /// The default is effectively unlimited.  If you know that you very rarely<br/>
    /// send small packets, you can protect yourself from malicious senders by<br/>
    /// lowering this number.
    /// </para>
    ///
    /// <para>
    /// In particular, if you are NOT using the reliability layer and are only using<br/>
    /// SteamNetworkingSockets for datagram transport, setting this to a very low<br/>
    /// number may be beneficial.  (We recommend a value of 2.)  Make sure your sender
    /// disables Nagle!
    /// </para>
    /// </summary>
    RecvMaxSegmentsPerPacket = 50,

    /// <summary>
    /// <para>
    /// [connection int64] Get/set userdata as a configuration option.<br/>
    /// The default value is -1.   You may want to set the user data as<br/>
    /// a config value, instead of using ISteamNetworkingSockets::SetConnectionUserData<br/>
    /// in two specific instances:
    /// </para>
    ///
    /// <para>
    /// - You wish to set the userdata atomically when creating<br/>
    ///   an outbound connection, so that the userdata is filled in properly<br/>
    ///   for any callbacks that happen.  However, note that this trick<br/>
    ///   only works for connections initiated locally!  For incoming<br/>
    ///   connections, multiple state transitions may happen and<br/>
    ///   callbacks be queued, before you are able to service the first<br/>
    ///   callback!  Be careful!
    /// </para>
    ///
    /// <para>
    /// - You can set the default userdata for all newly created connections<br/>
    ///   by setting this value at a higher level (e.g. on the listen<br/>
    ///   socket or at the global level.)  Then this default<br/>
    ///   value will be inherited when the connection is created.<br/>
    ///   This is useful in case -1 is a valid userdata value, and you<br/>
    ///   wish to use something else as the default value so you can<br/>
    ///   tell if it has been set or not.
    /// </para>
    ///
    /// <para>
    ///   HOWEVER: once a connection is created, the effective value is<br/>
    ///   then bound to the connection.  Unlike other connection options,<br/>
    ///   if you change it again at a higher level, the new value will not<br/>
    ///   be inherited by connections.
    /// </para>
    ///
    /// <para>
    /// Using the userdata field in callback structs is not advised because<br/>
    /// of tricky race conditions.  Instead, you might try one of these methods:
    /// </para>
    ///
    /// <para>
    /// - Use a separate map with the HSteamNetConnection as the key.<br/>
    /// - Fetch the userdata from the connection in your callback<br/>
    ///   using ISteamNetworkingSockets::GetConnectionUserData, to<br/>
    ///    ensure you have the current value.
    /// </para>
    /// </summary>
    ConnectionUserData = 40,

    /// <summary>
    /// [connection int32] Minimum send rate clamp, in bytes/sec.<br/>
    /// At the time of this writing these two options should always be set to<br/>
    /// the same value, to manually configure a specific send rate.  The default<br/>
    /// value is 256K.  Eventually we hope to have the library estimate the bandwidth<br/>
    /// of the channel and set the send rate to that estimated bandwidth, and these<br/>
    /// values will only set limits on that send rate.
    /// </summary>
    SendRateMin = 10,

    /// <summary>
    /// [connection int32] Maximum send rate clamp, in bytes/sec.<br/>
    /// At the time of this writing these two options should always be set to<br/>
    /// the same value, to manually configure a specific send rate.  The default<br/>
    /// value is 256K.  Eventually we hope to have the library estimate the bandwidth<br/>
    /// of the channel and set the send rate to that estimated bandwidth, and these<br/>
    /// values will only set limits on that send rate.
    /// </summary>
    SendRateMax = 11,

    /// <summary>
    /// [connection int32] Nagle time, in microseconds.  When SendMessage is called, if<br/>
    /// the outgoing message is less than the size of the MTU, it will be<br/>
    /// queued for a delay equal to the Nagle timer value.  This is to ensure<br/>
    /// that if the application sends several small messages rapidly, they are<br/>
    /// coalesced into a single packet.<br/>
    /// See historical RFC 896.  Value is in microseconds.<br/>
    /// Default is 5000us (5ms).
    /// </summary>
    NagleTime = 12,

    /// <summary>
    /// <para>
    /// [connection int32] Don't automatically fail IP connections that don't have<br/>
    /// strong auth.  On clients, this means we will attempt the connection even if<br/>
    /// we don't know our identity or can't get a cert.  On the server, it means that<br/>
    /// we won't automatically reject a connection due to a failure to authenticate.<br/>
    /// (You can examine the incoming connection and decide whether to accept it.)
    /// </para>
    ///
    /// <para>
    /// 0: Don't attempt or accept unauthorized connections<br/>
    /// 1: Attempt authorization when connecting, and allow unauthorized peers, but emit warnings<br/>
    /// 2: don't attempt authentication, or complain if peer is unauthenticated
    /// </para>
    ///
    /// <para>
    /// This is a dev configuration value, and you should not let users modify it in
    /// production.
    /// </para>
    /// </summary>
    IP_AllowWithoutAuth = 23,

    /// <summary>
    /// [connection int32] The same as IP_AllowWithoutAuth, but will only apply<br/>
    /// for connections to/from localhost addresses.  Whichever value is larger<br/>
    /// (more permissive) will be used.
    /// </summary>
    IPLocalHost_AllowWithoutAuth = 52,

    /// <summary>
    /// [connection int32] Do not send UDP packets with a payload of<br/>
    /// larger than N bytes.  If you set this, MTU_DataSize<br/>
    /// is automatically adjusted
    /// </summary>
    MTU_PacketSize = 32,

    /// <summary>
    /// [connection int32] (read only) Maximum message size you can send that<br/>
    /// will not fragment, based on MTU_PacketSize
    /// </summary>
    MTU_DataSize = 33,

    /// <summary>
    /// <para>
    /// [connection int32] Allow unencrypted (and unauthenticated) communication.<br/>
    /// 0: Not allowed (the default)<br/>
    /// 1: Allowed, but prefer encrypted<br/>
    /// 2: Allowed, and preferred<br/>
    /// 3: Required.  (Fail the connection if the peer requires encryption.)
    /// </para>
    ///
    /// <para>
    /// This is a dev configuration value, since its purpose is to disable encryption.<br/>
    /// You should not let users modify it in production.  (But note that it requires<br/>
    /// the peer to also modify their value in order for encryption to be disabled.)
    /// </para>
    /// </summary>
    Unencrypted = 34,

    /// <summary>
    /// <para>
    /// [connection int32] Set this to 1 on outbound connections and listen sockets,<br/>
    /// to enable "symmetric connect mode", which is useful in the following<br/>
    /// common peer-to-peer use case:
    /// </para>
    ///
    /// <para>
    /// - The two peers are "equal" to each other.  (Neither is clearly the "client"<br/>
    ///   or "server".)<br/>
    /// - Either peer may initiate the connection, and indeed they may do this<br/>
    ///   at the same time<br/>
    /// - The peers only desire a single connection to each other, and if both<br/>
    ///   peers initiate connections simultaneously, a protocol is needed for them<br/>
    ///   to resolve the conflict, so that we end up with a single connection.
    ///   </para>
    ///
    /// <para>
    /// This use case is both common, and involves subtle race conditions and tricky<br/>
    /// pitfalls, which is why the API has support for dealing with it.
    /// </para>
    ///
    /// <para>
    /// If an incoming connection arrives on a listen socket or via custom signaling,<br/>
    /// and the application has not attempted to make a matching outbound connection<br/>
    /// in symmetric mode, then the incoming connection can be accepted as usual.<br/>
    /// A "matching" connection means that the relevant endpoint information matches.<br/>
    /// (At the time this comment is being written, this is only supported for P2P<br/>
    /// connections, which means that the peer identities must match, and the virtual<br/>
    /// port must match.  At a later time, symmetric mode may be supported for other<br/>
    /// connection types.)
    /// </para>
    ///
    /// <para>
    /// If connections are initiated by both peers simultaneously, race conditions<br/>
    /// can arise, but fortunately, most of them are handled internally and do not<br/>
    /// require any special awareness from the application.  However, there<br/>
    /// is one important case that application code must be aware of:<br/>
    /// If application code attempts an outbound connection using a ConnectXxx<br/>
    /// function in symmetric mode, and a matching incoming connection is already<br/>
    /// waiting on a listen socket, then instead of forming a new connection,<br/>
    /// the ConnectXxx call will accept the existing incoming connection, and return<br/>
    /// a connection handle to this accepted connection.<br/>
    /// IMPORTANT: in this case, a SteamNetConnectionStatusChangedCallback_t<br/>
    /// has probably *already* been posted to the queue for the incoming connection!<br/>
    /// (Once callbacks are posted to the queue, they are not modified.)  It doesn't<br/>
    /// matter if the callback has not been consumed by the app.  Thus, application<br/>
    /// code that makes use of symmetric connections must be aware that, when processing a<br/>
    /// SteamNetConnectionStatusChangedCallback_t for an incoming connection, the<br/>
    /// m_hConn may refer to a new connection that the app has has not<br/>
    /// seen before (the usual case), but it may also refer to a connection that<br/>
    /// has already been accepted implicitly through a call to Connect()!  In this<br/>
    /// case, AcceptConnection() will return k_EResultDuplicateRequest.
    /// </para>
    ///
    /// <para>
    /// Only one symmetric connection to a given peer (on a given virtual port)<br/>
    /// may exist at any given time.  If client code attempts to create a connection,<br/>
    /// and a (live) connection already exists on the local host, then either the<br/>
    /// existing connection will be accepted as described above, or the attempt<br/>
    /// to create a new connection will fail.  Furthermore, linger mode functionality<br/>
    /// is not supported on symmetric connections.
    /// </para>
    ///
    /// <para>
    /// A more complicated race condition can arise if both peers initiate a connection<br/>
    /// at roughly the same time.  In this situation, each peer will receive an incoming<br/>
    /// connection from the other peer, when the application code has already initiated<br/>
    /// an outgoing connection to that peer.  The peers must resolve this conflict and<br/>
    /// decide who is going to act as the "server" and who will act as the "client".<br/>
    /// Typically the application does not need to be aware of this case as it is handled<br/>
    /// internally.  On both sides, the will observe their outbound connection being<br/>
    /// "accepted", although one of them one have been converted internally to act<br/>
    /// as the "server".
    /// </para>
    ///
    /// <para>
    /// In general, symmetric mode should be all-or-nothing: do not mix symmetric<br/>
    /// connections with a non-symmetric connection that it might possible "match"<br/>
    /// with.  If you use symmetric mode on any connections, then both peers should<br/>
    /// use it on all connections, and the corresponding listen socket, if any.  The<br/>
    /// behaviour when symmetric and ordinary connections are mixed is not defined by<br/>
    /// this API, and you should not rely on it.  (This advice only applies when connections<br/>
    /// might possibly "match".  For example, it's OK to use all symmetric mode<br/>
    /// connections on one virtual port, and all ordinary, non-symmetric connections<br/>
    /// on a different virtual port, as there is no potential for ambiguity.)
    /// </para>
    ///
    /// <para>
    /// When using the feature, you should set it in the following situations on<br/>
    /// applicable objects:
    /// </para>
    ///
    /// <para>
    /// - When creating an outbound connection using ConnectXxx function<br/>
    /// - When creating a listen socket.  (Note that this will automatically cause<br/>
    ///   any accepted connections to inherit the flag.)<br/>
    /// - When using custom signaling, before accepting an incoming connection.
    /// </para>
    ///
    /// <para>
    /// Setting the flag on listen socket and accepted connections will enable the<br/>
    /// API to automatically deal with duplicate incoming connections, even if the<br/>
    /// local host has not made any outbound requests.  (In general, such duplicate<br/>
    /// requests from a peer are ignored internally and will not be visible to the<br/>
    /// application code.  The previous connection must be closed or resolved first.)
    /// </para>
    /// </summary>
    SymmetricConnect = 37,

    /// <summary>
    /// <para>
    /// [connection int32] For connection types that use "virtual ports", this can be used<br/>
    /// to assign a local virtual port.  For incoming connections, this will always be the<br/>
    /// virtual port of the listen socket (or the port requested by the remote host if custom<br/>
    /// signaling is used and the connection is accepted), and cannot be changed.  For<br/>
    /// connections initiated locally, the local virtual port will default to the same as the<br/>
    /// requested remote virtual port, if you do not specify a different option when creating<br/>
    /// the connection.  The local port is only relevant for symmetric connections, when<br/>
    /// determining if two connections "match."  In this case, if you need the local and remote<br/>
    /// port to differ, you can set this value.
    /// </para>
    ///
    /// <para>
    /// You can also read back this value on listen sockets.
    /// </para>
    ///
    /// <para>
    /// This value should not be read or written in any other context.
    /// </para>
    /// </summary>
    LocalVirtualPort = 38,

    /// <summary>
    /// [connection int32] Enable Dual wifi band support for this connection<br/>
    /// 0 = no, 1 = yes, 2 = simulate it for debugging, even if dual wifi not available
    /// </summary>
    DualWifi_Enable = 39,

    /// <summary>
    /// [connection int32] True to enable diagnostics reporting through<br/>
    /// generic platform UI.  (Only available on Steam.)
    /// </summary>
    EnableDiagnosticsUI = 46,

    /// <summary>
    /// <para>
    /// [connection int32] Send of time-since-previous-packet values in each UDP packet.<br/>
    /// This add a small amount of packet overhead but allows for detailed jitter measurements<br/>
    /// to be made by the receiver.
    /// </para>
    ///
    /// <para>
    /// -  0: disables the sending<br/>
    /// -  1: enables sending<br/>
    /// - -1: (the default) Use the default for the connection type.  For plain UDP connections,<br/>
    ///       this is disabled, and for relayed connections, it is enabled.  Note that relays<br/>
    ///       always send the value.
    /// </para>
    /// </summary>
    SendTimeSincePreviousPacket = 59,

    // Simulating network conditions
    //
    // These are global (not per-connection) because they apply at
    // a relatively low UDP layer.

    /// <summary>
    /// [global float, 0--100] Randomly discard N pct of packets instead of sending<br/>
    /// This is a global option only, since it is applied at a low level<br/>
    /// where we don't have much context
    /// </summary>
    FakePacketLoss_Send = 2,

    /// <summary>
    /// [global float, 0--100] Randomly discard N pct of packets instead of receving<br/>
    /// This is a global option only, since it is applied at a low level<br/>
    /// where we don't have much context
    /// </summary>
    FakePacketLoss_Recv = 3,

    /// <summary>
    /// [global int32].  Delay all outbound packets by N ms
    /// </summary>
    FakePacketLag_Send = 4,

    /// <summary>
    /// [global int32].  Delay all inbound packets by N ms
    /// </summary>
    FakePacketLag_Recv = 5,

    /// <summary>
    /// <para>
    /// Simulated jitter/clumping.
    /// </para>
    ///
    /// <para>
    /// For each packet, a jitter value is determined (which may<br/>
    /// be zero).  This amount is added as extra delay to the<br/>
    /// packet.  When a subsequent packet is queued, it receives its<br/>
    /// own random jitter amount from the current time.  if this would<br/>
    /// result in the packets being delivered out of order, the later<br/>
    /// packet queue time is adjusted to happen after the first packet.<br/>
    /// Thus simulating jitter by itself will not reorder packets, but it<br/>
    /// can "clump" them.
    /// </para>
    ///
    /// <para>
    /// - Avg: A random jitter time is generated using an exponential<br/>
    ///   distribution using this value as the mean (ms).  The default<br/>
    ///   is zero, which disables random jitter.<br/>
    /// - Max: Limit the random jitter time to this value (ms).<br/>
    /// - Pct: odds (0-100) that a random jitter value for the packet<br/>
    ///   will be generated.  Otherwise, a jitter value of zero<br/>
    ///   is used, and the packet will only be delayed by the jitter<br/>
    ///   system if necessary to retain order, due to the jitter of a<br/>
    ///   previous packet.
    /// </para>
    ///
    /// <para>
    /// All values are [global float]
    /// </para>
    ///
    /// <para>
    /// Fake jitter is simulated after fake lag, but before reordering.
    /// </para>
    /// </summary>
    FakePacketJitter_Send_Avg = 53,

    /// <summary>
    /// <para>
    /// Simulated jitter/clumping.
    /// </para>
    ///
    /// <para>
    /// For each packet, a jitter value is determined (which may<br/>
    /// be zero).  This amount is added as extra delay to the<br/>
    /// packet.  When a subsequent packet is queued, it receives its<br/>
    /// own random jitter amount from the current time.  if this would<br/>
    /// result in the packets being delivered out of order, the later<br/>
    /// packet queue time is adjusted to happen after the first packet.<br/>
    /// Thus simulating jitter by itself will not reorder packets, but it<br/>
    /// can "clump" them.
    /// </para>
    ///
    /// <para>
    /// - Avg: A random jitter time is generated using an exponential<br/>
    ///   distribution using this value as the mean (ms).  The default<br/>
    ///   is zero, which disables random jitter.<br/>
    /// - Max: Limit the random jitter time to this value (ms).<br/>
    /// - Pct: odds (0-100) that a random jitter value for the packet<br/>
    ///   will be generated.  Otherwise, a jitter value of zero<br/>
    ///   is used, and the packet will only be delayed by the jitter<br/>
    ///   system if necessary to retain order, due to the jitter of a<br/>
    ///   previous packet.
    /// </para>
    ///
    /// <para>
    /// All values are [global float]
    /// </para>
    ///
    /// <para>
    /// Fake jitter is simulated after fake lag, but before reordering.
    /// </para>
    /// </summary>
    FakePacketJitter_Send_Max = 54,

    /// <summary>
    /// <para>
    /// Simulated jitter/clumping.
    /// </para>
    ///
    /// <para>
    /// For each packet, a jitter value is determined (which may<br/>
    /// be zero).  This amount is added as extra delay to the<br/>
    /// packet.  When a subsequent packet is queued, it receives its<br/>
    /// own random jitter amount from the current time.  if this would<br/>
    /// result in the packets being delivered out of order, the later<br/>
    /// packet queue time is adjusted to happen after the first packet.<br/>
    /// Thus simulating jitter by itself will not reorder packets, but it<br/>
    /// can "clump" them.
    /// </para>
    ///
    /// <para>
    /// - Avg: A random jitter time is generated using an exponential<br/>
    ///   distribution using this value as the mean (ms).  The default<br/>
    ///   is zero, which disables random jitter.<br/>
    /// - Max: Limit the random jitter time to this value (ms).<br/>
    /// - Pct: odds (0-100) that a random jitter value for the packet<br/>
    ///   will be generated.  Otherwise, a jitter value of zero<br/>
    ///   is used, and the packet will only be delayed by the jitter<br/>
    ///   system if necessary to retain order, due to the jitter of a<br/>
    ///   previous packet.
    /// </para>
    ///
    /// <para>
    /// All values are [global float]
    /// </para>
    ///
    /// <para>
    /// Fake jitter is simulated after fake lag, but before reordering.
    /// </para>
    /// </summary>
    FakePacketJitter_Send_Pct = 55,

    /// <summary>
    /// <para>
    /// Simulated jitter/clumping.
    /// </para>
    ///
    /// <para>
    /// For each packet, a jitter value is determined (which may<br/>
    /// be zero).  This amount is added as extra delay to the<br/>
    /// packet.  When a subsequent packet is queued, it receives its<br/>
    /// own random jitter amount from the current time.  if this would<br/>
    /// result in the packets being delivered out of order, the later<br/>
    /// packet queue time is adjusted to happen after the first packet.<br/>
    /// Thus simulating jitter by itself will not reorder packets, but it<br/>
    /// can "clump" them.
    /// </para>
    ///
    /// <para>
    /// - Avg: A random jitter time is generated using an exponential<br/>
    ///   distribution using this value as the mean (ms).  The default<br/>
    ///   is zero, which disables random jitter.<br/>
    /// - Max: Limit the random jitter time to this value (ms).<br/>
    /// - Pct: odds (0-100) that a random jitter value for the packet<br/>
    ///   will be generated.  Otherwise, a jitter value of zero<br/>
    ///   is used, and the packet will only be delayed by the jitter<br/>
    ///   system if necessary to retain order, due to the jitter of a<br/>
    ///   previous packet.
    /// </para>
    ///
    /// <para>
    /// All values are [global float]
    /// </para>
    ///
    /// <para>
    /// Fake jitter is simulated after fake lag, but before reordering.
    /// </para>
    /// </summary>
    FakePacketJitter_Recv_Avg = 56,

    /// <summary>
    /// <para>
    /// Simulated jitter/clumping.
    /// </para>
    ///
    /// <para>
    /// For each packet, a jitter value is determined (which may<br/>
    /// be zero).  This amount is added as extra delay to the<br/>
    /// packet.  When a subsequent packet is queued, it receives its<br/>
    /// own random jitter amount from the current time.  if this would<br/>
    /// result in the packets being delivered out of order, the later<br/>
    /// packet queue time is adjusted to happen after the first packet.<br/>
    /// Thus simulating jitter by itself will not reorder packets, but it<br/>
    /// can "clump" them.
    /// </para>
    ///
    /// <para>
    /// - Avg: A random jitter time is generated using an exponential<br/>
    ///   distribution using this value as the mean (ms).  The default<br/>
    ///   is zero, which disables random jitter.<br/>
    /// - Max: Limit the random jitter time to this value (ms).<br/>
    /// - Pct: odds (0-100) that a random jitter value for the packet<br/>
    ///   will be generated.  Otherwise, a jitter value of zero<br/>
    ///   is used, and the packet will only be delayed by the jitter<br/>
    ///   system if necessary to retain order, due to the jitter of a<br/>
    ///   previous packet.
    /// </para>
    ///
    /// <para>
    /// All values are [global float]
    /// </para>
    ///
    /// <para>
    /// Fake jitter is simulated after fake lag, but before reordering.
    /// </para>
    /// </summary>
    FakePacketJitter_Recv_Max = 57,

    /// <summary>
    /// <para>
    /// Simulated jitter/clumping.
    /// </para>
    ///
    /// <para>
    /// For each packet, a jitter value is determined (which may<br/>
    /// be zero).  This amount is added as extra delay to the<br/>
    /// packet.  When a subsequent packet is queued, it receives its<br/>
    /// own random jitter amount from the current time.  if this would<br/>
    /// result in the packets being delivered out of order, the later<br/>
    /// packet queue time is adjusted to happen after the first packet.<br/>
    /// Thus simulating jitter by itself will not reorder packets, but it<br/>
    /// can "clump" them.
    /// </para>
    ///
    /// <para>
    /// - Avg: A random jitter time is generated using an exponential<br/>
    ///   distribution using this value as the mean (ms).  The default<br/>
    ///   is zero, which disables random jitter.<br/>
    /// - Max: Limit the random jitter time to this value (ms).<br/>
    /// - Pct: odds (0-100) that a random jitter value for the packet<br/>
    ///   will be generated.  Otherwise, a jitter value of zero<br/>
    ///   is used, and the packet will only be delayed by the jitter<br/>
    ///   system if necessary to retain order, due to the jitter of a<br/>
    ///   previous packet.
    /// </para>
    ///
    /// <para>
    /// All values are [global float]
    /// </para>
    ///
    /// <para>
    /// Fake jitter is simulated after fake lag, but before reordering.
    /// </para>
    /// </summary>
    FakePacketJitter_Recv_Pct = 58,

    /// <summary>
    /// <para>
    /// [global float] 0-100 Percentage of packets we will add additional<br/>
    /// delay to.  If other packet(s) are sent/received within this delay<br/>
    /// window (that doesn't also randomly receive the same extra delay),<br/>
    /// then the packets become reordered.
    /// </para>
    ///
    /// <para>
    /// This mechanism is primarily intended to generate out-of-order<br/>
    /// packets.  To simulate random jitter, use the FakePacketJitter.<br/>
    /// Fake packet reordering is applied after fake lag and jitter
    /// </para>
    /// </summary>
    FakePacketReorder_Send = 6,

    /// <summary>
    /// <para>
    /// [global float] 0-100 Percentage of packets we will add additional<br/>
    /// delay to.  If other packet(s) are sent/received within this delay<br/>
    /// window (that doesn't also randomly receive the same extra delay),<br/>
    /// then the packets become reordered.
    /// </para>
    ///
    /// <para>
    /// This mechanism is primarily intended to generate out-of-order<br/>
    /// packets.  To simulate random jitter, use the FakePacketJitter.<br/>
    /// Fake packet reordering is applied after fake lag and jitter
    /// </para>
    /// </summary>
    FakePacketReorder_Recv = 7,

    /// <summary>
    /// [global int32] Extra delay, in ms, to apply to reordered<br/>
    /// packets.  The same time value is used for sending and receiving.
    /// </summary>
    FakePacketReorder_Time = 8,

    /// <summary>
    /// [global float 0--100] Globally duplicate some percentage of packets.
    /// </summary>
    FakePacketDup_Send = 26,

    /// <summary>
    /// [global float 0--100] Globally duplicate some percentage of packets.
    /// </summary>
    FakePacketDup_Recv = 27,

    /// <summary>
    /// [global int32] Amount of delay, in ms, to delay duplicated packets.<br/>
    /// (We chose a random delay between 0 and this value)
    /// </summary>
    FakePacketDup_TimeMax = 28,

    /// <summary>
    /// [global int32] Trace every UDP packet, similar to Wireshark or tcpdump.<br/>
    /// Value is max number of bytes to dump.  -1 disables tracing.<br/>
    /// 0 only traces the info but no actual data bytes
    /// </summary>
    PacketTraceMaxBytes = 41,

    /// <summary>
    /// [global int32] Global UDP token bucket rate limits.<br/>
    /// "Rate" refers to the steady state rate. (Bytes/sec, the<br/>
    /// rate that tokens are put into the bucket.)  "Burst"<br/>
    /// refers to the max amount that could be sent in a single<br/>
    /// burst.  (In bytes, the max capacity of the bucket.)<br/>
    /// Rate=0 disables the limiter entirely, which is the default.<br/>
    /// Burst=0 disables burst.  (This is not realistic.  A<br/>
    /// burst of at least 4K is recommended; the default is higher.)
    /// </summary>
    FakeRateLimit_Send_Rate = 42,

    /// <summary>
    /// [global int32] Global UDP token bucket rate limits.<br/>
    /// "Rate" refers to the steady state rate. (Bytes/sec, the<br/>
    /// rate that tokens are put into the bucket.)  "Burst"<br/>
    /// refers to the max amount that could be sent in a single<br/>
    /// burst.  (In bytes, the max capacity of the bucket.)<br/>
    /// Rate=0 disables the limiter entirely, which is the default.<br/>
    /// Burst=0 disables burst.  (This is not realistic.  A<br/>
    /// burst of at least 4K is recommended; the default is higher.)
    /// </summary>
    FakeRateLimit_Send_Burst = 43,

    /// <summary>
    /// [global int32] Global UDP token bucket rate limits.<br/>
    /// "Rate" refers to the steady state rate. (Bytes/sec, the<br/>
    /// rate that tokens are put into the bucket.)  "Burst"<br/>
    /// refers to the max amount that could be sent in a single<br/>
    /// burst.  (In bytes, the max capacity of the bucket.)<br/>
    /// Rate=0 disables the limiter entirely, which is the default.<br/>
    /// Burst=0 disables burst.  (This is not realistic.  A<br/>
    /// burst of at least 4K is recommended; the default is higher.)
    /// </summary>
    FakeRateLimit_Recv_Rate = 44,

    /// <summary>
    /// [global int32] Global UDP token bucket rate limits.<br/>
    /// "Rate" refers to the steady state rate. (Bytes/sec, the<br/>
    /// rate that tokens are put into the bucket.)  "Burst"<br/>
    /// refers to the max amount that could be sent in a single<br/>
    /// burst.  (In bytes, the max capacity of the bucket.)<br/>
    /// Rate=0 disables the limiter entirely, which is the default.<br/>
    /// Burst=0 disables burst.  (This is not realistic.  A<br/>
    /// burst of at least 4K is recommended; the default is higher.)
    /// </summary>
    FakeRateLimit_Recv_Burst = 45,

    /// <summary>
    /// <para>
    /// Timeout used for out-of-order correction.  This is used when we see a small<br/>
    /// gap in the sequence number on a packet flow.  For example let's say we are<br/>
    /// processing packet 105 when the most recent one was 103.  104 might have dropped,<br/>
    /// but there is also a chance that packets are simply being reordered.  It is very<br/>
    /// common on certain types of connections for packet 104 to arrive very soon after 105,<br/>
    /// especially if 104 was large and 104 was small.  In this case, when we see packet 105<br/>
    /// we will shunt it aside and pend it, in the hopes of seeing 104 soon after.  If 104<br/>
    /// arrives before the a timeout occurs, then we can deliver the packets in order to the<br/>
    /// remainder of packet processing, and we will record this as a "correctable" out-of-order<br/>
    /// situation.  If the timer expires, then we will process packet 105, and assume for now<br/>
    /// that 104 has dropped.  (If 104 later arrives, we will process it, but that will be<br/>
    /// accounted for as uncorrected.)
    /// </para>
    ///
    /// <para>
    /// The default value is 1000 microseconds.  Note that the Windows scheduler does not<br/>
    /// have microsecond precision.
    /// </para>
    ///
    /// <para>
    /// Set the value to 0 to disable out of order correction at the packet layer.<br/>
    /// In many cases we are still effectively able to correct the situation because<br/>
    /// reassembly of message fragments is tolerant of fragments packets arriving out of<br/>
    /// order.  Also, when messages are decoded and inserted into the queue for the app<br/>
    /// to receive them, we will correct out of order messages that have not been<br/>
    /// dequeued by the app yet.  However, when out-of-order packets are corrected<br/>
    /// at the packet layer, they will not reduce the connection quality measure.<br/>
    /// (E.g. SteamNetConnectionRealTimeStatus_t::m_flConnectionQualityLocal)
    /// </para>
    /// </summary>
    OutOfOrderCorrectionWindowMicroseconds = 51,

    // Callbacks

    // On Steam, you may use the default Steam callback dispatch mechanism.  If you prefer
    // to not use this dispatch mechanism (or you are not running with Steam), or you want
    // to associate specific functions with specific listen sockets or connections, you can
    // register them as configuration values.
    //
    // Note also that ISteamNetworkingUtils has some helpers to set these globally.

    /// <summary>
    /// <para>
    /// [connection FnSteamNetConnectionStatusChanged] Callback that will be invoked<br/>
    /// when the state of a connection changes.
    /// </para>
    ///
    /// <para>
    /// IMPORTANT: callbacks are dispatched to the handler that is in effect at the time<br/>
    /// the event occurs, which might be in another thread.  For example, immediately after<br/>
    /// creating a listen socket, you may receive an incoming connection.  And then immediately<br/>
    /// after this, the remote host may close the connection.  All of this could happen<br/>
    /// before the function to create the listen socket has returned.  For this reason,<br/>
    /// callbacks usually must be in effect at the time of object creation.  This means<br/>
    /// you should set them when you are creating the listen socket or connection, or have<br/>
    /// them in effect so they will be inherited at the time of object creation.
    /// </para>
    ///
    /// <para>
    /// For example:
    /// </para>
    ///
    /// <para>
    /// <code>
    /// exterm void MyStatusChangedFunc( SteamNetConnectionStatusChangedCallback_t *info );
    /// SteamNetworkingConfigValue_t opt; opt.SetPtr( Callback_ConnectionStatusChanged, MyStatusChangedFunc );
    /// SteamNetworkingIPAddr localAddress; localAddress.Clear();
    /// HSteamListenSocket hListenSock = SteamNetworkingSockets()->CreateListenSocketIP( localAddress, 1, &amp;opt );
    /// </code>
    /// </para>
    ///
    /// <para>
    /// When accepting an incoming connection, there is no atomic way to switch the<br/>
    /// callback.  However, if the connection is DOA, AcceptConnection() will fail, and<br/>
    /// you can fetch the state of the connection at that time.
    /// </para>
    ///
    /// <para>
    /// If all connections and listen sockets can use the same callback, the simplest<br/>
    /// method is to set it globally before you create any listen sockets or connections.
    /// </para>
    /// </summary>
    Callback_ConnectionStatusChanged = 201,

    /// <summary>
    /// [global FnSteamNetAuthenticationStatusChanged] Callback that will be invoked<br/>
    /// when our auth state changes.  If you use this, install the callback before creating<br/>
    /// any connections or listen sockets, and don't change it.<br/>
    /// See: ISteamNetworkingUtils::SetGlobalCallback_SteamNetAuthenticationStatusChanged
    /// </summary>
    Callback_AuthStatusChanged = 202,

    /// <summary>
    /// [global FnSteamRelayNetworkStatusChanged] Callback that will be invoked<br/>
    /// when our auth state changes.  If you use this, install the callback before creating<br/>
    /// any connections or listen sockets, and don't change it.<br/>
    /// See: ISteamNetworkingUtils::SetGlobalCallback_SteamRelayNetworkStatusChanged
    /// </summary>
    Callback_RelayNetworkStatusChanged = 203,

    /// <summary>
    /// [global FnSteamNetworkingMessagesSessionRequest] Callback that will be invoked<br/>
    /// when a peer wants to initiate a SteamNetworkingMessagesSessionRequest.<br/>
    /// See: ISteamNetworkingUtils::SetGlobalCallback_MessagesSessionRequest
    /// </summary>
    Callback_MessagesSessionRequest = 204,

    /// <summary>
    /// [global FnSteamNetworkingMessagesSessionFailed] Callback that will be invoked<br/>
    /// when a session you have initiated, or accepted either fails to connect, or loses<br/>
    /// connection in some unexpected way.<br/>
    /// See: ISteamNetworkingUtils::SetGlobalCallback_MessagesSessionFailed
    /// </summary>
    Callback_MessagesSessionFailed = 205,

    /// <summary>
    /// [global FnSteamNetworkingSocketsCreateConnectionSignaling] Callback that will<br/>
    /// be invoked when we need to create a signaling object for a connection<br/>
    /// initiated locally.  See: ISteamNetworkingSockets::ConnectP2P,<br/>
    /// ISteamNetworkingMessages.
    /// </summary>
    Callback_CreateConnectionSignaling = 206,

    /// <summary>
    /// [global FnSteamNetworkingFakeIPResult] Callback that's invoked when<br/>
    /// a FakeIP allocation finishes.  See: ISteamNetworkingSockets::BeginAsyncRequestFakeIP,<br/>
    /// ISteamNetworkingUtils::SetGlobalCallback_FakeIPResult
    /// </summary>
    Callback_FakeIPResult = 207,

    // P2P connection settings

    /// <summary>
    /// [connection string] Comma-separated list of STUN servers that can be used<br/>
    /// for NAT piercing.  If you set this to an empty string, NAT piercing will<br/>
    /// not be attempted.  Also if "public" candidates are not allowed for<br/>
    /// P2P_Transport_ICE_Enable, then this is ignored.
    /// </summary>
    P2P_STUN_ServerList = 103,

    /// <summary>
    /// [connection int32] What types of ICE candidates to share with the peer.<br/>
    /// See k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_xxx values
    /// </summary>
    P2P_Transport_ICE_Enable = 104,

    /// <summary>
    /// [connection int32] When selecting P2P transport, add various<br/>
    /// penalties to the scores for selected transports.  (Route selection<br/>
    /// scores are on a scale of milliseconds.  The score begins with the<br/>
    /// route ping time and is then adjusted.)
    /// </summary>
    P2P_Transport_ICE_Penalty = 105,

    /// <summary>
    /// [connection int32] When selecting P2P transport, add various<br/>
    /// penalties to the scores for selected transports.  (Route selection<br/>
    /// scores are on a scale of milliseconds.  The score begins with the<br/>
    /// route ping time and is then adjusted.)
    /// </summary>
    P2P_Transport_SDR_Penalty = 106,

    P2P_TURN_ServerList = 107,
    P2P_TURN_UserList = 108,
    P2P_TURN_PassList = 109,

    P2P_Transport_ICE_Implementation = 110,

    // Settings for SDR relayed connections

    /// <summary>
    /// [global int32] If the first N pings to a port all fail, mark that port as unavailable for<br/>
    /// a while, and try a different one.  Some ISPs and routers may drop the first<br/>
    /// packet, so setting this to 1 may greatly disrupt communications.
    /// </summary>
    SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,

    /// <summary>
    /// [global int32] If N consecutive pings to a port fail, after having received successful<br/>
    /// communication, mark that port as unavailable for a while, and try a<br/>
    /// different one.
    /// </summary>
    SDRClient_ConsecutitivePingTimeoutsFail = 20,

    /// <summary>
    /// [global int32] Minimum number of lifetime pings we need to send, before we think our estimate<br/>
    /// is solid.  The first ping to each cluster is very often delayed because of NAT,<br/>
    /// routers not having the best route, etc.  Until we've sent a sufficient number<br/>
    /// of pings, our estimate is often inaccurate.  Keep pinging until we get this<br/>
    /// many pings.
    /// </summary>
    SDRClient_MinPingsBeforePingAccurate = 21,

    /// <summary>
    /// [global int32] Set all steam datagram traffic to originate from the same<br/>
    /// local port. By default, we open up a new UDP socket (on a different local<br/>
    /// port) for each relay.  This is slightly less optimal, but it works around<br/>
    /// some routers that don't implement NAT properly.  If you have intermittent<br/>
    /// problems talking to relays that might be NAT related, try toggling<br/>
    /// this flag
    /// </summary>
    SDRClient_SingleSocket = 22,

    /// <summary>
    /// [global string] Code of relay cluster to force use.  If not empty, we will<br/>
    /// only use relays in that cluster.  E.g. 'iad'<br/>
    /// </summary>
    SDRClient_ForceRelayCluster = 29,

    /// <summary>
    /// <para>
    /// [connection string] For development, a base-64 encoded ticket generated<br/>
    /// using the cert tool.  This can be used to connect to a gameserver via SDR<br/>
    /// without a ticket generated using the game coordinator.  (You will still<br/>
    /// need a key that is trusted for your app, however.)
    /// </para>
    ///
    /// <para>
    /// This can also be passed using the SDR_DEVTICKET environment variable
    /// </para>
    /// </summary>
    SDRClient_DevTicket = 30,

    /// <summary>
    /// [global string] For debugging.  Override list of relays from the config with<br/>
    /// this set (maybe just one).  Comma-separated list.
    /// </summary>
    SDRClient_ForceProxyAddr = 31,

    /// <summary>
    /// <para>
    /// [global string] For debugging.  Force ping times to clusters to be the specified<br/>
    /// values.  A comma separated list of (cluster)=(ms) values.  E.g. "sto=32,iad=100"
    /// </para>
    ///
    /// <para>
    /// This is a dev configuration value, you probably should not let users modify it<br/>
    /// in production.
    /// </para>
    /// </summary>
    SDRClient_FakeClusterPing = 36,

    /// <summary>
    /// [global int32] When probing the SteamDatagram network, we limit exploration<br/>
    /// to the closest N POPs, based on our current best approximated ping to that POP.
    /// </summary>
    SDRClient_LimitPingProbesToNearestN = 60,

    // Log levels for debugging information of various subsystems.
    // Higher numeric values will cause more stuff to be printed.
    // See ISteamNetworkingUtils::SetDebugOutputFunction for more
    // information
    //
    // The default for all values is k_ESteamNetworkingSocketsDebugOutputType_Warning.

    /// <summary>
    /// [connection int32] RTT calculations for inline pings and replies
    /// </summary>
    LogLevel_AckRTT = 13,

    /// <summary>
    /// [connection int32] log SNP packets send/recv
    /// </summary>
    LogLevel_PacketDecode = 14,

    /// <summary>
    /// [connection int32] log each message send/recv
    /// </summary>
    LogLevel_Message = 15,

    /// <summary>
    /// [connection int32] dropped packets
    /// </summary>
    LogLevel_PacketGaps = 16,

    /// <summary>
    /// [connection int32] P2P rendezvous messages
    /// </summary>
    LogLevel_P2PRendezvous = 17,

    /// <summary>
    /// [global int32] Ping relays
    /// </summary>
    LogLevel_SDRRelayPings = 18,

    // Experimental values.  These are subject to be deleted or change at any time,
    // do not set them, except as a result of an explicit and advanced user opt-in,
    // and do not write anything that depends on them existing, or have any particular
    // behaviour.

    // [global int32] ECN value to send in every packet.<br/>
    // -1 = The default, and means "auto".  We will set ECN=1, if it appears that the local internet connection appears to understand it and there may be some benefit.<br/>
    // 0..2 = use that value.
    ECN = 999,

    // [global int32] If true, send and request different TOS values in probes to relays<br/>
    // to try to deduce if there is any bleaching or mutating of the TOS field in either direction
    SDRClient_EnableTOSProbes = 998,
}
