// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// A lobby chat room state has changed, this is usually sent when a user has joined or left the lobby.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyChatUpdate_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 6;

    /// <summary>
    /// The Steam ID of the lobby.
    /// </summary>
    public ulong SteamIDLobby;

    /// <summary>
    /// The user who's status in the lobby just changed - can be recipient.
    /// </summary>
    public ulong SteamIDUserChanged;

    /// <summary>
    /// Chat member who made the change.<br/>
    /// This can be different from <see cref="SteamIDUserChanged"/> if kicking, muting, etc.<br/>
    /// For example, if one user kicks another from the lobby, this will be set to the id of the user who initiated the kick.
    /// </summary>
    public ulong SteamIDMakingChange;

    /// <summary>
    /// Bitfield of <see cref="EChatMemberStateChange"/> values.
    /// </summary>
    public EChatMemberStateChange ChatMemberStateChange;

    public static int CallbackParamId => CallbackId;
}
