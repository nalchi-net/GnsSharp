// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// This callback is posted whenever a connection is created, destroyed, or changes state.<br/>
/// The m_info field will contain a complete description of the connection at the time the<br/>
/// change occurred and the callback was posted.  In particular, m_eState will have the<br/>
/// new connection state.
/// </para>
///
/// <para>
/// You will usually need to listen for this callback to know when:<br/>
/// - A new connection arrives on a listen socket.<br/>
///   m_info.m_hListenSocket will be set, m_eOldState = k_ESteamNetworkingConnectionState_None,<br/>
///   and m_info.m_eState = k_ESteamNetworkingConnectionState_Connecting.<br/>
///   See ISteamNetworkigSockets::AcceptConnection.<br/>
/// - A connection you initiated has been accepted by the remote host.<br/>
///   m_eOldState = k_ESteamNetworkingConnectionState_Connecting, and<br/>
///   m_info.m_eState = k_ESteamNetworkingConnectionState_Connected.<br/>
///   Some connections might transition to k_ESteamNetworkingConnectionState_FindingRoute first.<br/>
/// - A connection has been actively rejected or closed by the remote host.<br/>
///   m_eOldState = k_ESteamNetworkingConnectionState_Connecting or k_ESteamNetworkingConnectionState_Connected,<br/>
///   and m_info.m_eState = k_ESteamNetworkingConnectionState_ClosedByPeer.  m_info.m_eEndReason<br/>
///   and m_info.m_szEndDebug will have for more details.<br/>
///   NOTE: upon receiving this callback, you must still destroy the connection using<br/>
///   ISteamNetworkingSockets::CloseConnection to free up local resources.  (The details<br/>
///   passed to the function are not used in this case, since the connection is already closed.)<br/>
/// - A problem was detected with the connection, and it has been closed by the local host.<br/>
///   The most common failure is timeout, but other configuration or authentication failures<br/>
///   can cause this.  m_eOldState = k_ESteamNetworkingConnectionState_Connecting or<br/>
///   k_ESteamNetworkingConnectionState_Connected, and m_info.m_eState = k_ESteamNetworkingConnectionState_ProblemDetectedLocally.<br/>
///   m_info.m_eEndReason and m_info.m_szEndDebug will have for more details.<br/>
///   NOTE: upon receiving this callback, you must still destroy the connection using<br/>
///   ISteamNetworkingSockets::CloseConnection to free up local resources.  (The details<br/>
///   passed to the function are not used in this case, since the connection is already closed.)
/// </para>
///
/// <para>
/// Remember that callbacks are posted to a queue, and networking connections can<br/>
/// change at any time.  It is possible that the connection has already changed<br/>
/// state by the time you process this callback.
/// </para>
///
/// <para>
/// Also note that callbacks will be posted when connections are created and destroyed by your own API calls.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamNetConnectionStatusChangedCallback_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamNetworkingSocketsCallbacks + 1;

    /// <summary>
    /// Connection handle
    /// </summary>
    public HSteamNetConnection Conn;

    /// <summary>
    /// Full connection info
    /// </summary>
    public SteamNetConnectionInfo_t Info;

    /// <summary>
    /// Previous state.  (Current state is in m_info.m_eState)
    /// </summary>
    public ESteamNetworkingConnectionState OldState;

    public static int CallbackParamId => CallbackId;
}
