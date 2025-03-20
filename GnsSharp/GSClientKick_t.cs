// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the game server should kick the user.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct GSClientKick_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamGameServerCallbacks + 3;

    /// <summary>
    /// The Steam ID of the player that should be kicked.
    /// </summary>
    public CSteamID SteamID;

    /// <summary>
    /// The reason the player is being kicked.
    /// </summary>
    public EDenyReason DenyReason;

    public static int CallbackParamId => CallbackId;
}
