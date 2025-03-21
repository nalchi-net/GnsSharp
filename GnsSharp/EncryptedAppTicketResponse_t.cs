// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when an encrypted application ticket has been received.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct EncryptedAppTicketResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 54;

    /// <summary>
    /// Was the call successful?<br/>
    /// Possible results:<br/>
    /// * <see cref="EResult.OK"/> - Success!<br/>
    /// * <see cref="EResult.NoConnection"/> - A connection to Steam could not be established.<br/>
    /// * <see cref="EResult.DuplicateRequest"/> - There is already a pending request.<br/>
    /// * <see cref="EResult.LimitExceeded"/> - This call is subject to a 60 second rate limit, and you have exceeded that.
    /// </summary>
    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
