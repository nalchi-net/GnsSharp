// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// High level connection status
/// </summary>
public enum ESteamNetworkingConnectionState : int
{
    /// <summary>
    /// Dummy value used to indicate an error condition in the API.<br/>
    /// Specified connection doesn't exist or has already been closed.
    /// </summary>
    None = 0,

    /// <summary>
    /// <para>
    /// We are trying to establish whether peers can talk to each other,<br/>
    /// whether they WANT to talk to each other, perform basic auth,<br/>
    /// and exchange crypt keys.
    /// </para>
    ///
    /// <para>
    /// - For connections on the "client" side (initiated locally):<br/>
    ///   We're in the process of trying to establish a connection.<br/>
    ///   Depending on the connection type, we might not know who they are.<br/>
    ///   Note that it is not possible to tell if we are waiting on the<br/>
    ///   network to complete handshake packets, or for the application layer<br/>
    ///   to accept the connection.
    /// </para>
    ///
    /// <para>
    /// - For connections on the "server" side (accepted through listen socket):<br/>
    ///   We have completed some basic handshake and the client has presented<br/>
    ///   some proof of identity.  The connection is ready to be accepted<br/>
    ///   using AcceptConnection().
    ///   </para>
    ///
    /// <para>
    /// In either case, any unreliable packets sent now are almost certain<br/>
    /// to be dropped.  Attempts to receive packets are guaranteed to fail.<br/>
    /// You may send messages if the send mode allows for them to be queued.<br/>
    /// but if you close the connection before the connection is actually<br/>
    /// established, any queued messages will be discarded immediately.<br/>
    /// (We will not attempt to flush the queue and confirm delivery to the<br/>
    /// remote host, which ordinarily happens when a connection is closed.)
    /// </para>
    /// </summary>
    Connecting = 1,

    /// <summary>
    /// Some connection types use a back channel or trusted 3rd party<br/>
    /// for earliest communication.  If the server accepts the connection,<br/>
    /// then these connections switch into the rendezvous state.  During this<br/>
    /// state, we still have not yet established an end-to-end route (through<br/>
    /// the relay network), and so if you send any messages unreliable, they<br/>
    /// are going to be discarded.
    /// </summary>
    FindingRoute = 2,

    /// <summary>
    /// We've received communications from our peer (and we know<br/>
    /// who they are) and are all good.  If you close the connection now,<br/>
    /// we will make our best effort to flush out any reliable sent data that<br/>
    /// has not been acknowledged by the peer.  (But note that this happens<br/>
    /// from within the application process, so unlike a TCP connection, you are<br/>
    /// not totally handing it off to the operating system to deal with it.)
    /// </summary>
    Connected = 3,

    /// <summary>
    /// <para>
    /// Connection has been closed by our peer, but not closed locally.<br/>
    /// The connection still exists from an API perspective.  You must close the<br/>
    /// handle to free up resources.  If there are any messages in the inbound queue,<br/>
    /// you may retrieve them.  Otherwise, nothing may be done with the connection<br/>
    /// except to close it.
    /// </para>
    ///
    /// <para>
    /// This stats is similar to CLOSE_WAIT in the TCP state machine.
    /// </para>
    /// </summary>
    ClosedByPeer = 4,

    /// <summary>
    /// <para>
    /// A disruption in the connection has been detected locally.  (E.g. timeout,<br/>
    /// local internet connection disrupted, etc.)
    /// </para>
    ///
    /// <para>
    /// The connection still exists from an API perspective.  You must close the<br/>
    /// handle to free up resources.
    /// </para>
    ///
    /// <para>
    /// Attempts to send further messages will fail.  Any remaining received messages<br/>
    /// in the queue are available.
    /// </para>
    /// </summary>
    ProblemDetectedLocally = 5,

    // The following values are used internally and will not be returned by any API.
    // We document them here to provide a little insight into the state machine that is used
    // under the hood.

    /// <summary>
    /// We've disconnected on our side, and from an API perspective the connection is closed.<br/>
    /// No more data may be sent or received.  All reliable data has been flushed, or else<br/>
    /// we've given up and discarded it.  We do not yet know for sure that the peer knows<br/>
    /// the connection has been closed, however, so we're just hanging around so that if we do<br/>
    /// get a packet from them, we can send them the appropriate packets so that they can<br/>
    /// know why the connection was closed (and not have to rely on a timeout, which makes<br/>
    /// it appear as if something is wrong).
    /// </summary>
    FinWait = -1,

    /// <summary>
    /// <para>
    /// We've disconnected on our side, and from an API perspective the connection is closed.<br/>
    /// No more data may be sent or received.  From a network perspective, however, on the wire,<br/>
    /// we have not yet given any indication to the peer that the connection is closed.<br/>
    /// We are in the process of flushing out the last bit of reliable data.  Once that is done,<br/>
    /// we will inform the peer that the connection has been closed, and transition to the<br/>
    /// FinWait state.
    /// </para>
    ///
    /// <para>
    /// Note that no indication is given to the remote host that we have closed the connection,<br/>
    /// until the data has been flushed.  If the remote host attempts to send us data, we will<br/>
    /// do whatever is necessary to keep the connection alive until it can be closed properly.<br/>
    /// But in fact the data will be discarded, since there is no way for the application to<br/>
    /// read it back.  Typically this is not a problem, as application protocols that utilize<br/>
    /// the lingering functionality are designed for the remote host to wait for the response<br/>
    /// before sending any more data.
    /// </para>
    /// </summary>
    Linger = -2,

    /// <summary>
    /// Connection is completely inactive and ready to be destroyed
    /// </summary>
    Dead = -3,
}
