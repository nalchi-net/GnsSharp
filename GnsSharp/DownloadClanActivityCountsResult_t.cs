// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a Steam group activity has been received.<br/>
/// (<see cref="ISteamFriends.DownloadClanActivityCounts"/> call has finished.)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct DownloadClanActivityCountsResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 41;

    /// <summary>
    /// Was the call successful?
    /// </summary>
    public bool Success;

    public static int CallbackParamId => CallbackId;
}
