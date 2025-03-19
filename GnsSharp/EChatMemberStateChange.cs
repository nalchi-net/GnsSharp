// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;

/// <summary>
/// Flags describing how a users lobby state has changed.
/// This is provided from <see cref="LobbyChatUpdate_t"/>.
/// </summary>
[Flags]
public enum EChatMemberStateChange : uint
{
    // Specific to joining / leaving the chatroom

    /// <summary>
    /// This user has joined or is joining the lobby.
    /// </summary>
    Entered = 1,

    /// <summary>
    /// This user has left or is leaving the lobby.
    /// </summary>
    Left = 2,

    /// <summary>
    /// User disconnected without leaving the lobby first.
    /// </summary>
    Disconnected = 4,

    /// <summary>
    /// The user has been kicked.
    /// </summary>
    Kicked = 8,

    /// <summary>
    /// The user has been kicked and banned.
    /// </summary>
    Banned = 16,
}
