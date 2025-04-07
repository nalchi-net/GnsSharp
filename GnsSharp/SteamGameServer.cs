// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Text;

public static class SteamGameServer
{
    /// <summary>
    /// Pass to SteamGameServer_Init to indicate that the same UDP port will be used for game traffic<br/>
    /// UDP queries for server browser pings and LAN discovery.  In this case, Steam will not open up a<br/>
    /// socket to handle server browser queries, and you must use ISteamGameServer::HandleIncomingPacket<br/>
    /// and ISteamGameServer::GetNextOutgoingPacket to handle packets related to server discovery on your socket.
    /// </summary>
    public const ushort QueryPortShared = 0xffff;

    /// <summary>
    /// <para>
    /// Initialize SteamGameServer client and interface objects, and set server properties which may not be changed.
    /// </para>
    ///
    /// <para>
    /// After calling this function, you should set any additional server parameters, and then<br/>
    /// call ISteamGameServer::LogOnAnonymous() or ISteamGameServer::LogOn()
    /// </para>
    ///
    /// <para>
    /// - unIP will usually be zero.  If you are on a machine with multiple IP addresses, you can pass a non-zero<br/>
    ///   value here and the relevant sockets will be bound to that IP.  This can be used to ensure that<br/>
    ///   the IP you desire is the one used in the server browser.<br/>
    /// - usGamePort is the port that clients will connect to for gameplay.  You will usually open up your<br/>
    ///   own socket bound to this port.<br/>
    /// - usQueryPort is the port that will manage server browser related duties and info<br/>
    ///   pings from clients.  If you pass STEAMGAMESERVER_QUERY_PORT_SHARED for usQueryPort, then it<br/>
    ///   will use "GameSocketShare" mode, which means that the game is responsible for sending and receiving<br/>
    ///   UDP packets for the master  server updater.  (See ISteamGameServer::HandleIncomingPacket and<br/>
    ///   ISteamGameServer::GetNextOutgoingPacket.)<br/>
    /// - The version string should be in the form x.x.x.x, and is used by the master server to detect when the<br/>
    ///   server is out of date.  (Only servers with the latest version will be listed.)
    /// </para>
    ///
    /// <para>
    /// On success k_ESteamAPIInitResult_OK is returned.  Otherwise, if pOutErrMsg is non-NULL,<br/>
    /// it will receive a non-localized message that explains the reason for the failure
    /// </para>
    /// </summary>
    public static ESteamAPIInitResult InitEx(uint ip, ushort gamePort, ushort queryPort, EServerMode serverMode, string versionString, out string? outErrMsg)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have GameServer API");
#elif GNS_SHARP_STEAMWORKS_SDK

        int capacity =
              Constants.SteamNetworkingSocketsInterfaceVersion.Length + 1
            + Constants.SteamNetworkingUtilsInterfaceVersion.Length + 1
            + Constants.SteamUtilsInterfaceVersion.Length + 1
            + Constants.SteamNetworkingMessagesInterfaceVersion.Length + 1

            // + Constants.SteamGameServerInterfaceVersion.Length + 1
            // + Constants.SteamGameServerStatsInterfaceVersion.Length + 1
            // + Constants.SteamHttpInterfaceVersion.Length + 1
            // + Constants.SteamInventoryInterfaceVersion.Length + 1
            // + Constants.SteamNetworkingInterfaceVersion.Length + 1
            // + Constants.SteamUgcInterfaceVersion.Length + 1
            + 1;

        StringBuilder versions = new(capacity);

        versions.Append(Constants.SteamNetworkingSocketsInterfaceVersion).Append('\0');
        versions.Append(Constants.SteamNetworkingUtilsInterfaceVersion).Append('\0');
        versions.Append(Constants.SteamUtilsInterfaceVersion).Append('\0');
        versions.Append(Constants.SteamNetworkingMessagesInterfaceVersion).Append('\0');

        // versions.Append(Constants.SteamGameServerInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamGameServerStatsInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamHttpInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamInventoryInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamNetworkingInterfaceVersion).Append('\0');
        // versions.Append(Constants.SteamUgcInterfaceVersion).Append('\0');
        versions.Append('\0');

        ESteamAPIInitResult result = Native.SteamInternal_GameServer_Init_V2(ip, gamePort, queryPort, serverMode, versionString, versions.ToString(), out SteamErrMsg msg);

        if (result != ESteamAPIInitResult.OK)
        {
            ReadOnlySpan<byte> span = msg;
            outErrMsg = Utf8StringHelper.NullTerminatedSpanToString(span);
        }
        else
        {
            outErrMsg = null;

            GnsSharpCore.Init(isGameServer: true);

            Native.SteamAPI_ManualDispatch_Init();
        }

        return result;
#endif
    }

    /// <summary>
    /// This function is included for compatibility with older SDK.<br/>
    /// You can use it if you don't care about decent error handling
    /// </summary>
    public static bool Init(uint ip, ushort gamePort, ushort queryPort, EServerMode serverMode, string versionString)
    {
        return InitEx(ip, gamePort, queryPort, serverMode, versionString, out _) == ESteamAPIInitResult.OK;
    }

    /// <summary>
    /// Shutdown SteamGameSeverXxx interfaces, log out, and free resources.
    /// </summary>
    public static void Shutdown()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have GameServer API");
#elif GNS_SHARP_STEAMWORKS_SDK
        GnsSharpCore.Shutdown(isGameServer: true);
        Native.SteamGameServer_Shutdown();
#endif
    }

    /// <summary>
    /// Most Steam API functions allocate some amount of thread-local memory for<br/>
    /// parameter storage. Calling SteamGameServer_ReleaseCurrentThreadMemory()<br/>
    /// will free all API-related memory associated with the calling thread.<br/>
    /// This memory is released automatically by SteamGameServer_RunCallbacks(),<br/>
    /// so single-threaded servers do not need to explicitly call this function.
    /// </summary>
    public static void ReleaseCurrentThreadMemory()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have GameServer API");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ReleaseCurrentThreadMemory();
#endif
    }

    public static void RunCallbacks()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have GameServer API");
#elif GNS_SHARP_STEAMWORKS_SDK
        Dispatcher.RunCallbacks(isGameServer: true);
#endif
    }

    public static bool BSecure()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have GameServer API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamGameServer_BSecure();
#endif
    }

    public static CSteamID GetSteamID()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have GameServer API");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamGameServer_GetSteamID();
#endif
    }
}
