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
    /// For messages received on connections: what connection did this come from?
    /// For outgoing messages: what connection to send it to?
    /// Not used when using the ISteamNetworkingMessages interface
    /// </summary>
    public HSteamNetConnection Connection;

    /// <summary>
    /// For inbound messages: Who sent this to us?
    /// For outbound messages on connections: not used.
    /// For outbound messages on the ad-hoc ISteamNetworkingMessages interface: who should we send this to?
    /// </summary>
    public SteamNetworkingIdentity IdentityPeer;

    /// <summary>
    /// For messages received on connections, this is the user data
    /// associated with the connection.
    ///
    /// This is *usually* the same as calling GetConnection() and then
    /// fetching the user data associated with that connection, but for
    /// the following subtle differences:
    ///
    /// - This user data will match the connection's user data at the time
    ///   is captured at the time the message is returned by the API.
    ///   If you subsequently change the userdata on the connection,
    ///   this won't be updated.
    /// - This is an inline call, so it's *much* faster.
    /// - You might have closed the connection, so fetching the user data
    ///   would not be possible.
    ///
    /// Not used when sending messages.
    /// </summary>
    public long ConnectionUserData;

    /// <summary>
    /// Local timestamp when the message was received
    /// Not used for outbound messages.
    /// </summary>
    public long UsecTimeReceived;

    /// <summary>
    /// Message number assigned by the sender.  This is not used for outbound
    /// messages.  Note that if multiple lanes are used, each lane has its own
    /// message numbers, which are assigned sequentially, so messages from
    /// different lanes will share the same numbers.
    /// </summary>
    public long MessageNumber;

    /// <summary>
    /// Function used to free up m_pData.  This mechanism exists so that
    /// apps can create messages with buffers allocated from their own
    /// heap, and pass them into the library.  This function will
    /// usually be something like:
    ///
    /// free( pMsg->m_pData );
    /// </summary>
    public IntPtr FreeDataFuncPtr;

    /// <summary>
    /// Function to used to decrement the internal reference count and, if
    /// it's zero, release the message.  You should not set this function pointer,
    /// or need to access this directly!  Use the Release() function instead!
    /// </summary>
    public IntPtr ReleaseFuncPtr;

    /// <summary>
    /// When using ISteamNetworkingMessages, the channel number the message was received on
    /// (Not used for messages sent or received on "connections")
    /// </summary>
    public int Channel;

    /// <summary>
    /// Bitmask of k_nSteamNetworkingSend_xxx flags.
    /// For received messages, only the k_nSteamNetworkingSend_Reliable bit is valid.
    /// For outbound messages, all bits are relevant
    /// </summary>
    public ESteamNetworkingSendType Flags;

    /// <summary>
    /// Arbitrary user data that you can use when sending messages using
    /// ISteamNetworkingUtils::AllocateMessage and ISteamNetworkingSockets::SendMessage.
    /// (The callback you set in m_pfnFreeData might use this field.)
    ///
    /// Not used for received messages.
    /// </summary>
    public long UserData;

    /// <summary>
    /// For outbound messages, which lane to use?  See ISteamNetworkingSockets::ConfigureConnectionLanes.
    /// For inbound messages, what lane was the message received on?
    /// </summary>
    public ushort IdxLane;

    private ushort pad1;

    /// <summary>
    /// You MUST call this when you're done with the object,
    /// to free up memory, etc.
    /// </summary>
    public static void Release(IntPtr ptr)
    {
        Native.SteamAPI_SteamNetworkingMessage_t_Release(ptr);
    }
}
