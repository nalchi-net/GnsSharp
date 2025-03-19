// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a user has joined a Steam group chat that we are in.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameConnectedChatJoin_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 39;

    /// <summary>
    /// The Steam ID of the chat that a user has joined.
    /// </summary>
    public CSteamID SteamIDClanChat;

    /// <summary>
    /// The Steam ID of the user that has joined the chat.
    /// </summary>
    public CSteamID SteamIDUser;

    public static int CallbackParamId => CallbackId;
}
