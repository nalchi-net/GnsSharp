// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result of a request to store the user stats.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct UserStatsStored_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 2;

    /// <summary>
    /// Game ID that these stats are for.
    /// </summary>
    public ulong GameID;

    /// <summary>
    /// Returns whether the call was successful or not.
    /// </summary>
    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
