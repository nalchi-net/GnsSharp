// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

#if GNS_SHARP_STEAMWORKS_SDK

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

internal class Dispatcher
{
    private const int CallbackBigGroupSize = 100;
    private const int CallbackSmallGroupSize = 10;

    public static void RunCallbacks(bool isGameServer)
    {
        HSteamPipe pipe = isGameServer ? Native.SteamGameServer_GetHSteamPipe() : Native.SteamAPI_GetHSteamPipe();

        // Manual callback loop
        Native.SteamAPI_ManualDispatch_RunFrame(pipe);
        while (Native.SteamAPI_ManualDispatch_GetNextCallback(pipe, out CallbackMsg_t msg))
        {
            // The primary callback switch-case
            //
            // This should generate jump tables when every callback groups are ported
            switch (msg.CallbackId / CallbackBigGroupSize)
            {
                case Constants.SteamUtilsCallbacks / CallbackBigGroupSize:
                    var utils = isGameServer ? ISteamUtils.GameServer : ISteamUtils.User;
                    utils!.OnDispatch(pipe, ref msg);
                    break;

                case Constants.SteamNetworkingSocketsCallbacks / CallbackBigGroupSize:
                    switch ((msg.CallbackId % CallbackBigGroupSize) / CallbackSmallGroupSize)
                    {
                        case (Constants.SteamNetworkingSocketsCallbacks % CallbackBigGroupSize) / CallbackSmallGroupSize:
                            var networkingSockets = isGameServer ? ISteamNetworkingSockets.GameServer : ISteamNetworkingSockets.User;
                            networkingSockets!.OnDispatch(ref msg);
                            break;

                        case (Constants.SteamNetworkingUtilsCallbacks % CallbackBigGroupSize) / CallbackSmallGroupSize:
                            var networkingUtils = isGameServer ? ISteamNetworkingUtils.GameServer : ISteamNetworkingUtils.User;
                            networkingUtils!.OnDispatch(ref msg);
                            break;

                        default:
                            Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on Dispatcher.RunCallbacks()");
                            break;
                    }

                    break;

                case Constants.SteamMatchmakingCallbacks / CallbackBigGroupSize:
                    ISteamMatchmaking.User!.OnDispatch(ref msg);
                    break;

                case Constants.SteamFriendsCallbacks / CallbackBigGroupSize:
                    ISteamFriends.User!.OnDispatch(ref msg);
                    break;

                // TODO: Add all the other callbacks
                default:
                    Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on Dispatcher.RunCallbacks()");
                    break;
            }

            Native.SteamAPI_ManualDispatch_FreeLastCallback(pipe);
        }
    }
}

#endif
