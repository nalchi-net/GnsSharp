// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace GnsSharp;

/// <summary>
/// <para>
/// Posted when we fail to establish a connection, or we detect that communications<br/>
/// have been disrupted it an unusual way.  There is no notification when a peer proactively<br/>
/// closes the session.  ("Closed by peer" is not a concept of UDP-style communications, and<br/>
/// SteamNetworkingMessages is primarily intended to make porting UDP code easy.)
/// </para>
///
/// <para>
/// Remember: callbacks are asynchronous.   See notes on SendMessageToUser,<br/>
/// and k_nSteamNetworkingSend_AutoRestartBrokenSession in particular.
/// </para>
///
/// <para>
/// Also, if a session times out due to inactivity, no callbacks will be posted.  The only<br/>
/// way to detect that this is happening is that querying the session state may return<br/>
/// none, connecting, and findingroute again.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SteamNetworkingMessagesSessionFailed_t
{
    public const int Callback = Constants.SteamNetworkingMessagesCallbacks + 2;

    /// <summary>
    /// Detailed info about the session that failed.
    /// SteamNetConnectionInfo_t::m_identityRemote indicates who this session
    /// was with.
    /// </summary>
    public SteamNetConnectionInfo_t Info;
}
