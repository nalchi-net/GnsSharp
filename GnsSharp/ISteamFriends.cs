// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/// <summary>
/// Interface to access information about individual users and interact with the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a>.
/// </summary>
public class ISteamFriends
{
    /// <summary>
    /// Maximum size in bytes that chat room, lobby, or chat/lobby member metadata may have.
    /// </summary>
    public const int ChatMetadataMax = 8192;

    /// <summary>
    /// The maximum amount of rich presence keys that can be set.
    /// </summary>
    public const int MaxRichPresenceKeys = 30;

    /// <summary>
    /// The maximum length that a rich presence key can be.
    /// </summary>
    public const int MaxRichPresenceKeyLength = 64;

    /// <summary>
    /// The maximum length that a rich presence value can be.
    /// </summary>
    public const int MaxRichPresenceValueLength = 256;

    /// <summary>
    /// The maximum number of followers that will be returned in a <see cref="FriendsEnumerateFollowingList_t"/> call result at once.
    /// </summary>
    public const int EnumerateFollowersMax = 50;

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamFriends()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        this.ptr = Native.SteamAPI_SteamFriends_v018();
#endif
    }

#pragma warning disable CS0067 // The event is never used

    /// <summary>
    /// Called when a large avatar is loaded in from a previous <see cref="GetLargeFriendAvatar"/> call<br/>
    /// if you have tried requesting it when it was unavailable.
    /// </summary>
    public event Callback<AvatarImageLoaded_t>? AvatarImageLoaded;

    /// <summary>
    /// Marks the return of a request officer list call.
    /// </summary>
    public event Callback<ClanOfficerListResponse_t>? ClanOfficerListResponse;

    /// <summary>
    /// Called when a Steam group activity has been received.<br/>
    /// (<see cref="DownloadClanActivityCounts"/> call has finished.)
    /// </summary>
    public event Callback<DownloadClanActivityCountsResult_t>? DownloadClanActivityCountsResult;

    /// <summary>
    /// Called when Rich Presence data has been updated for a user,<br/>
    /// this can happen automatically when friends in the same game update their rich presence, or after a call to <see cref="ISteamFriends.RequestFriendRichPresence"/>.
    /// </summary>
    public event Callback<FriendRichPresenceUpdate_t>? FriendRichPresenceUpdate;

    /// <summary>
    /// Returns the result of EnumerateFollowingList.
    /// </summary>
    public event Callback<FriendsEnumerateFollowingList_t>? FriendsEnumerateFollowingList;

    /// <summary>
    /// Returns the result of <see cref="ISteamFriends.GetFollowerCount"/>.
    /// </summary>
    public event Callback<FriendsGetFollowerCount_t>? FriendsGetFollowerCount;

    /// <summary>
    /// Returns the result of <see cref="ISteamFriends.IsFollowing"/>.
    /// </summary>
    public event Callback<FriendsIsFollowing_t>? FriendsIsFollowing;

    /// <summary>
    /// Called when a user has joined a Steam group chat that we are in.
    /// </summary>
    public event Callback<GameConnectedChatJoin_t>? GameConnectedChatJoin;

    /// <summary>
    /// Called when a user has left a Steam group chat that the we are in.
    /// </summary>
    public event Callback<GameConnectedChatLeave_t>? GameConnectedChatLeave;

    /// <summary>
    /// Called when a chat message has been received in a Steam group chat that we are in.
    /// </summary>
    public event Callback<GameConnectedClanChatMsg_t>? GameConnectedClanChatMsg;

    /// <summary>
    /// Called when chat message has been received from a friend.
    /// </summary>
    public event Callback<GameConnectedFriendChatMsg_t>? GameConnectedFriendChatMsg;

    /// <summary>
    /// Called when the user tries to join a lobby from their friends list or from an invite.<br/>
    /// The game client should attempt to connect to specified lobby when this is received.<br/>
    /// If the game isn't running yet then the game will be automatically launched with the command line parameter <c>+connect_lobby &lt;64-bit lobby Steam ID&gt;</c> instead.
    /// </summary>
    /// <remarks>
    /// NOTE: This callback is made when joining a lobby.<br/>
    /// If the user is attempting to join a game but not a lobby, then the callback <see cref="GameRichPresenceJoinRequested_t"/> will be made.
    /// </remarks>
    public event Callback<GameLobbyJoinRequested_t>? GameLobbyJoinRequested;

    /// <summary>
    /// Posted when the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a> activates or deactivates.<br/>
    /// The game can use this to pause or resume single player games.
    /// </summary>
    public event Callback<GameOverlayActivated_t>? GameOverlayActivated;

    /// <summary>
    /// Called when the user tries to join a game from their friends list or after a user accepts an invite by a friend with <see cref="InviteUserToGame"/>.
    /// </summary>
    /// <remarks>
    /// NOTE: This callback is made when joining a game.<br/>
    /// If the user is attempting to join a lobby, then the callback <see cref="GameLobbyJoinRequested_t"/> will be made.
    /// </remarks>
    public event Callback<GameRichPresenceJoinRequested_t>? GameRichPresenceJoinRequested;

    /// <summary>
    /// Called when the user tries to join a different game server from their friends list.<br/>
    /// The game client should attempt to connect to the specified server when this is received.
    /// </summary>
    public event Callback<GameServerChangeRequested_t>? GameServerChangeRequested;

    /// <summary>
    /// Posted when the user has attempted to join a Steam group chat via <see cref="JoinClanChatRoom"/>
    /// </summary>
    public event Callback<JoinClanChatRoomCompletionResult_t>? JoinClanChatRoomCompletionResult;

    /// <summary>
    /// Called whenever a friends' status changes.
    /// </summary>
    public event Callback<PersonaStateChange_t>? PersonaStateChange;

    /// <summary>
    /// Dispatched when an overlay browser instance is navigated to a protocol/scheme registered by <see cref="RegisterProtocolInOverlayBrowser"/>
    /// </summary>
    public event Callback<OverlayBrowserProtocolNavigation_t>? OverlayBrowserProtocolNavigation;

    /// <summary>
    /// Invoked when the status of unread messages changes
    /// </summary>
    public event Callback<UnreadChatMessagesChanged_t>? UnreadChatMessagesChanged;

    /// <summary>
    /// Callback for when a user's equipped Steam Community profile items have changed.<br/>
    /// This can be for the current user or their friends.
    /// </summary>
    public event Callback<EquippedProfileItemsChanged_t>? EquippedProfileItemsChanged;

#pragma warning restore CS0067

    public static ISteamFriends? User { get; internal set; }

    /// <summary>
    /// <para>
    /// Gets the current user's persona (display) name.
    /// </para>
    /// <para>
    /// This is the same name that is displayed the user's community profile page.
    /// </para>
    /// <para>
    /// To get the persona name of other users use <see cref="GetFriendPersonaName"/>.
    /// </para>
    /// </summary>
    /// <returns><see cref="string"/> The current user's persona name. Guaranteed to not be <c>null</c>.</returns>
    public string GetPersonaName()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        IntPtr raw = Native.SteamAPI_ISteamFriends_GetPersonaName(this.ptr);
        return Marshal.PtrToStringUTF8(raw)!;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the friend status of the current user.
    /// </para>
    /// <para>
    /// To get the state of other users use GetFriendPersonaState.
    /// </para>
    /// </summary>
    /// <returns><see cref="EPersonaState"/> The friend state of the current user. (Online, Offline, In-Game, etc)</returns>
    public EPersonaState GetPersonaState()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetPersonaState(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the number of users the client knows about who meet a specified criteria. (Friends, blocked, users on the same server, etc)
    /// </para>
    /// <para>
    /// This can be used to iterate over all of the users by calling GetFriendByIndex to get the Steam IDs of each user.
    /// </para>
    /// </summary>
    /// <param name="friendFlags">A combined union (binary "or") of one or more <see cref="EFriendFlags"/>.</param>
    /// <returns>
    /// <para>
    /// <see cref="int"/> The number of users that meet the specified criteria.
    /// </para>
    /// <para>
    /// NOTE: Returns <c>-1</c> if the current user is not logged on.
    /// </para>
    /// </returns>
    public int GetFriendCount(EFriendFlags friendFlags)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendCount(this.ptr, friendFlags);
#endif
    }

    /// <summary>
    /// Gets the Steam ID of the user at the given index.
    /// </summary>
    /// <param name="friendIndex">An index between <c>0</c> and <see cref="GetFriendCount"/>.</param>
    /// <param name="friendFlags">A combined union (binary "or") of <see cref="EFriendFlags"/>.<br/>This must be the same value as used in the previous call to <see cref="GetFriendCount"/>.</param>
    /// <returns><see cref="CSteamID"/> Invalid indices return <see cref="CSteamID.Nil"/>.</returns>
    public CSteamID GetFriendByIndex(int friendIndex, EFriendFlags friendFlags)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendByIndex(this.ptr, friendIndex, friendFlags);
#endif
    }

    /// <summary>
    /// Gets a relationship to a specified user.
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the other user.</param>
    /// <returns><see cref="EFriendRelationship"/> How the users know each other.</returns>
    public EFriendRelationship GetFriendRelationship(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendRelationship(this.ptr, steamIDFriend);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the current status of the specified user.
    /// </para>
    /// <para>
    /// This will only be known to the current user if the other user is in their friends list, on the same game server, in a chat room or lobby, or in a small Steam group with the local user.
    /// </para>
    /// <para>
    /// To get the state of the current user use <see cref="GetPersonaState"/>.
    /// </para>
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the other user.</param>
    /// <returns><see cref="EPersonaState"/>The friend state of the specified user. (Online, Offline, In-Game, etc)</returns>
    public EPersonaState GetFriendPersonaState(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendPersonaState(this.ptr, steamIDFriend);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the specified user's persona (display) name.
    /// </para>
    /// <para>
    /// This will only be known to the current user if the other user is in their friends list, on the same game server, in a chat room or lobby, or in a small Steam group with the local user.
    /// </para>
    /// To get the persona name of the current user use <see cref="GetPersonaName"/>.
    /// </summary>
    /// <remarks>
    /// NOTE: Upon on first joining a lobby, chat room, or game server the current user will not known the name of the other users automatically;<br/>
    /// that information will arrive asynchronously via <see cref="PersonaStateChange_t"/> callbacks.
    /// </remarks>
    /// <param name="steamIDFriend">The Steam ID of the other user.</param>
    /// <returns>
    /// <para>
    /// <see cref="string"/> The current user's persona name. Guaranteed to not be <c>null</c>.
    /// </para>
    /// <para>
    /// Returns an empty string (<c>""</c>), or <c>"[unknown]"</c> if the Steam ID is invalid or not known to the caller.
    /// </para>
    /// </returns>
    public string GetFriendPersonaName(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        IntPtr raw = Native.SteamAPI_ISteamFriends_GetFriendPersonaName(this.ptr, steamIDFriend);
        return Marshal.PtrToStringUTF8(raw)!;
#endif
    }

    /// <summary>
    /// Checks if the specified friend is in a game, and gets info about the game if they are.
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the other user.</param>
    /// <param name="friendGameInfo">Fills in the details if the user is in a game.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the user is a friend and is in a game; otherwise, <c>false</c>.</returns>
    public bool GetFriendGamePlayed(CSteamID steamIDFriend, out FriendGameInfo_t friendGameInfo)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendGamePlayed(this.ptr, steamIDFriend, out friendGameInfo);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets one of the previous display names for the specified user.
    /// </para>
    /// <para>
    /// This only works for display names that the current user has seen on the local computer.
    /// </para>
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the other user.</param>
    /// <param name="personaNameIndex">The index of the history to receive. <c>0</c> is their current persona name, <c>1</c> is their most recent before they changed it, etc.</param>
    /// <returns>
    /// <see cref="string"/> The players old persona name at the given index.<br/>
    /// Returns an empty string when there are no further items in the history.
    /// </returns>
    public string? GetFriendPersonaNameHistory(CSteamID steamIDFriend, int personaNameIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetFriendPersonaNameHistory(this.ptr, steamIDFriend, personaNameIndex);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the Steam level of the specified user.
    /// </para>
    /// <para>
    /// You can use the local users Steam ID (<see cref="ISteamUser.GetSteamID"/>) to get their level.
    /// </para>
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the user.</param>
    /// <returns>
    /// <para>
    /// <see cref="int"/> The Steam level if it's available.
    /// </para>
    /// <para>
    /// If the Steam level is not immediately available for the specified user then this returns <c>0</c> and queues it to be downloaded from the Steam servers.<br/>
    /// When it gets downloaded a <see cref="PersonaStateChange_t"/> callback will be posted with <see cref="PersonaStateChange_t.ChangeFlags"/> including <see cref="EPersonaChange.SteamLevel"/>.
    /// </para>
    /// </returns>
    public int GetFriendSteamLevel(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendSteamLevel(this.ptr, steamIDFriend);
#endif
    }

    /// <summary>
    /// Gets the nickname that the current user has set for the specified user.
    /// </summary>
    /// <remarks>
    /// DEPRECATED: GetPersonaName follows the Steam nickname preferences, so apps shouldn't need to care about nicknames explicitly.
    /// </remarks>
    /// <param name="steamIDPlayer">The Steam ID of the user.</param>
    /// <returns><see cref="string"/> <c>null</c> if the no nickname has been set for that user.</returns>
    public string? GetPlayerNickname(CSteamID steamIDPlayer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetPlayerNickname(this.ptr, steamIDPlayer);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the number of friends groups (tags) the user has created.
    /// </para>
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetFriendsGroupIDByIndex"/> can be used to get the ID of each friend group.
    /// </para>
    /// <para>
    /// This is not to be confused with Steam groups. Those can be obtained with <see cref="GetClanCount"/>.
    /// </para>
    /// </summary>
    /// <returns><see cref="int"/> The number of friends groups the current user has.</returns>
    public int GetFriendsGroupCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendsGroupCount(this.ptr);
#endif
    }

    /// <summary>
    /// Gets the friends group ID for the given index.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetFriendsGroupCount"/> before calling this.
    /// </remarks>
    /// <param name="friendGroupIndex">An index between <c>0</c> and <see cref="GetFriendsGroupCount"/>.</param>
    /// <returns><see cref="FriendsGroupID_t"/> Invalid indices return <see cref="FriendsGroupID_t.Invalid"/>.</returns>
    public FriendsGroupID_t GetFriendsGroupIDByIndex(int friendGroupIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex(this.ptr, friendGroupIndex);
#endif
    }

    /// <summary>
    /// Gets the name for the given friends group.
    /// </summary>
    /// <param name="friendsGroupID">The friends group ID to get the name of.</param>
    /// <returns><see cref="string"/> The friend groups name. Returns <c>null</c> if the group ID is invalid.</returns>
    public string? GetFriendsGroupName(FriendsGroupID_t friendsGroupID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetFriendsGroupName(this.ptr, friendsGroupID);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the number of friends in a given friends group.
    /// </para>
    /// <para>
    /// This should be called before getting the list of friends with GetFriendsGroupMembersList.
    /// </para>
    /// </summary>
    /// <param name="friendsGroupID">The friends group ID to get the number of friends in.</param>
    /// <returns><see cref="int"/> The number of friends in the specified friends group.</returns>
    public int GetFriendsGroupMembersCount(FriendsGroupID_t friendsGroupID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendsGroupMembersCount(this.ptr, friendsGroupID);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the number of friends in the given friends group.
    /// </para>
    /// <para>
    /// If fewer friends exist than requested those positions' Steam IDs will be invalid.
    /// </para>
    /// <para>
    /// You must call <see cref="GetFriendsGroupMembersCount"/> before calling this to set up the <paramref name="outSteamIDMembers"/> array with an appropriate size!
    /// </para>
    /// </summary>
    /// <param name="friendsGroupID">The friends group ID to get the members list of.</param>
    /// <param name="outSteamIDMembers">
    /// Returns the Steam IDs of the friends by setting them in this array.<br/>
    /// This should match the value returned by <see cref="GetFriendsGroupMembersCount"/>.
    /// </param>
    public void GetFriendsGroupMembersList(FriendsGroupID_t friendsGroupID, Span<CSteamID> outSteamIDMembers)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_GetFriendsGroupMembersList(this.ptr, friendsGroupID, outSteamIDMembers, outSteamIDMembers.Length);
#endif
    }

    /// <summary>
    /// Checks if the user meets the specified criteria. (Friends, blocked, users on the same server, etc)
    /// </summary>
    /// <param name="steamIDFriend">The Steam user to check the friend status of.</param>
    /// <param name="friendFlags">A combined union (binary "or") of one or more <see cref="EFriendFlags"/>.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the specified user meets any of the criteria specified in <paramref name="friendFlags"/>; otherwise, <c>false</c>.</returns>
    public bool HasFriend(CSteamID steamIDFriend, EFriendFlags friendFlags)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_HasFriend(this.ptr, steamIDFriend, friendFlags);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the number of Steam groups that the current user is a member of.
    /// </para>
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetClanByIndex"/> can be used to get the Steam ID of each Steam group.
    /// </para>
    /// <example>
    /// <code>
    /// void ListSteamGroups() {
    ///     int nGroups = SteamFriends()->GetClanCount();
    ///     printf( "Listing %d Steam Groups\n", nGroups );
    ///     for ( int i = 0; i &lt; nGroups; ++i )
    ///     {
    ///         CSteamID groupSteamID = SteamFriends()->GetClanByIndex( i );
    ///         const char *szGroupName = SteamFriends()->GetClanName( groupSteamID );
    ///         const char *szGroupTag = SteamFriends()->GetClanTag( groupSteamID );
    ///
    ///         int nOnline, nInGame, nChatting;
    ///         bool success = SteamFriends()->GetClanActivityCounts( groupSteamID, &amp;nOnline, &amp;nInGame, &amp;nChatting );
    ///         printf( "Group %d - ID: %lld - Name: '%s' - Tag: '%s'\n", i, groupSteamID.ConvertToUint64(), szGroupName, szGroupTag );
    ///         printf( " - Online: %d, In Game: %d, Chatting: %d\n", nOnline, nInGame, nChatting );
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    /// <returns><see cref="int"/> The number of Steam groups that the user is a member of.</returns>
    public int GetClanCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanCount(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// This API is deprecated.
    /// </para>
    /// <para>
    /// Gets the Steam group's Steam ID at the given index.
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: You must call GetClanCount before calling this.
    /// </remarks>
    /// <param name="clanIndex">An index between <c>0</c> and <see cref="GetClanCount"/>.</param>
    /// <returns><see cref="CSteamID"/> Invalid indices return <see cref="CSteamID.Nil"/>.</returns>
    public CSteamID GetClanByIndex(int clanIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanByIndex(this.ptr, clanIndex);
#endif
    }

    /// <summary>
    /// Gets the display name for the specified Steam group; if the local client knows about it.
    /// </summary>
    /// <param name="steamIDClan">The Steam group to get the name of.</param>
    /// <returns><see cref="string"/> The Steam group's name. Returns an empty string (<c>""</c>) if the provided Steam ID is invalid or the user does not know about the group.</returns>
    public string? GetClanName(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetClanName(this.ptr, steamIDClan);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the unique tag (abbreviation) for the specified Steam group; If the local client knows about it.
    /// </para>
    /// <para>
    /// The Steam group abbreviation is a unique way for people to identify the group and is limited to 12 characters.<br/>
    /// In some games this will appear next to the name of group members.
    /// </para>
    /// </summary>
    /// <param name="steamIDClan">The Steam group to get the tag of.</param>
    /// <returns><see cref="string"/> The Steam groups tag. Returns an empty string (<c>""</c>) if the provided Steam ID is invalid or the user does not know about the group.</returns>
    public string? GetClanTag(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetClanTag(this.ptr, steamIDClan);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the most recent information we have about what the users in a Steam Group are doing.
    /// </para>
    /// <para>
    /// This can only retrieve data that the local client knows about.<br/>
    /// To refresh the data or get data from a group other than one that the current user is a member of you must call <see cref="DownloadClanActivityCounts"/>.
    /// </para>
    /// </summary>
    /// <param name="steamIDClan">The Steam group to get the activity of.</param>
    /// <param name="online">Returns the number of members that are online.</param>
    /// <param name="inGame">Returns the number members that are in game (excluding those with their status set to offline).</param>
    /// <param name="chatting">Returns the number of members in the group chat room.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the data was successfully returned.
    /// <c>false</c> if the provided Steam ID is invalid or the local client does not have info about the Steam group and sets all the other parameters to <c>0</c>.
    /// </returns>
    public bool GetClanActivityCounts(CSteamID steamIDClan, out int online, out int inGame, out int chatting)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanActivityCounts(this.ptr, steamIDClan, out online, out inGame, out chatting);
#endif
    }

    /// <summary>
    /// <para>
    /// Refresh the Steam Group activity data or get the data from groups other than one that the current user is a member.
    /// </para>
    /// <para>
    /// After receiving the callback you can then use <see cref="GetClanActivityCounts"/> to get the up to date user counts.
    /// </para>
    /// <para>
    /// For clans a user is a member of, they will have reasonably up-to-date information, but for others you'll have to download the info to have the latest.
    /// </para>
    /// </summary>
    /// <param name="steamIDClans">A list of steam groups to get the updated data for.</param>
    /// <returns><see cref="CallTask&lt;DownloadClanActivityCountsResult_t&gt;"/> that will return <see cref="DownloadClanActivityCountsResult_t"/> when awaited.</returns>
    public CallTask<DownloadClanActivityCountsResult_t>? DownloadClanActivityCounts(Span<CSteamID> steamIDClans)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, Span<CSteamID>, int, DownloadClanActivityCountsResult_t>(
        Native.SteamAPI_ISteamFriends_DownloadClanActivityCounts, this.ptr, steamIDClans, steamIDClans.Length);

        return task;
#endif
    }

    /// <summary>
    /// Get the number of users in a source (Steam group, chat room, lobby, or game server).
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: Large Steam groups cannot be iterated by the local user.
    /// </para>
    /// <para>
    /// NOTE: If you're getting the number of lobby members then you should use <see cref="ISteamMatchmaking.GetNumLobbyMembers"/> instead.
    /// </para>
    /// <para>
    /// NOTE: The current user must be in a lobby to retrieve CSteamIDs of other users in that lobby.
    /// </para>
    /// </remarks>
    /// <param name="steamIDSource">The Steam group, chat room, lobby or game server to get the user count of.</param>
    /// <returns><see cref="int"/> <c>0</c> if the Steam ID provided is invalid or if the local user doesn't have the data available.</returns>
    public int GetFriendCountFromSource(CSteamID steamIDSource)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendCountFromSource(this.ptr, steamIDSource);
#endif
    }

    /// <summary>
    /// Gets the Steam ID at the given index from a source (Steam group, chat room, lobby, or game server).
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetFriendCountFromSource"/> before calling this.
    /// </remarks>
    /// <param name="steamIDSource">This MUST be the same source used in the previous call to <see cref="GetFriendCountFromSource"/>!</param>
    /// <param name="friendIndex">An index between <c>0</c> and <see cref="GetFriendCountFromSource"/>.</param>
    /// <returns><see cref="CSteamID"/> Invalid indices return <see cref="CSteamID.Nil"/>.</returns>
    public CSteamID GetFriendFromSourceByIndex(CSteamID steamIDSource, int friendIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendFromSourceByIndex(this.ptr, steamIDSource, friendIndex);
#endif
    }

    /// <summary>
    /// <para>
    /// This API is deprecated.
    /// </para>
    /// <para>
    /// Checks if a specified user is in a source (Steam group, chat room, lobby, or game server).
    /// </para>
    /// </summary>
    /// <param name="steamIDUser">The user to check if they are in the source.</param>
    /// <param name="steamIDSource">The source to check for the user.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the local user can see that steamIDUser is a member or in steamIDSource; otherwise, <c>false</c>.</returns>
    public bool IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_IsUserInSource(this.ptr, steamIDUser, steamIDSource);
#endif
    }

    /// <summary>
    /// <para>
    /// Let Steam know that the user is currently using voice chat in game.<br/>
    /// (User is in a game pressing the talk button)
    /// </para>
    /// <para>
    /// This will suppress the microphone for all voice communication in the Steam UI.
    /// </para>
    /// </summary>
    /// <param name="steamIDUser">Unused.</param>
    /// <param name="speaking">Did the user start speaking in game (<c>true</c>) or stopped speaking in game (<c>false</c>)?</param>
    public void SetInGameVoiceSpeaking(CSteamID steamIDUser, bool speaking)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_SetInGameVoiceSpeaking(this.ptr, steamIDUser, speaking);
#endif
    }

    // activates the game overlay, with an optional dialog to open
    // valid options include "Friends", "Community", "Players", "Settings", "OfficialGameGroup", "Stats", "Achievements",
    // "chatroomgroup/nnnn"

    /// <summary>
    /// <para>
    /// Activates the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a> to a specific dialog.
    /// </para>
    /// <para>
    /// This is equivalent to calling <see cref="ActivateGameOverlayToUser"/> with <c>steamID</c> set to <see cref="ISteamUser.GetSteamID"/>.
    /// </para>
    /// </summary>
    /// <param name="dialog">The dialog to open.<br/>
    /// Valid options are: "friends", "community", "players", "settings", "officialgamegroup", "stats", "achievements".</param>
    public void ActivateGameOverlay(string dialog)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ActivateGameOverlay(this.ptr, dialog);
#endif
    }

    /// <summary>
    /// <para>
    /// Activates <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a> to a specific dialog.
    /// </para>
    /// <para>
    /// Valid pchDialog options are:<br/>
    /// * "steamid" - Opens the overlay web browser to the specified user or groups profile.<br/>
    /// * "chat" - Opens a chat window to the specified user, or joins the group chat.<br/>
    /// * "jointrade" - Opens a window to a Steam Trading session that was started with the ISteamEconomy/StartTrade Web API.<br/>
    /// * "stats" - Opens the overlay web browser to the specified user's stats.<br/>
    /// * "achievements" - Opens the overlay web browser to the specified user's achievements.<br/>
    /// * "friendadd" - Opens the overlay in minimal mode prompting the user to add the target user as a friend.<br/>
    /// * "friendremove" - Opens the overlay in minimal mode prompting the user to remove the target friend.<br/>
    /// * "friendrequestaccept" - Opens the overlay in minimal mode prompting the user to accept an incoming friend invite.<br/>
    /// * "friendrequestignore" - Opens the overlay in minimal mode prompting the user to ignore an incoming friend invite.
    /// </para>
    /// </summary>
    /// <param name="dialog">The dialog to open.</param>
    /// <param name="steamID">The Steam ID of the context to open this dialog to.</param>
    public void ActivateGameOverlayToUser(string dialog, CSteamID steamID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ActivateGameOverlayToUser(this.ptr, dialog, steamID);
#endif
    }

    // activates game overlay web browser directly to the specified URL
    // full address with protocol type is required, e.g. http://www.steamgames.com/

    /// <summary>
    /// Activates <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a> web browser directly to the specified URL.
    /// </summary>
    /// <param name="url">The webpage to open. (A fully qualified address with the protocol is required, e.g."http://www.steampowered.com")</param>
    /// <param name="mode">Mode for the web page. Defaults to <see cref="EActivateGameOverlayToWebPageMode.Default"/></param>
    public void ActivateGameOverlayToWebPage(string url, EActivateGameOverlayToWebPageMode mode = EActivateGameOverlayToWebPageMode.Default)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage(this.ptr, url, mode);
#endif
    }

    /// <summary>
    /// <para>
    /// Activates the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a> to the Steam store page for the provided app.
    /// </para>
    /// <para>
    /// Using k_uAppIdInvalid brings the user to the front page of the Steam store.
    /// </para>
    /// </summary>
    /// <param name="appID">The app ID to show the store page of.</param>
    /// <param name="flag">Flags to modify the behavior when the page opens.</param>
    public void ActivateGameOverlayToStore(AppId_t appID, EOverlayToStoreFlag flag)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ActivateGameOverlayToStore(this.ptr, appID, flag);
#endif
    }

    /// <summary>
    /// <para>
    /// Mark a target user as 'played with'.<br/>
    /// This is a client-side only feature that requires that the calling user is in game.
    /// </para>
    /// <para>
    /// You can view the players you have recently played with <a href="http://steamcommunity.com/my/friends/coplay/">here</a> on the Steam community and in the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a>.
    /// </para>
    /// <remarks>
    /// NOTE: The current user must be in game with the other player for the association to work.
    /// </remarks>
    /// </summary>
    /// <param name="steamIDUserPlayedWith">The other user that we have played with.</param>
    public void SetPlayedWith(CSteamID steamIDUserPlayedWith)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_SetPlayedWith(this.ptr, steamIDUserPlayedWith);
#endif
    }

    /// <summary>
    /// Activates the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a> to open the invite dialog.<br/>
    /// Invitations sent from this dialog will be for the provided lobby.
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby that selected users will be invited to.</param>
    public void ActivateGameOverlayInviteDialog(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets a handle to the small (32*32px) avatar for the specified user.
    /// </para>
    /// <para>
    /// You can pass in <see cref="ISteamUser.GetSteamID"/> to get the current user's avatar.
    /// </para>
    /// <remarks>
    /// NOTE: This only works for users that the local user knows about.<br/>
    /// They will automatically know about their friends, people on leaderboards they've requested, or people in the same source as them (Steam group, chat room, lobby, or game server).<br/>
    /// If they don't know about them then you must call <see cref="RequestUserInformation"/> to cache the avatar locally.
    /// </remarks>
    /// </summary>
    /// <returns>
    /// <see cref="int"/> A Steam image handle which is used with <see cref="ISteamUtils.GetImageSize"/> and <see cref="ISteamUtils.GetImageRGBA"/>.<br/>
    /// Returns <c>0</c> if no avatar is set for the user.
    /// </returns>
    public int GetSmallFriendAvatar(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetSmallFriendAvatar(this.ptr, steamIDFriend);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets a handle to the medium (64*64px) avatar for the specified user.
    /// </para>
    /// <para>
    /// You can pass in <see cref="ISteamUser.GetSteamID"/> to get the current user's avatar.
    /// </para>
    /// <remarks>
    /// NOTE: This only works for users that the local user knows about.<br/>
    /// They will automatically know about their friends, people on leaderboards they've requested, or people in the same source as them (Steam group, chat room, lobby, or game server).<br/>
    /// If they don't know about them then you must call <see cref="RequestUserInformation"/> to cache the avatar locally.
    /// </remarks>
    /// </summary>
    /// <returns>
    /// <see cref="int"/> A Steam image handle which is used with <see cref="ISteamUtils.GetImageSize"/> and <see cref="ISteamUtils.GetImageRGBA"/>.<br/>
    /// Returns <c>0</c> if no avatar is set for the user.
    /// </returns>
    public int GetMediumFriendAvatar(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetMediumFriendAvatar(this.ptr, steamIDFriend);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets a handle to the large (128*128px) avatar for the specified user.
    /// </para>
    /// <para>
    /// You can pass in <see cref="ISteamUser.GetSteamID"/> to get the current user's avatar.
    /// </para>
    /// <remarks>
    /// NOTE: This only works for users that the local user knows about.<br/>
    /// They will automatically know about their friends, people on leaderboards they've requested, or people in the same source as them (Steam group, chat room, lobby, or game server).<br/>
    /// If they don't know about them then you must call <see cref="RequestUserInformation"/> to cache the avatar locally.
    /// </remarks>
    /// </summary>
    /// <returns>
    /// <see cref="int"/> A Steam image handle which is used with <see cref="ISteamUtils.GetImageSize"/> and <see cref="ISteamUtils.GetImageRGBA"/>.<br/>
    /// Returns <c>0</c> if no avatar is set for the user.<br/>
    /// Returns <c>-1</c> if the avatar image data has not been loaded yet and requests that it gets download. In this case wait for a <see cref="AvatarImageLoaded"/> callback and then call this again.<br/>
    /// Triggers a <see cref="AvatarImageLoaded"/> callback.
    /// </returns>
    public int GetLargeFriendAvatar(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetLargeFriendAvatar(this.ptr, steamIDFriend);
#endif
    }

    // requests information about a user - persona name & avatar
    // if bRequireNameOnly is set, then the avatar of a user isn't downloaded
    // - it's a lot slower to download avatars and churns the local cache, so if you don't need avatars, don't request them
    // if returns true, it means that data is being requested, and a PersonaStateChanged_t callback will be posted when it's retrieved
    // if returns false, it means that we already have all the details about that user, and functions can be called immediately

    /// <summary>
    /// Requests the persona name and optionally the avatar of a specified user.
    /// </summary>
    /// <remarks>
    /// NOTE: It's a lot slower to download avatars and churns the local cache, so if you don't need avatars, don't request them.
    /// </remarks>
    /// <param name="steamIDUser">The user to request the information of.</param>
    /// <param name="requireNameOnly">Retrieve the Persona name only (<c>true</c>)? Or both the name and the avatar (<c>false</c>)?</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> means that the data has being requested, and a <see cref="PersonaStateChange"/> callback will be posted when it's retrieved.<br/>
    /// <c>false</c> means that we already have all the details about that user, and functions that require this information can be used immediately.<br/>
    /// Triggers a <see cref="PersonaStateChange"/> callback.
    /// </returns>
    public bool RequestUserInformation(CSteamID steamIDUser, bool requireNameOnly)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_RequestUserInformation(this.ptr, steamIDUser, requireNameOnly);
#endif
    }

    /// <summary>
    /// Requests information about a Steam group officers (administrators and moderators).
    /// </summary>
    /// <remarks>
    /// NOTE: You can only ask about Steam groups that a user is a member of.<br/>
    /// NOTE: This won't download avatars for the officers automatically. If no avatar image is available for an officer, then call <see cref="RequestUserInformation"/> to download the avatar.
    /// </remarks>
    /// <param name="steamIDClan">The Steam group to get the officers list for.</param>
    /// <returns><see cref="CallTask&lt;ClanOfficerListResponse_t&gt;"/> that will return <see cref="ClanOfficerListResponse_t"/> when awaited.</returns>
    public CallTask<ClanOfficerListResponse_t>? RequestClanOfficerList(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, CSteamID, ClanOfficerListResponse_t>(
        Native.SteamAPI_ISteamFriends_RequestClanOfficerList, this.ptr, steamIDClan);

        return task;
#endif
    }

    // iteration of clan officers - can only be done when a RequestClanOfficerList() call has completed

    /// <summary>
    /// Gets the owner of a Steam Group.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="RequestClanOfficerList"/> before this to get the required data!
    /// </remarks>
    /// <param name="steamIDClan">The Steam ID of the Steam group to get the owner for.</param>
    /// <returns><see cref="CSteamID"/> Returns <see cref="CSteamID.Nil"/> if <paramref name="steamIDClan"/> is invalid or if <see cref="RequestClanOfficerList"/> has not been called for it.</returns>
    public CSteamID GetClanOwner(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanOwner(this.ptr, steamIDClan);
#endif
    }

    // returns the number of officers in a clan (including the owner)

    /// <summary>
    /// <para>
    /// Gets the number of officers (administrators and moderators) in a specified Steam group.
    /// </para>
    /// <para>
    /// This also includes the owner of the Steam group.
    /// </para>
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetClanOfficerByIndex"/> can be used to get the Steam ID of each officer.
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="RequestClanOfficerList"/> before this to get the required data!
    /// </remarks>
    /// <param name="steamIDClan">The Steam group to get the officer count of.</param>
    /// <returns>
    /// <see cref="int"/> The number of officers in the Steam group.<br/>
    /// Returns <c>0</c> if <paramref name="steamIDClan"/> is invalid or if <see cref="RequestClanOfficerList"/> has not been called for it.
    /// </returns>
    public int GetClanOfficerCount(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanOfficerCount(this.ptr, steamIDClan);
#endif
    }

    /// <summary>
    /// Gets the Steam ID of the officer at the given index in a Steam group.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetClanOfficerCount"/> before calling this.
    /// </remarks>
    /// <param name="steamIDClan">This must be the same steam group used in the previous call toGetClanOfficerCount!</param>
    /// <param name="officerIndex">An index between <c>0</c> and <see cref="GetClanOfficerCount"/>.</param>
    /// <returns>
    /// <see cref="CSteamID"/> <see cref="CSteamID.Nil"/> if <paramref name="steamIDClan"/> or <paramref name="officerIndex"/> are invalid.
    /// </returns>
    public CSteamID GetClanOfficerByIndex(CSteamID steamIDClan, int officerIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanOfficerByIndex(this.ptr, steamIDClan, officerIndex);
#endif
    }

    /// <summary>
    /// <para>
    /// Sets a Rich Presence key/value for the current user that is automatically shared to all friends playing the same game.
    /// </para>
    /// <para>
    /// Each user can have up to 20 keys set as defined by <see cref="MaxRichPresenceKeys"/>.
    /// </para>
    /// <para>
    /// There are two special keys used for viewing/joining games:<br/>
    /// * "status" - A UTF-8 string that will show up in the 'view game info' dialog in the Steam friends list.<br/>
    /// * "connect" - A UTF-8 string that contains the command-line for how a friend can connect to a game. This enables the 'join game' button in the 'view game info' dialog, in the steam friends list right click menu, and on the players Steam community profile. Be sure your app implements <see cref="ISteamApps.GetLaunchCommandLine"/> so you can disable the popup warning when launched via a command line.
    /// </para>
    /// <para>
    /// There are three additional special keys used by the <a href="https://steamcommunity.com/updates/chatupdate">new Steam Chat</a>:<br/>
    /// * "steam_display" - Names a rich presence localization token that will be displayed in the viewing user's selected language in the Steam client UI. See <a href="https://partner.steamgames.com/doc/api/ISteamFriends#richpresencelocalization">Rich Presence Localization</a> for more info, including a link to a page for testing this rich presence data. If steam_display is not set to a valid localization tag, then rich presence will not be displayed in the Steam client.<br/>
    /// * "steam_player_group" - When set, indicates to the Steam client that the player is a member of a particular group. Players in the same group may be organized together in various places in the Steam UI. This string could identify a party, a server, or whatever grouping is relevant for your game. The string itself is not displayed to users.<br/>
    /// * "steam_player_group_size" - When set, indicates the total number of players in the steam_player_group. The Steam client may use this number to display additional information about a group when all of the members are not part of a user's friends list. (For example, "Bob, Pete, and 4 more".)
    /// </para>
    /// <para>
    /// You can clear all of the keys for the current user with <see cref="ClearRichPresence"/>
    /// </para>
    /// <para>
    /// To get rich presence keys for friends see: <see cref="GetFriendRichPresence"/>.
    /// </para>
    /// </summary>
    /// <param name="key">The rich presence 'key' to set. This can not be longer than specified in <see cref="MaxRichPresenceKeyLength"/>.</param>
    /// <param name="value">
    /// The rich presence 'value' to associate with <paramref name="key"/>. This can not be longer than specified in <see cref="MaxRichPresenceValueLength"/>.<br/>
    /// If this is set to an empty string (<c>""</c>) or <c>null</c> then the key is removed if it's set.
    /// </param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the rich presence was set successfully.<br/>
    /// <c>false</c> under the following conditions:<br/>
    /// * <paramref name="key"/> was longer than <see cref="MaxRichPresenceKeyLength"/> or had a length of <c>0</c>.<br/>
    /// * pchValue was longer than <see cref="MaxRichPresenceValueLength"/>.<br/>
    /// * The user has reached the maximum amount of rich presence keys as defined by <see cref="MaxRichPresenceKeys"/>.
    /// </returns>
    public bool SetRichPresence(string key, string value)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_SetRichPresence(this.ptr, key, value);
#endif
    }

    /// <summary>
    /// Clears all of the current user's Rich Presence key/values.
    /// </summary>
    public void ClearRichPresence()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ClearRichPresence(this.ptr);
#endif
    }

    /// <summary>
    /// Get a Rich Presence value from a specified friend.
    /// </summary>
    /// <param name="steamIDFriend">The friend to get the Rich Presence value for.</param>
    /// <param name="key">The Rich Presence key to request.</param>
    /// <returns><see cref="string"/> Returns an empty string (<c>""</c>) if the specified key is not set.</returns>
    public string? GetFriendRichPresence(CSteamID steamIDFriend, string key)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetFriendRichPresence(this.ptr, steamIDFriend, key);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the number of Rich Presence keys that are set on the specified user.
    /// </para>
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetFriendRichPresenceKeyByIndex"/> to get the rich presence keys.
    /// </para>
    /// <para>
    /// This is typically only ever used for debugging purposes.
    /// </para>
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the user to get the Rich Presence Key Count of.</param>
    /// <returns><see cref="int"/> Returns <c>0</c> if there is no Rich Presence information for the specified user.</returns>
    public int GetFriendRichPresenceKeyCount(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount(this.ptr, steamIDFriend);
#endif
    }

    /// <param name="steamIDFriend">This should be the same user provided to the previous call to <see cref="GetFriendRichPresenceKeyCount"/>!</param>
    /// <param name="keyIndex">An index between <c>0</c> and <see cref="GetFriendRichPresenceKeyCount"/>.</param>
    /// <returns><see cref="string"/> Returns an empty string (<c>""</c>) if the index is invalid or the specified user has no Rich Presence data available.</returns>
    public string? GetFriendRichPresenceKeyByIndex(CSteamID steamIDFriend, int keyIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex(this.ptr, steamIDFriend, keyIndex);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Requests Rich Presence data from a specific user.
    /// </para>
    /// <para>
    /// This is used to get the Rich Presence information from a user that is not a friend of the current user, like someone in the same lobby or game server.
    /// </para>
    /// <para>
    /// This function is rate limited, if you call this too frequently for a particular user then it will just immediately post a callback without requesting new data from the server.
    /// </para>
    /// <para>
    /// Triggers a <see cref="FriendRichPresenceUpdate"/> callback.
    /// </para>
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the user to request the rich presence of.</param>
    public void RequestFriendRichPresence(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_RequestFriendRichPresence(this.ptr, steamIDFriend);
#endif
    }

    /// <summary>
    /// <para>
    /// Invites a friend or clan member to the current game using a special invite string.
    /// </para>
    /// <para>
    /// If the target user accepts the invite then the <paramref name="connectString"/> gets added to the command-line when launching the game.<br/>
    /// If the game is already running for that user, then they will receive a <see cref="GameRichPresenceJoinRequested"/> callback with the connect string.
    /// </para>
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the friend to invite.</param>
    /// <param name="connectString">A string that lets the friend know how to join the game (I.E. the game server IP). This can not be longer than specified in <see cref="MaxRichPresenceValueLength"/>.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the invite was successfully sent.<br/>
    /// <c>false</c> under the following conditions:<br/>
    /// * The Steam ID provided to steamIDFriend was invalid.<br/>
    /// * The Steam ID provided to steamIDFriend is not a friend or does not share the same Steam Group as the current user.<br/>
    /// * The value provided to pchConnectString was too long.<br/>
    /// Triggers a <see cref="GameRichPresenceJoinRequested"/> callback.
    /// </returns>
    public bool InviteUserToGame(CSteamID steamIDFriend, string connectString)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_InviteUserToGame(this.ptr, steamIDFriend, connectString);
#endif
    }

    // recently-played-with friends iteration
    // this iterates the entire list of users recently played with, across games
    // GetFriendCoplayTime() returns as a unix time

    /// <summary>
    /// <para>
    /// Gets the number of players that the current user has recently played with, across all games.
    /// </para>
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetCoplayFriend"/> can be used to get the Steam ID of each player.
    /// </para>
    /// <para>
    /// These players are have been set with previous calls to <see cref="SetPlayedWith"/>.
    /// </para>
    /// </summary>
    /// <returns><see cref="int"/> The number of users that the current user has recently played with.</returns>
    public int GetCoplayFriendCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetCoplayFriendCount(this.ptr);
#endif
    }

    /// <summary>
    /// Gets the Steam ID of the recently played with user at the given index.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetCoplayFriendCount"/> before calling this.
    /// </remarks>
    /// <param name="coplayFriend">An index between <c>0</c> and <see cref="GetCoplayFriendCount"/>.</param>
    /// <returns><see cref="CSteamID"/> Invalid indices return <see cref="CSteamID.Nil"/>.</returns>
    public CSteamID GetCoplayFriend(int coplayFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetCoplayFriend(this.ptr, coplayFriend);
#endif
    }

    /// <summary>
    /// Gets the timestamp of when the user played with someone on their recently-played-with list.
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the user on the recently-played-with list to get the timestamp for.</param>
    /// <returns>
    /// <see cref="int"/> The time is provided in Unix epoch format (seconds since Jan 1st 1970).
    /// Steam IDs not in the recently-played-with list return <c>0</c>.
    /// </returns>
    public int GetFriendCoplayTime(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendCoplayTime(this.ptr, steamIDFriend);
#endif
    }

    /// <summary>
    /// Gets the app ID of the game that user played with someone on their recently-played-with list.
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the user on the recently-played-with list to get the game played.</param>
    /// <returns><see cref="AppId_t"/> Steam IDs not in the recently-played-with list return <see cref="AppId_t.Invalid"/>.</returns>
    public AppId_t GetFriendCoplayGame(CSteamID steamIDFriend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendCoplayGame(this.ptr, steamIDFriend);
#endif
    }

    // chat interface for games

    /// <summary>
    /// <para>
    /// Allows the user to join Steam group (clan) chats right within the game.
    /// </para>
    /// <para>
    /// The behavior is somewhat complicated, because the user may or may not be already in the group chat from outside the game or in the overlay.
    /// </para>
    /// <para>
    /// You can use <see cref="ActivateGameOverlayToUser"/> to open the in-game overlay version of the chat.
    /// </para>
    /// <para>
    /// If you have joined a Steam group chat then you should be watching for the following callbacks:<br/>
    /// * <see cref="GameConnectedClanChatMsg"/><br/>
    /// * <see cref="GameConnectedChatJoin"/><br/>
    /// * <see cref="GameConnectedChatLeave"/>
    /// </para>
    /// </summary>
    /// <param name="steamIDClan">The Steam ID of the Steam group to join.</param>
    /// <returns>
    /// <see cref="CallTask&lt;JoinClanChatRoomCompletionResult_t&gt;"/> that will return <see cref="JoinClanChatRoomCompletionResult_t"/> when awaited.<br/>
    /// Triggers a <see cref="GameConnectedChatJoin"/> callback.<br/>
    /// Triggers a <see cref="GameConnectedClanChatMsg"/> callback.
    /// </returns>
    public CallTask<JoinClanChatRoomCompletionResult_t>? JoinClanChatRoom(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, CSteamID, JoinClanChatRoomCompletionResult_t>(
        Native.SteamAPI_ISteamFriends_JoinClanChatRoom, this.ptr, steamIDClan);

        return task;
#endif
    }

    /// <summary>
    /// Leaves a Steam group chat that the user has previously entered with <see cref="JoinClanChatRoom"/>.
    /// </summary>
    /// <param name="steamIDClan">The Steam ID of the Steam group chat to leave.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if user is in the specified chat room, otherwise <c>false</c>.
    /// Triggers a <see cref="GameConnectedChatLeave"/> callback.
    /// </returns>
    public bool LeaveClanChatRoom(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_LeaveClanChatRoom(this.ptr, steamIDClan);
#endif
    }

    /// <summary>
    /// <para>
    /// This API is deprecated.
    /// </para>
    /// <para>
    /// Get the number of users in a Steam group chat.
    /// </para>
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetChatMemberByIndex"/> can be used to get the Steam ID of each person in the chat.
    /// </para>
    /// <remarks>
    /// <para>
    /// NOTE: Large steam groups cannot be iterated by the local user.
    /// </para>
    /// <para>
    /// NOTE: The current user must be in a lobby to retrieve the Steam IDs of other users in that lobby.
    /// </para>
    /// </remarks>
    /// </summary>
    /// <param name="steamIDClan">The Steam group to get the chat count of.</param>
    /// <returns><see cref="int"/> <c>0</c> if the Steam ID provided is invalid or if the local user doesn't have the data available.</returns>
    public int GetClanChatMemberCount(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanChatMemberCount(this.ptr, steamIDClan);
#endif
    }

    /// <summary>
    /// Gets the Steam ID at the given index in a Steam group chat.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetClanChatMemberCount"/> before calling this.
    /// </remarks>
    /// <param name="steamIDClan">This MUST be the same source used in the previous call to <see cref="GetClanChatMemberCount"/>!</param>
    /// <param name="userIndex">An index between <c>0</c> and <see cref="GetClanChatMemberCount"/>.</param>
    /// <returns><see cref="CSteamID"/> Invalid indices return <see cref="CSteamID.Nil"/>.</returns>
    public CSteamID GetChatMemberByIndex(CSteamID steamIDClan, int userIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetChatMemberByIndex(this.ptr, steamIDClan, userIndex);
#endif
    }

    /// <summary>
    /// Sends a message to a Steam group chat room.
    /// </summary>
    /// <param name="steamIDClanChat">The Steam ID of the group chat to send the message to.</param>
    /// <param name="text">The message to send. This can be up to 2048 characters long.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the message was successfully sent.<br/>
    /// <c>false</c> under one of the following circumstances:<br/>
    /// * The current user is not in the specified group chat.<br/>
    /// * The current user is not connected to Steam.<br/>
    /// * The current user is rate limited.<br/>
    /// * The current user is chat restricted.<br/>
    /// * The message in pchText exceeds 2048 characters.
    /// </returns>
    public bool SendClanChatMessage(CSteamID steamIDClanChat, string text)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_SendClanChatMessage(this.ptr, steamIDClanChat, text);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the data from a Steam group chat room message.
    /// </para>
    /// <para>
    /// This should only ever be called in response to a <see cref="GameConnectedClanChatMsg"/> callback.
    /// </para>
    /// </summary>
    /// <param name="steamIDClanChat">The Steam ID of the Steam group chat room.</param>
    /// <param name="messageIndex">The index of the message. This should be the <see cref="GameConnectedClanChatMsg_t.MessageID"/>.</param>
    /// <param name="text">The buffer where the chat message will be copied into.<br/>
    /// (Should be big enough to hold 2048 UTF-8 characters. So 8192 bytes + 1 for '\0')</param>
    /// <param name="chatEntryType">Returns the type of chat entry that was received.</param>
    /// <param name="steamidChatter">Returns the Steam ID of the user that sent the message.</param>
    /// <returns>
    /// <see cref="int"/> The number of bytes copied into prgchText.<br/>
    /// Returns <c>0</c> and sets <paramref name="chatEntryType"/> to <see cref="EChatEntryType.Invalid"/> if the current user is not in the specified Steam group chat room or if the index provided in <paramref name="messageIndex"/> is invalid.
    /// </returns>
    public int GetClanChatMessage(CSteamID steamIDClanChat, int messageIndex, Span<byte> text, out EChatEntryType chatEntryType, out CSteamID steamidChatter)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetClanChatMessage(this.ptr, steamIDClanChat, messageIndex, text, text.Length, out chatEntryType, out steamidChatter);
#endif
    }

    /// <summary>
    /// Checks if a user in the Steam group chat room is an admin.
    /// </summary>
    /// <param name="steamIDClanChat">The Steam ID of the Steam group chat room.</param>
    /// <param name="steamIDUser">The Steam ID of the user to check the admin status of.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the specified user is an admin.<br/>
    /// <c>false</c> if the user is not an admin, if the current user is not in the chat room specified, or the specified user is not in the chat room.
    /// </returns>
    public bool IsClanChatAdmin(CSteamID steamIDClanChat, CSteamID steamIDUser)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_IsClanChatAdmin(this.ptr, steamIDClanChat, steamIDUser);
#endif
    }

    // interact with the Steam (game overlay / desktop)

    /// <summary>
    /// Checks if the Steam Group chat room is open in the Steam UI.
    /// </summary>
    /// <param name="steamIDClanChat">The Steam ID of the Steam group chat room to check.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the specified Steam group chat room is opened; otherwise, <c>false</c>.<br/>
    /// This also returns <c>false</c> if the specified Steam group chat room is unknown.
    /// </returns>
    public bool IsClanChatWindowOpenInSteam(CSteamID steamIDClanChat)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam(this.ptr, steamIDClanChat);
#endif
    }

    /// <summary>
    /// Opens the specified Steam group chat room in the Steam UI.
    /// </summary>
    /// <param name="steamIDClanChat">The Steam ID of the Steam group chat room to open.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the user successfully entered the Steam group chat room.<br/>
    /// <c>false</c> in one of the following situations:<br/>
    /// * The provided Steam group chat room does not exist or the user does not have access to join it.<br/>
    /// * The current user is currently rate limited.<br/>
    /// * The current user is chat restricted.
    /// </returns>
    public bool OpenClanChatWindowInSteam(CSteamID steamIDClanChat)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_OpenClanChatWindowInSteam(this.ptr, steamIDClanChat);
#endif
    }

    /// <summary>
    /// Closes the specified Steam group chat room in the Steam UI.
    /// </summary>
    /// <param name="steamIDClanChat">The Steam ID of the Steam group chat room to close.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the user successfully left the Steam group chat room.
    /// <c>false</c> if the user is not in the provided Steam group chat room.
    /// </returns>
    public bool CloseClanChatWindowInSteam(CSteamID steamIDClanChat)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_CloseClanChatWindowInSteam(this.ptr, steamIDClanChat);
#endif
    }

    // peer-to-peer chat interception
    // this is so you can show P2P chats inline in the game

    /// <summary>
    /// <para>
    /// Listens for Steam friends chat messages.
    /// </para>
    /// <para>
    /// You can then show these chats inline in the game. For example, the chat system in Dota 2.
    /// </para>
    /// <para>
    /// After enabling this you will receive <see cref="GameConnectedFriendChatMsg"/> callbacks whenever the user receives a chat message.<br/>
    /// You can get the actual message data from this callback with <see cref="GetFriendMessage"/>.<br/>
    /// You can send messages with <see cref="ReplyToFriendMessage"/>.
    /// </para>
    /// </summary>
    /// <param name="interceptEnabled">Turn friends message interception on (<c>true</c>) or off (<c>false</c>)?</param>
    /// <returns>
    /// <see cref="bool"/> Always returns <c>true</c>.
    /// Triggers a <see cref="GameConnectedFriendChatMsg"/> callback.
    /// </returns>
    public bool SetListenForFriendsMessages(bool interceptEnabled)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_SetListenForFriendsMessages(this.ptr, interceptEnabled);
#endif
    }

    /// <summary>
    /// Sends a message to a Steam friend.
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the friend to send the message to.</param>
    /// <param name="msgToSend">The message to send.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the message was successfully sent.
    /// <c>false</c> if the current user is rate limited or chat restricted.
    /// </returns>
    public bool ReplyToFriendMessage(CSteamID steamIDFriend, string msgToSend)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_ReplyToFriendMessage(this.ptr, steamIDFriend, msgToSend);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the data from a Steam friends message.
    /// </para>
    /// <para>
    /// This should only ever be called in response to a <see cref="GameConnectedFriendChatMsg"/> callback.
    /// </para>
    /// </summary>
    /// <param name="steamIDFriend">The Steam ID of the friend that sent this message.</param>
    /// <param name="messageID">The index of the message. This should be the <see cref="GameConnectedFriendChatMsg_t.MessageID"/>.</param>
    /// <param name="data">The buffer where the chat message will be copied into.</param>
    /// <param name="chatEntryType">Returns the type of chat entry that was received.</param>
    /// <returns>
    /// <see cref="int"/>The number of bytes copied into pvData.
    /// Returns <c>0</c> and sets <paramref name="chatEntryType"/> to <see cref="EChatEntryType.Invalid"/> if the current user is chat restricted,<br/>
    /// if the provided Steam ID is not a friend, or if the index provided in <paramref name="messageID"/> is invalid.
    /// </returns>
    public int GetFriendMessage(CSteamID steamIDFriend, int messageID, Span<byte> data, out EChatEntryType chatEntryType)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetFriendMessage(this.ptr, steamIDFriend, messageID, data, data.Length, out chatEntryType);
#endif
    }

    // following apis

    /// <summary>
    /// Gets the number of users following the specified user.
    /// </summary>
    /// <param name="steamID">The user to get the follower count for.</param>
    /// <returns><see cref="CallTask&lt;FriendsGetFollowerCount_t&gt;"/> that will return <see cref="FriendsGetFollowerCount_t"/> when awaited.</returns>
    public CallTask<FriendsGetFollowerCount_t>? GetFollowerCount(CSteamID steamID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, CSteamID, FriendsGetFollowerCount_t>(
        Native.SteamAPI_ISteamFriends_GetFollowerCount, this.ptr, steamID);

        return task;
#endif
    }

    /// <summary>
    /// Checks if the current user is following the specified user.
    /// </summary>
    /// <param name="steamID">The Steam ID of the check if we are following.</param>
    /// <returns><see cref="CallTask&lt;FriendsIsFollowing_t&gt;"/> that will return <see cref="FriendsIsFollowing_t"/> when awaited.</returns>
    public CallTask<FriendsIsFollowing_t>? IsFollowing(CSteamID steamID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, CSteamID, FriendsIsFollowing_t>(
        Native.SteamAPI_ISteamFriends_IsFollowing, this.ptr, steamID);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the list of users that the current user is following.
    /// </para>
    /// <para>
    /// You can be following people that are not your friends.<br/>
    /// Following allows you to receive updates when the person does things like post a new piece of content to the Steam Workshop.
    /// </para>
    /// <remarks>
    /// NOTE: This returns up to <see cref="EnumerateFollowersMax"/> users at once.<br/>
    /// If the current user is following more than that, you will need to call this repeatedly, with <paramref name="unStartIndex"/> set to the total number of followers that you have received so far.<br/>
    /// I.E. If you have received 50 followers, and the user is following 105, you will need to call this again with <paramref name="unStartIndex"/> = 50 to get the next 50, and then again with <paramref name="unStartIndex"/> = 100 to get the remaining 5 users.
    /// </remarks>
    /// </summary>
    /// <param name="unStartIndex">The index to start receiving followers from. This should be 0 on the initial call.</param>
    /// <returns><see cref="CallTask&lt;FriendsEnumerateFollowingList_t&gt;"/> that will return <see cref="FriendsEnumerateFollowingList_t"/> when awaited.</returns>
    public CallTask<FriendsEnumerateFollowingList_t>? EnumerateFollowingList(uint unStartIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, uint, FriendsEnumerateFollowingList_t>(
        Native.SteamAPI_ISteamFriends_EnumerateFollowingList, this.ptr, unStartIndex);

        return task;
#endif
    }

    /// <summary>
    /// Checks if the Steam group is public.
    /// </summary>
    /// <param name="steamIDClan">The Steam ID of the Steam group.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the specified group is public<br/>
    /// <c>false</c> if the specified group is not public
    /// </returns>
    public bool IsClanPublic(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_IsClanPublic(this.ptr, steamIDClan);
#endif
    }

    /// <summary>
    /// Checks if the Steam group is an official game group/community hub.
    /// </summary>
    /// <param name="steamIDClan">The Steam ID of the Steam group.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the specified group is an official game group/community hub
    /// <c>false</c> if the specified group is not an official game group/community hub
    /// </returns>
    public bool IsClanOfficialGameGroup(CSteamID steamIDClan)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_IsClanOfficialGameGroup(this.ptr, steamIDClan);
#endif
    }

    /// <summary>
    /// <para>
    /// Return the number of chats (friends or chat rooms) with unread messages.<br/>
    /// A "priority" message is one that would generate some sort of toast or<br/>
    /// notification, and depends on user settings.
    /// </para>
    /// <para>
    /// You can register for <see cref="UnreadChatMessagesChanged"/> callbacks to know when this<br/>
    /// has potentially changed.
    /// </para>
    /// </summary>
    public int GetNumChatsWithUnreadPriorityMessages()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetNumChatsWithUnreadPriorityMessages(this.ptr);
#endif
    }

    /// <summary>
    /// Activates game overlay to open the remote play together invite dialog.<br/>
    /// Invitations will be sent for remote play together
    /// </summary>
    public void ActivateGameOverlayRemotePlayTogetherInviteDialog(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// Call this before calling <see cref="ActivateGameOverlayToWebPage"/> to have the Steam Overlay Browser block navigations<br/>
    /// to your specified protocol (scheme) uris and instead dispatch a <see cref="OverlayBrowserProtocolNavigation"/> callback to your game.<br/>
    /// <see cref="ActivateGameOverlayToWebPage"/> must have been called with <see cref="EActivateGameOverlayToWebPageMode.Modal"/>
    /// </summary>
    public bool RegisterProtocolInOverlayBrowser(string protocol)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_RegisterProtocolInOverlayBrowser(this.ptr, protocol);
#endif
    }

    /// <summary>
    /// Activates the game overlay to open an invite dialog that will send the provided Rich Presence connect string to selected friends
    /// </summary>
    public void ActivateGameOverlayInviteDialogConnectString(string connectString)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialogConnectString(this.ptr, connectString);
#endif
    }

    // Steam Community items equipped by a user on their profile

    /// <summary>
    /// <para>
    /// Requests the list of equipped Steam Community profile items for the given user from Steam.
    /// </para>
    /// <para>
    /// You can register for <see cref="EquippedProfileItemsChanged"/> to know when a friend has changed their equipped profile items
    /// </para>
    /// </summary>
    /// <param name="steamID">The user that you want to retrieve equipped items for.</param>
    /// <returns><see cref="CallTask&lt;EquippedProfileItems_t&gt;"/> that will return <see cref="EquippedProfileItems_t"/> when awaited.</returns>
    public CallTask<EquippedProfileItems_t>? RequestEquippedProfileItems(CSteamID steamID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, CSteamID, EquippedProfileItems_t>(
        Native.SteamAPI_ISteamFriends_RequestEquippedProfileItems, this.ptr, steamID);

        return task;
#endif
    }

    /// <summary>
    /// After calling <see cref="RequestEquippedProfileItems"/>, you can use this function to check if the user has a type of profile item equipped or not.
    /// </summary>
    /// <param name="steamID">The user that you had already retrieved equipped items for</param>
    /// <param name="itemType">Type of item you want to see is equipped or not</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the itemType is equipped for the user
    /// <c>false</c> if the itemType is not equipped for the user
    /// </returns>
    public bool BHasEquippedProfileItem(CSteamID steamID, ECommunityProfileItemType itemType)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_BHasEquippedProfileItem(this.ptr, steamID, itemType);
#endif
    }

    /// <summary>
    /// Returns a string property for a user's equipped profile item.
    /// </summary>
    /// <param name="steamID">The user that you had already retrieved equipped items for</param>
    /// <param name="itemType">Type of item you are retrieving the property for</param>
    /// <param name="prop">The string property you want to retrieve</param>
    public string? GetProfileItemPropertyString(CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamFriends_GetProfileItemPropertyString(this.ptr, steamID, itemType, prop);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// Returns an unsigned integer property for a user's equipped profile item.
    /// </summary>
    /// <param name="steamID">The user that you had already retrieved equipped items for</param>
    /// <param name="itemType">Type of item you are retrieving the property for</param>
    /// <param name="prop">The unsigned integer property you want to retrieve</param>
    public uint GetProfileItemPropertyUint(CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamFriends");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamFriends_GetProfileItemPropertyUint(this.ptr, steamID, itemType, prop);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case AvatarImageLoaded_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<AvatarImageLoaded_t>();
                    this.AvatarImageLoaded?.Invoke(ref data);
                    break;
                }

            case ClanOfficerListResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<ClanOfficerListResponse_t>();
                    this.ClanOfficerListResponse?.Invoke(ref data);
                    break;
                }

            case DownloadClanActivityCountsResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<DownloadClanActivityCountsResult_t>();
                    this.DownloadClanActivityCountsResult?.Invoke(ref data);
                    break;
                }

            case FriendRichPresenceUpdate_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FriendRichPresenceUpdate_t>();
                    this.FriendRichPresenceUpdate?.Invoke(ref data);
                    break;
                }

            case FriendsEnumerateFollowingList_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FriendsEnumerateFollowingList_t>();
                    this.FriendsEnumerateFollowingList?.Invoke(ref data);
                    break;
                }

            case FriendsGetFollowerCount_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FriendsGetFollowerCount_t>();
                    this.FriendsGetFollowerCount?.Invoke(ref data);
                    break;
                }

            case FriendsIsFollowing_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FriendsIsFollowing_t>();
                    this.FriendsIsFollowing?.Invoke(ref data);
                    break;
                }

            case GameConnectedChatJoin_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameConnectedChatJoin_t>();
                    this.GameConnectedChatJoin?.Invoke(ref data);
                    break;
                }

            case GameConnectedChatLeave_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameConnectedChatLeave_t>();
                    this.GameConnectedChatLeave?.Invoke(ref data);
                    break;
                }

            case GameConnectedClanChatMsg_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameConnectedClanChatMsg_t>();
                    this.GameConnectedClanChatMsg?.Invoke(ref data);
                    break;
                }

            case GameConnectedFriendChatMsg_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameConnectedFriendChatMsg_t>();
                    this.GameConnectedFriendChatMsg?.Invoke(ref data);
                    break;
                }

            case GameLobbyJoinRequested_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameLobbyJoinRequested_t>();
                    this.GameLobbyJoinRequested?.Invoke(ref data);
                    break;
                }

            case GameOverlayActivated_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameOverlayActivated_t>();
                    this.GameOverlayActivated?.Invoke(ref data);
                    break;
                }

            case GameRichPresenceJoinRequested_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameRichPresenceJoinRequested_t>();
                    this.GameRichPresenceJoinRequested?.Invoke(ref data);
                    break;
                }

            case GameServerChangeRequested_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameServerChangeRequested_t>();
                    this.GameServerChangeRequested?.Invoke(ref data);
                    break;
                }

            case JoinClanChatRoomCompletionResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<JoinClanChatRoomCompletionResult_t>();
                    this.JoinClanChatRoomCompletionResult?.Invoke(ref data);
                    break;
                }

            case PersonaStateChange_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<PersonaStateChange_t>();
                    this.PersonaStateChange?.Invoke(ref data);
                    break;
                }

            case OverlayBrowserProtocolNavigation_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<OverlayBrowserProtocolNavigation_t>();
                    this.OverlayBrowserProtocolNavigation?.Invoke(ref data);
                    break;
                }

            case UnreadChatMessagesChanged_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<UnreadChatMessagesChanged_t>();
                    this.UnreadChatMessagesChanged?.Invoke(ref data);
                    break;
                }

            case EquippedProfileItemsChanged_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<EquippedProfileItemsChanged_t>();
                    this.EquippedProfileItemsChanged?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamFriends.OnDispatch()");
                break;
        }
    }

#endif
}
