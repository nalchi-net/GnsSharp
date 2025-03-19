// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// A chat (text or binary) message for this lobby has been received.<br/>
/// After getting this you must use <see cref="ISteamMatchmaking.GetLobbyChatEntry"/> to retrieve the contents of this message.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyChatMsg_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 7;

    /// <summary>
    /// The Steam ID of the lobby this message was sent in.
    /// </summary>
    public ulong SteamIDLobby;

    /// <summary>
    /// Steam ID of the user who sent this message.<br/>
    /// Note that it could have been the local user.
    /// </summary>
    public ulong SteamIDUser;

    /// <summary>
    /// Type of message received. This is actually a <see cref="EChatEntryType"/>.
    /// </summary>
    public byte ChatEntryType;

    /// <summary>
    /// The index of the chat entry to use with <see cref="ISteamMatchmaking.GetLobbyChatEntry"/>,<br/>
    /// this is not valid outside of the scope of this callback and should never be stored.
    /// </summary>
    public uint ChatID;

    public static int CallbackParamId => CallbackId;
}
