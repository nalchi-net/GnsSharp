// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// Object that describes a "location" on the Internet with sufficient<br/>
/// detail that we can reasonably estimate an upper bound on the ping between<br/>
/// the two hosts, even if a direct route between the hosts is not possible,<br/>
/// and the connection must be routed through the Steam Datagram Relay network.<br/>
/// This does not contain any information that identifies the host.  Indeed,<br/>
/// if two hosts are in the same building or otherwise have nearly identical<br/>
/// networking characteristics, then it's valid to use the same location<br/>
/// object for both of them.
/// </para>
///
/// <para>
/// NOTE: This object should only be used in the same process!  Do not serialize it,<br/>
/// send it over the wire, or persist it in a file or database!  If you need<br/>
/// to do that, convert it to a string representation using the methods in<br/>
/// ISteamNetworkingUtils().
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamNetworkPingLocation_t
{
    public Array512<byte> Data;
}
