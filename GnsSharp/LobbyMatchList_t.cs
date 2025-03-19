// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result when requesting the lobby list.<br/>
/// You should iterate over the returned lobbies with <see cref="ISteamMatchmaking.GetLobbyByIndex"/>, from 0 to <see cref="LobbiesMatching"/>-1.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyMatchList_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 10;

    /// <summary>
    /// Number of lobbies that matched search criteria and we have Steam IDs for.
    /// </summary>
    public uint LobbiesMatching;

    public static int CallbackParamId => CallbackId;
}
