// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the global achievement percentages have been received from the server.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GlobalAchievementPercentagesReady_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 10;

    /// <summary>
    /// Game ID which these achievement percentages are for.
    /// </summary>
    public ulong GameID;

    /// <summary>
    /// Result of the request. Returns:<br/>
    /// * <see cref="EResult.OK"/> - indicating success.<br/>
    /// * <see cref="EResult.InvalidState"/> - Stats haven't been loaded yet, Call RequestCurrentStats.<br/>
    /// * <see cref="EResult.Fail"/> - if the remote call fails or there are no global achievement percentages for the current AppId.
    /// </summary>
    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
