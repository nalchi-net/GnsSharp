// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

#if GNS_SHARP_STEAMWORKS_SDK

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

internal class Dispatcher
{
    public static void RunCallbacks(bool isGameServer)
    {
        HSteamPipe pipe = isGameServer ? Native.SteamGameServer_GetHSteamPipe() : Native.SteamAPI_GetHSteamPipe();

        // Manual callback loop
        Native.SteamAPI_ManualDispatch_RunFrame(pipe);
        while (Native.SteamAPI_ManualDispatch_GetNextCallback(pipe, out CallbackMsg_t msg))
        {
            // The primary callback switch-case
            switch (msg.CallbackId)
            {
                // Check for dispatching API call results
                case SteamAPICallCompleted_t.CallbackId:
                    HandleCallCompletedResult(pipe, ref msg);
                    break;

                case >= Constants.SteamNetworkingSocketsCallbacks and < Constants.SteamNetworkingMessagesCallbacks:
                    var networkingSockets = isGameServer ? ISteamNetworkingSockets.GameServer : ISteamNetworkingSockets.User;
                    networkingSockets!.OnDispatch(ref msg);
                    break;

                case >= Constants.SteamNetworkingUtilsCallbacks and < Constants.SteamRemoteStorageCallbacks:
                    var networkingUtils = isGameServer ? ISteamNetworkingUtils.GameServer : ISteamNetworkingUtils.User;
                    networkingUtils!.OnDispatch(ref msg);
                    break;

                // TODO: Add all the other callbacks
                default:
                    Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on Dispatcher.RunCallbacks()");
                    break;
            }

            Native.SteamAPI_ManualDispatch_FreeLastCallback(pipe);
        }
    }

    private static void HandleCallCompletedResult(HSteamPipe pipe, ref CallbackMsg_t msg)
    {
        ref var callCompleted = ref msg.GetCallbackParamAs<SteamAPICallCompleted_t>();

        // Get the stackalloc byte span that's aligned to 8 bytes boundary
        int rawResultSize = (int)callCompleted.Param;
        int alignedElemCount = (rawResultSize + (sizeof(ulong) - 1)) / sizeof(ulong); // ceiled division
        Span<ulong> callResultAligned = stackalloc ulong[alignedElemCount];
        Span<byte> callResult = MemoryMarshal.AsBytes(callResultAligned)[..rawResultSize];

        if (Native.SteamAPI_ManualDispatch_GetAPICallResult(pipe, callCompleted.AsyncCall, callResult, callResult.Length, callCompleted.Callback, out bool failed))
        {
            // TODO:
            // Dispatch the call result to the registered handler(s) for the
            // call identified by pCallCompleted->m_hAsyncCall
        }
    }
}

#endif
