// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Enumerate various causes of connection termination.  These are designed to work similar<br/>
/// to HTTP error codes: the numeric range gives you a rough classification as to the source<br/>
/// of the problem.
/// </summary>
public enum ESteamNetConnectionEnd : int
{
    /// <summary>
    /// Invalid/sentinel value
    /// </summary>
    Invalid = 0,

    // Application codes.  These are the values you will pass to
    // ISteamNetworkingSockets::CloseConnection.  You can use these codes if
    // you want to plumb through application-specific reason codes.
    // If you don't need this facility, feel free to always pass App_Generic.
    //
    // The distinction between "normal" and "exceptional" termination is
    // one you may use if you find useful, but it's not necessary for you
    // to do so.  The only place where we distinguish between normal and
    // exceptional is in connection analytics.  If a significant
    // proportion of connections terminates in an exceptional manner,
    // this can trigger an alert.

    /// <summary>
    /// 1xxx: Application ended the connection in a "usual" manner.<br/>
    ///       E.g.: user intentionally disconnected from the server,<br/>
    ///             gameplay ended normally, etc
    /// </summary>
    App_Min = 1000,

    App_Generic = App_Min,

    /// <summary>
    /// Use codes in this range for "normal" disconnection
    /// </summary>
    App_Max = 1999,

    /// <summary>
    /// 2xxx: Application ended the connection in some sort of exceptional<br/>
    ///       or unusual manner that might indicate a bug or configuration<br/>
    ///       issue.
    /// </summary>
    AppException_Min = 2000,

    AppException_Generic = AppException_Min,

    /// <summary>
    /// Use codes in this range for "unusual" disconnection
    /// </summary>
    AppException_Max = 2999,

    // System codes.  These will be returned by the system when
    // the connection state is k_ESteamNetworkingConnectionState_ClosedByPeer
    // or k_ESteamNetworkingConnectionState_ProblemDetectedLocally.  It is
    // illegal to pass a code in this range to ISteamNetworkingSockets::CloseConnection

    /// <summary>
    /// 3xxx: Connection failed or ended because of problem with the<br/>
    ///       local host or their connection to the Internet.
    /// </summary>
    Local_Min = 3000,

    /// <summary>
    /// You cannot do what you want to do because you're running in offline mode.
    /// </summary>
    Local_OfflineMode = 3001,

    /// <summary>
    /// <para>
    /// We're having trouble contacting many (perhaps all) relays.<br/>
    /// Since it's unlikely that they all went offline at once, the best<br/>
    /// explanation is that we have a problem on our end.  Note that we don't<br/>
    /// bother distinguishing between "many" and "all", because in practice,<br/>
    /// it takes time to detect a connection problem, and by the time<br/>
    /// the connection has timed out, we might not have been able to<br/>
    /// actively probe all of the relay clusters, even if we were able to<br/>
    /// contact them at one time.  So this code just means that:
    /// </para>
    ///
    /// <para>
    /// * We don't have any recent successful communication with any relay.<br/>
    /// * We have evidence of recent failures to communicate with multiple relays.
    /// </para>
    /// </summary>
    Local_ManyRelayConnectivity = 3002,

    /// <summary>
    /// A hosted server is having trouble talking to the relay<br/>
    /// that the client was using, so the problem is most likely<br/>
    /// on our end
    /// </summary>
    Local_HostedServerPrimaryRelay = 3003,

    /// <summary>
    /// We're not able to get the SDR network config.  This is<br/>
    /// *almost* always a local issue, since the network config<br/>
    /// comes from the CDN, which is pretty darn reliable.
    /// </summary>
    Local_NetworkConfig = 3004,

    /// <summary>
    /// Steam rejected our request because we don't have rights<br/>
    /// to do this.
    /// </summary>
    Local_Rights = 3005,

    /// <summary>
    /// ICE P2P rendezvous failed because we were not able to<br/>
    /// determine our "public" address (e.g. reflexive address via STUN)
    ///
    /// If relay fallback is available (it always is on Steam), then<br/>
    /// this is only used internally and will not be returned as a high<br/>
    /// level failure.
    /// </summary>
    Local_P2P_ICE_NoPublicAddresses = 3006,

    Local_Max = 3999,

    /// <summary>
    /// 4xxx: Connection failed or ended, and it appears that the<br/>
    ///       cause does NOT have to do with the local host or their<br/>
    ///       connection to the Internet.  It could be caused by the<br/>
    ///       remote host, or it could be somewhere in between.
    /// </summary>
    Remote_Min = 4000,

    /// <summary>
    /// The connection was lost, and as far as we can tell our connection<br/>
    /// to relevant services (relays) has not been disrupted.  This doesn't<br/>
    /// mean that the problem is "their fault", it just means that it doesn't<br/>
    /// appear that we are having network issues on our end.
    /// </summary>
    Remote_Timeout = 4001,

    /// <summary>
    /// Something was invalid with the cert or crypt handshake<br/>
    /// info you gave me, I don't understand or like your key types, etc.
    /// </summary>
    Remote_BadCrypt = 4002,

    /// <summary>
    /// You presented me with a cert that was I was able to parse<br/>
    /// and *technically* we could use encrypted communication.<br/>
    /// But there was a problem that prevents me from checking your identity<br/>
    /// or ensuring that somebody int he middle can't observe our communication.<br/>
    /// E.g.: - the CA key was missing (and I don't accept unsigned certs)<br/>
    /// - The CA key isn't one that I trust,<br/>
    /// - The cert doesn't was appropriately restricted by app, user, time, data center, etc.<br/>
    /// - The cert wasn't issued to you.<br/>
    /// - etc
    /// </summary>
    Remote_BadCert = 4003,

    /// <summary>
    /// Something wrong with the protocol version you are using.<br/>
    /// (Probably the code you are running is too old.)
    /// </summary>
    Remote_BadProtocolVersion = 4006,

    /// <summary>
    /// <para>
    /// NAT punch failed failed because we never received any public<br/>
    /// addresses from the remote host.  (But we did receive some<br/>
    /// signals form them.)
    /// </para>
    ///
    /// <para>
    /// If relay fallback is available (it always is on Steam), then<br/>
    /// this is only used internally and will not be returned as a high<br/>
    /// level failure.
    /// </para>
    /// </summary>
    Remote_P2P_ICE_NoPublicAddresses = 4007,

    Remote_Max = 4999,

    /// <summary>
    /// 5xxx: Connection failed for some other reason.
    /// </summary>
    Misc_Min = 5000,

    /// <summary>
    /// A failure that isn't necessarily the result of a software bug,<br/>
    /// but that should happen rarely enough that it isn't worth specifically<br/>
    /// writing UI or making a localized message for.<br/>
    /// The debug string should contain further details.
    /// </summary>
    Misc_Generic = 5001,

    /// <summary>
    /// Generic failure that is most likely a software bug.
    /// </summary>
    Misc_InternalError = 5002,

    /// <summary>
    /// The connection to the remote host timed out, but we<br/>
    /// don't know if the problem is on our end, in the middle,<br/>
    /// or on their end.
    /// </summary>
    Misc_Timeout = 5003,

    /// <summary>
    /// There's some trouble talking to Steam.
    /// </summary>
    Misc_SteamConnectivity = 5005,

    /// <summary>
    /// A server in a dedicated hosting situation has no relay sessions<br/>
    /// active with which to talk back to a client.  (It's the client's<br/>
    /// job to open and maintain those sessions.)
    /// </summary>
    Misc_NoRelaySessionsToClient = 5006,

    /// <summary>
    /// P2P rendezvous failed in a way that we don't have more specific<br/>
    /// information
    /// </summary>
    Misc_P2P_Rendezvous = 5008,

    /// <summary>
    /// <para>
    /// NAT punch failed, probably due to NAT/firewall configuration.
    /// </para>
    ///
    /// <para>
    /// If relay fallback is available (it always is on Steam), then<br/>
    /// this is only used internally and will not be returned as a high<br/>
    /// level failure.
    /// </para>
    /// </summary>
    Misc_P2P_NAT_Firewall = 5009,

    /// <summary>
    /// <para>
    /// Our peer replied that it has no record of the connection.<br/>
    /// This should not happen ordinarily, but can happen in a few<br/>
    /// exception cases:
    /// </para>
    ///
    /// <para>
    /// - This is an old connection, and the peer has already cleaned<br/>
    ///   up and forgotten about it.  (Perhaps it timed out and they<br/>
    ///   closed it and were not able to communicate this to us.)<br/>
    /// - A bug or internal protocol error has caused us to try to<br/>
    ///   talk to the peer about the connection before we received<br/>
    ///   confirmation that the peer has accepted the connection.<br/>
    /// - The peer thinks that we have closed the connection for some<br/>
    ///   reason (perhaps a bug), and believes that is it is<br/>
    ///   acknowledging our closure.
    /// </para>
    /// </summary>
    Misc_PeerSentNoConnection = 5010,

    Misc_Max = 5999,
}
