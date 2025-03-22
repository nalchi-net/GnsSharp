// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the latest stats and achievements for a specific user (including the local user) have been received from the server.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct UserStatsReceived_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 1;

    /// <summary>
    /// Game ID that these stats are for.
    /// </summary>
    public ulong GameID;

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
