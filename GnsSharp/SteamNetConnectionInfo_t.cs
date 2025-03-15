// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

#pragma warning disable SA1202 // Elements should be ordered by access

/// <summary>
/// Describe the state of a connection.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamNetConnectionInfo_t
{
    /// <summary>
    /// Who is on the other end?<br/>
    /// Depending on the connection type and phase of the connection, we might not know
    /// </summary>
    public SteamNetworkingIdentity IdentityRemote;

    /// <summary>
    /// Arbitrary user data set by the local application code
    /// </summary>
    public long UserData;

    /// <summary>
    /// Handle to listen socket this was connected on, or k_HSteamListenSocket_Invalid if we initiated the connection
    /// </summary>
    public HSteamListenSocket ListenSocket;

    /// <summary>
    /// Remote address.  Might be all 0's if we don't know it, or if this is N/A.<br/>
    /// (E.g. Basically everything except direct UDP connection.)
    /// </summary>
    public SteamNetworkingIPAddr AddrRemote;

    private ushort pad1;

    /// <summary>
    /// What data center is the remote host in?  (0 if we don't know.)
    /// </summary>
    public SteamNetworkingPOPID IdPOPRemote;

    /// <summary>
    /// What relay are we using to communicate with the remote host?<br/>
    /// (0 if not applicable.)
    /// </summary>
    public SteamNetworkingPOPID IdPOPRelay;

    /// <summary>
    /// High level state of the connection
    /// </summary>
    public ESteamNetworkingConnectionState State;

    /// <summary>
    /// Basic cause of the connection termination or problem.<br/>
    /// See ESteamNetConnectionEnd for the values used
    /// </summary>
    public int EndReason;

    private Array128<byte> endDebug;

    private Array128<byte> connectionDescription;

    /// <summary>
    /// Misc flags.  Bitmask of k_nSteamNetworkConnectionInfoFlags_Xxxx
    /// </summary>
    public int Flags;

    /// <summary>
    /// Internal stuff, room to change API easily
    /// </summary>
    private Array63<uint> reserved;

    /// <summary>
    /// Human-readable, but non-localized explanation for connection<br/>
    /// termination or problem.  This is intended for debugging /<br/>
    /// diagnostic purposes only, not to display to users.  It might<br/>
    /// have some details specific to the issue.
    /// </summary>
    public readonly string? EndDebug
    {
        get => Utf8StringHelper.NullTerminatedSpanToString(this.endDebug);
    }

    /// <summary>
    /// <para>
    /// Debug description.  This includes the internal connection ID,<br/>
    /// connection type (and peer information), and any name<br/>
    /// given to the connection by the app.  This string is used in various<br/>
    /// internal logging messages.
    /// </para>
    ///
    /// <para>
    /// Note that the connection ID *usually* matches the HSteamNetConnection<br/>
    /// handle, but in certain cases with symmetric connections it might not.
    /// </para>
    /// </summary>
    public string? ConnectionDescription
    {
        get => Utf8StringHelper.NullTerminatedSpanToString(this.connectionDescription);
    }
}

#pragma warning restore SA1202 // Elements should be ordered by access
