// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Marks the return of a request officer list call.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct ClanOfficerListResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 35;

    /// <summary>
    /// The Steam group that we just got the officer list for.
    /// </summary>
    public CSteamID SteamIDClan;

    /// <summary>
    /// The number of officers in the group. This is the same as <see cref="ISteamFriends.GetClanOfficerCount"/>.
    /// </summary>
    public int Officers;

    /// <summary>
    /// Was the call successful? If it wasn't this may indicate a temporary loss of connection to Steam.<br/>
    /// If this returns <c>true</c>, this does not necessarily mean that all of the info for this Steam group has been downloaded.
    /// </summary>
    public bool Success;

    public static int CallbackParamId => CallbackId;
}
