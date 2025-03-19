// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Posted when the user has attempted to join a Steam group chat via <see cref="ISteamFriends.JoinClanChatRoom"/>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct JoinClanChatRoomCompletionResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 42;

    /// <summary>
    /// The Steam ID of the chat that the user has joined.
    /// </summary>
    public CSteamID SteamIDClanChat;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EChatRoomEnterResponse ChatRoomEnterResponse;

    public static int CallbackParamId => CallbackId;
}
