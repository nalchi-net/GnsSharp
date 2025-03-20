// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a large avatar is loaded in from a previous <see cref="ISteamFriends.GetLargeFriendAvatar"/> call<br/>
/// if you have tried requesting it when it was unavailable.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct AvatarImageLoaded_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 34;

    /// <summary>
    /// The Steam ID that the avatar has been loaded for.
    /// </summary>
    public CSteamID SteamID;

    /// <summary>
    /// The Steam image handle of the now loaded image.
    /// </summary>
    public int Image;

    /// <summary>
    /// Width of the loaded image.
    /// </summary>
    public int Wide;

    /// <summary>
    /// Height of the loaded image.
    /// </summary>
    public int Tall;

    public static int CallbackParamId => CallbackId;
}
