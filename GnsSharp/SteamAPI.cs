// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Text;

/// <summary>
/// <para>
/// Steam API setup and shutdown
/// </para>
///
/// <para>
/// These functions manage loading, initializing and shutdown of the steamclient.dll
/// </para>
/// </summary>
public static class SteamAPI
{
    // Initializing the Steamworks SDK
    // -----------------------------
    //
    // There are three different methods you can use to initialize the Steamworks SDK, depending on
    // your project's environment. You should only use one method in your project.
    //
    // If you are able to include this C++ header in your project, we recommend using the following
    // initialization methods. They will ensure that all ISteam* interfaces defined in other
    // C++ header files have versions that are supported by the user's Steam Client:
    // - SteamAPI_InitEx() for new projects so you can show a detailed error message to the user
    // - SteamAPI_Init() for existing projects that only display a generic error message
    //
    // If you are unable to include this C++ header in your project and are dynamically loading
    // Steamworks SDK methods from dll/so, you can use the following method:
    // - SteamAPI_InitFlat()

    /// <summary>
    /// <para>
    /// See "Initializing the Steamworks SDK" above for how to choose an init method.<br/>
    /// On success k_ESteamAPIInitResult_OK is returned. Otherwise, returns a value that can be used<br/>
    /// to create a localized error message for the user. If pOutErrMsg is non-NULL,<br/>
    /// it will receive an example error message, in English, that explains the reason for the failure.
    /// </para>
    ///
    /// <para>
    /// Example usage:
    /// <code>
    ///   SteamErrMsg errMsg;
    ///   if ( SteamAPI_Init(&amp;errMsg) != k_ESteamAPIInitResult_OK )
    ///       FatalError( "Failed to init Steam.  %s", errMsg );
    /// </code>
    /// </para>
    /// </summary>
    public static ESteamAPIInitResult InitEx(out string? outErrMsg)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("For open source stand-alone GNS, use GameNetworkingSockets.Init() instead");
#elif GNS_SHARP_STEAMWORKS_SDK

        int capacity =
              Constants.SteamNetworkingSocketsInterfaceVersion.Length + 1
            + Constants.SteamNetworkingUtilsInterfaceVersion.Length + 1
            + Constants.SteamUtilsInterfaceVersion.Length + 1
            + Constants.SteamMatchmakingInterfaceVersion.Length + 1

            // + Constants.SteamAppsInterfaceVersion.Length + 1
            // + Constants.SteamControllerInterfaceVersion.Length + 1
            // + Constants.SteamFriendsInterfaceVersion.Length + 1
            // + Constants.SteamGameSearchInterfaceVersion.Length + 1
            // + Constants.SteamHtmlSurfaceInterfaceVersion.Length + 1
            // + Constants.SteamHttpInterfaceVersion.Length + 1
            // + Constants.SteamInputInterfaceVersion.Length + 1
            // + Constants.SteamInventoryInterfaceVersion.Length + 1
            // + Constants.SteamMatchmakingServersInterfaceVersion.Length + 1
            // + Constants.SteamMusicRemoteInterfaceVersion.Length + 1
            // + Constants.SteamMusicInterfaceVersion.Length + 1
            // + Constants.SteamNetworkingMessagesInterfaceVersion.Length + 1
            // + Constants.SteamNetworkingInterfaceVersion.Length + 1
            // + Constants.SteamParentalSettingsInterfaceVersion.Length + 1
            // + Constants.SteamPartiesInterfaceVersion.Length + 1
            // + Constants.SteamRemotePlayInterfaceVersion.Length + 1
            // + Constants.SteamRemoteStorageInterfaceVersion.Length + 1
            // + Constants.SteamScreenshotsInterfaceVersion.Length + 1
            // + Constants.SteamUgcInterfaceVersion.Length + 1
            // + Constants.SteamUserStatsInterfaceVersion.Length + 1
            // + Constants.SteamUserInterfaceVersion.Length + 1
            // + Constants.SteamVideoInterfaceVersion.Length + 1
            + 1;

        StringBuilder versions = new(capacity);

        versions.Append(Constants.SteamNetworkingSocketsInterfaceVersion).Append('\0');
        versions.Append(Constants.SteamNetworkingUtilsInterfaceVersion).Append('\0');
        versions.Append(Constants.SteamUtilsInterfaceVersion).Append('\0');
        versions.Append(Constants.SteamMatchmakingInterfaceVersion).Append('\0');

        // versions.Append(Constants.SteamAppsInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamControllerInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamFriendsInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamGameSearchInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamHtmlSurfaceInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamHttpInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamInputInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamInventoryInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamMatchmakingServersInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamMusicRemoteInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamMusicInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamNetworkingMessagesInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamNetworkingInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamParentalSettingsInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamPartiesInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamRemotePlayInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamRemoteStorageInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamScreenshotsInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamUgcInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamUserStatsInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamUserInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamVideoInterfaceVersion).Append('\0');
        versions.Append('\0');

        ESteamAPIInitResult result = Native.SteamInternal_SteamAPI_Init(versions.ToString(), out SteamErrMsg msg);

        if (result != ESteamAPIInitResult.OK)
        {
            ReadOnlySpan<byte> span = msg;
            outErrMsg = Utf8StringHelper.NullTerminatedSpanToString(span);
        }
        else
        {
            outErrMsg = null;

            GnsSharpCore.Init(isGameServer: false);

            Native.SteamAPI_ManualDispatch_Init();
        }

        return result;
#endif
    }

    /// <summary>
    /// See "Initializing the Steamworks SDK" above for how to choose an init method.<br/>
    /// Returns true on success
    /// </summary>
    public static bool Init()
    {
        return InitEx(out _) == ESteamAPIInitResult.OK;
    }

    /// <summary>
    /// SteamAPI_Shutdown should be called during process shutdown if possible.
    /// </summary>
    public static void Shutdown()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("For open source stand-alone GNS, use GameNetworkingSockets.Kill() instead");
#elif GNS_SHARP_STEAMWORKS_SDK
        GnsSharpCore.Shutdown(isGameServer: false);
        Native.SteamAPI_Shutdown();
#endif
    }

    /// <summary>
    /// <para>
    /// SteamAPI_RestartAppIfNecessary ensures that your executable was launched through Steam.
    /// </para>
    ///
    /// <para>
    /// Returns true if the current process should terminate. Steam is now re-launching your application.
    /// </para>
    ///
    /// <para>
    /// Returns false if no action needs to be taken. This means that your executable was started through<br/>
    /// the Steam client, or a steam_appid.txt file is present in your game's directory (for development).<br/>
    /// Your current process should continue if false is returned.
    /// </para>
    ///
    /// <para>
    /// NOTE: If you use the Steam DRM wrapper on your primary executable file, this check is unnecessary<br/>
    /// since the DRM wrapper will ensure that your application was launched properly through Steam.
    /// </para>
    /// </summary>
    public static bool RestartAppIfNecessary(uint ownAppID)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have RestartAppIfNecessary()");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_RestartAppIfNecessary(ownAppID);
#endif
    }

    /// <summary>
    /// Many Steam API functions allocate a small amount of thread-local memory for parameter storage.<br/>
    /// SteamAPI_ReleaseCurrentThreadMemory() will free API memory associated with the calling thread.<br/>
    /// This function is also called automatically by SteamAPI_RunCallbacks(), so a single-threaded<br/>
    /// program never needs to explicitly call this function.
    /// </summary>
    public static void ReleaseCurrentThreadMemory()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ReleaseCurrentThreadMemory()");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ReleaseCurrentThreadMemory();
#endif
    }

    public static void RunCallbacks()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("For open source stand-alone GNS, use ISteamNetworkingSockets.User.RunCallbacks() instead");
#elif GNS_SHARP_STEAMWORKS_SDK
        Dispatcher.RunCallbacks(isGameServer: false);
#endif
    }
}
