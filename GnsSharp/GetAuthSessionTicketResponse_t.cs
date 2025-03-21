// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result when creating an auth session ticket.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GetAuthSessionTicketResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 63;

    /// <summary>
    /// The handle to the ticket that was created.
    /// </summary>
    public HAuthTicket AuthTicket;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
