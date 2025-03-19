// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Currently unused!<br/>
/// If you want to implement kicking at this time then do it with a special packet sent with <see cref="ISteamMatchmaking.SendLobbyChatMsg(CSteamID, string)"/>, when the user gets the packet they should call <see cref="ISteamMatchmaking.LeaveLobby"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyKicked_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 12;

    /// <summary>
    /// Lobby
    /// </summary>
    public ulong SteamIDLobby;

    /// <summary>
    /// User who kicked you - possibly the ID of the lobby itself
    /// </summary>
    public ulong SteamIDAdmin;

    /// <summary>
    /// true if you were kicked from the lobby due to the user losing connection to Steam (currently always true)
    /// </summary>
    public bool KickedDueToDisconnect;

    public static int CallbackParamId => CallbackId;
}
