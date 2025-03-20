// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result when getting the latests stats and achievements for a user from the server.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct GSStatsReceived_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamGameServerStatsCallbacks;

    /// <summary>
    /// Returns whether the call was successful or not.<br/>
    /// If the user has no stats, this will be set to <see cref="EResult.Fail"/>.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The user whose stats were retrieved.
    /// </summary>
    public CSteamID SteamIDUser;

    public static int CallbackParamId => CallbackId;
}
