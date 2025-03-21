// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Sent every minute when a appID is owned via a timed trial.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct TimedTrialStatus_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamAppsCallbacks + 30;

    /// <summary>
    /// AppID that is in a timed trial.
    /// </summary>
    public AppId_t AppID;

    /// <summary>
    /// If true, the user is currently offline. Time allowed / played refers to offline time, not total time.
    /// </summary>
    public bool IsOffline;

    /// <summary>
    /// How many seconds the app can be played in total.
    /// </summary>
    public uint SecondsAllowed;

    /// <summary>
    /// How many seconds the app was already played.
    /// </summary>
    public uint SecondsPlayed;

    public static int CallbackParamId => CallbackId;
}
