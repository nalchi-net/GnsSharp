// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Recieved upon attempting to enter a lobby.<br/>
/// Lobby metadata is available to use immediately after receiving this.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyEnter_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 4;

    /// <summary>
    /// The steam ID of the Lobby you have entered.
    /// </summary>
    public ulong SteamIDLobby;

    /// <summary>
    /// Unused - Always 0.
    /// </summary>
    public uint ChatPermissions;

    /// <summary>
    /// If <c>true</c>, then only invited users may join.
    /// </summary>
    public bool Locked;

    /// <summary>
    /// This is actually a <see cref="EChatRoomEnterResponse"/> value.<br/>
    /// This will be set to <see cref="EChatRoomEnterResponse.Success"/> if the lobby was successfully joined, otherwise it will be <see cref="EChatRoomEnterResponse.Error"/>.
    /// </summary>
    public EChatRoomEnterResponse ChatRoomEnterResponse;

    public static int CallbackParamId => CallbackId;
}
