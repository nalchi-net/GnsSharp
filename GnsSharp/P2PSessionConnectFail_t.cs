// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when packets can't get through to the specified user.<br/>
/// All queued packets unsent at this point will be dropped, further attempts to send will retry making the connection (but will be dropped if we fail again).
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct P2PSessionConnectFail_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamNetworkingCallbacks + 3;

    /// <summary>
    /// User we were trying to send the packets to.
    /// </summary>
    public CSteamID SteamIDRemote;

    /// <summary>
    /// Indicates the reason why we're having trouble. Actually a <see cref="EP2PSessionError"/>.
    /// </summary>
    public byte P2PSessionError;

    public static int CallbackParamId => CallbackId;
}
