// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct SteamNetworkingMessage_t
{
    /// <summary>
    /// Message payload
    /// </summary>
    public IntPtr Data;

    /// <summary>
    /// Size of the payload.
    /// </summary>
    public int Size;

    /// <summary>
    /// For messages received on connections: what connection did this come from?<br/>
    /// For outgoing messages: what connection to send it to?<br/>
    /// Not used when using the ISteamNetworkingMessages interface
    /// </summary>
    public HSteamNetConnection Connection;

    /// <summary>
    /// For inbound messages: Who sent this to us?<br/>
    /// For outbound messages on connections: not used.<br/>
    /// For outbound messages on the ad-hoc ISteamNetworkingMessages interface: who should we send this to?
    /// </summary>
    public SteamNetworkingIdentity IdentityPeer;

    /// <summary>
    /// <para>
    /// For messages received on connections, this is the user data<br/>
    /// associated with the connection.
    /// </para>
    ///
    /// <para>
    /// This is *usually* the same as calling GetConnection() and then<br/>
    /// fetching the user data associated with that connection, but for<br/>
    /// the following subtle differences:
    /// </para>
    ///
    /// <para>
    /// - This user data will match the connection's user data at the time<br/>
    ///   is captured at the time the message is returned by the API.<br/>
    ///   If you subsequently change the userdata on the connection,<br/>
    ///   this won't be updated.<br/>
    /// - This is an inline call, so it's *much* faster.<br/>
    /// - You might have closed the connection, so fetching the user data<br/>
    ///   would not be possible.
    /// </para>
    ///
    /// <para>
    /// Not used when sending messages.
    /// </para>
    /// </summary>
    public long ConnectionUserData;

    /// <summary>
    /// Local timestamp when the message was received<br/>
    /// Not used for outbound messages.
    /// </summary>
    public SteamNetworkingMicroseconds TimeReceived;

    /// <summary>
    /// Message number assigned by the sender.  This is not used for outbound<br/>
    /// messages.  Note that if multiple lanes are used, each lane has its own<br/>
    /// message numbers, which are assigned sequentially, so messages from<br/>
    /// different lanes will share the same numbers.
    /// </summary>
    public long MessageNumber;

    /// <summary>
    /// <para>
    /// Function used to free up m_pData.  This mechanism exists so that<br/>
    /// apps can create messages with buffers allocated from their own<br/>
    /// heap, and pass them into the library.  This function will<br/>
    /// usually be something like:
    /// </para>
    ///
    /// <para>
    /// free( pMsg->m_pData );
    /// </para>
    /// </summary>
    public IntPtr FreeDataFuncPtr;

    /// <summary>
    /// Function to used to decrement the internal reference count and, if<br/>
    /// it's zero, release the message.  You should not set this function pointer,<br/>
    /// or need to access this directly!  Use the Release() function instead!
    /// </summary>
    public IntPtr ReleaseFuncPtr;

    /// <summary>
    /// When using ISteamNetworkingMessages, the channel number the message was received on<br/>
    /// (Not used for messages sent or received on "connections")
    /// </summary>
    public int Channel;

    /// <summary>
    /// Bitmask of k_nSteamNetworkingSend_xxx flags.<br/>
    /// For received messages, only the k_nSteamNetworkingSend_Reliable bit is valid.<br/>
    /// For outbound messages, all bits are relevant
    /// </summary>
    public ESteamNetworkingSendType Flags;

    /// <summary>
    /// <para>
    /// Arbitrary user data that you can use when sending messages using<br/>
    /// ISteamNetworkingUtils::AllocateMessage and ISteamNetworkingSockets::SendMessage.<br/>
    /// (The callback you set in m_pfnFreeData might use this field.)
    /// </para>
    ///
    /// <para>
    /// Not used for received messages.
    /// </para>
    /// </summary>
    public long UserData;

    /// <summary>
    /// For outbound messages, which lane to use?  See ISteamNetworkingSockets::ConfigureConnectionLanes.<br/>
    /// For inbound messages, what lane was the message received on?
    /// </summary>
    public ushort IdxLane;

    private ushort pad1;

    /// <summary>
    /// You MUST call this when you're done with the object,<br/>
    /// to free up memory, etc.
    /// </summary>
    public static void Release(IntPtr ptr)
    {
        Native.SteamAPI_SteamNetworkingMessage_t_Release(ptr);
    }
}
