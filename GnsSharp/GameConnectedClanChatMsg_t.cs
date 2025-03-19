// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a chat message has been received in a Steam group chat that we are in.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameConnectedClanChatMsg_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 38;

    /// <summary>
    /// The Steam ID of the chat that the message was received in.
    /// </summary>
    public CSteamID SteamIDClanChat;

    /// <summary>
    /// The Steam ID of the user that sent the message.
    /// </summary>
    public CSteamID SteamIDUser;

    /// <summary>
    /// The index of the message to get the actual data from with <see cref="ISteamFriends.GetClanChatMessage"/>.
    /// </summary>
    public int MessageID;

    public static int CallbackParamId => CallbackId;
}
