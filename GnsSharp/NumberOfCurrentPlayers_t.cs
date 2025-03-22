// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Gets the current number of players for the current AppId.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct NumberOfCurrentPlayers_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 7;

    /// <summary>
    /// Was the call successful? Returns <c>1</c> if it was; otherwise, <c>0</c> on failure.
    /// </summary>
    public bool Success;

    /// <summary>
    /// Number of players currently playing.
    /// </summary>
    public int Players;

    public static int CallbackParamId => CallbackId;
}
