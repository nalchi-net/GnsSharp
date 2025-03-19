// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Invoked when the status of unread messages changes
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct UnreadChatMessagesChanged_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 48;

    public static int CallbackParamId => CallbackId;
}
