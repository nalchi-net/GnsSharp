// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result when creating an webapi ticket from <see cref="ISteamUser.GetAuthTicketForWebApi"/> .
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GetTicketForWebApiResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 68;
    public const int TicketMaxLength = 2560;

    /// <summary>
    /// The handle to the ticket that was created.
    /// </summary>
    public HAuthTicket AuthTicket;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The length of the ticket that was created.
    /// </summary>
    public int TicketLength;

    /// <summary>
    /// The ticket that was created.
    /// </summary>
    public Array2560<byte> Ticket;

    public static int CallbackParamId => CallbackId;
}
