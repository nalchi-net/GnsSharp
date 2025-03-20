// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Diagnostics;

public static class GnsSharpCore
{
#if GNS_SHARP_OPENSOURCE_GNS
    public const BackendKind Backend = BackendKind.OpenSource;
#elif GNS_SHARP_STEAMWORKS_SDK
    public const BackendKind Backend = BackendKind.Steamworks;
#else
    public const BackendKind Backend = BackendKind.None;
#endif

    public enum BackendKind
    {
        None,

        /// <summary>
        /// Using stand-alone open source GameNetworkingSockets as a backend.
        /// </summary>
        OpenSource,

        /// <summary>
        /// Using Steamworks SDK version of GNS as a backend.
        /// </summary>
        Steamworks,
    }

    internal static void Init(bool isGameServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Debug.Assert(!isGameServer, "Open source GNS doesn't have GameServer API");
#endif

        if (isGameServer)
        {
            ISteamNetworkingSockets.GameServer = new(isGameServer);
            ISteamNetworkingUtils.GameServer = new();

#if GNS_SHARP_STEAMWORKS_SDK
            ISteamUtils.GameServer = new(isGameServer);
#endif
        }
        else
        {
            ISteamNetworkingSockets.User = new(isGameServer);
            ISteamNetworkingUtils.User = new();
            ISteamMatchmaking.User = new();
            ISteamFriends.User = new();

#if GNS_SHARP_STEAMWORKS_SDK
            ISteamUtils.User = new(isGameServer);
#endif
        }
    }

    internal static void Shutdown(bool isGameServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Debug.Assert(!isGameServer, "Open source GNS doesn't have GameServer API");
#endif

        if (isGameServer)
        {
            ISteamNetworkingSockets.GameServer = null;
            ISteamNetworkingUtils.GameServer = null;

#if GNS_SHARP_STEAMWORKS_SDK
            ISteamUtils.GameServer = null;
#endif
        }
        else
        {
            ISteamNetworkingSockets.User = null;
            ISteamNetworkingUtils.User = null;
            ISteamMatchmaking.User = null;
            ISteamFriends.User = null;

#if GNS_SHARP_STEAMWORKS_SDK
            ISteamUtils.User = null;
#endif
        }
    }
}
