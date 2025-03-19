// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Callback for when a user's equipped Steam Community profile items have changed.<br/>
/// This can be for the current user or their friends.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct EquippedProfileItemsChanged_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 50;

    public CSteamID SteamID;

    public static int CallbackParamId => CallbackId;
}
