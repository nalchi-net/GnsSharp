// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// A struct used to describe our readiness to participate in authenticated,<br/>
/// encrypted communication.  In order to do this we need:
/// </para>
///
/// <para>
/// - The list of trusted CA certificates that might be relevant for this<br/>
///   app.<br/>
/// - A valid certificate issued by a CA.
/// </para>
///
/// <para>
/// This callback is posted whenever the state of our readiness changes.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamNetAuthenticationStatus_t
{
    public const int CallbackId = Constants.SteamNetworkingSocketsCallbacks + 2;

    /// <summary>
    /// Status
    /// </summary>
    public ESteamNetworkingAvailability Avail;

    private Array256<byte> debugMsg;

    /// <summary>
    /// Non-localized English language status.  For diagnostic/debugging
    /// purposes only.
    /// </summary>
    public readonly string? DebugMsg
    {
        get => Utf8StringHelper.NullTerminatedSpanToString(this.debugMsg);
    }
}
