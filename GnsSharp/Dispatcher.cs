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
        while (Native.SteamAPI_ManualDispatch_GetNextCallback(pipe, out CallbackMsg_t callbackMsg))
        {
            // Check for dispatching API call results
            if (callbackMsg.Callback == SteamAPICallCompleted_t.CallbackId)
            {
                HandleCallCompletedResult(pipe, ref callbackMsg);
            }
            else if (callbackMsg.Callback == SteamNetConnectionStatusChangedCallback_t.CallbackId)
            {
                HandleConnStatusChanged(ref callbackMsg);
            }
            else
            {
                // TODO:
                // Look at callback.m_iCallback to see what kind of callback it is,
                // and dispatch to appropriate handler(s)
            }

            Native.SteamAPI_ManualDispatch_FreeLastCallback(pipe);
        }
    }

    private static void HandleCallCompletedResult(HSteamPipe hSteamPipe, ref CallbackMsg_t callbackMsg)
    {
        Span<SteamAPICallCompleted_t> callCompleted;
        unsafe
        {
            callCompleted = new Span<SteamAPICallCompleted_t>((void*)callbackMsg.Param, 1);
        }

        // Get the stackalloc byte span that's aligned to 8 bytes boundary
        int rawResultSize = (int)callCompleted[0].Param;
        int alignedElemCount = (rawResultSize + (sizeof(ulong) - 1)) / sizeof(ulong); // ceiled division
        Span<ulong> callResultAligned = stackalloc ulong[alignedElemCount];
        Span<byte> callResult = MemoryMarshal.AsBytes(callResultAligned)[..rawResultSize];

        if (Native.SteamAPI_ManualDispatch_GetAPICallResult(hSteamPipe, callCompleted[0].AsyncCall, callResult, callResult.Length, callCompleted[0].Callback, out bool failed))
        {
            // TODO:
            // Dispatch the call result to the registered handler(s) for the
            // call identified by pCallCompleted->m_hAsyncCall
        }
    }

    private static void HandleConnStatusChanged(ref CallbackMsg_t callbackMsg)
    {
        Span<SteamNetConnectionStatusChangedCallback_t> connChanged;
        unsafe
        {
            connChanged = new Span<SteamNetConnectionStatusChangedCallback_t>((void*)callbackMsg.Param, 1);
        }

        // Get the callback function pointer registered for this connection
        SizeT resultSize;
        unsafe
        {
            resultSize = (SizeT)sizeof(IntPtr);
        }

        SizeT prevResultSize = resultSize;
        Span<IntPtr> connChangedCallbackPtr = stackalloc IntPtr[1];

        // This approach has a flaw that:
        // When the connection is closed, it won't receive status changed callback for `ESteamNetworkingConnectionState.None`.
        // Because, at that point, the connection handle should be already invalidated,
        // thus can't get the callback function pointer associated with it.
        //
        // But `ESteamNetworkingConnectionState.None` status is rarely useful;
        // Managing seperate pointer table only for that doesn't look too good.
        var getConfigValueResult = ISteamNetworkingUtils.User!.GetConfigValue(ESteamNetworkingConfigValue.Callback_ConnectionStatusChanged, ESteamNetworkingConfigScope.Connection, (IntPtr)(uint)connChanged[0].Conn, out ESteamNetworkingConfigDataType outDataType, MemoryMarshal.AsBytes(connChangedCallbackPtr), ref resultSize);

        if (getConfigValueResult == ESteamNetworkingGetConfigValueResult.OK || getConfigValueResult == ESteamNetworkingGetConfigValueResult.OKInherited)
        {
            Debug.Assert(prevResultSize == resultSize, $"Config value size expected {prevResultSize}, got {resultSize}");

            // Call the callback if it is set
            if (connChangedCallbackPtr[0] != IntPtr.Zero)
            {
                var connChangedCallback = Marshal.GetDelegateForFunctionPointer<FnSteamNetConnectionStatusChanged>(connChangedCallbackPtr[0]);

                connChangedCallback(ref connChanged[0]);
            }
        }
    }
}

#endif
