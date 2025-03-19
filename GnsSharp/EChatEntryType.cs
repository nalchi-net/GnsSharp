// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Purpose: Chat Entry Types (previously was only friend-to-friend message types)
/// </summary>
public enum EChatEntryType : int
{
    Invalid = 0,

    /// <summary>
    /// Normal text message from another user
    /// </summary>
    ChatMsg = 1,

    /// <summary>
    /// Another user is typing (not used in multi-user chat)
    /// </summary>
    Typing = 2,

    /// <summary>
    /// Invite from other user into that users current game
    /// </summary>
    InviteGame = 3,

    /// <summary>
    /// text emote message (deprecated, should be treated as ChatMsg)
    /// </summary>
    Emote = 4,

    /// <summary>
    /// user has left the conversation ( closed chat window )
    /// </summary>
    LeftConversation = 6,

    // Above are previous FriendMsgType entries, now merged into more generic chat entry types

    /// <summary>
    /// user has entered the conversation (used in multi-user chat and group chat)
    /// </summary>
    Entered = 7,

    /// <summary>
    /// user was kicked (data: 64-bit steamid of actor performing the kick)
    /// </summary>
    WasKicked = 8,

    /// <summary>
    /// user was banned (data: 64-bit steamid of actor performing the ban)
    /// </summary>
    WasBanned = 9,

    /// <summary>
    /// user disconnected
    /// </summary>
    Disconnected = 10,

    /// <summary>
    /// a chat message from user's chat history or offilne message
    /// </summary>
    HistoricalChat = 11,

    /// <summary>
    /// a link was removed by the chat filter.
    /// </summary>
    LinkBlocked = 14,
}
