// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when chat message has been received from a friend.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameConnectedFriendChatMsg_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 43;

    /// <summary>
    /// The Steam ID of the friend that sent the message.
    /// </summary>
    public CSteamID SteamIDUser;

    /// <summary>
    /// The index of the message to get the actual data from with <see cref="ISteamFriends.GetFriendMessage"/>.
    /// </summary>
    public int MessageID;

    public static int CallbackParamId => CallbackId;
}
