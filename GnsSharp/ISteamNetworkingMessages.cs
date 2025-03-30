// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// The non-connection-oriented interface to send and receive messages<br/>
/// (whether they be "clients" or "servers").<br/>
/// This API is intended to make it easy to port non-connection-oriented code to take advantage of P2P connectivity and <a href="https://partner.steamgames.com/doc/features/multiplayer/steamdatagramrelay">Steam Datagram Relay</a>.
/// </para>
///
/// <para>
/// <see cref="ISteamNetworkingSockets"/> is connection-oriented (like TCP), meaning you<br/>
/// need to listen and connect, and then you send messages using a connection<br/>
/// handle.  <see cref="ISteamNetworkingMessages"/> is more like UDP, in that you can just send<br/>
/// messages to arbitrary peers at any time.  The underlying connections are<br/>
/// established implicitly.
/// </para>
///
/// <para>
/// Under the hood <see cref="ISteamNetworkingMessages"/> works on top of the <see cref="ISteamNetworkingSockets"/><br/>
/// code, so you get the same routing and messaging efficiency.  The difference is<br/>
/// mainly in your responsibility to explicitly establish a connection and<br/>
/// the type of feedback you get about the state of the connection.  Both<br/>
/// interfaces can do "P2P" communications, and both support both unreliable<br/>
/// and reliable messages, fragmentation and reassembly.<br/>
/// If you're on Steam, both can be used to take advantage of <a href="https://partner.steamgames.com/doc/features/multiplayer/steamdatagramrelay">Steam Datagram Relay</a> to talk to dedicated servers.
/// </para>
///
/// <para>
/// The primary purpose of this interface is to be "like UDP", so that UDP-based code<br/>
/// can be ported easily to take advantage of relayed connections.  If you find<br/>
/// yourself needing more low level information or control, or to be able to better<br/>
/// handle failure, then you probably need to use <see cref="ISteamNetworkingSockets"/> directly.<br/>
/// Also, note that if your main goal is to obtain a connection between two peers<br/>
/// without concerning yourself with assigning roles of "client" and "server",<br/>
/// you may find the symmetric connection mode of <see cref="ISteamNetworkingSockets"/> useful.<br/>
/// (See <see cref="ESteamNetworkingConfigValue.SymmetricConnect"/>.)
/// </para>
/// </summary>
public class ISteamNetworkingMessages
{
    private IntPtr ptr = IntPtr.Zero;

#if GNS_SHARP_STEAMWORKS_SDK
    private bool isGameServer;
#endif

    internal ISteamNetworkingMessages(bool isGameServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        if (isGameServer)
        {
            this.ptr = Native.SteamAPI_SteamGameServerNetworkingMessages_SteamAPI_v002();
        }
        else
        {
            this.ptr = Native.SteamAPI_SteamNetworkingMessages_SteamAPI_v002();
        }

        this.isGameServer = isGameServer;
#endif
    }

    public static ISteamNetworkingMessages? User { get; internal set; }

    public static ISteamNetworkingMessages? GameServer { get; internal set; }

    public IntPtr Ptr => this.ptr;

    /// <summary>
    /// <para>
    /// Sends a message to the specified host.  If we don't already have a session with that user,<br/>
    /// a session is implicitly created.  There might be some handshaking that needs to happen<br/>
    /// before we can actually begin sending message data.  If this handshaking fails and we can't<br/>
    /// get through, an error will be posted via the callback <see cref="SteamNetworkingMessagesSessionFailed_t"/>.<br/>
    /// There is no notification when the operation succeeds.  (You should have the peer send a reply<br/>
    /// for this purpose.)
    /// </para>
    ///
    /// <para>
    /// Sending a message to a host will also implicitly accept any incoming connection from that host.
    /// </para>
    ///
    /// <para>
    /// <paramref name="sendFlags"/> is a bitmask of <see cref="ESteamNetworkingSendType"/> options
    /// </para>
    ///
    /// <para>
    /// <paramref name="remoteChannel"/> is a routing number you can use to help route message to different systems.<br/>
    /// You'll have to call ReceiveMessagesOnChannel() with the same channel number in order to retrieve<br/>
    /// the data on the other end.
    /// </para>
    ///
    /// <para>
    /// Using different channels to talk to the same user will still use the same underlying<br/>
    /// connection, saving on resources.  If you don't need this feature, use <c>0</c>.<br/>
    /// Otherwise, small integers are the most efficient.
    /// </para>
    ///
    /// <para>
    /// It is guaranteed that reliable messages to the same host on the same channel<br/>
    /// will be be received by the remote host (if they are received at all) exactly once,<br/>
    /// and in the same order that they were sent.
    /// </para>
    ///
    /// <para>
    /// NO other order guarantees exist!  In particular, unreliable messages may be dropped,<br/>
    /// received out of order with respect to each other and with respect to reliable data,<br/>
    /// or may be received multiple times.  Messages on different channels are *not* guaranteed<br/>
    /// to be received in the order they were sent.
    /// </para>
    ///
    /// <para>
    /// A note for those familiar with TCP/IP ports, or converting an existing codebase that<br/>
    /// opened multiple sockets:  You might notice that there is only one channel, and with<br/>
    /// TCP/IP each endpoint has a port number.  You can think of the channel number as the<br/>
    /// *destination* port.  If you need each message to also include a "source port" (so the<br/>
    /// recipient can route the reply), then just put that in your message.  That is essentially<br/>
    /// how UDP works!
    /// </para>
    ///
    /// <returns>
    /// - <see cref="EResult.OK"/> on success.<br/>
    /// - <see cref="EResult.NoConnection"/>, if the session has failed or was closed by the peer and<br/>
    ///   <see cref="ESteamNetworkingSendType.AutoRestartBrokenSession"/> was not specified.  (You can<br/>
    ///   use <see cref="GetSessionConnectionInfo"/> to get the details.)  In order to acknowledge the<br/>
    ///   broken session and start a new one, you must call <see cref="CloseSessionWithUser"/>, or you may<br/>
    ///   repeat the call with <see cref="ESteamNetworkingSendType.AutoRestartBrokenSession"/>.  See<br/>
    ///   <see cref="ESteamNetworkingSendType.AutoRestartBrokenSession"/> for more details.<br/>
    /// - See <see cref="ISteamNetworkingSockets.SendMessageToConnection"/> for more possible return values
    /// </returns>
    /// </summary>
    public EResult SendMessageToUser(in SteamNetworkingIdentity identityRemote, ReadOnlySpan<byte> data, int sendFlags, int remoteChannel)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_SendMessageToUser(this.ptr, in identityRemote, data, (uint)data.Length, sendFlags, remoteChannel);
#endif
    }

    /// <summary>
    /// <para>
    /// Reads the next message that has been sent from another user via <see cref="SendMessageToUser"/> on the given channel.
    /// Returns number of messages returned into your list.  (<c>0</c> if no message are available on that channel.)
    /// </para>
    ///
    /// <para>
    /// When you're done with the message object(s), make sure and call <see cref="SteamNetworkingMessage_t.Release"/>!
    /// </para>
    /// </summary>
    public int ReceiveMessagesOnChannel(int localChannel, Span<IntPtr> outMessages)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_ReceiveMessagesOnChannel(this.ptr, localChannel, outMessages, outMessages.Length);
#endif
    }

    // Although this API allows for sending messages to peers ad-hoc, under the hood ISteamNetworkingSockets connections are used.
    // We refer to these connections as "sessions" in this context.
    // However, when a remote peer initiates contact, no messages can be received from the peer until the session is accepted, either by sending outbound communications to the peer, or with AcceptSessionWithUser.
    // Sessions automatically time out when communications are idle, but you can terminate them early to free up resources, when you know you are finished.

    /// <summary>
    /// <para>
    /// Call this in response to a <see cref="SteamNetworkingMessagesSessionRequest_t"/> callback.<br/>
    /// <see cref="SteamNetworkingMessagesSessionRequest_t"/> are posted when a user tries to send you a message,<br/>
    /// and you haven't tried to talk to them first.  If you don't want to talk to them, just ignore<br/>
    /// the request.  If the user continues to send you messages, <see cref="SteamNetworkingMessagesSessionRequest_t"/><br/>
    /// callbacks will continue to be posted periodically.
    /// </para>
    ///
    /// <para>
    /// Returns <c>false</c> if there is no session with the user pending or otherwise.  If there is an<br/>
    /// existing active session, this function will return <c>true</c>, even if it is not pending.
    /// </para>
    ///
    /// <para>
    /// Calling <see cref="SendMessageToUser"/> will implicitly accepts any pending session request to that user.
    /// </para>
    /// </summary>
    public bool AcceptSessionWithUser(in SteamNetworkingIdentity identityRemote)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_AcceptSessionWithUser(this.ptr, in identityRemote);
#endif
    }

    /// <summary>
    /// <para>
    /// Call this when you're done talking to a user to immediately free up resources under-the-hood.<br/>
    /// If the remote user tries to send data to you again, another <see cref="SteamNetworkingMessagesSessionRequest_t"/><br/>
    /// callback will be posted.
    /// </para>
    ///
    /// <para>
    /// Note that sessions that go unused for a few minutes are automatically timed out.
    /// </para>
    /// </summary>
    public bool CloseSessionWithUser(in SteamNetworkingIdentity identityRemote)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_CloseSessionWithUser(this.ptr, in identityRemote);
#endif
    }

    /// <summary>
    /// Call this  when you're done talking to a user on a specific channel.  Once all<br/>
    /// open channels to a user have been closed, the open session to the user will be<br/>
    /// closed, and any new data from this user will trigger a<br/>
    /// <see cref="SteamNetworkingMessagesSessionRequest_t"/> callback
    /// </summary>
    public bool CloseChannelWithUser(in SteamNetworkingIdentity identityRemote, int localChannel)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_CloseChannelWithUser(this.ptr, in identityRemote, localChannel);
#endif
    }

    /// <summary>
    /// <para>
    /// Returns information about the latest state of a connection, if any, with the given peer.<br/>
    /// Primarily intended for debugging purposes, but can also be used to get more detailed<br/>
    /// failure information.  (See <see cref="SendMessageToUser"/> and <see cref="ESteamNetworkingSendType.AutoRestartBrokenSession"/>.)
    /// </para>
    ///
    /// <para>
    /// Returns the value of <see cref="SteamNetConnectionInfo_t.State"/>, or <see cref="ESteamNetworkingConnectionState.None"/><br/>
    /// if no connection exists with specified peer.  You may pass nullptr for either parameter if<br/>
    /// you do not need the corresponding details.  Note that sessions time out after a while,<br/>
    /// so if a connection fails, or <see cref="SendMessageToUser"/> returns <see cref="EResult.NoConnection"/>, you cannot wait<br/>
    /// indefinitely to obtain the reason for failure.
    /// </para>
    /// </summary>
    public ESteamNetworkingConnectionState GetSessionConnectionInfo(in SteamNetworkingIdentity identityRemote, out SteamNetConnectionInfo_t connectionInfo, out SteamNetConnectionRealTimeStatus_t quickStatus)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_GetSessionConnectionInfo(this.ptr, in identityRemote, out connectionInfo, out quickStatus);
#endif
    }

    /// <summary>
    /// <para>
    /// Returns information about the latest state of a connection, if any, with the given peer.<br/>
    /// Primarily intended for debugging purposes, but can also be used to get more detailed<br/>
    /// failure information.  (See <see cref="SendMessageToUser"/> and <see cref="ESteamNetworkingSendType.AutoRestartBrokenSession"/>.)
    /// </para>
    ///
    /// <para>
    /// Returns the value of <see cref="SteamNetConnectionInfo_t.State"/>, or <see cref="ESteamNetworkingConnectionState.None"/><br/>
    /// if no connection exists with specified peer.  You may pass nullptr for either parameter if<br/>
    /// you do not need the corresponding details.  Note that sessions time out after a while,<br/>
    /// so if a connection fails, or <see cref="SendMessageToUser"/> returns <see cref="EResult.NoConnection"/>, you cannot wait<br/>
    /// indefinitely to obtain the reason for failure.
    /// </para>
    /// </summary>
    public ESteamNetworkingConnectionState GetSessionConnectionInfo(in SteamNetworkingIdentity identityRemote, out SteamNetConnectionRealTimeStatus_t quickStatus)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_GetSessionConnectionInfo(this.ptr, in identityRemote, IntPtr.Zero, out quickStatus);
#endif
    }

    /// <summary>
    /// <para>
    /// Returns information about the latest state of a connection, if any, with the given peer.<br/>
    /// Primarily intended for debugging purposes, but can also be used to get more detailed<br/>
    /// failure information.  (See <see cref="SendMessageToUser"/> and <see cref="ESteamNetworkingSendType.AutoRestartBrokenSession"/>.)
    /// </para>
    ///
    /// <para>
    /// Returns the value of <see cref="SteamNetConnectionInfo_t.State"/>, or <see cref="ESteamNetworkingConnectionState.None"/><br/>
    /// if no connection exists with specified peer.  You may pass nullptr for either parameter if<br/>
    /// you do not need the corresponding details.  Note that sessions time out after a while,<br/>
    /// so if a connection fails, or <see cref="SendMessageToUser"/> returns <see cref="EResult.NoConnection"/>, you cannot wait<br/>
    /// indefinitely to obtain the reason for failure.
    /// </para>
    /// </summary>
    public ESteamNetworkingConnectionState GetSessionConnectionInfo(in SteamNetworkingIdentity identityRemote, out SteamNetConnectionInfo_t connectionInfo)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_GetSessionConnectionInfo(this.ptr, in identityRemote, out connectionInfo, IntPtr.Zero);
#endif
    }

    /// <summary>
    /// <para>
    /// Returns information about the latest state of a connection, if any, with the given peer.<br/>
    /// Primarily intended for debugging purposes, but can also be used to get more detailed<br/>
    /// failure information.  (See <see cref="SendMessageToUser"/> and <see cref="ESteamNetworkingSendType.AutoRestartBrokenSession"/>.)
    /// </para>
    ///
    /// <para>
    /// Returns the value of <see cref="SteamNetConnectionInfo_t.State"/>, or <see cref="ESteamNetworkingConnectionState.None"/><br/>
    /// if no connection exists with specified peer.  You may pass nullptr for either parameter if<br/>
    /// you do not need the corresponding details.  Note that sessions time out after a while,<br/>
    /// so if a connection fails, or <see cref="SendMessageToUser"/> returns <see cref="EResult.NoConnection"/>, you cannot wait<br/>
    /// indefinitely to obtain the reason for failure.
    /// </para>
    /// </summary>
    public ESteamNetworkingConnectionState GetSessionConnectionInfo(in SteamNetworkingIdentity identityRemote)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't expose ISteamNetworkingMessages flat API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingMessages_GetSessionConnectionInfo(this.ptr, in identityRemote, IntPtr.Zero, IntPtr.Zero);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case SteamNetworkingMessagesSessionRequest_t.CallbackId:
                {
                    // This one should match the usage pattern of open source GNS.
                    // So, it's not exposed as an event.
                    this.HandleSessionRequest(ref msg);
                    break;
                }

            case SteamNetworkingMessagesSessionFailed_t.CallbackId:
                {
                    // This one should match the usage pattern of open source GNS.
                    // So, it's not exposed as an event.
                    this.HandleSessionFailed(ref msg);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamNetworkingMessages.OnDispatch()");
                break;
        }
    }

    /// <summary>
    /// <para>
    /// Handles session request callback.
    /// </para>
    ///
    /// <para>
    /// This is a pretty hacky solution to support the same usage on both the open source GNS and Steamworks.
    /// </para>
    /// </summary>
    private void HandleSessionRequest(ref CallbackMsg_t msg)
    {
        ref var sessionReq = ref msg.GetCallbackParamAs<SteamNetworkingMessagesSessionRequest_t>();

        // Get the callback function pointer registered for this connection
        SizeT resultSize;
        unsafe
        {
            resultSize = (SizeT)sizeof(IntPtr);
        }

        SizeT prevResultSize = resultSize;
        Span<IntPtr> sessionReqCallbackPtr = stackalloc IntPtr[1];

        var netUtils = this.isGameServer ? ISteamNetworkingUtils.GameServer! : ISteamNetworkingUtils.User!;

        var getConfigValueResult = netUtils.GetConfigValue(ESteamNetworkingConfigValue.Callback_MessagesSessionRequest, ESteamNetworkingConfigScope.Global, IntPtr.Zero, MemoryMarshal.AsBytes(sessionReqCallbackPtr), ref resultSize);

        if (getConfigValueResult == ESteamNetworkingGetConfigValueResult.OK || getConfigValueResult == ESteamNetworkingGetConfigValueResult.OKInherited)
        {
            Debug.Assert(prevResultSize == resultSize, $"Config value size expected {prevResultSize}, got {resultSize}");

            // Call the callback if it is set
            if (sessionReqCallbackPtr[0] != IntPtr.Zero)
            {
                var sessionReqCallback = Marshal.GetDelegateForFunctionPointer<FnSteamNetworkingMessagesSessionRequest>(sessionReqCallbackPtr[0]);

                sessionReqCallback(ref sessionReq);
            }
        }
    }

    /// <summary>
    /// <para>
    /// Handles session failed callback.
    /// </para>
    ///
    /// <para>
    /// This is a pretty hacky solution to support the same usage on both the open source GNS and Steamworks.
    /// </para>
    /// </summary>
    private void HandleSessionFailed(ref CallbackMsg_t msg)
    {
        ref var sessionFailed = ref msg.GetCallbackParamAs<SteamNetworkingMessagesSessionFailed_t>();

        // Get the callback function pointer registered for this connection
        SizeT resultSize;
        unsafe
        {
            resultSize = (SizeT)sizeof(IntPtr);
        }

        SizeT prevResultSize = resultSize;
        Span<IntPtr> sessionFailedCallbackPtr = stackalloc IntPtr[1];

        var netUtils = this.isGameServer ? ISteamNetworkingUtils.GameServer! : ISteamNetworkingUtils.User!;

        var getConfigValueResult = netUtils.GetConfigValue(ESteamNetworkingConfigValue.Callback_MessagesSessionFailed, ESteamNetworkingConfigScope.Global, IntPtr.Zero, MemoryMarshal.AsBytes(sessionFailedCallbackPtr), ref resultSize);

        if (getConfigValueResult == ESteamNetworkingGetConfigValueResult.OK || getConfigValueResult == ESteamNetworkingGetConfigValueResult.OKInherited)
        {
            Debug.Assert(prevResultSize == resultSize, $"Config value size expected {prevResultSize}, got {resultSize}");

            // Call the callback if it is set
            if (sessionFailedCallbackPtr[0] != IntPtr.Zero)
            {
                var sessionFailedCallback = Marshal.GetDelegateForFunctionPointer<FnSteamNetworkingMessagesSessionFailed>(sessionFailedCallbackPtr[0]);

                sessionFailedCallback(ref sessionFailed);
            }
        }
    }
#endif
}
