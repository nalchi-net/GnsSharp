// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the global stats have been received from the server.<br/>
/// Returned as a result of <see cref="ISteamUserStats.RequestGlobalStats"/>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GlobalStatsReceived_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 12;

    /// <summary>
    /// Game ID which these global stats are for.
    /// </summary>
    public ulong GameID;

    /// <summary>
    /// The result of the request. Returns:<br/>
    /// * <see cref="EResult.OK"/> - indicating success.<br/>
    /// * <see cref="EResult.InvalidState"/> - Stats haven't been loaded yet, Call RequestCurrentStats.<br/>
    /// * <see cref="EResult.Fail"/> - if the remote call fails.
    /// </summary>
    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
