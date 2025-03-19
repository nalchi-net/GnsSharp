// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a user has left a Steam group chat that the we are in.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameConnectedChatLeave_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 40;

    /// <summary>
    /// The Steam ID of the chat that a user has left.
    /// </summary>
    public CSteamID SteamIDClanChat;

    /// <summary>
    /// The Steam ID of the user that has left the chat.
    /// </summary>
    public CSteamID SteamIDUser;

    /// <summary>
    /// Was the user kicked by an officer (<c>true</c>), or not (<c>false</c>)?
    /// </summary>
    public bool Kicked;

    /// <summary>
    /// Was the user's connection to Steam dropped (<c>true</c>), or did they leave via other means (<c>false</c>)?
    /// </summary>
    public bool Dropped;

    public static int CallbackParamId => CallbackId;
}
