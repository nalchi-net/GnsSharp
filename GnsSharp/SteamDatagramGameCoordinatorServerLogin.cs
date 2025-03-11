// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// Structure that describes a gameserver attempting to authenticate<br/>
/// with your central server allocator / matchmaking service ("game coordinator").<br/>
/// This is useful because the game coordinator needs to know:
/// </para>
///
/// <para>
/// - What data center is the gameserver running in?<br/>
/// - The routing blob of the gameserver<br/>
/// - Is the gameserver actually trusted?
/// </para>
///
/// <para>
/// Using this structure, you can securely communicate this information<br/>
/// to your server, and you can do this without maintaining any<br/>
/// whitelists or tables of IP addresses.
/// </para>
///
/// <para>
/// See ISteamNetworkingSockets::GetGameCoordinatorServerLogin
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamDatagramGameCoordinatorServerLogin
{
    /// <summary>
    /// Server's identity
    /// </summary>
    public SteamNetworkingIdentity Identity;

    /// <summary>
    /// Routing info.  Note that this includes the POPID
    /// </summary>
    public SteamDatagramHostedAddress Routing;

    /// <summary>
    /// AppID that the server thinks it is running
    /// </summary>
    public AppId_t AppID;

    /// <summary>
    /// Unix timestamp when this was generated
    /// </summary>
    public RTime32 RTime;

    /// <summary>
    /// Size of application data
    /// </summary>
    public int AppDataSize;

    /// <summary>
    /// Application data.  This is any additional information
    /// that you need to identify the server not contained above.
    /// (E.g. perhaps a public IP as seen by the coordinator service.)
    /// </summary>
    public Array2048<byte> AppData;
}
