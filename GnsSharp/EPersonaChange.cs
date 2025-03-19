// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;

/// <summary>
/// used in PersonaStateChange_t::m_nChangeFlags to describe what's changed about a user<br/>
/// these flags describe what the client has learned has changed recently, so on startup you'll see a name, avatar &amp; relationship change for every friend
/// </summary>
[Flags]
public enum EPersonaChange : int
{
    Name = 0x0001,
    Status = 0x0002,
    ComeOnline = 0x0004,
    GoneOffline = 0x0008,
    GamePlayed = 0x0010,
    GameServer = 0x0020,
    Avatar = 0x0040,
    JoinedSource = 0x0080,
    LeftSource = 0x0100,
    RelationshipChanged = 0x0200,
    NameFirstSet = 0x0400,
    Broadcast = 0x0800,
    Nickname = 0x1000,
    SteamLevel = 0x2000,
    RichPresence = 0x4000,
}
