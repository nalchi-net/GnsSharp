// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result of a request to store the user stats.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct GSStatsStored_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamGameServerStatsCallbacks + 1;

    /// <summary>
    /// Returns whether the call was successful or not.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The user for whom the stats were stored
    /// </summary>
    public CSteamID SteamIDUser;

    public static int CallbackParamId => CallbackId;
}
