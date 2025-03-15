// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

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

    public static void SetupInterfaces()
    {
        ISteamNetworkingSockets.Setup();
        ISteamNetworkingUtils.Setup();
    }
}
