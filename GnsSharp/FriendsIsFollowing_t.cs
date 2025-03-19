// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Returns the result of <see cref="ISteamFriends.IsFollowing"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FriendsIsFollowing_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 45;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The Steam ID that was checked.
    /// </summary>
    public CSteamID SteamID;

    /// <summary>
    /// Are we following the user? (<c>true</c>) or not? (<c>false</c>)
    /// </summary>
    public bool IsFollowing;

    public static int CallbackParamId => CallbackId;
}
