// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// Callback indicating that a user's stats have been unloaded.
/// </para>
/// <para>
/// Call <see cref="ISteamUserStats.RequestUserStats"/> again before accessing stats for this user.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct UserStatsUnloaded_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserStatsCallbacks + 8;

    /// <summary>
    /// User whose stats have been unloaded.
    /// </summary>
    public CSteamID SteamIDUser;

    public static int CallbackParamId => CallbackId;
}
