// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// List of possible errors returned by SendP2PPacket, these will be sent in the P2PSessionConnectFail_t callback.
/// </summary>
public enum EP2PSessionError : int
{
    /// <summary>
    /// There was no error.
    /// </summary>
    None = 0,

    /// <summary>
    /// The local user doesn't own the app that is running.
    /// </summary>
    NoRightsToApp = 2,

    /// <summary>
    /// The connection timed out because the target user didn't respond, perhaps they aren't calling AcceptP2PSessionWithUser.
    /// Corporate firewalls can also block this (NAT traversal is not firewall traversal), make sure that UDP ports 3478, 4379, and 4380 are open in an outbound direction.
    /// </summary>
    Timeout = 4,
}
