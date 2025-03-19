// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Information about the game a friend is playing.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FriendGameInfo_t
{
    /// <summary>
    /// The game ID that the friend is playing.
    /// </summary>
    public CGameID GameID;

    /// <summary>
    /// The IP of the server the friend is playing on.
    /// </summary>
    public uint GameIP;

    /// <summary>
    /// The port of the server the friend is playing on.
    /// </summary>
    public ushort GamePort;

    /// <summary>
    /// The query port of the server the friend is playing on.
    /// </summary>
    public ushort QueryPort;

    /// <summary>
    /// The Steam ID of the lobby the friend is in.
    /// </summary>
    public CSteamID SteamIDLobby;
}
