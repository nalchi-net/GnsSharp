// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// <para>
/// Functions for clients to access matchmaking services, favorites, and to operate on game lobbies.
/// </para>
///
/// <para>
/// See <a href="https://partner.steamgames.com/doc/features/multiplayer/matchmaking">Steam Matchmaking &amp; Lobbies</a> for more information.
/// </para>
/// </summary>
public class ISteamMatchmaking
{
    /// <summary>
    /// Maximum number of characters a lobby metadata key can be.
    /// </summary>
    public const int MaxLobbyKeyLength = 255;

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamMatchmaking()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        this.ptr = Native.SteamAPI_SteamMatchmaking_v009();
#endif
    }

#pragma warning disable CS0067 // The event is never used

    public event Callback<FavoritesListAccountsUpdated_t>? FavoritesListAccountsUpdated;

    /// <summary>
    /// A server was added/removed from the favorites list, you should refresh now.
    /// </summary>
    public event Callback<FavoritesListChanged_t>? FavoritesListChanged;

    /// <summary>
    /// A chat (text or binary) message for this lobby has been received.<br/>
    /// After getting this you must use <see cref="GetLobbyChatEntry"/> to retrieve the contents of this message.
    /// </summary>
    public event Callback<LobbyChatMsg_t>? LobbyChatMsg;

    /// <summary>
    /// A lobby chat room state has changed, this is usually sent when a user has joined or left the lobby.
    /// </summary>
    public event Callback<LobbyChatUpdate_t>? LobbyChatUpdate;

    /// <summary>
    /// Result of our request to create a Lobby.<br/>
    /// At this point, the lobby has been joined and is ready for use, a <see cref="LobbyEnter_t"/> callback will also be received (since the local user is joining their own lobby).
    /// </summary>
    public event Callback<LobbyCreated_t>? LobbyCreated;

    /// <summary>
    /// <para>
    /// The lobby metadata has changed.
    /// </para>
    ///
    /// <para>
    /// If m_ulSteamIDMember is a user in the lobby, then use GetLobbyMemberData to access per-user details;<br/>
    /// otherwise, if <see cref="LobbyDataUpdate_t.SteamIDMember"/> == <see cref="LobbyDataUpdate_t.SteamIDLobby"/>, use GetLobbyData to access the lobby metadata.
    /// </para>
    /// </summary>
    public event Callback<LobbyDataUpdate_t>? LobbyDataUpdate;

    /// <summary>
    /// Recieved upon attempting to enter a lobby.<br/>
    /// Lobby metadata is available to use immediately after receiving this.
    /// </summary>
    public event Callback<LobbyEnter_t>? LobbyEnter;

    /// <summary>
    /// A game server has been set via <see cref="SetLobbyGameServer"/> for all of the members of the lobby to join.<br/>
    /// It's up to the individual clients to take action on this;<br/>
    /// the typical game behavior is to leave the lobby and connect to the specified game server;<br/>
    /// but the lobby may stay open throughout the session if desired.
    /// </summary>
    public event Callback<LobbyGameCreated_t>? LobbyGameCreated;

    /// <summary>
    /// <para>
    /// Someone has invited you to join a Lobby.<br/>
    /// Normally you don't need to do anything with this, as the Steam UI will also display a '&lt;user&gt; has invited you to the lobby, join?' notification and message.
    /// </para>
    ///
    /// <para>
    /// If the user outside a game chooses to join, your game will be launched with the parameter <c>+connect_lobby &lt;64-bit lobby id&gt;</c>, or with the callback <see cref="GameLobbyJoinRequested_t"/> if they're already in-game.
    /// </para>
    /// </summary>
    public event Callback<LobbyInvite_t>? LobbyInvite;

    /// <summary>
    /// Currently unused!<br/>
    /// If you want to implement kicking at this time then do it with a special packet sent with <see cref="SendLobbyChatMsg(CSteamID, string)"/>, when the user gets the packet they should call <see cref="LeaveLobby"/>.
    /// </summary>
    public event Callback<LobbyKicked_t>? LobbyKicked;

    /// <summary>
    /// Result when requesting the lobby list.<br/>
    /// You should iterate over the returned lobbies with <see cref="ISteamMatchmaking.GetLobbyByIndex"/>, from 0 to <see cref="LobbyMatchList_t.LobbiesMatching"/>-1.
    /// </summary>
    public event Callback<LobbyMatchList_t>? LobbyMatchList;

#pragma warning restore CS0067

    public static ISteamMatchmaking? User { get; internal set; }

    // game server favorites storage
    // saves basic details about a multiplayer game server locally

    /// <summary>
    /// Gets the number of favorite and recent game servers the user has stored locally.
    /// </summary>
    /// <seealso cref="AddFavoriteGame"/>
    /// <seealso cref="RemoveFavoriteGame"/>
    public int GetFavoriteGameCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetFavoriteGameCount(this.ptr);
#endif
    }

    /// <summary>
    /// Gets the details of the favorite game server by index.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetFavoriteGameCount"/> before calling this.
    /// </remarks>
    /// <param name="game">The index of the favorite game server to get the details of. This must be between 0 and <see cref="GetFavoriteGameCount"/></param>
    /// <param name="appID">Returns the App ID this server is for.</param>
    /// <param name="ip">Returns the IP address of the server in host order, i.e 127.0.0.1 == 0x7f000001.</param>
    /// <param name="connPort">Returns the port used to connect to the server, in host order.</param>
    /// <param name="queryPort">Returns the port used to query the server, in host order.</param>
    /// <param name="flags">Returns whether the server is on the favorites list or the history list. See <see cref="EFavoriteFlags"/> for more information.</param>
    /// <param name="lastPlayedOnServer">Returns the time the server was last added to the favorites list in Unix epoch format (seconds since Jan 1st, 1970).</param>
    /// <returns>bool <c>true</c> if the details were successfully retrieved. <c>false</c> if <paramref name="game"/> was an invalid index.</returns>
    /// <seealso cref="AddFavoriteGame"/>
    /// <seealso cref="RemoveFavoriteGame"/>
    public bool GetFavoriteGame(int game, out AppId_t appID, out uint ip, out ushort connPort, out ushort queryPort, out uint flags, out RTime32 lastPlayedOnServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetFavoriteGame(this.ptr, game, out appID, out ip, out connPort, out queryPort, out flags, out lastPlayedOnServer);
#endif
    }

    /// <summary>
    /// Adds the game server to the local favorites list or updates the time played of the server if it already exists in the list.
    /// </summary>
    /// <param name="appID">The App ID of the game.</param>
    /// <param name="ip">The IP address of the server in host order, i.e 127.0.0.1 == 0x7f000001.</param>
    /// <param name="connPort">The port used to connect to the server, in host order.</param>
    /// <param name="queryPort">The port used to query the server, in host order.</param>
    /// <param name="flags">Sets the whether the server should be added to the favorites list or history list. See <see cref="EFavoriteFlags"/> for more information.</param>
    /// <param name="lastPlayedOnServer">This should be the current time in Unix epoch format (seconds since Jan 1st, 1970).</param>
    /// <returns>int</returns>
    public int AddFavoriteGame(AppId_t appID, uint ip, ushort connPort, ushort queryPort, uint flags, RTime32 lastPlayedOnServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_AddFavoriteGame(this.ptr, appID, ip, connPort, queryPort, flags, lastPlayedOnServer);
#endif
    }

    /// <summary>
    /// Removes the game server from the local favorites list.
    /// </summary>
    /// <param name="appID">The App ID of the game.</param>
    /// <param name="ip">The IP address of the server in host order, i.e 127.0.0.1 == 0x7f000001.</param>
    /// <param name="connPort">The port used to connect to the server, in host order.</param>
    /// <param name="queryPort">The port used to query the server, in host order.</param>
    /// <param name="flags">Whether the server is on the favorites list or history list. See <see cref="EFavoriteFlags"/> for more information.</param>
    /// <returns>bool <c>true</c> if the server was removed; otherwise, <c>false</c> if the specified server was not on the users local favorites list.</returns>
    public bool RemoveFavoriteGame(AppId_t appID, uint ip, ushort connPort, ushort queryPort, uint flags)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_RemoveFavoriteGame(this.ptr, appID, ip, connPort, queryPort, flags);
#endif
    }

    ///////
    // Game lobby functions

    /// <summary>
    /// <para>
    /// Get a filtered list of relevant lobbies.
    /// </para>
    ///
    /// <para>
    /// There can only be one active lobby search at a time.
    /// The old request will be canceled if a new one is started.
    /// Depending on the users connection to the Steam back-end, this call can take from 300ms to 5 seconds to complete, and has a timeout of 20 seconds.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: To filter the results you MUST call the <c>AddRequestLobbyList*</c> functions before calling this. The filters are cleared on each call to this function.
    /// </para>
    /// <para>
    /// NOTE: If <see cref="AddRequestLobbyListDistanceFilter"/> is not called, <see cref="ELobbyDistanceFilter.Default"/> will be used, which will only find matches in the same or nearby regions.
    /// </para>
    /// <para>
    /// NOTE: This will only return lobbies that are not full, and only lobbies that are <see cref="ELobbyType.Public"/> or <see cref="ELobbyType.Invisible"/>, and are set to joinable with <see cref="SetLobbyJoinable"/>.
    /// </para>
    /// <para>
    /// NOTE: This also returns as a callback for compatibility with older applications, but you should use the call result if possible.
    /// </para>
    /// </remarks>
    public CallTask<LobbyMatchList_t> RequestLobbyList()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<LobbyMatchList_t>(() => Native.SteamAPI_ISteamMatchmaking_RequestLobbyList(this.ptr));

        return task;
#endif
    }

    /// <summary>
    /// Adds a string comparison filter to the next <see cref="RequestLobbyList"/> call.
    /// </summary>
    /// <param name="keyToMatch">The filter key name to match. This can not be longer than <see cref="MaxLobbyKeyLength"/>.</param>
    /// <param name="valueToMatch">The string to match.</param>
    /// <param name="comparisonType">The type of comparison to make.</param>
    public void AddRequestLobbyListStringFilter(string keyToMatch, string valueToMatch, ELobbyComparison comparisonType)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter(this.ptr, keyToMatch, valueToMatch, comparisonType);
#endif
    }

    /// <summary>
    /// Adds a numerical comparison filter to the next <see cref="RequestLobbyList"/> call.
    /// </summary>
    /// <param name="keyToMatch">The filter key name to match. This can not be longer than <see cref="MaxLobbyKeyLength"/>.</param>
    /// <param name="valueToMatch">The string to match.</param>
    /// <param name="comparisonType">The type of comparison to make.</param>
    public void AddRequestLobbyListNumericalFilter(string keyToMatch, int valueToMatch, ELobbyComparison comparisonType)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter(this.ptr, keyToMatch, valueToMatch, comparisonType);
#endif
    }

    /// <summary>
    /// <para>
    /// Sorts the results closest to the specified value.
    /// </para>
    ///
    /// <para>
    /// Near filters don't actually filter out values, they just influence how the results are sorted.<br/>
    /// You can specify multiple near filters, with the first near filter influencing the most, and the last near filter influencing the least.
    /// </para>
    /// </summary>
    /// <param name="keyToMatch">The filter key name to match. This can not be longer than <see cref="MaxLobbyKeyLength"/>.</param>
    /// <param name="valueToBeCloseTo">The value that lobbies will be sorted on.</param>
    public void AddRequestLobbyListNearValueFilter(string keyToMatch, int valueToBeCloseTo)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter(this.ptr, keyToMatch, valueToBeCloseTo);
#endif
    }

    /// <summary>
    /// Filters to only return lobbies with the specified number of open slots available.
    /// </summary>
    /// <param name="slotsAvailable">The number of open slots that must be open.</param>
    public void AddRequestLobbyListFilterSlotsAvailable(int slotsAvailable)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(this.ptr, slotsAvailable);
#endif
    }

    /// <summary>
    /// Sets the physical distance for which we should search for lobbies, this is based on the users IP address and a IP location map on the Steam backed.
    /// </summary>
    /// <param name="lobbyDistanceFilter">Specifies the maximum distance.</param>
    public void AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter lobbyDistanceFilter)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter(this.ptr, lobbyDistanceFilter);
#endif
    }

    /// <summary>
    /// Sets the maximum number of lobbies to return. The lower the count the faster it is to download the lobby results &amp; details to the client.
    /// </summary>
    /// <param name="maxResults">The maximum number of lobbies to return.</param>
    public void AddRequestLobbyListResultCountFilter(int maxResults)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter(this.ptr, maxResults);
#endif
    }

    /// <summary>
    /// Unused - Checks the player compatibility based on the frenemy system.
    /// </summary>
    public void AddRequestLobbyListCompatibleMembersFilter(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// Gets the Steam ID of the lobby at the specified index after receiving the <see cref="RequestLobbyList"/> results.
    /// </summary>
    /// <param name="lobbyIndex">The index of the lobby to get the Steam ID of, from 0 to <see cref="LobbyMatchList_t.LobbiesMatching"/>.</param>
    /// <returns><see cref="CSteamID"/> Returns k_steamIDNil if the provided index is invalid or there are no lobbies found.</returns>
    public CSteamID GetLobbyByIndex(int lobbyIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetLobbyByIndex(this.ptr, lobbyIndex);
#endif
    }

    /// <summary>
    /// <para>
    /// Create a new matchmaking lobby on the Steam servers.
    /// </para>
    ///
    /// <para>
    /// If private, then the lobby will not be returned by any <see cref="RequestLobbyList"/> call;<br/>
    /// The <see cref="CSteamID"/> of the lobby will need to be communicated via game channels or via InviteUserToLobby().
    /// </para>
    ///
    /// The <see cref="LobbyEnter_t"/> callback is also received since the local user has joined their own lobby.
    /// </summary>
    /// <param name="lobbyType">The type and visibility of this lobby. This can be changed later via <see cref="SetLobbyType"/>.</param>
    /// <param name="maxMembers">The maximum number of players that can join this lobby. This can not be above 250.</param>
    /// <returns>
    /// <see cref="CallTask&lt;LobbyCreated_t&gt;"/> that will return <see cref="LobbyCreated_t"/> when awaited.<br/>
    /// Triggers a <see cref="LobbyEnter_t"/> callback.<br/>
    /// Triggers a <see cref="LobbyDataUpdate_t"/> callback.<br/>
    /// If the results returned via the LobbyCreated_t call result indicate success then the lobby is joined &amp; ready to use at this point.
    /// </returns>
    public CallTask<LobbyCreated_t> CreateLobby(ELobbyType lobbyType, int maxMembers)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<LobbyCreated_t>(() => Native.SteamAPI_ISteamMatchmaking_CreateLobby(this.ptr, lobbyType, maxMembers));

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Joins an existing lobby.
    /// </para>
    ///
    /// <para>
    /// The lobby Steam ID can be obtained either from a search with <see cref="RequestLobbyList"/>, joining on a friend, or from an invite.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to join.</param>
    /// <returns>
    /// <see cref="CallTask&lt;LobbyEnter_t&gt;"/> that will return <see cref="LobbyEnter_t"/> when awaited.<br/>
    /// Check <see cref="LobbyEnter_t.ChatRoomEnterResponse"/> to see if was successful.<br/>
    /// Lobby metadata is available to use immediately on this call completing.<br/>
    /// Triggers a <see cref="LobbyDataUpdate_t"/> callback.
    /// </returns>
    public CallTask<LobbyEnter_t> JoinLobby(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<LobbyEnter_t>(() => Native.SteamAPI_ISteamMatchmaking_JoinLobby(this.ptr, steamIDLobby));

        return task;
#endif
    }

    /// <summary>
    /// Leave a lobby that the user is currently in;<br/>
    /// This will take effect immediately on the client side, other users in the lobby will be notified by a <see cref="LobbyChatUpdate_t"/> callback.
    /// </summary>
    /// <param name="steamIDLobby">The lobby to leave.</param>
    public void LeaveLobby(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_LeaveLobby(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// <para>
    /// Invite another user to the lobby.
    /// </para>
    ///
    /// <para>
    /// The target user will receive a <see cref="LobbyInvite_t"/> callback.
    /// </para>
    ///
    /// <para>
    /// If the specified user clicks the join link, a <see cref="GameLobbyJoinRequested_t"/> callback will be posted if the user is in-game,<br/>
    /// or if the game isn't running yet then the game will be automatically launched with the command line parameter <c>+connect_lobby &lt;64-bit lobby Steam ID&gt;</c> instead.
    /// </para>
    /// </summary>
    /// <remarks>NOTE: This call doesn't check if the other user was successfully invited.</remarks>
    /// <param name="steamIDLobby">The Steam ID of the lobby to invite the user to.</param>
    /// <param name="steamIDInvitee">The Steam ID of the person who will be invited.</param>
    /// <returns>bool <c>true</c> if the invite was successfully sent; otherwise, <c>false</c> if the local user isn't in a lobby, no connection to Steam could be made, or the specified user is invalid.</returns>
    public bool InviteUserToLobby(CSteamID steamIDLobby, CSteamID steamIDInvitee)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_InviteUserToLobby(this.ptr, steamIDLobby, steamIDInvitee);
#endif
    }

    // Lobby iteration, for viewing details of users in a lobby
    // only accessible if the lobby user is a member of the specified lobby
    // persona information for other lobby members (name, avatar, etc.) will be asynchronously received
    // and accessible via ISteamFriends interface

    /// <summary>
    /// <para>
    /// Gets the number of users in a lobby.
    /// </para>
    ///
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetLobbyMemberByIndex"/> can be used to get the Steam ID of each person in the lobby.<br/>
    /// Persona information for other lobby members (name, avatar, etc.) is automatically received and accessible via the <see cref="ISteamFriends"/> interface.
    /// </para>
    /// </summary>
    /// <remarks>NOTE: The current user must be in the lobby to retrieve the Steam IDs of other users in that lobby.</remarks>
    /// <param name="steamIDLobby">The Steam ID of the lobby to get the number of members of.</param>
    /// <returns><see cref="int"/> The number of members in the lobby, 0 if the current user has no data from the lobby.</returns>
    public int GetNumLobbyMembers(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetNumLobbyMembers(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// Gets the Steam ID of the lobby member at the given index.
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: You must call <see cref="GetNumLobbyMembers"/> before calling this.
    /// </para>
    /// <para>
    /// NOTE: The current user must be in the lobby to retrieve the Steam IDs of other users in that lobby.
    /// </para>
    /// </remarks>
    /// <param name="steamIDLobby">This MUST be the same lobby used in the previous call to <see cref="GetNumLobbyMembers"/>!</param>
    /// <param name="memberIndex">An index between 0 and <see cref="GetNumLobbyMembers"/>.</param>
    /// <returns><see cref="CSteamID"/> Invalid indices return k_steamIDNil.</returns>
    public CSteamID GetLobbyMemberByIndex(CSteamID steamIDLobby, int memberIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex(this.ptr, steamIDLobby, memberIndex);
#endif
    }

    /// <summary>
    /// Get data associated with this lobby<br/>
    /// takes a simple key, and returns the string associated with it<br/>
    /// "" will be returned if no value is set, or if steamIDLobby is invalid
    /// </summary>

    /// <summary>
    /// Gets the metadata associated with the specified key from the specified lobby.
    /// </summary>
    /// <remarks>
    /// NOTE: This can only get metadata from lobbies that the client knows about, either after receiving a list of lobbies from <see cref="LobbyMatchList_t"/>, retrieving the data with <see cref="RequestLobbyData"/> or after joining a lobby.
    /// </remarks>
    /// <param name="steamIDLobby">The Steam ID of the lobby to get the metadata from.</param>
    /// <param name="key">The key to get the value of.</param>
    /// <returns><see cref="string"/> Returns an empty string (<c>""</c>) if no value is set for this key, or if <paramref name="steamIDLobby"/> is invalid.</returns>
    public string? GetLobbyData(CSteamID steamIDLobby, string key)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamMatchmaking_GetLobbyData(this.ptr, steamIDLobby, key);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Sets a key/value pair in the lobby metadata.<br/>
    /// This can be used to set the the lobby name, current map, game mode, etc.
    /// </para>
    ///
    /// <para>
    /// Each user in the lobby will be receive notification of the lobby data change via a <see cref="LobbyDataUpdate_t"/> callback, and any new users joining will receive any existing data.
    /// </para>
    ///
    /// <para>
    /// This will only send the data if it has changed.<br/>
    /// There is a slight delay before sending the data so you can call this repeatedly to set all the data you need to and it will automatically be batched up and sent after the last sequential call.
    /// </para>
    ///
    /// To reset a key, just set it to <c>""</c>.<br/>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to set the metadata for.</param>
    /// <param name="key">The key to set the data for. This can not be longer than <see cref="MaxLobbyKeyLength"/>.</param>
    /// <param name="value">The value to set. This can not be longer than <see cref="ISteamFriends.ChatMetadataMax"/>.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the data has been set successfully. <c>false</c> if <paramref name="steamIDLobby"/> was invalid, or the key/value are too long.</returns>
    public bool SetLobbyData(CSteamID steamIDLobby, string key, string value)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_SetLobbyData(this.ptr, steamIDLobby, key, value);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the number of metadata keys set on the specified lobby.
    /// </para>
    ///
    /// <para>
    /// This is used for iteration, after calling this then <see cref="GetLobbyDataByIndex"/> can be used to get the key/value pair of each piece of metadata.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: This can only get metadata from lobbies that the client knows about, either after receiving a list of lobbies from <see cref="LobbyMatchList_t"/>, retrieving the data with <see cref="RequestLobbyData"/> or after joining a lobby.
    /// </para>
    /// <para>
    /// NOTE: This should typically only ever be used for debugging purposes.
    /// </para>
    /// </remarks>
    /// <param name="steamIDLobby">The Steam ID of the lobby to get the data count from.</param>
    /// <returns><see cref="int"/> Returns <c>0</c> if <paramref name="steamIDLobby"/> is invalid.</returns>
    public int GetLobbyDataCount(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetLobbyDataCount(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// Gets a lobby metadata key/value pair by index.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetLobbyDataCount"/> before calling this.
    /// </remarks>
    /// <param name="steamIDLobby">This MUST be the same lobby used in the previous call to <see cref="GetLobbyDataCount"/>!</param>
    /// <param name="lobbyDataIndex">An index between 0 and <see cref="GetLobbyDataCount"/>.</param>
    /// <param name="key">Returns the name of the key at the specified index by copying it into this buffer.</param>
    /// <param name="value">Returns the value associated with the key at the specified index by copying it into this buffer.</param>
    /// <returns><see cref="bool"/> <c>true</c> upon success; otherwise, <c>false</c> if the <paramref name="steamIDLobby"/> or <paramref name="lobbyDataIndex"/> are invalid.</returns>
    public bool GetLobbyDataByIndex(CSteamID steamIDLobby, int lobbyDataIndex, out string? key, out string? value)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Span<byte> keyRaw = stackalloc byte[MaxLobbyKeyLength];
        Span<byte> valueRaw = stackalloc byte[ISteamFriends.ChatMetadataMax];

        bool result = Native.SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex(this.ptr, steamIDLobby, lobbyDataIndex, keyRaw, keyRaw.Length, valueRaw, valueRaw.Length);

        if (result)
        {
            key = Utf8StringHelper.NullTerminatedSpanToString(keyRaw);
            value = Utf8StringHelper.NullTerminatedSpanToString(valueRaw);
        }
        else
        {
            key = null;
            value = null;
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Removes a metadata key from the lobby.
    /// </para>
    /// <para>
    /// This can only be done by the owner of the lobby.
    /// </para>
    /// <para>
    /// This will only send the data if the key existed.<br/>
    /// There is a slight delay before sending the data so you can call this repeatedly to set all the data you need to and it will automatically be batched up and sent after the last sequential call.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to delete the metadata for.</param>
    /// <param name="key">The key to delete the data for.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the key/value was successfully deleted; otherwise, <c>false</c> if <paramref name="steamIDLobby"/> or <paramref name="key"/> are invalid.</returns>
    public bool DeleteLobbyData(CSteamID steamIDLobby, string key)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_DeleteLobbyData(this.ptr, steamIDLobby, key);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets per-user metadata from another player in the specified lobby.
    /// </para>
    /// <para>
    /// This can only be queried from members in lobbies that you are currently in.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby that the other player is in.</param>
    /// <param name="steamIDUser">The Steam ID of the player to get the metadata from.</param>
    /// <param name="key">The key to get the value of.</param>
    /// <returns>
    /// <see cref="string"/> Returns <c>null</c> if <paramref name="steamIDLobby"/> is invalid, or <paramref name="steamIDUser"/> is not in the lobby.<br/>
    /// Returns an empty string (<c>""</c>) if <paramref name="key"/> is not set for the player.
    /// </returns>
    public string? GetLobbyMemberData(CSteamID steamIDLobby, CSteamID steamIDUser, string key)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamMatchmaking_GetLobbyMemberData(this.ptr, steamIDLobby, steamIDUser, key);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Sets per-user metadata for the local user.
    /// </para>
    /// <para>
    /// Each user in the lobby will be receive notification of the lobby data change via a <see cref="LobbyDataUpdate_t"/> callback, and any new users joining will receive any existing data.
    /// </para>
    /// <para>
    /// There is a slight delay before sending the data so you can call this repeatedly to set all the data you need to and it will automatically be batched up and sent after the last sequential call.
    /// </para>
    /// <para>
    /// Triggers a <see cref="LobbyDataUpdate_t"/> callback.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to set our metadata in.</param>
    /// <param name="key">The key to set the data for. This can not be longer than <see cref="MaxLobbyKeyLength"/>.</param>
    /// <param name="value">The value to set. This can not be longer than <see cref="ISteamFriends.ChatMetadataMax"/>.</param>
    public void SetLobbyMemberData(CSteamID steamIDLobby, string key, string value)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_SetLobbyMemberData(this.ptr, steamIDLobby, key, value);
#endif
    }

    /// <summary>
    /// <para>
    /// Broadcasts a chat (text or binary data) message to the all of the users in the lobby.
    /// </para>
    /// <para>
    /// All users in the lobby (including the local user) will receive a <see cref="LobbyChatMsg_t"/> callback with the message.
    /// </para>
    /// <para>
    /// If you're sending binary data, you should prefix a header to the message so that you know to treat it as your custom data rather than a plain old text message.
    /// </para>
    /// <para>
    /// For communication that needs to be arbitrated (for example having a user pick from a set of characters, and making sure only one user has picked a character), you can use the lobby owner as the decision maker.<br/>
    /// <see cref="GetLobbyOwner"/> returns the current lobby owner. There is guaranteed to always be one and only one lobby member who is the owner.<br/>
    /// So for the choose-a-character scenario, the user who is picking a character would send the binary message 'I want to be Zoe', the lobby owner would see that message, see if it was OK, and broadcast the appropriate result (user X is Zoe).
    /// </para>
    /// <para>
    /// These messages are sent via the Steam back-end, and so the bandwidth available is limited. For higher-volume traffic like voice or game data, you'll want to use the <a href="https://partner.steamgames.com/doc/features/multiplayer/networking">Steam Networking API</a>.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to send the chat message to.</param>
    /// <param name="msgBody">This can be text or binary data, up to 4 Kilobytes in size. If it's a text message then this should be strlen( text ) + 1 to include the null terminator.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the message was successfully sent. <c>false</c> if the message is too small or too large, or no connection to Steam could be made.
    /// Triggers a <see cref="LobbyChatMsg_t"/> callback.
    /// </returns>
    public bool SendLobbyChatMsg(CSteamID steamIDLobby, ReadOnlySpan<byte> msgBody)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_SendLobbyChatMsg(this.ptr, steamIDLobby, msgBody, msgBody.Length);
#endif
    }

    /// <summary>
    /// <para>
    /// Broadcasts a chat (text or binary data) message to the all of the users in the lobby.
    /// </para>
    /// <para>
    /// All users in the lobby (including the local user) will receive a <see cref="LobbyChatMsg_t"/> callback with the message.
    /// </para>
    /// <para>
    /// If you're sending binary data, you should prefix a header to the message so that you know to treat it as your custom data rather than a plain old text message.
    /// </para>
    /// <para>
    /// For communication that needs to be arbitrated (for example having a user pick from a set of characters, and making sure only one user has picked a character), you can use the lobby owner as the decision maker.<br/>
    /// <see cref="GetLobbyOwner"/> returns the current lobby owner. There is guaranteed to always be one and only one lobby member who is the owner.<br/>
    /// So for the choose-a-character scenario, the user who is picking a character would send the binary message 'I want to be Zoe', the lobby owner would see that message, see if it was OK, and broadcast the appropriate result (user X is Zoe).
    /// </para>
    /// <para>
    /// These messages are sent via the Steam back-end, and so the bandwidth available is limited. For higher-volume traffic like voice or game data, you'll want to use the <a href="https://partner.steamgames.com/doc/features/multiplayer/networking">Steam Networking API</a>.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to send the chat message to.</param>
    /// <param name="msgBody">This can be text or binary data, up to 4 Kilobytes in size. If it's a text message then this should be strlen( text ) + 1 to include the null terminator.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the message was successfully sent. <c>false</c> if the message is too small or too large, or no connection to Steam could be made.
    /// Triggers a <see cref="LobbyChatMsg_t"/> callback.
    /// </returns>
    public bool SendLobbyChatMsg(CSteamID steamIDLobby, string msgBody)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        int len = Encoding.UTF8.GetByteCount(msgBody) + 1;
        Span<byte> raw = stackalloc byte[len];

        int bytesWritten = Encoding.UTF8.GetBytes(msgBody.AsSpan(), raw);
        raw[bytesWritten] = (byte)0;

        return Native.SteamAPI_ISteamMatchmaking_SendLobbyChatMsg(this.ptr, steamIDLobby, raw, raw.Length);
#endif
    }

    /// <summary>
    /// Gets the data from a lobby chat message after receiving a <see cref="LobbyChatMsg_t"/> callback.
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to get the chat entry from. This should almost always be <see cref="LobbyChatMsg_t.SteamIDUser"/>.</param>
    /// <param name="chatID">The index of the chat entry in the lobby. This should almost always be <see cref="LobbyChatMsg_t.ChatID"/>.</param>
    /// <param name="data">Returns the message data by copying it into this buffer. This buffer should be up to 4 Kilobytes.</param>
    /// <returns><see cref="int"/> The number of bytes copied into pvData.</returns>
    public int GetLobbyChatEntry(CSteamID steamIDLobby, int chatID, Span<byte> data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetLobbyChatEntry(this.ptr, steamIDLobby, chatID, IntPtr.Zero, data, data.Length, IntPtr.Zero);
#endif
    }

    /// <summary>
    /// <para>
    /// Refreshes all of the metadata for a lobby that you're not in right now.
    /// </para>
    /// <para>
    /// You will never do this for lobbies you're a member of, that data will always be up to date.<br/>
    /// You can use this to refresh lobbies that you have obtained from <see cref="RequestLobbyList"/> or that are available via friends.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to refresh the metadata of.</param>
    /// <returns>
    /// <para>
    /// <see cref="bool"/> <c>true</c> if the request was successfully sent to the server. <c>false</c> if no connection to Steam could be made, or <paramref name="steamIDLobby"/> is invalid.<br/>
    /// Triggers a <see cref="LobbyDataUpdate_t"/> callback.
    /// </para>
    /// <para>
    /// If the specified lobby doesn't exist, <see cref="LobbyDataUpdate_t.Success"/> will be set to <c>false</c>.
    /// </para>
    /// </returns>
    public bool RequestLobbyData(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_RequestLobbyData(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// <para>
    /// Sets the game server associated with the lobby.
    /// </para>
    /// This can only be set by the owner of the lobby.
    /// <para>
    /// Either the IP/Port or the Steam ID of the game server must be valid, depending on how you want the clients to be able to connect.
    /// </para>
    /// <para>
    /// A <see cref="LobbyGameCreated_t"/> callback will be sent to all players in the lobby, usually at this point, the users will join the specified game server.
    /// </para>
    /// <para>
    /// Usually at this point, the users will join the specified game server.
    /// </para>
    /// <para>
    /// Triggers a <see cref="LobbyGameCreated_t"/> callback.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to set the game server information for.</param>
    /// <param name="gameServerIP">Sets the IP address of the game server, in host order, i.e 127.0.0.1 == 0x7f000001.</param>
    /// <param name="gameServerPort">Sets the connection port of the game server, in host order.</param>
    /// <param name="steamIDGameServer">Sets the Steam ID of the game server. Use k_steamIDNil if you're not setting this.</param>
    public void SetLobbyGameServer(CSteamID steamIDLobby, uint gameServerIP, ushort gameServerPort, CSteamID steamIDGameServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamMatchmaking_SetLobbyGameServer(this.ptr, steamIDLobby, gameServerIP, gameServerPort, steamIDGameServer);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the details of a game server set in a lobby.
    /// </para>
    ///
    /// <para>
    /// Either the IP/Port or the Steam ID of the game server has to be valid, depending on how you want the clients to be able to connect.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to get the game server information from.</param>
    /// <param name="gameServerIP">Returns the IP address of the game server, in host order, i.e 127.0.0.1 == 0x7f000001, if it's set.</param>
    /// <param name="gameServerPort">Returns the connection port of the game server, in host order, if it's set.</param>
    /// <param name="steamIDGameServer">Returns the Steam ID of the game server, if it's set.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the lobby is valid and has a valid game server set; otherwise, <c>false</c>.</returns>
    public bool GetLobbyGameServer(CSteamID steamIDLobby, out uint gameServerIP, out ushort gameServerPort, out CSteamID steamIDGameServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetLobbyGameServer(this.ptr, steamIDLobby, out gameServerIP, out gameServerPort, out steamIDGameServer);
#endif
    }

    /// <summary>
    /// <para>
    /// Set the maximum number of players that can join the lobby.
    /// </para>
    ///
    /// <para>
    /// This is also set when you create the lobby with <see cref="CreateLobby"/>.<br/>
    /// This can only be set by the owner of the lobby.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to set the member limit for.</param>
    /// <param name="maxMembers">The maximum number of players allowed in this lobby. This can not be above 250.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the limit was successfully set.
    /// <c>false</c> if you are not the owner of the specified lobby.
    /// </returns>
    public bool SetLobbyMemberLimit(CSteamID steamIDLobby, int maxMembers)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit(this.ptr, steamIDLobby, maxMembers);
#endif
    }

    /// <summary>
    /// The current limit on the # of users who can join the lobby.<br/>
    /// Returns <c>0</c> if no limit is defined.
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to get the member limit of.</param>
    /// <returns><see cref="int"/> Returns <c>0</c> if no metadata is available for the specified lobby.</returns>
    public int GetLobbyMemberLimit(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// <para>
    /// Updates what type of lobby this is.
    /// </para>
    ///
    /// <para>
    /// This is also set when you create the lobby with CreateLobby.<br/>
    /// This can only be set by the owner of the lobby.
    /// </para>
    ///
    /// <para>
    /// Only lobbies that are <see cref="ELobbyType.Public"/> or <see cref="ELobbyType.Invisible"/>, and are set to joinable, will be returned by <see cref="RequestLobbyList"/> calls.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby to set the type of.</param>
    /// <param name="lobbyType">The new lobby type to that will be set.</param>
    /// <returns><see cref="bool"/> <c>true</c> upon success; otherwise, <c>false</c> if you're not the owner of the lobby.</returns>
    public bool SetLobbyType(CSteamID steamIDLobby, ELobbyType lobbyType)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_SetLobbyType(this.ptr, steamIDLobby, lobbyType);
#endif
    }

    /// <summary>
    /// <para>
    /// Sets whether or not a lobby is joinable by other players. This always defaults to enabled for a new lobby.
    /// </para>
    /// <para>
    /// If joining is disabled, then no players can join, even if they are a friend or have been invited.
    /// </para>
    /// <para>
    /// Lobbies with joining disabled will not be returned from a lobby search.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby</param>
    /// <param name="lobbyJoinable">Enable (<c>true</c>) or disable (<c>false</c>) allowing users to join this lobby?</param>
    /// <returns><see cref="bool"/> <c>true</c> upon success; otherwise, <c>false</c> if you're not the owner of the lobby.</returns>
    public bool SetLobbyJoinable(CSteamID steamIDLobby, bool lobbyJoinable)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_SetLobbyJoinable(this.ptr, steamIDLobby, lobbyJoinable);
#endif
    }

    /// <summary>
    /// <para>
    /// Returns the current lobby owner.
    /// </para>
    /// <para>
    /// There always one lobby owner - if the current owner leaves, another user in the lobby will become the owner automatically. It is possible (but rare) to join a lobby just as the owner is leaving, thus entering a lobby with self as the owner.
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: You must be a member of the lobby to access this.
    /// </remarks>
    /// <param name="steamIDLobby">The Steam ID of the lobby to get the owner of.</param>
    /// <returns><see cref="CSteamID"/> Returns k_steamIDNil if you're not in the lobby.</returns>
    public CSteamID GetLobbyOwner(CSteamID steamIDLobby)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_GetLobbyOwner(this.ptr, steamIDLobby);
#endif
    }

    /// <summary>
    /// <para>
    /// Changes who the lobby owner is.
    /// </para>
    ///
    /// <para>
    /// This can only be set by the owner of the lobby.<br/>
    /// This will trigger a <see cref="LobbyDataUpdate_t"/> for all of the users in the lobby, each user should update their local state to reflect the new owner.<br/>
    /// This is typically accomplished by displaying a crown icon next to the owners name.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the lobby where the owner change will take place.</param>
    /// <param name="steamIDNewOwner">The Steam ID of the user that will be the new owner of the lobby, they must be in the lobby.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the owner was successfully changed.<br/>
    /// <c>false</c> if you're not the current owner of the lobby, <paramref name="steamIDNewOwner"/> is not a member in the lobby, or if no connection to Steam could be made.<br/>
    /// Triggers a <see cref="LobbyDataUpdate_t"/> callback.
    /// </returns>
    public bool SetLobbyOwner(CSteamID steamIDLobby, CSteamID steamIDNewOwner)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_SetLobbyOwner(this.ptr, steamIDLobby, steamIDNewOwner);
#endif
    }

    /// <summary>
    /// <para>
    /// Unused - Link two lobbies for the purposes of checking player compatibility using the frenemy system.
    /// </para>
    /// <para>
    /// You must be the lobby owner of both lobbies.
    /// </para>
    /// </summary>
    /// <param name="steamIDLobby">The Steam ID of the primary lobby.</param>
    /// <param name="steamIDLobbyDependent">The Steam ID that will be linked to the primary lobby.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the request was successfully sent to the Steam server.<br/>
    /// <c>false</c> if the local user isn't the owner of both lobbies, or no connection to Steam could be made.
    /// </returns>
    public bool SetLinkedLobby(CSteamID steamIDLobby, CSteamID steamIDLobbyDependent)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamMatchmaking");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamMatchmaking_SetLinkedLobby(this.ptr, steamIDLobby, steamIDLobbyDependent);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case FavoritesListChanged_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FavoritesListChanged_t>();
                    this.FavoritesListChanged?.Invoke(ref data);
                    break;
                }

            case LobbyChatMsg_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyChatMsg_t>();
                    this.LobbyChatMsg?.Invoke(ref data);
                    break;
                }

            case LobbyChatUpdate_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyChatUpdate_t>();
                    this.LobbyChatUpdate?.Invoke(ref data);
                    break;
                }

            case LobbyCreated_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyCreated_t>();
                    this.LobbyCreated?.Invoke(ref data);
                    break;
                }

            case LobbyDataUpdate_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyDataUpdate_t>();
                    this.LobbyDataUpdate?.Invoke(ref data);
                    break;
                }

            case LobbyEnter_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyEnter_t>();
                    this.LobbyEnter?.Invoke(ref data);
                    break;
                }

            case LobbyGameCreated_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyGameCreated_t>();
                    this.LobbyGameCreated?.Invoke(ref data);
                    break;
                }

            case LobbyInvite_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyInvite_t>();
                    this.LobbyInvite?.Invoke(ref data);
                    break;
                }

            case LobbyKicked_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyKicked_t>();
                    this.LobbyKicked?.Invoke(ref data);
                    break;
                }

            case LobbyMatchList_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LobbyMatchList_t>();
                    this.LobbyMatchList?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamMatchmaking.OnDispatch()");
                break;
        }
    }

#endif
}
