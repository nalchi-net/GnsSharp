// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace GnsSharp;

/// <summary>
/// Posted when a remote host is sending us a message, and we do not already have a session with them
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SteamNetworkingMessagesSessionRequest_t
{
    public const int Callback = Constants.SteamNetworkingMessagesCallbacks + 1;

    /// <summary>
    /// user who wants to talk to us
    /// </summary>
    public SteamNetworkingIdentity IdentityRemote;
}
