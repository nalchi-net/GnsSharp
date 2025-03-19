// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <a href="https://partner.steamgames.com/doc/sdk/api#callresults">CallResult</a> from <see cref="ISteamFriends.RequestEquippedProfileItems"/>. Also sent as a <a href="https://partner.steamgames.com/doc/sdk/api#callbacks">Callback</a>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct EquippedProfileItems_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 51;

    public EResult Result;
    public CSteamID SteamID;
    public bool HasAnimatedAvatar;
    public bool HasAvatarFrame;
    public bool HasProfileModifier;
    public bool HasProfileBackground;
    public bool HasMiniProfileBackground;
    public bool FromCache;

    public static int CallbackParamId => CallbackId;
}
