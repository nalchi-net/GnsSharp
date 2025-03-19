// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Result of our request to create a Lobby.<br/>
/// At this point, the lobby has been joined and is ready for use, a <see cref="LobbyEnter_t"/> callback will also be received (since the local user is joining their own lobby).
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LobbyCreated_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 13;

    /// <summary>
    /// <para>
    /// The result of the operation.
    /// </para>
    ///
    /// <para>
    /// Possible values:<br/>
    /// * <see cref="EResult.OK"/> - The lobby was successfully created.<br/>
    /// * <see cref="EResult.Fail"/> - The server responded, but with an unknown internal error.<br/>
    /// * <see cref="EResult.Timeout"/> - The message was sent to the Steam servers, but it didn't respond.<br/>
    /// * <see cref="EResult.LimitExceeded"/> - Your game client has created too many lobbies and is being rate limited.<br/>
    /// * <see cref="EResult.AccessDenied"/> - Your game isn't set to allow lobbies, or your client does haven't rights to play the game<br/>
    /// * <see cref="EResult.NoConnection"/> - Your Steam client doesn't have a connection to the back-end.
    /// </para>
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The Steam ID of the lobby that was created, 0 if failed.
    /// </summary>
    public ulong SteamIDLobby;

    public static int CallbackParamId => CallbackId;
}
