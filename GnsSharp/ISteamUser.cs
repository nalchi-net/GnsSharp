// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;

/// <summary>
/// <para>
/// Functions for accessing and manipulating a steam account associated with one client instance.
/// </para>
///
/// <para>
/// This is also where the APIs for <a href="https://partner.steamgames.com/doc/features/voice">Steam Voice</a> are exposed.
/// </para>
/// </summary>
public class ISteamUser
{
    private IntPtr ptr = IntPtr.Zero;

    internal ISteamUser()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        this.ptr = Native.SteamAPI_SteamUser_v023();
#endif
    }

#pragma warning disable CS0067 // The event is never used

    /// <summary>
    /// Sent by the Steam server to the client telling it to disconnect from the specified game server,<br/>
    /// which it may be in the process of or already connected to.<br/>
    /// The game client should immediately disconnect upon receiving this message.<br/>
    /// This can usually occur if the user doesn't have rights to play on the game server.
    /// </summary>
    public event Callback<ClientGameServerDeny_t>? ClientGameServerDeny;

    /// <summary>
    /// <para>
    /// Sent for games with enabled anti indulgence / duration control, for enabled users.<br/>
    /// Lets the game know whether the feature applies to the user, whether the user needs to exit the game soon, and the remaining daily playtime for the user.
    /// </para>
    ///
    /// <para>
    /// This callback is fired asynchronously in response to timers triggering.<br/>
    /// It is also fired in response to calls to GetDurationControl().
    /// </para>
    /// </summary>
    public event Callback<DurationControl_t>? DurationControl;

    /// <summary>
    /// Called when an encrypted application ticket has been received.
    /// </summary>
    public event Callback<EncryptedAppTicketResponse_t>? EncryptedAppTicketResponse;

    /// <summary>
    /// Sent to your game in response to a steam://gamewebcallback/ command from a user clicking a link in the Steam overlay browser.
    /// You can use this to add support for external site signups where you want to pop back into the browser after some web page signup sequence, and optionally get back some detail about that.
    /// </summary>
    public event Callback<GameWebCallback_t>? GameWebCallback;

    /// <summary>
    /// Result when creating an auth session ticket.
    /// </summary>
    public event Callback<GetAuthSessionTicketResponse_t>? GetAuthSessionTicketResponse;

    /// <summary>
    /// Result when creating an webapi ticket from <see cref="GetAuthTicketForWebApi"/> .
    /// </summary>
    public event Callback<GetTicketForWebApiResponse_t>? GetTicketForWebApiResponse;

    /// <summary>
    /// Called when the callback system for this client is in an error state (and has flushed pending callbacks)<br/>
    /// When getting this message the client should disconnect from Steam, reset any stored Steam state and reconnect.<br/>
    /// This usually occurs in the rare event the Steam client has some kind of fatal error.
    /// </summary>
    public event Callback<IPCFailure_t>? IPCFailure;

    /// <summary>
    /// Called whenever the users licenses (owned packages) changes.
    /// </summary>
    public event Callback<LicensesUpdated_t>? LicensesUpdated;

    /// <summary>
    /// Called when a user has responded to a microtransaction authorization request.
    /// </summary>
    public event Callback<MicroTxnAuthorizationResponse_t>? MicroTxnAuthorizationResponse;

    /// <summary>
    /// Called when a connection attempt has failed.<br/>
    /// This will occur periodically if the Steam client is not connected, and has failed when retrying to establish a connection.
    /// </summary>
    public event Callback<SteamServerConnectFailure_t>? SteamServerConnectFailure;

    /// <summary>
    /// Called when a connections to the Steam back-end has been established.<br/>
    /// This means the Steam client now has a working connection to the Steam servers.<br/>
    /// Usually this will have occurred before the game has launched, and should only be seen if the user has dropped connection due to a networking issue or a Steam server update.
    /// </summary>
    public event Callback<SteamServersConnected_t>? SteamServersConnected;

    /// <summary>
    /// Called if the client has lost connection to the Steam servers.
    /// Real-time services will be disabled until a matching <see cref="SteamServersConnected_t"/> has been posted.
    /// </summary>
    public event Callback<SteamServersDisconnected_t>? SteamServersDisconnected;

    /// <summary>
    /// Response when we have recieved the authentication URL after a call to <see cref="RequestStoreAuthURL"/>.
    /// </summary>
    public event Callback<StoreAuthURLResponse_t>? StoreAuthURLResponse;

    /// <summary>
    /// Called when an auth ticket has been validated.
    /// </summary>
    public event Callback<ValidateAuthTicketResponse_t>? ValidateAuthTicketResponse;

    /// <summary>
    /// Sent in response to <see cref="GetMarketEligibility"/>.
    /// </summary>
    public event Callback<MarketEligibilityResponse_t>? MarketEligibilityResponse;

#pragma warning restore CS0067

    public static ISteamUser? User { get; internal set; }

    public IntPtr Ptr => this.ptr;

    /// <summary>
    /// <para>
    /// Gets Steam user handle that this interface represents.
    /// </para>
    /// <para>
    /// This is only used internally by the API, and by a few select interfaces that support multi-user.
    /// </para>
    /// </summary>
    public HSteamUser GetHSteamUser()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetHSteamUser(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Checks if the current user's Steam client is connected to the Steam servers.
    /// </para>
    /// <para>
    /// If it's not then no real-time services provided by the Steamworks API will be enabled. The Steam client will automatically be trying to recreate the connection as often as possible.<br/>
    /// When the connection is restored a SteamServersConnected_t callback will be posted.
    /// </para>
    /// <para>
    /// You usually don't need to check for this yourself.<br/>
    /// All of the API calls that rely on this will check internally.<br/>
    /// Forcefully disabling stuff when the player loses access is usually not a very good experience for the player and you could be preventing them from accessing APIs that do not need a live connection to Steam.
    /// </para>
    /// </summary>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the Steam client current has a live connection to the Steam servers;<br/>
    /// otherwise, <c>false</c> if there is no active connection due to either a networking issue on the local machine, or the Steam server is down/busy.
    /// </returns>
    public bool BLoggedOn()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BLoggedOn(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the Steam ID of the account currently logged into the Steam client.<br/>
    /// This is commonly called the 'current user', or 'local user'.
    /// </para>
    /// <para>
    /// A Steam ID is a unique identifier for a Steam accounts, Steam groups, Lobbies and Chat rooms, and used to differentiate users in all parts of the Steamworks API.
    /// </para>
    /// </summary>
    public CSteamID GetSteamID()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetSteamID(this.ptr);
#endif
    }

    // Multiplayer Authentication functions

    /// <summary>
    /// <para>
    /// This starts the state machine for authenticating the game client with the game server.
    /// </para>
    /// <para>
    /// It is the client portion of a three-way handshake between the client, the game server, and the steam servers.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// </para>
    /// DEPRECATED! This function will be removed from the SDK in an upcoming version.<br/>
    /// Please migrate to BeginAuthSession and related functions.
    /// <para>
    /// NOTE: When you're done with the connection you must call <see cref="TerminateGameConnection_DEPRECATED"/>.
    /// </para>
    /// <para>
    /// NOTE: This is part of the old user authentication API and should not be mixed with the new API.
    /// </para>
    /// </remarks>
    /// <param name="authBlob">A pointer to empty memory that will be filled in with the authentication token. Should be at least 2048 bytes.</param>
    /// <param name="steamIDGameServer">The Steam ID of the game server, received from the game server by the client.</param>
    /// <param name="ipServer">The IP address of the game server in host order, i.e 127.0.0.1 == 0x7f000001.</param>
    /// <param name="portServer">The connection port of the game server, in host order.</param>
    /// <param name="secure">Whether or not the client thinks that the game server is reporting itself as secure (i.e. VAC is running.)</param>
    /// <returns>
    /// <para>
    /// The number of bytes written to pBlob.
    /// </para>
    /// <para>
    /// Returns 0 indicating failure, signifying that the pAuthBlob buffer passed in was too small, and the call has failed.
    /// </para>
    /// <para>
    /// The contents of pAuthBlob should then be sent to the game server, for it to use to complete the authentication process.
    /// </para>
    /// </returns>
    public int InitiateGameConnection_DEPRECATED(Span<byte> authBlob, CSteamID steamIDGameServer, uint ipServer, ushort portServer, bool secure)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_InitiateGameConnection_DEPRECATED(this.ptr, authBlob, authBlob.Length, steamIDGameServer, ipServer, portServer, secure);
#endif
    }

    /// <summary>
    /// <para>
    /// Notify the game server that we are disconnecting.
    /// </para>
    /// <para>
    /// This needs to occur when the game client leaves the specified game server, needs to match with the <see cref="InitiateGameConnection_DEPRECATED"/> call.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// DEPRECATED!  This function will be removed from the SDK in an upcoming version.<br/>
    /// Please migrate to BeginAuthSession and related functions.
    /// </para>
    /// <para>
    /// NOTE: This is part of the old user authentication API and should not be mixed with the new API.
    /// </para>
    /// </remarks>
    public void TerminateGameConnection_DEPRECATED(uint ipServer, ushort portServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUser_TerminateGameConnection_DEPRECATED(this.ptr, ipServer, portServer);
#endif
    }

    // Legacy functions

    /// <summary>
    /// Deprecated - Only used by only a few games to track usage events before <a href="https://partner.steamgames.com/doc/features/achievements">Stats and Achievements</a> was introduced.
    /// </summary>
    public void TrackAppUsageEvent(CGameID gameID, int appUsageEvent, string extraInfo)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUser_TrackAppUsageEvent(this.ptr, gameID, appUsageEvent, extraInfo);
#endif
    }

    /// <summary>
    /// Deprecated - Only used by only a few games to track usage events before <a href="https://partner.steamgames.com/doc/features/achievements">Stats and Achievements</a> was introduced.
    /// </summary>
    public void TrackAppUsageEvent(CGameID gameID, int appUsageEvent)
    {
        this.TrackAppUsageEvent(gameID, appUsageEvent, string.Empty);
    }

    /// <summary>
    /// Deprecated - You should use the ISteamRemoteStorage API from Steam Cloud instead.
    /// </summary>
    public bool GetUserDataFolder(Span<byte> buffer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetUserDataFolder(this.ptr, buffer, buffer.Length);
#endif
    }

    /// <summary>
    /// <para>
    /// Starts voice recording.
    /// </para>
    /// <para>
    /// Once started, use <see cref="GetAvailableVoice"/> and <see cref="GetVoice"/> to get the data, and then call <see cref="StopVoiceRecording"/> when the user has released their push-to-talk hotkey or the game session has completed.
    /// </para>
    /// <para>
    /// See <a href="https://partner.steamgames.com/doc/features/voice">Steam Voice</a> for more information.
    /// </para>
    /// </summary>
    public void StartVoiceRecording()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUser_StartVoiceRecording(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Stops voice recording.
    /// </para>
    /// <para>
    /// Because people often release push-to-talk keys early, the system will keep recording for a little bit after this function is called.<br/>
    /// As such, <see cref="GetVoice"/> should continue to be called until it returns <see cref="EVoiceResult.NotRecording"/>, only then will voice recording be stopped.
    /// </para>
    /// </summary>
    public void StopVoiceRecording()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUser_StopVoiceRecording(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Checks to see if there is captured audio data available from <see cref="GetVoice"/>, and gets the size of the data.
    /// </para>
    /// <para>
    /// Most applications will only use compressed data and should ignore the other parameters, which exist primarily for backwards compatibility.<br/>
    /// See <see cref="GetVoice"/> for further explanation of "uncompressed" data.
    /// </para>
    /// <para>
    /// See <a href="https://partner.steamgames.com/doc/features/voice">Steam Voice</a> for more information.
    /// </para>
    /// </summary>
    /// <param name="compressed">Returns the size of the available voice data in bytes.</param>
    public EVoiceResult GetAvailableVoice(out uint compressed)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetAvailableVoice(this.ptr, out compressed, IntPtr.Zero, 0);
#endif
    }

    /// <summary>
    /// <para>
    /// Read captured audio data from the microphone buffer.
    /// </para>
    /// <para>
    /// The compressed data can be transmitted by your application and decoded back into raw audio data using DecompressVoice on the other side.<br/>
    /// The compressed data provided is in an arbitrary format and is not meant to be played directly.
    /// </para>
    /// <para>
    /// This should be called once per frame, and at worst no more than four times a second to keep the microphone input delay as low as possible.<br/>
    /// Calling this any less may result in gaps in the returned stream.
    /// </para>
    /// <para>
    /// It is recommended that you pass in an 8 kilobytes or larger destination buffer for compressed audio.<br/>
    /// Static buffers are recommended for performance reasons.<br/>
    /// However, if you would like to allocate precisely the right amount of space for a buffer before each call you may use GetAvailableVoice to find out how much data is available to be read.
    /// </para>
    /// <para>
    /// See <a href="https://partner.steamgames.com/doc/features/voice">Steam Voice</a> for more information.
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: "Uncompressed" audio is a deprecated feature and should not be used<br/>
    /// by most applications. It is raw single-channel 16-bit PCM wave data which<br/>
    /// may have been run through preprocessing filters and/or had silence removed,<br/>
    /// so the uncompressed audio could have a shorter duration than you expect.<br/>
    /// There may be no data at all during long periods of silence. Also, fetching<br/>
    /// uncompressed audio will cause GetVoice to discard any leftover compressed<br/>
    /// audio, so you must fetch both types at once. Finally, GetAvailableVoice is<br/>
    /// not precisely accurate when the uncompressed size is requested. So if you<br/>
    /// really need to use uncompressed audio, you should call GetVoice frequently<br/>
    /// with two very large (20KiB+) output buffers instead of trying to allocate<br/>
    /// perfectly-sized buffers. But most applications should ignore all of these<br/>
    /// details and simply leave the "uncompressed" parameters as <c>null</c>/<c>0</c>.
    /// </remarks>
    /// <param name="destBuffer">The buffer where the audio data will be copied into.</param>
    /// <param name="bytesWritten">Returns the number of bytes written into pDestBuffer.<br/>
    /// This should always be the size returned by <see cref="ISteamUser.GetAvailableVoice"/>.</param>
    public EVoiceResult GetVoice(Span<byte> destBuffer, out uint bytesWritten)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetVoice(this.ptr, true, destBuffer, (uint)destBuffer.Length, out bytesWritten, false, IntPtr.Zero, 0, IntPtr.Zero, 0);
#endif
    }

    /// <summary>
    /// <para>
    /// Decodes the compressed voice data returned by <see cref="GetVoice"/>.
    /// </para>
    /// <para>
    /// The output data is raw single-channel 16-bit PCM audio. The decoder supports any sample rate from 11025 to 48000. See <see cref="GetVoiceOptimalSampleRate"/> for more information.
    /// </para>
    /// <para>
    /// It is recommended that you start with a 20KiB buffer and then reallocate as necessary.
    /// </para>
    /// <para>
    /// See <a href="https://partner.steamgames.com/doc/features/voice">Steam Voice</a> for more information.
    /// </para>
    /// </summary>
    /// <param name="compressed">The compressed data received from <see cref="GetVoice"/>.</param>
    /// <param name="destBuffer">The buffer where the raw audio data will be returned.<br/>
    /// This can then be passed to your audio subsystems for playback.</param>
    /// <param name="bytesWritten">Returns the number of bytes written to pDestBuffer, or size of the buffer required to decompress the given data if cbDestBufferSize is not large enough (and <see cref="EVoiceResult.BufferTooSmall"/> is returned).</param>
    /// <param name="desiredSampleRate">The sample rate that will be returned.<br/>
    /// This can be from 11025 to 48000, you should either use the rate that works best for your audio playback system, which likely takes the users audio hardware into account, or you can use <see cref="GetVoiceOptimalSampleRate"/> to get the native sample rate of the Steam voice decoder.</param>
    /// <returns><see cref="EVoiceResult"/> The internal sample rate of the Steam Voice decoder.</returns>
    public EVoiceResult DecompressVoice(ReadOnlySpan<byte> compressed, Span<byte> destBuffer, out uint bytesWritten, uint desiredSampleRate)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_DecompressVoice(this.ptr, compressed, (uint)compressed.Length, destBuffer, (uint)destBuffer.Length, out bytesWritten, desiredSampleRate);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the native sample rate of the Steam voice decoder.
    /// </para>
    /// <para>
    /// Using this sample rate for DecompressVoice will perform the least CPU processing.<br/>
    /// However, the final audio quality will depend on how well the audio device (and/or your application's audio output SDK) deals with lower sample rates.<br/>
    /// You may find that you get the best audio output quality when you ignore this function and use the native sample rate of your audio output device, which is usually 48000 or 44100.
    /// </para>
    /// <para>
    /// See <a href="https://partner.steamgames.com/doc/features/voice">Steam Voice</a> for more information.
    /// </para>
    /// </summary>
    public uint GetVoiceOptimalSampleRate()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetVoiceOptimalSampleRate(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Retrieve an authentication ticket to be sent to the entity who wishes to authenticate you.
    /// </para>
    /// <para>
    /// After calling this you can send the ticket to the entity where they can then call <see cref="BeginAuthSession"/>/<see cref="ISteamGameServer.BeginAuthSession"/> to verify this entity's integrity.
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: This API can not be used to create a ticket for use by the <a href="https://partner.steamgames.com/doc/webapi/ISteamUserAuth#AuthenticateUserTicket">ISteamUserAuth/AuthenticateUserTicket</a> Web API.<br/>
    /// Use the <see cref="GetAuthTicketForWebApi"/> call instead
    /// </remarks>
    /// <param name="ticket">
    /// The buffer where the new auth ticket will be copied into if the call was successful.<br/>
    /// Typically a buffer size of 1024 will be sufficient.<br/>
    /// However, in certain cases (e.g., when an application has a large amount of available DLC), a larger buffer size may be required.
    /// </param>
    /// <param name="ticketSize">Returns the length of the actual ticket.</param>
    /// <param name="identityRemote">The optional identity of the remote system that will authenticate the ticket.<br/>
    /// If it is peer-to-peer then the user steam ID.<br/>
    /// If it is a game server, then the game server steam ID may be used if it was obtained from a trusted 3rd party, otherwise use the IP address.<br/>
    /// If it is a service, a string identifier of that service if one if provided.</param>
    /// <returns>
    /// <para>
    /// <see cref="HAuthTicket"/> A handle to the auth ticket. When you're done interacting with the entity you must call CancelAuthTicket on the handle.<br/>
    /// Triggers a <see cref="GetAuthSessionTicketResponse"/> callback.
    /// </para>
    /// <para>
    /// Returns <see cref="HAuthTicket.Invalid"/> if the call fails.
    /// </para>
    /// </returns>
    public HAuthTicket GetAuthSessionTicket(Span<byte> ticket, out uint ticketSize, in SteamNetworkingIdentity identityRemote)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetAuthSessionTicket(this.ptr, ticket, ticket.Length, out ticketSize, in identityRemote);
#endif
    }

    // Request a ticket which will be used for webapi "ISteamUserAuth\AuthenticateUserTicket"
    // pchIdentity is an optional input parameter to identify the service the ticket will be sent to
    // the ticket will be returned in callback GetTicketForWebApiResponse_t

    /// <summary>
    /// <para>
    /// Retrieve an authentication ticket to be sent to the entity that wishes to authenticate you using the <a href="https://partner.steamgames.com/doc/webapi/ISteamUserAuth#AuthenticateUserTicket">ISteamUserAuth/AuthenticateUserTicket</a> Web API.
    /// </para>
    /// <para>
    /// The calling application must wait for the <see cref="GetTicketForWebApiResponse"/> callback generated by the API call to access the ticket.
    /// </para>
    /// <para>
    /// It is best practice to use an identity string for each service that will consume tickets.
    /// </para>
    /// <remarks>
    /// NOTE: This API can not be used to create a ticket for use by the <see cref="BeginAuthSession"/>/<see cref="ISteamGameServer.BeginAuthSession"/>.<br/>
    /// Use the GetAuthSessionTicket API instead
    /// </remarks>
    /// </summary>
    /// <param name="identity">The identity of the remote service that will authenticate the ticket.<br/>
    /// The service should provide a string identifier. Pass <c>null</c> if none was provided.</param>
    /// <returns>
    /// <para>
    /// <see cref="HAuthTicket"/> A handle to the auth ticket. When you're done interacting with the entity you must call CancelAuthTicket on the handle.<br/>
    /// Triggers a <see cref="GetTicketForWebApiResponse"/> callback.
    /// </para>
    /// <para>
    /// Returns <see cref="HAuthTicket.Invalid"/> if the call fails.
    /// </para>
    /// </returns>
    public HAuthTicket GetAuthTicketForWebApi(string? identity)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        if (identity != null)
        {
            return Native.SteamAPI_ISteamUser_GetAuthTicketForWebApi(this.ptr, identity);
        }

        return Native.SteamAPI_ISteamUser_GetAuthTicketForWebApi(this.ptr, IntPtr.Zero);
#endif
    }

    /// <summary>
    /// <para>
    /// Authenticate the ticket from the entity Steam ID to be sure it is valid and isn't reused.<br/>
    /// Note that identity is not confirmed until the callback <see cref="ValidateAuthTicketResponse"/> is received and the return value in that callback is checked for success.
    /// </para>
    /// <para>
    /// The ticket is created on the entity with <see cref="GetAuthSessionTicket"/> or <see cref="ISteamGameServer.GetAuthSessionTicket"/> and then needs to be provided over the network for the other end to validate.
    /// </para>
    /// <para>
    /// This registers for additional <see cref="ValidateAuthTicketResponse"/> callbacks if the entity goes offline or cancels the ticket.<br/>
    /// See <see cref="EAuthSessionResponse"/> for more information.
    /// </para>
    /// <para>
    /// When the multiplayer session terminates you must call <see cref="EndAuthSession"/>.
    /// </para>
    /// </summary>
    /// <param name="authTicket">The auth ticket to validate. The size must be the <c>ticketSize</c> provided by the call that created this ticket.</param>
    /// <param name="steamID">The entity's Steam ID that sent this ticket.</param>
    /// <returns><see cref="EBeginAuthSessionResult"/> Triggers a <see cref="ValidateAuthTicketResponse"/> callback.</returns>
    /// <seealso href="https://partner.steamgames.com/doc/features/auth">User Authentication and Ownership</seealso>
    public EBeginAuthSessionResult BeginAuthSession(ReadOnlySpan<byte> authTicket, CSteamID steamID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BeginAuthSession(this.ptr, authTicket, authTicket.Length, steamID);
#endif
    }

    /// <summary>
    /// Ends an auth session that was started with <see cref="BeginAuthSession"/>.<br/>
    /// This should be called when no longer playing with the specified entity.
    /// </summary>
    /// <param name="steamID">The entity to end the active auth session with.</param>
    /// <seealso href="https://partner.steamgames.com/doc/features/auth">User Authentication and Ownership</seealso>
    public void EndAuthSession(CSteamID steamID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUser_EndAuthSession(this.ptr, steamID);
#endif
    }

    /// <summary>
    /// Cancels an auth ticket received from <see cref="GetAuthSessionTicket"/>.<br/>
    /// This should be called when no longer playing with the specified entity.
    /// </summary>
    /// <param name="authTicket">The active auth ticket to cancel.</param>
    /// <seealso href="https://partner.steamgames.com/doc/features/auth">User Authentication and Ownership</seealso>
    public void CancelAuthTicket(HAuthTicket authTicket)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUser_CancelAuthTicket(this.ptr, authTicket);
#endif
    }

    // After receiving a user's authentication data, and passing it to BeginAuthSession, use this function
    // to determine if the user owns downloadable content specified by the provided AppID.

    /// <summary>
    /// <para>
    /// Checks if the user owns a specific piece of <a href="https://partner.steamgames.com/doc/store/application/dlc">Downloadable Content (DLC)</a>.
    /// </para>
    /// <para>
    /// This can only be called after sending the users auth ticket to <see cref="ISteamGameServer.BeginAuthSession"/>
    /// </para>
    /// </summary>
    /// <param name="steamID">The Steam ID of the user that sent the auth ticket.</param>
    /// <param name="appID">The DLC App ID to check if the user owns it.</param>
    /// <seealso href="https://partner.steamgames.com/doc/features/auth">User Authentication and Ownership</seealso>
    public EUserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId_t appID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_UserHasLicenseForApp(this.ptr, steamID, appID);
#endif
    }

    // returns true if this users looks like they are behind a NAT device. Only valid once the user has connected to steam
    // (i.e a SteamServersConnected_t has been issued) and may not catch all forms of NAT.

    /// <summary>
    /// <para>
    /// Checks if the current user looks like they are behind a NAT device.
    /// </para>
    /// <para>
    /// This is only valid if the user is connected to the Steam servers and may not catch all forms of NAT.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if the current user is behind a NAT, otherwise <c>false</c>.</returns>
    public bool BIsBehindNAT()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BIsBehindNAT(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Set the rich presence data for an unsecured game server that the user is playing on.<br/>
    /// This allows friends to be able to view the game info and join your game.
    /// </para>
    /// <para>
    /// When you are using Steam authentication system this call is never required, the auth system automatically sets the appropriate rich presence.
    /// </para>
    /// </summary>
    /// <param name="steamIDGameServer">This should be <see cref="CSteamID.NonSteamGS"/> if you're setting the IP/Port, otherwise it should be <see cref="CSteamID.Nil"/> if you're clearing this.</param>
    /// <param name="ipServer">The IP of the game server in host order, i.e 127.0.0.1 == 0x7f000001.</param>
    /// <param name="portServer">The connection port of the game server, in host order.</param>
    public void AdvertiseGame(CSteamID steamIDGameServer, uint ipServer, ushort portServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUser_AdvertiseGame(this.ptr, steamIDGameServer, ipServer, portServer);
#endif
    }

    /// <summary>
    /// <para>
    /// Requests an application ticket encrypted with the secret "encrypted app ticket key".
    /// </para>
    /// <para>
    /// The encryption key can be obtained from the <a href="https://partner.steamgames.com/apps/sdkauth/">Encrypted App Ticket Key</a> page on the App Admin for your app.
    /// </para>
    /// <para>
    /// There can only be one <see cref="EncryptedAppTicketResponse_t"/> pending, and this call is subject to a 60 second rate limit.
    /// </para>
    /// <para>
    /// After receiving the response you should call <see cref="GetEncryptedAppTicket"/> to get the ticket data,<br/>
    /// and then you need to send it to a secure server to be decrypted with the <see cref="SteamEncryptedAppTicket"/> functions.
    /// </para>
    /// </summary>
    /// <param name="dataToInclude">The data which will be encrypted into the ticket.</param>
    /// <returns><see cref="CallTask&lt;EncryptedAppTicketResponse_t&gt;"/> that will return <see cref="EncryptedAppTicketResponse_t"/> when awaited.</returns>
    public CallTask<EncryptedAppTicketResponse_t>? RequestEncryptedAppTicket(Span<byte> dataToInclude)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, Span<byte>, int, EncryptedAppTicketResponse_t>(
        Native.SteamAPI_ISteamUser_RequestEncryptedAppTicket, this.ptr, dataToInclude, dataToInclude.Length);

        return task;
#endif
    }

    // Retrieves a finished ticket.

    /// <summary>
    /// <para>
    /// Retrieve an encrypted ticket.
    /// </para>
    /// <para>
    /// This should be called after requesting an encrypted app ticket with <see cref="RequestEncryptedAppTicket"/> and receiving the <see cref="EncryptedAppTicketResponse_t"/> call result.
    /// </para>
    /// <para>
    /// You should then pass this encrypted ticket to your secure servers to be decrypted using your secret key using <see cref="SteamEncryptedAppTicket.BDecryptTicket"/>.
    /// </para>
    /// <para>
    /// Upon exit, <paramref name="ticketSize"/> will be either the size of the ticket copied into your buffer<br/>
    /// (if <c>true</c> was returned), or the size needed (if <c>false</c> was returned).  To determine the<br/>
    /// proper size of the ticket, you can pass an empty span to <paramref name="ticket"/>; if a ticket<br/>
    /// is available, <paramref name="ticketSize"/> will contain the size needed, otherwise it will be <c>0</c>.
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: If you call this without calling <see cref="RequestEncryptedAppTicket"/>, the call may succeed but you will likely get a stale ticket.
    /// </remarks>
    /// <param name="ticket">The encrypted app ticket is copied into this buffer.</param>
    /// <param name="ticketSize">Returns the number of bytes copied into <paramref name="ticket"/>.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the call successfully returned an app ticket into pTicket.<br/>
    /// <c>false</c> under the following conditions:<br/>
    /// * <paramref name="ticket"/> is an empty span.<br/>
    /// * <paramref name="ticket"/> is too small to hold this ticket.<br/>
    /// * There was no ticket available. (Did you wait for <see cref="EncryptedAppTicketResponse_t"/>?)
    /// </returns>
    public bool GetEncryptedAppTicket(Span<byte> ticket, out uint ticketSize)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetEncryptedAppTicket(this.ptr, ticket, ticket.Length, out ticketSize);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the level of the users Steam badge for your game.
    /// </para>
    /// <para>
    /// The user can have two different badges for a series; the regular badge (max level 5) and the foil badge (max level 1).
    /// </para>
    /// </summary>
    /// <param name="series">If you only have one set of cards, the series will be <c>1</c>.</param>
    /// <param name="foil">Check if they have received the foil badge.</param>
    /// <returns><see cref="int"/> The level of the badge, <c>0</c> if they don't have it.</returns>
    public int GetGameBadgeLevel(int series, bool foil)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetGameBadgeLevel(this.ptr, series, foil);
#endif
    }

    /// <summary>
    /// Gets the Steam level of the user, as shown on their Steam community profile.
    /// </summary>
    /// <returns><see cref="int"/> The level of the current user.</returns>
    public int GetPlayerSteamLevel()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_GetPlayerSteamLevel(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Requests a URL which authenticates an in-game browser for store check-out, and then redirects to the specified URL.
    /// </para>
    /// <para>
    /// As long as the in-game browser accepts and handles session cookies,<br/>
    /// Steam microtransaction checkout pages will automatically recognize the user instead of presenting a login page.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: The URL has a very short lifetime to prevent history-snooping attacks,<br/>
    /// so you should only call this API when you are about to launch the browser,<br/>
    /// or else immediately navigate to the result URL using a hidden browser window.
    /// </para>
    /// <para>
    /// NOTE: The resulting authorization cookie has an expiration time of one day,<br/>
    /// so it would be a good idea to request and visit a new auth URL every 12 hours.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <see cref="CallTask&lt;StoreAuthURLResponse_t&gt;"/> that will return <see cref="StoreAuthURLResponse_t"/> when awaited.<br/>
    /// Returns <c>null</c> if no connection to the Steam servers could be made.
    /// </returns>
    public CallTask<StoreAuthURLResponse_t>? RequestStoreAuthURL(string redirectUrl)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, string, StoreAuthURLResponse_t>(
    Native.SteamAPI_ISteamUser_RequestStoreAuthURL, this.ptr, redirectUrl);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Checks whether the current user has verified their phone number.
    /// </para>
    /// <para>
    /// See the <a href="https://support.steampowered.com/kb_article.php?ref=8625-wrah-9030">Steam Guard Mobile Authenticator</a> page on the customer facing Steam Support site for more information.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if the current user has phone verification enabled; otherwise, <c>false</c>.</returns>
    public bool BIsPhoneVerified()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BIsPhoneVerified(this.ptr);
#endif
    }

    // gets whether the user has two factor enabled on their account

    /// <summary>
    /// <para>
    /// Checks whether the current user has Steam Guard two factor authentication enabled on their account.
    /// </para>
    /// <para>
    /// See the <a href="https://support.steampowered.com/kb_article.php?ref=8625-wrah-9030">Steam Guard Mobile Authenticator</a> page on the customer facing Steam Support site for more information.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if the current user has two factor authentication enabled; otherwise, <c>false</c>.</returns>
    public bool BIsTwoFactorEnabled()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BIsTwoFactorEnabled(this.ptr);
#endif
    }

    /// <summary>
    /// Checks whether the user's phone number is used to uniquely identify them.
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if the current user's phone uniquely verifies their identity; otherwise, <c>false</c>.</returns>
    public bool BIsPhoneIdentifying()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BIsPhoneIdentifying(this.ptr);
#endif
    }

    /// <summary>
    /// Checks whether the current user's phone number is awaiting (re)verification.
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if current user's phone is requiring verification; otherwise, <c>false</c>.</returns>
    public bool BIsPhoneRequiringVerification()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BIsPhoneRequiringVerification(this.ptr);
#endif
    }

    /// <returns>
    /// <see cref="CallTask&lt;MarketEligibilityResponse_t&gt;"/> that will return <see cref="MarketEligibilityResponse_t"/> when awaited.<br/>
    /// </returns>
    public CallTask<MarketEligibilityResponse_t>? GetMarketEligibility()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, MarketEligibilityResponse_t>(
    Native.SteamAPI_ISteamUser_GetMarketEligibility, this.ptr);

        return task;
#endif
    }

    /// <summary>
    /// Retrieves anti indulgence / duration control for current user / game combination.
    /// </summary>
    /// <returns>
    /// <see cref="CallTask&lt;DurationControl_t&gt;"/> that will return <see cref="DurationControl_t"/> when awaited.<br/>
    /// Returns <c>null</c> if no connection to the Steam servers could be made.
    /// </returns>
    public CallTask<DurationControl_t>? GetDurationControl()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, DurationControl_t>(
    Native.SteamAPI_ISteamUser_GetDurationControl, this.ptr);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Allows the game to specify the offline/online gameplay state for steam china duration control.
    /// </para>
    /// <para>
    /// Advise steam china duration control system about the online state of the game.<br/>
    /// This will prevent offline gameplay time from counting against a user's playtime limits.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if the online state was set successfully; otherwise, <c>false</c>.</returns>
    public bool BSetDurationControlOnlineState(EDurationControlOnlineState newState)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUser");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUser_BSetDurationControlOnlineState(this.ptr, newState);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case ClientGameServerDeny_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<ClientGameServerDeny_t>();
                    this.ClientGameServerDeny?.Invoke(ref data);
                    break;
                }

            case DurationControl_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<DurationControl_t>();
                    this.DurationControl?.Invoke(ref data);
                    break;
                }

            case EncryptedAppTicketResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<EncryptedAppTicketResponse_t>();
                    this.EncryptedAppTicketResponse?.Invoke(ref data);
                    break;
                }

            case GameWebCallback_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GameWebCallback_t>();
                    this.GameWebCallback?.Invoke(ref data);
                    break;
                }

            case GetAuthSessionTicketResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GetAuthSessionTicketResponse_t>();
                    this.GetAuthSessionTicketResponse?.Invoke(ref data);
                    break;
                }

            case GetTicketForWebApiResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GetTicketForWebApiResponse_t>();
                    this.GetTicketForWebApiResponse?.Invoke(ref data);
                    break;
                }

            case IPCFailure_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<IPCFailure_t>();
                    this.IPCFailure?.Invoke(ref data);
                    break;
                }

            case LicensesUpdated_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LicensesUpdated_t>();
                    this.LicensesUpdated?.Invoke(ref data);
                    break;
                }

            case MicroTxnAuthorizationResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<MicroTxnAuthorizationResponse_t>();
                    this.MicroTxnAuthorizationResponse?.Invoke(ref data);
                    break;
                }

            case SteamServerConnectFailure_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<SteamServerConnectFailure_t>();
                    this.SteamServerConnectFailure?.Invoke(ref data);
                    break;
                }

            case SteamServersConnected_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<SteamServersConnected_t>();
                    this.SteamServersConnected?.Invoke(ref data);
                    break;
                }

            case SteamServersDisconnected_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<SteamServersDisconnected_t>();
                    this.SteamServersDisconnected?.Invoke(ref data);
                    break;
                }

            case StoreAuthURLResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<StoreAuthURLResponse_t>();
                    this.StoreAuthURLResponse?.Invoke(ref data);
                    break;
                }

            case ValidateAuthTicketResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<ValidateAuthTicketResponse_t>();
                    this.ValidateAuthTicketResponse?.Invoke(ref data);
                    break;
                }

            case MarketEligibilityResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<MarketEligibilityResponse_t>();
                    this.MarketEligibilityResponse?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamUser.OnDispatch()");
                break;
        }
    }

#endif
}
