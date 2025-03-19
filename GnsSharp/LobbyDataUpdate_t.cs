// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// The lobby metadata has changed.
/// </para>
///
/// <para>
/// If m_ulSteamIDMember is a user in the lobby, then use GetLobbyMemberData to access per-user details;<br/>
/// otherwise, if <see cref="SteamIDMember"/> == <see cref="SteamIDLobby"/>, use GetLobbyData to access the lobby metadata.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyDataUpdate_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 5;

    /// <summary>
    /// The Steam ID of the Lobby.
    /// </summary>
    public ulong SteamIDLobby;

    /// <summary>
    /// Steam ID of either the member whose data changed, or the room itself.
    /// </summary>
    public ulong SteamIDMember;

    /// <summary>
    /// <c>true</c> if the lobby data was successfully changed, otherwise <c>false</c>.
    /// </summary>
    public byte Success;

    public static int CallbackParamId => CallbackId;
}
