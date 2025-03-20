// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a user has been denied to connection to this game server.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct GSClientDeny_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamGameServerCallbacks + 2;

    /// <summary>
    /// The Steam ID of the user that attempted to connect.
    /// </summary>
    public CSteamID SteamID;

    /// <summary>
    /// The reason the player was denied.
    /// </summary>
    public EDenyReason DenyReason;

    /// <summary>
    /// An optional text message explaining the deny reason.<br/>
    /// Typically unused except for logging.
    /// </summary>
    public Array128<byte> OptionalText;

    public static int CallbackParamId => CallbackId;
}
