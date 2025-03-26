// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/// <summary>
/// Provides functions for accessing and submitting stats, achievements, and leaderboards.
/// </summary>
public class ISteamUserStats
{
    /// <summary>
    /// Maximum number of bytes for stat and achievement names (UTF-8 encoded).
    /// </summary>
    public const int StatNameMax = 128;

    /// <summary>
    /// Maximum number of bytes for a leaderboard name (UTF-8 encoded).
    /// </summary>
    public const int LeaderboardNameMax = 128;

    /// <summary>
    /// Maximum number of details in <c>int32</c> that you can store for a single leaderboard entry.
    /// </summary>
    public const int LeaderboardDetailsMax = 64;

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamUserStats()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        this.ptr = Native.SteamAPI_SteamUserStats_v013();
#endif
    }

#pragma warning disable CS0067 // The event is never used

    /// <summary>
    /// Called when the global achievement percentages have been received from the server.
    /// </summary>
    public event Callback<GlobalAchievementPercentagesReady_t>? GlobalAchievementPercentagesReady;

    /// <summary>
    /// Called when the global stats have been received from the server.<br/>
    /// Returned as a result of <see cref="RequestGlobalStats"/>
    /// </summary>
    public event Callback<GlobalStatsReceived_t>? GlobalStatsReceived;

    /// <summary>
    /// Call Result when finding a leaderboard, returned as a result of <see cref="FindOrCreateLeaderboard"/> or <see cref="FindLeaderboard"/>.
    /// </summary>
    public event Callback<LeaderboardFindResult_t>? LeaderboardFindResult;

    /// <summary>
    /// Call result for <see cref="DownloadLeaderboardEntries"/> when scores for a leaderboard have been downloaded and are ready to be retrieved.
    /// After calling you must use <see cref="GetDownloadedLeaderboardEntry"/> to retrieve the info for each downloaded entry.
    /// </summary>
    public event Callback<LeaderboardScoresDownloaded_t>? LeaderboardScoresDownloaded;

    /// <summary>
    /// Call result for <see cref="UploadLeaderboardScore"/> indicating that a leaderboard score has been uploaded.
    /// </summary>
    public event Callback<LeaderboardScoreUploaded_t>? LeaderboardScoreUploaded;

    /// <summary>
    /// Call result for <see cref="AttachLeaderboardUGC"/> indicating that user generated content has been attached to one of the current user's leaderboard entries.
    /// </summary>
    public event Callback<LeaderboardUGCSet_t>? LeaderboardUGCSet;

    /// <summary>
    /// Gets the current number of players for the current AppId.
    /// </summary>
    public event Callback<NumberOfCurrentPlayers_t>? NumberOfCurrentPlayers;

    /// <summary>
    /// Result of an achievement icon that has been fetched
    /// </summary>
    public event Callback<UserAchievementIconFetched_t>? UserAchievementIconFetched;

    /// <summary>
    /// Result of a request to store the achievements on the server, or an "indicate progress" call.<br/>
    /// If both <see cref="UserAchievementStored_t.CurProgress"/> and <see cref="UserAchievementStored_t.MaxProgress"/> are zero, that means the achievement has been fully unlocked.
    /// </summary>
    public event Callback<UserAchievementStored_t>? UserAchievementStored;

    /// <summary>
    /// Called when the latest stats and achievements for a specific user (including the local user) have been received from the server.
    /// </summary>
    public event Callback<UserStatsReceived_t>? UserStatsReceived;

    /// <summary>
    /// Result of a request to store the user stats.
    /// </summary>
    public event Callback<UserStatsStored_t>? UserStatsStored;

    /// <summary>
    /// <para>
    /// Callback indicating that a user's stats have been unloaded.
    /// </para>
    /// <para>
    /// Call <see cref="RequestUserStats"/> again before accessing stats for this user.
    /// </para>
    /// </summary>
    public event Callback<UserStatsUnloaded_t>? UserStatsUnloaded;

#pragma warning restore CS0067

    public static ISteamUserStats? User { get; internal set; }

    public IntPtr Ptr => this.ptr;

    // Note: this call is no longer required as it is managed by the Steam client
    // The game stats and achievements will be synchronized with Steam before
    // the game process begins.
    // public bool RequestCurrentStats()  { }

    // Data accessors

    /// <summary>
    /// <para>
    /// Gets the current value of the a stat for the current user.
    /// </para>
    /// <para>
    /// To receive stats for other users use <see cref="GetUserStat(CSteamID, string, out int)"/>.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * The type passed to this function must match the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool GetStat(string name, out int data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetStatInt32(this.ptr, name, out data);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the current value of the a stat for the current user.
    /// </para>
    /// <para>
    /// To receive stats for other users use <see cref="GetUserStat(CSteamID, string, out float)"/>.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * The type passed to this function must match the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool GetStat(string name, out float data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetStatFloat(this.ptr, name, out data);
#endif
    }

    // Set / update data

    /// <summary>
    /// <para>
    /// Sets / updates the value of a given stat for the current user.
    /// </para>
    /// <para>
    /// This call only modifies Steam's in-memory state and is very cheap.<br/>
    /// Doing so allows Steam to persist the changes even in the event of a game crash or unexpected shutdown.<br/>
    /// To submit the stats to the server you must call <see cref="StoreStats"/> .
    /// </para>
    /// <para>
    /// If this is returning <c>false</c> and everything appears correct, then check to ensure that your changes in the App Admin panel of the Steamworks website are published.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The new value of the stat. This must be an absolute value, it will not increment or decrement for you.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * The type passed to this function must match the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool SetStat(string name, int data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_SetStatInt32(this.ptr, name, data);
#endif
    }

    /// <summary>
    /// <para>
    /// Sets / updates the value of a given stat for the current user.
    /// </para>
    /// <para>
    /// This call only modifies Steam's in-memory state and is very cheap.<br/>
    /// Doing so allows Steam to persist the changes even in the event of a game crash or unexpected shutdown.<br/>
    /// To submit the stats to the server you must call <see cref="StoreStats"/> .
    /// </para>
    /// <para>
    /// If this is returning <c>false</c> and everything appears correct, then check to ensure that your changes in the App Admin panel of the Steamworks website are published.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The new value of the stat. This must be an absolute value, it will not increment or decrement for you.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * The type passed to this function must match the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool SetStat(string name, float data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_SetStatFloat(this.ptr, name, data);
#endif
    }

    /// <summary>
    /// <para>
    /// Updates an AVGRATE stat with new values.
    /// </para>
    /// <para>
    /// This call only modifies Steam's in-memory state and is very cheap.<br/>
    /// Doing so allows Steam to persist the changes even in the event of a game crash or unexpected shutdown.<br/>
    /// To submit the stats to the server you must call <see cref="StoreStats"/>.
    /// </para>
    /// <para>
    /// If this is returning <c>false</c> and everything appears correct, then check to ensure that your changes in the App Admin panel of the Steamworks website are published.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="countThisSession">The value accumulation since the last call to this function.</param>
    /// <param name="sessionLength">The amount of time in seconds since the last call to this function.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * The type must be AVGRATE in the Steamworks Partner backend.
    /// </returns>
    public bool UpdateAvgRateStat(string name, float countThisSession, double sessionLength)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_UpdateAvgRateStat(this.ptr, name, countThisSession, sessionLength);
#endif
    }

    // Achievement flag accessors

    /// <summary>
    /// <para>
    /// Gets the unlock status of the Achievement.
    /// </para>
    /// <para>
    /// The equivalent function for other users is <see cref="GetUserAchievement"/>.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <param name="achieved">Returns the unlock status of the achievement.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The 'API Name' of the specified achievement exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// If the call is successful then the unlock status is returned via the <paramref name="achieved"/> parameter.
    /// </returns>
    public bool GetAchievement(string name, out bool achieved)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetAchievement(this.ptr, name, out achieved);
#endif
    }

    /// <summary>
    /// <para>
    /// Unlocks an achievement.
    /// </para>
    /// <para>
    /// You can unlock an achievement multiple times so you don't need to worry about only setting achievements that aren't already set.<br/>
    /// This call only modifies Steam's in-memory state so it is quite cheap.<br/>
    /// To send the unlock status to the server and to trigger the Steam overlay notification you must call <see cref="StoreStats"/>.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the Achievement to unlock.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified achievement "API Name" exists in App Admin on the Steamworks website, and the changes are published.
    /// </returns>
    public bool SetAchievement(string name)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_SetAchievement(this.ptr, name);
#endif
    }

    /// <summary>
    /// <para>
    /// Resets the unlock status of an achievement.
    /// </para>
    /// <para>
    /// This is primarily only ever used for testing.
    /// </para>
    /// <para>
    /// This call only modifies Steam's in-memory state so it is quite cheap.<br/>
    /// To send the unlock status to the server and to trigger the Steam overlay notification you must call <see cref="StoreStats"/>.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the Achievement to unlock.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified achievement "API Name" exists in App Admin on the Steamworks website, and the changes are published.
    /// </returns>
    public bool ClearAchievement(string name)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_ClearAchievement(this.ptr, name);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the achievement status, and the time it was unlocked if unlocked.
    /// </para>
    /// <para>
    /// If the return value is true, but the unlock time is zero, that means it was unlocked before Steam began tracking achievement unlock times (December 2009).<br/>
    /// The time is provided in Unix epoch format, seconds since January 1, 1970 UTC.
    /// </para>
    /// <para>
    /// The equivalent function for other users is <see cref="GetUserAchievementAndUnlockTime"/>.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <param name="achieved">Returns whether the current user has unlocked the achievement.</param>
    /// <param name="unlockTime">Returns the time that the achievement was unlocked; if pbAchieved is true.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified achievement "API Name" exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// If the call is successful then the achieved status and unlock time are provided via the arguments <paramref name="achieved"/> and <paramref name="unlockTime"/>.
    /// </returns>
    public bool GetAchievementAndUnlockTime(string name, out bool achieved, out uint unlockTime)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime(this.ptr, name, out achieved, out unlockTime);
#endif
    }

    /// <summary>
    /// <para>
    /// Send the changed stats and achievements data to the server for permanent storage.
    /// </para>
    /// <para>
    /// If this fails then nothing is sent to the server. It's advisable to keep trying until the call is successful.
    /// </para>
    /// This call can be rate limited. Call frequency should be on the order of minutes, rather than seconds.<br/>
    /// You should only be calling this during major state changes such as the end of a round, the map changing, or the user leaving a server.<br/>
    /// This call is required to display the achievement unlock notification dialog though, so if you have called <see cref="SetAchievement"/> then it's advisable to call this soon after that.
    /// <para>
    /// If you have stats or achievements that you have saved locally but haven't uploaded with this function when your application process ends then this function will automatically be called.
    /// </para>
    /// <para>
    /// You can find additional debug information written to the <c>%steam_install%\logs\stats_log.txt</c> file.
    /// </para>
    /// </summary>
    /// <returns>
    /// <para>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The current game has stats associated with it in the Steamworks Partner backend, and those stats are published.<br/>
    /// If the call is successful you will receive a <see cref="UserStatsStored"/> callback.
    /// </para>
    /// <para>
    /// If <see cref="UserStatsStored_t.Result"/> has a result of <see cref="EResult.InvalidParam"/>, then one or more stats uploaded has been rejected,<br/>
    /// either because they broke constraints or were out of date.<br/>
    /// In this case the server sends back updated values and the stats should be updated locally to keep in sync.<br/>
    /// </para>
    /// <para>
    /// If one or more achievements has been unlocked then this will also trigger a <see cref="UserAchievementStored"/> callback.
    /// </para>
    /// </returns>
    public bool StoreStats()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_StoreStats(this.ptr);
#endif
    }

    // Achievement / GroupAchievement metadata

    /// <summary>
    /// Gets the icon for an achievement.
    /// </summary>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <returns>
    /// <para>
    /// Triggers a <see cref="UserAchievementIconFetched"/> callback.<br/>
    /// The image is returned as a handle to be used with <see cref="ISteamUtils.GetImageRGBA"/> to get the actual image data.
    /// </para>
    /// <para>
    /// <see cref="int"/> An invalid handle of <c>0</c> will be returned under the following conditions:<br/>
    /// * The specified achievement does not exist in App Admin on the Steamworks website, or the changes are not published.<br/>
    /// * Steam is still fetching the image data from the server. This will trigger a <see cref="UserAchievementIconFetched"/> callback which will notify you when the image data is ready and provide you with a new handle. If the <see cref="UserAchievementIconFetched_t.IconHandle"/> in the callback is still <c>0</c>, then there is no image set for the specified achievement.
    /// </para>
    /// </returns>
    public int GetAchievementIcon(string name)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetAchievementIcon(this.ptr, name);
#endif
    }

    /// <summary>
    /// <para>
    /// Get general attributes for an achievement. Currently provides: Name, Description, and Hidden status.
    /// </para>
    /// <para>
    /// This receives the value from a dictionary/map keyvalue store, so you must provide one of the following keys.<br/>
    /// * <c>"name"</c> to retrive the localized achievement name in UTF8<br/>
    /// * <c>"desc"</c> to retrive the localized achievement description in UTF8<br/>
    /// * <c>"hidden"</c> for retrieving if an achievement is hidden. Returns <c>"0"</c> when not hidden, <c>"1"</c> when hidden
    /// </para>
    /// <para>
    /// This localization is provided based on the games language if it's set, otherwise it checks if a localization is avilable for the users Steam UI Language.<br/>
    /// If that fails too, then it falls back to english.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <param name="key">The 'key' to get a value for.</param>
    /// <returns>
    /// <see cref="string"/> This function returns the value as a string upon success if all of the following conditions are met; otherwise, an empty string: <c>""</c>.<br/>
    /// * The specified achievement exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * The specified <paramref name="key"/> is valid.
    /// </returns>
    public string? GetAchievementDisplayAttribute(string name, string key)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute(this.ptr, name, key);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Shows the user a pop-up notification with the current progress of an achievement.
    /// </para>
    /// <para>
    /// Calling this function will NOT set the progress or unlock the achievement, the game must do that manually by calling <see cref="SetStat(string, int)"/>!
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <param name="curProgress">The current progress.</param>
    /// <param name="maxProgress">The progress required to unlock the achievement.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified achievement exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * The specified achievement is not already unlocked.<br/>
    /// * <paramref name="curProgress"/> is less than <paramref name="maxProgress"/>.<br/>
    /// Triggers a <see cref="UserStatsStored"/> callback.<br/>
    /// Triggers a <see cref="UserAchievementStored"/> callback.
    /// </returns>
    public bool IndicateAchievementProgress(string name, uint curProgress, uint maxProgress)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_IndicateAchievementProgress(this.ptr, name, curProgress, maxProgress);
#endif
    }

    /// <summary>
    /// <para>
    /// Get the number of achievements defined in the App Admin panel of the Steamworks website.
    /// </para>
    /// <para>
    /// This is used for iterating through all of the achievements with <see cref="GetAchievementName"/>.
    /// </para>
    /// <para>
    /// In general games should not need these functions because they should have a list of existing achievements compiled into them.
    /// </para>
    /// </summary>
    /// <returns>
    /// The number of achievements. Returns <c>0</c> if the current App ID has no achievements.
    /// </returns>
    public uint GetNumAchievements()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetNumAchievements(this.ptr);
#endif
    }

    // Get achievement name iAchievement in [0,GetNumAchievements)

    /// <summary>
    /// Gets the 'API name' for an achievement index between <c>0</c> and <see cref="GetNumAchievements"/>.
    /// </summary>
    /// <param name="achievementIndex">Index of the achievement.</param>
    /// <returns>
    /// <see cref="string"/> The 'API Name' of the achievement, returns an empty string if iAchievement is not a valid index.<br/>
    /// The current App ID must have achievements.
    /// </returns>
    public string? GetAchievementName(uint achievementIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamUserStats_GetAchievementName(this.ptr, achievementIndex);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    // Friends stats & achievements

    /// <summary>
    /// <para>
    /// Asynchronously downloads stats and achievements for the specified user from the server.
    /// </para>
    /// <para>
    /// These stats are not automatically updated; you'll need to call this function again to refresh any data that may have change.
    /// </para>
    /// <para>
    /// To keep from using too much memory, an least recently used cache (LRU) is maintained and other user's stats will occasionally be unloaded.<br/>
    /// When this happens a <see cref="UserStatsUnloaded"/> callback is sent.<br/>
    /// After receiving this callback the user's stats will be unavailable until this function is called again.
    /// </para>
    /// <para>
    /// The equivalent function for game servers is <see cref="ISteamGameServerStats.RequestUserStats"/>.
    /// </para>
    /// </summary>
    /// <param name="steamIDUser">The Steam ID of the user to request stats for.</param>
    /// <returns>
    /// <see cref="CallTask&lt;UserStatsReceived_t&gt;"/> that will return <see cref="UserStatsReceived_t"/> when awaited.<br/>
    /// If the other user has no stats, <see cref="UserStatsReceived_t.Result"/> will be set to <see cref="EResult.Fail"/>
    /// </returns>
    public CallTask<UserStatsReceived_t>? RequestUserStats(CSteamID steamIDUser)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, CSteamID, UserStatsReceived_t>(Native.SteamAPI_ISteamUserStats_RequestUserStats, this.ptr, steamIDUser);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the current value of the a stat for the specified user.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestUserStats"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// <para>
    /// The equivalent function for the local user is <see cref="GetStat(string, out int)"/>, the equivalent function for game servers is <see cref="ISteamGameServerStats.GetUserStat"/>.
    /// </para>
    /// </summary>
    /// <param name="steamIDUser">The Steam ID of the user to get the stat for.</param>
    /// <param name="name">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * <see cref="RequestUserStats"/> has completed and successfully returned its callback.<br/>
    /// * The type does not match the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool GetUserStat(CSteamID steamIDUser, string name, out int data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetUserStatInt32(this.ptr, steamIDUser, name, out data);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the current value of the a stat for the specified user.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestUserStats"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// <para>
    /// The equivalent function for the local user is <see cref="GetStat(string, out float)"/>, the equivalent function for game servers is <see cref="ISteamGameServerStats.GetUserStat"/>.
    /// </para>
    /// </summary>
    /// <param name="steamIDUser">The Steam ID of the user to get the stat for.</param>
    /// <param name="name">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * <see cref="RequestUserStats"/> has completed and successfully returned its callback.<br/>
    /// * The type does not match the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool GetUserStat(CSteamID steamIDUser, string name, out float data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetUserStatFloat(this.ptr, steamIDUser, name, out data);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the unlock status of the Achievement.
    /// </para>
    /// <para>
    /// The equivalent function for the local user is <see cref="GetAchievement"/>, the equivalent function for game servers is <see cref="ISteamGameServerStats.GetUserAchievement"/>.
    /// </para>
    /// </summary>
    /// <param name="steamIDUser">The Steam ID of the user to get the achievement for.</param>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <param name="achieved">Returns the unlock status of the achievement.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * <see cref="RequestUserStats"/> has completed and successfully returned its callback.<br/>
    /// * The 'API Name' of the specified achievement exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// If the call is successful then the unlock status is returned via the <paramref name="achieved"/> parameter.
    /// </returns>
    public bool GetUserAchievement(CSteamID steamIDUser, string name, out bool achieved)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetUserAchievement(this.ptr, steamIDUser, name, out achieved);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the achievement status, and the time it was unlocked if unlocked.
    /// </para>
    /// <para>
    /// If the return value is true, but the unlock time is zero, that means it was unlocked before Steam began tracking achievement unlock times (December 2009).<br/>
    /// The time is provided in Unix epoch format, seconds since January 1, 1970 UTC.
    /// </para>
    /// <para>
    /// The equivalent function for the local user is <see cref="GetAchievementAndUnlockTime"/>.
    /// </para>
    /// </summary>
    /// <param name="steamIDUser">The Steam ID of the user to get the achievement for.</param>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <param name="achieved">Returns whether the current user has unlocked the achievement.</param>
    /// <param name="unlockTime">Returns the time that the achievement was unlocked; if pbAchieved is true.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * <see cref="RequestUserStats"/> has completed and successfully returned its callback.<br/>
    /// * The 'API Name' of the specified achievement exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// If the call is successful then the achieved status and unlock time are provided via the arguments <paramref name="achieved"/> and <paramref name="unlockTime"/>.
    /// </returns>
    public bool GetUserAchievementAndUnlockTime(CSteamID steamIDUser, string name, out bool achieved, out uint unlockTime)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime(this.ptr, steamIDUser, name, out achieved, out unlockTime);
#endif
    }

    /// <summary>
    /// <para>
    /// Resets the current users stats and, optionally achievements.
    /// </para>
    /// <para>
    /// This automatically calls StoreStats to persist the changes to the server.<br/>
    /// This should typically only be used for testing purposes during development.
    /// </para>
    /// </summary>
    /// <param name="achievementsToo">Also reset the user's achievements?</param>
    /// <returns><see cref="bool"/> <c>true</c> indicating success if successfully returned its callback; otherwise <c>false</c>.</returns>
    public bool ResetAllStats(bool achievementsToo)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_ResetAllStats(this.ptr, achievementsToo);
#endif
    }

    // Leaderboard functions

    /// <summary>
    /// <para>
    /// Gets a leaderboard by name, it will create it if it's not yet created.
    /// </para>
    /// <para>
    /// You must call either this or <see cref="FindLeaderboard"/> to obtain the leaderboard handle which is valid for the game session for each leaderboard you wish to access prior to calling any other Leaderboard functions.
    /// </para>
    /// <para>
    /// Leaderboards created with this function will not automatically show up in the Steam Community.<br/>
    /// You must manually set the Community Name field in the App Admin panel of the Steamworks website.<br/>
    /// As such it's generally recommended to prefer creating the leaderboards in the App Admin panel on the Steamworks website and using <see cref="FindLeaderboard"/> unless you're expected to have a large amount of dynamically created leaderboards.
    /// </para>
    /// <para>
    /// You should never pass <see cref="ELeaderboardSortMethod.None"/> for <paramref name="leaderboardSortMethod"/> or <see cref="ELeaderboardDisplayType.None"/> for <paramref name="leaderboardDisplayType"/> as this is undefined behavior.
    /// </para>
    /// </summary>
    /// <param name="leaderboardName">The name of the leaderboard to find or create. Must not be longer than <see cref="LeaderboardNameMax"/>.</param>
    /// <param name="leaderboardSortMethod">The sort order of the new leaderboard if it's created.</param>
    /// <param name="leaderboardDisplayType">The display type (used by the Steam Community web site) of the new leaderboard if it's created.</param>
    /// <returns><see cref="CallTask&lt;LeaderboardFindResult_t&gt;"/> that will return <see cref="LeaderboardFindResult_t"/> when awaited.</returns>
    public CallTask<LeaderboardFindResult_t>? FindOrCreateLeaderboard(string leaderboardName, ELeaderboardSortMethod leaderboardSortMethod, ELeaderboardDisplayType leaderboardDisplayType)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, string, ELeaderboardSortMethod, ELeaderboardDisplayType, LeaderboardFindResult_t>(Native.SteamAPI_ISteamUserStats_FindOrCreateLeaderboard, this.ptr, leaderboardName, leaderboardSortMethod, leaderboardDisplayType);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets a leaderboard by name.<br/>
    /// This won't create the leaderboard if it's not found.
    /// </para>
    /// <para>
    /// You must call either this or <see cref="FindOrCreateLeaderboard"/> to obtain the leaderboard handle which is valid for the game session for each leaderboard you wish to access prior to calling any other Leaderboard functions.
    /// </para>
    /// </summary>
    /// <param name="leaderboardName">The name of the leaderboard to find. Must not be longer than <see cref="LeaderboardNameMax"/>.</param>
    /// <returns><see cref="CallTask&lt;LeaderboardFindResult_t&gt;"/> that will return <see cref="LeaderboardFindResult_t"/> when awaited.</returns>
    public CallTask<LeaderboardFindResult_t>? FindLeaderboard(string leaderboardName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, string, LeaderboardFindResult_t>(Native.SteamAPI_ISteamUserStats_FindLeaderboard, this.ptr, leaderboardName);

        return task;
#endif
    }

    /// <summary>
    /// Returns the name of a leaderboard handle.
    /// </summary>
    /// <param name="steamLeaderboard">A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <returns><see cref="string"/> The name of the leaderboard. Returns an empty string if the leaderboard handle is invalid.</returns>
    public string? GetLeaderboardName(SteamLeaderboard_t steamLeaderboard)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamUserStats_GetLeaderboardName(this.ptr, steamLeaderboard);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Returns the total number of entries in a leaderboard.
    /// </para>
    /// <para>
    /// This is cached on a per leaderboard basis upon the first call to <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/> and is refreshed on each successful call to <see cref="DownloadLeaderboardEntries"/>, <see cref="DownloadLeaderboardEntriesForUsers"/>, and <see cref="UploadLeaderboardScore"/>.
    /// </para>
    /// </summary>
    /// <param name="steamLeaderboard">A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <returns><see cref="int"/> The number of entries in the leaderboard. Returns <c>0</c> if the leaderboard handle is invalid.</returns>
    public int GetLeaderboardEntryCount(SteamLeaderboard_t steamLeaderboard)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetLeaderboardEntryCount(this.ptr, steamLeaderboard);
#endif
    }

    /// <summary>
    /// Returns the sort order of a leaderboard handle.
    /// </summary>
    /// <param name="steamLeaderboard">>A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <returns><see cref="ELeaderboardSortMethod"/> The sort method of the leaderboard. Returns <see cref="ELeaderboardSortMethod.None"/> if the leaderboard handle is invalid.</returns>
    public ELeaderboardSortMethod GetLeaderboardSortMethod(SteamLeaderboard_t steamLeaderboard)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetLeaderboardSortMethod(this.ptr, steamLeaderboard);
#endif
    }

    /// <summary>
    /// Returns the display type of a leaderboard handle.
    /// </summary>
    /// <param name="steamLeaderboard">A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <returns>The display type of the leaderboard. Returns <see cref="ELeaderboardDisplayType.None"/> if the leaderboard handle is invalid.</returns>
    public ELeaderboardDisplayType GetLeaderboardDisplayType(SteamLeaderboard_t steamLeaderboard)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetLeaderboardDisplayType(this.ptr, steamLeaderboard);
#endif
    }

    /// <summary>
    /// <para>
    /// Fetches a series of leaderboard entries for a specified leaderboard.
    /// </para>
    /// <para>
    /// You can ask for more entries than exist, then this will return as many as do exist.
    /// </para>
    /// <para>
    /// If you want to download entries for an arbitrary set of users, such as all of the users on a server then you can use <see cref="DownloadLeaderboardEntriesForUsers"/> which takes an array of Steam IDs.
    /// </para>
    /// <para>
    /// You must call <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/> to get a <see cref="SteamLeaderboard_t"/> prior to calling this function.
    /// </para>
    /// <para>
    /// <see cref="ELeaderboardDataRequest.Global"/> requests rows in the leaderboard from the full table, with <paramref name="rangeStart"/> &amp; <paramref name="rangeEnd"/> in the range [1, TotalEntries]<br/>
    /// <see cref="ELeaderboardDataRequest.GlobalAroundUser"/> requests rows around the current user, <paramref name="rangeStart"/> being negate<br/>
    /// e.g. <c>DownloadLeaderboardEntries( hLeaderboard, k_ELeaderboardDataRequestGlobalAroundUser, -3, 3 )</c> will return 7 rows, 3 before the user, 3 after<br/>
    /// <see cref="ELeaderboardDataRequest.Friends"/> requests all the rows for friends of the current user
    /// </para>
    /// </summary>
    /// <param name="steamLeaderboard">A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <param name="leaderboardDataRequest">The type of data request to make.</param>
    /// <param name="rangeStart">The index to start downloading entries relative to <paramref name="leaderboardDataRequest"/>.</param>
    /// <param name="rangeEnd">The last index to retrieve entries for relative to <paramref name="leaderboardDataRequest"/>.</param>
    /// <returns>
    /// <see cref="CallTask&lt;LeaderboardScoresDownloaded_t&gt;"/> that will return <see cref="LeaderboardScoresDownloaded_t"/> when awaited.<br/>
    /// <see cref="LeaderboardScoresDownloaded_t"/> will contain a handle to pull the results from <see cref="GetDownloadedLeaderboardEntry"/>
    /// </returns>
    public CallTask<LeaderboardScoresDownloaded_t>? DownloadLeaderboardEntries(SteamLeaderboard_t steamLeaderboard, ELeaderboardDataRequest leaderboardDataRequest, int rangeStart, int rangeEnd)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, SteamLeaderboard_t, ELeaderboardDataRequest, int, int, LeaderboardScoresDownloaded_t>(Native.SteamAPI_ISteamUserStats_DownloadLeaderboardEntries, this.ptr, steamLeaderboard, leaderboardDataRequest, rangeStart, rangeEnd);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Fetches leaderboard entries for an arbitrary set of users on a specified leaderboard.
    /// </para>
    /// <para>
    /// A maximum of 100 users can be downloaded at a time, with only one outstanding call at a time.<br/>
    /// If a user doesn't have an entry on the specified leaderboard, they won't be included in the result.
    /// </para>
    /// <para>
    /// If you want to download entries based on their ranking or friends of the current user then you should use <see cref="DownloadLeaderboardEntries"/>.
    /// </para>
    /// <para>
    /// You must call <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/> to get a <see cref="SteamLeaderboard_t"/> prior to calling this function.
    /// </para>
    /// </summary>
    /// <param name="steamLeaderboard">A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <param name="users">An array of Steam IDs to get the leaderboard entries for.</param>
    /// <returns>
    /// <br/>
    /// <see cref="CallTask&lt;LeaderboardScoresDownloaded_t&gt;"/> that will return <see cref="LeaderboardScoresDownloaded_t"/> when awaited.<br/>
    /// Returns <c>null</c> indicating an error if the number of users is greater than 100 or if one of the Steam IDs is invalid.
    /// </returns>
    public CallTask<LeaderboardScoresDownloaded_t>? DownloadLeaderboardEntriesForUsers(SteamLeaderboard_t steamLeaderboard, Span<CSteamID> users)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, SteamLeaderboard_t, Span<CSteamID>, int, LeaderboardScoresDownloaded_t>(Native.SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers, this.ptr, steamLeaderboard, users, users.Length);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Retrieves the data for a single leaderboard entry.
    /// </para>
    /// <para>
    /// You should use a for loop from <c>0</c> to <see cref="LeaderboardScoresDownloaded_t.EntryCount"/> to get all the downloaded entries.<br/>
    /// Once you've accessed all the entries, the data will be freed, and the <see cref="SteamLeaderboardEntries_t"/> handle will become invalid.
    /// </para>
    /// <para>
    /// Optionally details may be returned for the entry via the <paramref name="details"/>.
    /// </para>
    /// <code>
    /// void OnLeaderboardScoresDownloaded( LeaderboardScoresDownloaded_t *pCallback )
    /// {
    ///     for ( int i = 0; i &lt; pCallback-&gt;m_cEntryCount; i++ )
    ///     {
    ///         LeaderboardEntry_t leaderboardEntry;
    ///         int32 details[3]; // We know that we store this many initially.
    ///         SteamUserStats()->GetDownloadedLeaderboardEntry( pCallback->m_hSteamLeaderboardEntries, i, &amp;leaderboardEntry, details, 3 );
    ///         assert( leaderboardEntry.m_cDetails == 3 );
    ///         //...
    ///     }
    /// }
    /// </code>
    /// </summary>
    /// <param name="steamLeaderboardEntries">A leaderboard entries handle obtained from the most recently received <see cref="LeaderboardScoresDownloaded_t"/> call result.</param>
    /// <param name="index">The index of the leaderboard entry to receive, must be between <c>0</c> and <see cref="LeaderboardScoresDownloaded_t.EntryCount"/>.</param>
    /// <param name="leaderboardEntry">Variable where the entry will be returned to.</param>
    /// <param name="details">A preallocated array where the details of this entry get returned into.</param>
    /// <returns>
    /// <para>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// <paramref name="steamLeaderboardEntries"/> must be a valid handle from the last received <see cref="LeaderboardScoresDownloaded_t"/> call result.<br/>
    /// <paramref name="index"/> must be between <c>0</c> and <see cref="LeaderboardScoresDownloaded_t.EntryCount"/>
    /// </para>
    /// <para>
    /// If the call is successful then the entry is returned via the parameter <paramref name="leaderboardEntry"/> and <paramref name="details"/> is filled with the unlock details.
    /// </para>
    /// </returns>
    public bool GetDownloadedLeaderboardEntry(SteamLeaderboardEntries_t steamLeaderboardEntries, int index, out LeaderboardEntry_t leaderboardEntry, Span<int> details)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry(this.ptr, steamLeaderboardEntries, index, out leaderboardEntry, details, details.Length);
#endif
    }

    /// <summary>
    /// <para>
    /// Uploads a user score to a specified leaderboard.
    /// </para>
    /// <para>
    /// Details are optional game-defined information which outlines how the user got that score.<br/>
    /// For example if it's a racing style time based leaderboard you could store the timestamps when the player hits each checkpoint.<br/>
    /// If you have collectibles along the way you could use bit fields as booleans to store the items the player picked up in the playthrough.
    /// </para>
    /// <para>
    /// Uploading scores to Steam is rate limited to 10 uploads per 10 minutes and you may only have one outstanding call to this function at a time.
    /// </para>
    /// </summary>
    /// <param name="steamLeaderboard">A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <param name="leaderboardUploadScoreMethod">Do you want to force the score to change, or keep the previous score if it was better?</param>
    /// <param name="score">The score to upload.</param>
    /// <param name="scoreDetails">Optional: Array containing the details surrounding the unlocking of this score. The number of elements must not exceed <see cref="LeaderboardDetailsMax"/></param>
    /// <returns><see cref="CallTask&lt;LeaderboardScoreUploaded_t&gt;"/> that will return <see cref="LeaderboardScoreUploaded_t"/> when awaited.</returns>
    public CallTask<LeaderboardScoreUploaded_t>? UploadLeaderboardScore(SteamLeaderboard_t steamLeaderboard, ELeaderboardUploadScoreMethod leaderboardUploadScoreMethod, int score, ReadOnlySpan<int> scoreDetails)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, SteamLeaderboard_t, ELeaderboardUploadScoreMethod, int, ReadOnlySpan<int>, int, LeaderboardScoreUploaded_t>(Native.SteamAPI_ISteamUserStats_UploadLeaderboardScore, this.ptr, steamLeaderboard, leaderboardUploadScoreMethod, score, scoreDetails, scoreDetails.Length);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Attaches a piece of user generated content the current user's entry on a leaderboard.
    /// </para>
    /// <para>
    /// This content could be a replay of the user achieving the score or a ghost to race against.<br/>
    /// The attached handle will be available when the entry is retrieved and can be accessed by other users using <see cref="GetDownloadedLeaderboardEntry"/> which contains <see cref="LeaderboardEntry_t.UGC"/>.<br/>
    /// To create and download user generated content see the documentation for the Steam Workshop.
    /// </para>
    /// <para>
    /// Once attached, the content will be available even if the underlying Cloud file is changed or deleted by the user.
    /// </para>
    /// <para>
    /// You must call <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/> to get a <see cref="SteamLeaderboard_t"/> prior to calling this function.
    /// </para>
    /// </summary>
    /// <param name="steamLeaderboard">A leaderboard handle obtained from <see cref="FindLeaderboard"/> or <see cref="FindOrCreateLeaderboard"/>.</param>
    /// <param name="ugc">Handle to a piece of user generated content that was shared using <see cref="ISteamRemoteStorage.FileShare"/>.</param>
    /// <returns><see cref="CallTask&lt;LeaderboardUGCSet_t&gt;"/> that will return <see cref="LeaderboardUGCSet_t"/> when awaited.</returns>
    public CallTask<LeaderboardUGCSet_t>? AttachLeaderboardUGC(SteamLeaderboard_t steamLeaderboard, UGCHandle_t ugc)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, SteamLeaderboard_t, UGCHandle_t, LeaderboardUGCSet_t>(Native.SteamAPI_ISteamUserStats_AttachLeaderboardUGC, this.ptr, steamLeaderboard, ugc);

        return task;
#endif
    }

    /// <summary>
    /// Asynchronously retrieves the total number of players currently playing the current game.<br/>
    /// Both online and in offline mode.
    /// </summary>
    /// <returns><see cref="CallTask&lt;NumberOfCurrentPlayers_t&gt;"/> that will return <see cref="NumberOfCurrentPlayers_t"/> when awaited.</returns>
    public CallTask<NumberOfCurrentPlayers_t>? GetNumberOfCurrentPlayers()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, NumberOfCurrentPlayers_t>(Native.SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers, this.ptr);

        return task;
#endif
    }

    /// <summary>
    /// Asynchronously fetch the data for the percentage of players who have received each achievement for the current game globally.
    /// </summary>
    /// <returns><see cref="CallTask&lt;GlobalAchievementPercentagesReady_t&gt;"/> that will return <see cref="GlobalAchievementPercentagesReady_t"/> when awaited.</returns>
    public CallTask<GlobalAchievementPercentagesReady_t>? RequestGlobalAchievementPercentages()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, GlobalAchievementPercentagesReady_t>(Native.SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages, this.ptr);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the info on the most achieved achievement for the game.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestGlobalAchievementPercentages"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// </summary>
    /// <param name="name">String to return the 'API Name' of the achievement into.</param>
    /// <param name="nameBufLen">The UTF-8 size in bytes of <paramref name="name"/>, should be at least as long as your longest achievement 'API Name'.</param>
    /// <param name="percent">Variable to return the percentage of people that have unlocked this achievement from <c>0</c> to <c>100</c>.</param>
    /// <param name="achieved">Variable to return whether the current user has unlocked this achievement.</param>
    /// <returns>
    /// <para>
    /// <see cref="int"/> Returns <c>-1</c> if <see cref="RequestGlobalAchievementPercentages"/> has not been called or if there are no global achievement percentages for this app Id.
    /// </para>
    /// <para>
    /// If the call is successful it returns an iterator which should be used with <see cref="GetNextMostAchievedAchievementInfo"/>.
    /// </para>
    /// </returns>
    public int GetMostAchievedAchievementInfo(out string? name, uint nameBufLen, out float percent, out bool achieved)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        Span<byte> raw = stackalloc byte[(int)nameBufLen];

        int result = Native.SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo(this.ptr, raw, (uint)raw.Length, out percent, out achieved);

        if (result != -1)
        {
            name = Utf8StringHelper.NullTerminatedSpanToString(raw);
        }
        else
        {
            name = null;
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the info on the next most achieved achievement for the game.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestGlobalAchievementPercentages"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// </summary>
    /// <param name="iteratorPrevious">Iterator returned from the previous call to this function or from <see cref="GetMostAchievedAchievementInfo"/></param>
    /// <param name="name">String buffer to return the 'API Name' of the achievement into.</param>
    /// <param name="nameBufLen">The UTF-8 size in bytes of <paramref name="name"/>, should be at least as long as your longest achievement 'API Name'.</param>
    /// <param name="percent">Variable to return the percentage of people that have unlocked this achievement from <c>0</c> to <c>100</c>.</param>
    /// <param name="achieved">Variable to return whether the current user has unlocked this achievement.</param>
    /// <returns>
    /// <para>
    /// <see cref="int"/> Returns <c>-1</c> if one of the following conditions are met:<br/>
    /// * <see cref="RequestGlobalAchievementPercentages"/> has not been called<br/>
    /// * After the last achievement has been iterated<br/>
    /// * There are no global achievement percentages for this app Id.
    /// </para>
    /// <para>
    /// If the call is successful it returns an iterator which should be used with subsequent calls to this function.
    /// </para>
    /// </returns>
    public int GetNextMostAchievedAchievementInfo(int iteratorPrevious, out string? name, uint nameBufLen, out float percent, out bool achieved)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK

        Span<byte> raw = stackalloc byte[(int)nameBufLen];

        int result = Native.SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo(this.ptr, iteratorPrevious, raw, (uint)raw.Length, out percent, out achieved);

        if (result != -1)
        {
            name = Utf8StringHelper.NullTerminatedSpanToString(raw);
        }
        else
        {
            name = null;
        }

        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Returns the percentage of users who have unlocked the specified achievement.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestGlobalAchievementPercentages"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// </summary>
    /// <param name="name">The 'API Name' of the achievement.</param>
    /// <param name="percent">Variable to return the percentage of people that have unlocked this achievement from <c>0</c> to <c>100</c>.</param>
    /// <returns><see cref="bool"/> Returns <c>true</c> upon success; otherwise <c>false</c> if <see cref="RequestGlobalAchievementPercentages"/> has not been called or if the specified 'API Name' does not exist in the global achievement percentages.</returns>
    public bool GetAchievementAchievedPercent(string name, out float percent)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetAchievementAchievedPercent(this.ptr, name, out percent);
#endif
    }

    /// <summary>
    /// Asynchronously fetches global stats data, which is available for stats marked as "aggregated" in the App Admin panel of the Steamworks website.
    /// </summary>
    /// <param name="historyDays">How many days of day-by-day history to retrieve in addition to the overall totals. The limit is 60.</param>
    /// <returns><see cref="CallTask&lt;GlobalStatsReceived_t&gt;"/> that will return <see cref="GlobalStatsReceived_t"/> when awaited.</returns>
    public CallTask<GlobalStatsReceived_t>? RequestGlobalStats(int historyDays)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, int, GlobalStatsReceived_t>(Native.SteamAPI_ISteamUserStats_RequestGlobalStats, this.ptr, historyDays);

        return task;
#endif
    }

    // Gets the lifetime totals for an aggregated stat

    /// <summary>
    /// <para>
    /// Gets the lifetime totals for an aggregated stat.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestGlobalAchievementPercentages"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// </summary>
    /// <param name="statName">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * <see cref="RequestGlobalStats"/> has completed and successfully returned its callback.<br/>
    /// * The type matches the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool GetGlobalStat(string statName, out long data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetGlobalStatInt64(this.ptr, statName, out data);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the lifetime totals for an aggregated stat.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestGlobalAchievementPercentages"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// </summary>
    /// <param name="statName">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <see cref="bool"/> This function returns <c>true</c> upon success if all of the following conditions are met; otherwise, <c>false</c>.<br/>
    /// * The specified stat exists in App Admin on the Steamworks website, and the changes are published.<br/>
    /// * <see cref="RequestGlobalStats"/> has completed and successfully returned its callback.<br/>
    /// * The type matches the type listed in the App Admin panel of the Steamworks website.
    /// </returns>
    public bool GetGlobalStat(string statName, out double data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetGlobalStatDouble(this.ptr, statName, out data);
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the lifetime totals for an aggregated stat.<br/>
    /// <paramref name="data"/> will be filled with daily values, starting with today.<br/>
    /// So when called, <c>data[0]</c> will be today, <c>data[1]</c> will be yesterday, and <c>data[2]</c> will be two days ago, etc.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestGlobalStats"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// </summary>
    /// <param name="statName">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <para>
    /// <see cref="int"/> The number of elements returned in the <paramref name="data"/> array.
    /// </para>
    /// <para>
    /// A value of <c>0</c> indicates failure for one of the following reasons:<br/>
    /// * The specified stat does not exist in App Admin on the Steamworks website, or the changes aren't published.<br/>
    /// * RequestGlobalStats has not been called or returned its callback, with at least 1 day of history.<br/>
    /// * The type does not match the type listed in the App Admin panel of the Steamworks website.<br/>
    /// * There is no history available.
    /// </para>
    /// </returns>
    public int GetGlobalStatHistory(string statName, Span<long> data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetGlobalStatHistoryInt64(this.ptr, statName, data, (uint)data.Length * sizeof(long));
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the lifetime totals for an aggregated stat.<br/>
    /// <paramref name="data"/> will be filled with daily values, starting with today.<br/>
    /// So when called, <c>data[0]</c> will be today, <c>data[1]</c> will be yesterday, and <c>data[2]</c> will be two days ago, etc.
    /// </para>
    /// <para>
    /// You must have called <see cref="RequestGlobalStats"/> and it needs to return successfully via its callback prior to calling this.
    /// </para>
    /// </summary>
    /// <param name="statName">The 'API Name' of the stat. Must not be longer than <see cref="StatNameMax"/>.</param>
    /// <param name="data">The variable to return the stat value into.</param>
    /// <returns>
    /// <para>
    /// <see cref="int"/> The number of elements returned in the <paramref name="data"/> array.
    /// </para>
    /// <para>
    /// A value of <c>0</c> indicates failure for one of the following reasons:<br/>
    /// * The specified stat does not exist in App Admin on the Steamworks website, or the changes aren't published.<br/>
    /// * RequestGlobalStats has not been called or returned its callback, with at least 1 day of history.<br/>
    /// * The type does not match the type listed in the App Admin panel of the Steamworks website.<br/>
    /// * There is no history available.
    /// </para>
    /// </returns>
    public int GetGlobalStatHistory(string statName, Span<double> data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetGlobalStatHistoryDouble(this.ptr, statName, data, (uint)data.Length * sizeof(double));
#endif
    }

    /// <summary>
    /// For achievements that have related Progress stats, use this to query what the bounds of that progress are.<br/>
    /// You may want this info to selectively call IndicateAchievementProgress when appropriate milestones of progress<br/>
    /// have been made, to show a progress notification to the user.
    /// </summary>
    public bool GetAchievementProgressLimits(string name, out int minProgress, out int maxProgress)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetAchievementProgressLimitsInt32(this.ptr, name, out minProgress, out maxProgress);
#endif
    }

    /// <summary>
    /// For achievements that have related Progress stats, use this to query what the bounds of that progress are.<br/>
    /// You may want this info to selectively call IndicateAchievementProgress when appropriate milestones of progress<br/>
    /// have been made, to show a progress notification to the user.
    /// </summary>
    public bool GetAchievementProgressLimits(string name, out float minProgress, out float maxProgress)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUserStats");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUserStats_GetAchievementProgressLimitsFloat(this.ptr, name, out minProgress, out maxProgress);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case GlobalAchievementPercentagesReady_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GlobalAchievementPercentagesReady_t>();
                    this.GlobalAchievementPercentagesReady?.Invoke(ref data);
                    break;
                }

            case GlobalStatsReceived_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GlobalStatsReceived_t>();
                    this.GlobalStatsReceived?.Invoke(ref data);
                    break;
                }

            case LeaderboardFindResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LeaderboardFindResult_t>();
                    this.LeaderboardFindResult?.Invoke(ref data);
                    break;
                }

            case LeaderboardScoresDownloaded_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LeaderboardScoresDownloaded_t>();
                    this.LeaderboardScoresDownloaded?.Invoke(ref data);
                    break;
                }

            case LeaderboardScoreUploaded_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LeaderboardScoreUploaded_t>();
                    this.LeaderboardScoreUploaded?.Invoke(ref data);
                    break;
                }

            case LeaderboardUGCSet_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LeaderboardUGCSet_t>();
                    this.LeaderboardUGCSet?.Invoke(ref data);
                    break;
                }

            case NumberOfCurrentPlayers_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<NumberOfCurrentPlayers_t>();
                    this.NumberOfCurrentPlayers?.Invoke(ref data);
                    break;
                }

            case UserAchievementIconFetched_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<UserAchievementIconFetched_t>();
                    this.UserAchievementIconFetched?.Invoke(ref data);
                    break;
                }

            case UserAchievementStored_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<UserAchievementStored_t>();
                    this.UserAchievementStored?.Invoke(ref data);
                    break;
                }

            case UserStatsReceived_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<UserStatsReceived_t>();
                    this.UserStatsReceived?.Invoke(ref data);
                    break;
                }

            case UserStatsStored_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<UserStatsStored_t>();
                    this.UserStatsStored?.Invoke(ref data);
                    break;
                }

            case UserStatsUnloaded_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<UserStatsUnloaded_t>();
                    this.UserStatsUnloaded?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamUserStats.OnDispatch()");
                break;
        }
    }
#endif
}
