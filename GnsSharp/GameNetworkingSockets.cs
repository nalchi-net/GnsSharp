// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.CompilerServices;

/// <summary>
/// High level interface to GameNetworkingSockets library.
/// </summary>
public static class GameNetworkingSockets
{
    /// <summary>
    /// <para>
    /// Initialize the library.  Optionally, you can set an initial identity for the default<br/>
    /// interface that is returned by SteamNetworkingSockets().<br/>
    /// </para>
    ///
    /// <para>
    /// On failure, false is returned, and a non-localized diagnostic message is returned.
    /// </para>
    /// </summary>
    public static bool Init(in SteamNetworkingIdentity identity, out string? errMsg)
    {
#if GNS_SHARP_OPENSOURCE_GNS

        bool result = Native.GameNetworkingSockets_Init(in identity, out SteamNetworkingErrMsg msg);

        if (!result)
        {
            ReadOnlySpan<byte> span = msg;
            errMsg = Utf8StringHelper.NullTerminatedSpanToString(span);
        }
        else
        {
            errMsg = null;

            GnsSharpCore.SetupInterfaces();
        }

        return result;

#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("On Steamworks SDK, use SteamAPI.Init() instead");
#endif
    }

    /// <summary>
    /// <para>
    /// Initialize the library.  Optionally, you can set an initial identity for the default<br/>
    /// interface that is returned by SteamNetworkingSockets().<br/>
    /// </para>
    ///
    /// <para>
    /// On failure, false is returned, and a non-localized diagnostic message is returned.
    /// </para>
    /// </summary>
    public static bool Init(out string? errMsg)
    {
        return Init(in Unsafe.NullRef<SteamNetworkingIdentity>(), out errMsg);
    }

    /// <summary>
    /// Close all connections and listen sockets and free all resources
    /// </summary>
    public static void Kill()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.GameNetworkingSockets_Kill();
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("On Steamworks SDK, use SteamAPI.Shutdown() instead");
#endif
    }

    /// <summary>
    /// Custom memory allocation methods.  If you call this, you MUST call it exactly once,<br/>
    /// before calling any other API function.  *Most* allocations will pass through these,<br/>
    /// especially all allocations that are per-connection.  A few allocations<br/>
    /// might still go to the default CRT malloc and operator new.<br/>
    /// To use this, you must compile the library with STEAMNETWORKINGSOCKETS_ENABLE_MEM_OVERRIDE
    /// </summary>
    public static unsafe void SetCustomMemoryAllocator(FPtrCustomMemoryMalloc malloc, FPtrCustomMemoryFree free, FPtrCustomMemoryRealloc realloc)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetCustomMemoryAllocator(malloc, free, realloc);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetCustomMemoryAllocator()");
#endif
    }

    /// <summary>
    /// Statistics about the global lock.
    /// </summary>
    public static void SetLockWaitWarningThreshold(SteamNetworkingMicroseconds usecThreshold)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetLockWaitWarningThreshold(usecThreshold);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetLockWaitWarningThreshold()");
#endif
    }

    /// <summary>
    /// Statistics about the global lock.
    /// </summary>
    public static unsafe void SetLockAcquiredCallback(FPtrSteamNetworkingSocketsLockWaitedFor callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetLockAcquiredCallback(callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetLockAcquiredCallback()");
#endif
    }

    /// <summary>
    /// Statistics about the global lock.
    /// </summary>
    public static void SetLockAcquiredCallback(FSteamNetworkingSocketsLockWaitedFor callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetLockAcquiredCallback(callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetLockAcquiredCallback()");
#endif
    }

    /// <summary>
    /// Statistics about the global lock.
    /// </summary>
    public static unsafe void SetLockHeldCallback(FPtrSteamNetworkingSocketsLockWaitedFor callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetLockHeldCallback(callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetLockHeldCallback()");
#endif
    }

    /// <summary>
    /// Statistics about the global lock.
    /// </summary>
    public static unsafe void SetLockHeldCallback(FSteamNetworkingSocketsLockWaitedFor callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetLockHeldCallback(callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetLockHeldCallback()");
#endif
    }

    /// <summary>
    /// Called from the service thread at initialization time.<br/>
    /// Use this to customize its priority / affinity, etc
    /// </summary>
    public static unsafe void SetServiceThreadInitCallback(FPtrSteamNetworkingSocketsServiceThreadInit callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetServiceThreadInitCallback(callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetServiceThreadInitCallback()");
#endif
    }

    /// <summary>
    /// Called from the service thread at initialization time.<br/>
    /// Use this to customize its priority / affinity, etc
    /// </summary>
    public static void SetServiceThreadInitCallback(FSteamNetworkingSocketsServiceThreadInit callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingSockets_SetServiceThreadInitCallback(callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        throw new NotImplementedException("Steamworks SDK doesn't have SetServiceThreadInitCallback()");
#endif
    }
}
