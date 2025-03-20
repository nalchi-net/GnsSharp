// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when an auth ticket has been validated.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct ValidateAuthTicketResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 43;

    /// <summary>
    /// The Steam ID of the entity that provided the auth ticket.
    /// </summary>
    public CSteamID SteamID;

    /// <summary>
    /// The result of the validation.
    /// </summary>
    public EAuthSessionResponse AuthSessionResponse;

    /// <summary>
    /// The Steam ID that owns the game, this will be different from <see cref="SteamID"/> if the game is being accessed via Steam Family Sharing.
    /// </summary>
    public CSteamID OwnerSteamID;

    public static int CallbackParamId => CallbackId;
}
