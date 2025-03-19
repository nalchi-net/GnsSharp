// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Returns the result of <see cref="ISteamFriends.GetFollowerCount"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FriendsGetFollowerCount_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 44;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The Steam ID of the user we requested the follower count for.
    /// </summary>
    public CSteamID SteamID;

    /// <summary>
    /// The number of followers the user has.
    /// </summary>
    public int Count;

    public static int CallbackParamId => CallbackId;
}
