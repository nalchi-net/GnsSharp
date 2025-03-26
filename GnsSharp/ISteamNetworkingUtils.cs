// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// Misc networking utilities for checking the local networking environment
/// and estimating pings.
/// </summary>
public class ISteamNetworkingUtils
{
    /// <summary>
    /// Max possible length of a ping location, in string format.  This is<br/>
    /// an extremely conservative worst case value which leaves room for future<br/>
    /// syntax enhancements.  Most strings in practice are a lot shorter.<br/>
    /// If you are storing many of these, you will very likely benefit from<br/>
    /// using dynamic memory.
    /// </summary>
    private const int MaxSteamNetworkingPingLocationString = 1024;

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamNetworkingUtils()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        this.ptr = Native.SteamAPI_SteamNetworkingUtils_v003();
#elif GNS_SHARP_STEAMWORKS_SDK
        this.ptr = Native.SteamAPI_SteamNetworkingUtils_SteamAPI_v004();
#endif
    }

#pragma warning disable CS0067 // The event is never used

    public event FnSteamRelayNetworkStatusChanged? SteamRelayNetworkStatusChanged;

#pragma warning restore CS0067

    /// <summary>
    /// For <see cref="ISteamNetworkingUtils"/>, the native pointer is actually the same with the <see cref="GameServer"/><br/>
    /// (i.e. It will share the same global settings with <see cref="GameServer"/> )
    /// </summary>
    public static ISteamNetworkingUtils? User { get; internal set; }

    /// <summary>
    /// For <see cref="ISteamNetworkingUtils"/>, the native pointer is actually the same with the <see cref="User"/><br/>
    /// (i.e. It will share the same global settings with <see cref="User"/> )
    /// </summary>
    public static ISteamNetworkingUtils? GameServer { get; internal set; }

    public IntPtr Ptr => this.ptr;

    // Efficient message sending

    /// <summary>
    /// <para>
    /// Allocate and initialize a message object.  Usually the reason<br/>
    /// you call this is to pass it to ISteamNetworkingSockets::SendMessages.<br/>
    /// The returned object will have all of the relevant fields cleared to zero.
    /// </para>
    ///
    /// <para>
    /// Optionally you can also request that this system allocate space to<br/>
    /// hold the payload itself.  If cbAllocateBuffer is nonzero, the system<br/>
    /// will allocate memory to hold a payload of at least cbAllocateBuffer bytes.<br/>
    /// m_pData will point to the allocated buffer, m_cbSize will be set to the<br/>
    /// size, and m_pfnFreeData will be set to the proper function to free up<br/>
    /// the buffer.
    /// </para>
    ///
    /// <para>
    /// If cbAllocateBuffer=0, then no buffer is allocated.  m_pData will be NULL,<br/>
    /// m_cbSize will be zero, and m_pfnFreeData will be NULL.  You will need to<br/>
    /// set each of these.
    /// </para>
    /// </summary>
    public IntPtr AllocateMessage(int allocateBuffer)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_AllocateMessage(this.ptr, allocateBuffer);
    }

    // Access to Steam Datagram Relay (SDR) network

    // Initialization and status check

    /// <summary>
    /// <para>
    /// If you know that you are going to be using the relay network (for example,<br/>
    /// because you anticipate making P2P connections), call this to initialize the<br/>
    /// relay network.  If you do not call this, the initialization will<br/>
    /// be delayed until the first time you use a feature that requires access<br/>
    /// to the relay network, which will delay that first access.
    /// </para>
    ///
    /// <para>
    /// You can also call this to force a retry if the previous attempt has failed.<br/>
    /// Performing any action that requires access to the relay network will also<br/>
    /// trigger a retry, and so calling this function is never strictly necessary,<br/>
    /// but it can be useful to call it a program launch time, if access to the<br/>
    /// relay network is anticipated.
    /// </para>
    ///
    /// <para>
    /// Use GetRelayNetworkStatus or listen for SteamRelayNetworkStatus_t<br/>
    /// callbacks to know when initialization has completed.<br/>
    /// Typically initialization completes in a few seconds.
    /// </para>
    ///
    /// <para>
    /// Note: dedicated servers hosted in known data centers do *not* need<br/>
    /// to call this, since they do not make routing decisions.  However, if<br/>
    /// the dedicated server will be using P2P functionality, it will act as<br/>
    /// a "client" and this should be called.
    /// </para>
    /// </summary>
    public void InitRelayNetworkAccess()
    {
        Native.SteamAPI_ISteamNetworkingUtils_InitRelayNetworkAccess(this.ptr);
    }

    /// <summary>
    /// <para>
    /// Fetch current status of the relay network.
    /// </para>
    ///
    /// <para>
    /// SteamRelayNetworkStatus_t is also a callback.  It will be triggered on<br/>
    /// both the user and gameserver interfaces any time the status changes, or<br/>
    /// ping measurement starts or stops.
    /// </para>
    ///
    /// <para>
    /// SteamRelayNetworkStatus_t::m_eAvail is returned.  If you want<br/>
    /// more details, you can pass a non-NULL value.
    /// </para>
    /// </summary>
    public ESteamNetworkingAvailability GetRelayNetworkStatus(out SteamRelayNetworkStatus_t details)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus(this.ptr, out details);
    }

    /// <summary>
    /// <para>
    /// Fetch current status of the relay network.
    /// </para>
    ///
    /// <para>
    /// SteamRelayNetworkStatus_t is also a callback.  It will be triggered on<br/>
    /// both the user and gameserver interfaces any time the status changes, or<br/>
    /// ping measurement starts or stops.
    /// </para>
    ///
    /// <para>
    /// SteamRelayNetworkStatus_t::m_eAvail is returned.  If you want<br/>
    /// more details, you can pass a non-NULL value.
    /// </para>
    /// </summary>
    public ESteamNetworkingAvailability GetRelayNetworkStatus()
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus(this.ptr, IntPtr.Zero);
    }

    // "Ping location" functions
    //
    // We use the ping times to the valve relays deployed worldwide to
    // generate a "marker" that describes the location of an Internet host.
    // Given two such markers, we can estimate the network latency between
    // two hosts, without sending any packets.  The estimate is based on the
    // optimal route that is found through the Valve network.  If you are
    // using the Valve network to carry the traffic, then this is precisely
    // the ping you want.  If you are not, then the ping time will probably
    // still be a reasonable estimate.
    //
    // This is extremely useful to select peers for matchmaking!
    //
    // The markers can also be converted to a string, so they can be transmitted.
    // We have a separate library you can use on your app's matchmaking/coordinating
    // server to manipulate these objects.  (See steamdatagram_gamecoordinator.h)

    /// <summary>
    /// <para>
    /// Return location info for the current host.  Returns the approximate<br/>
    /// age of the data, in seconds, or -1 if no data is available.
    /// </para>
    ///
    /// <para>
    /// It takes a few seconds to initialize access to the relay network.  If<br/>
    /// you call this very soon after calling InitRelayNetworkAccess,<br/>
    /// the data may not be available yet.
    /// </para>
    ///
    /// <para>
    /// This always return the most up-to-date information we have available<br/>
    /// right now, even if we are in the middle of re-calculating ping times.
    /// </para>
    /// </summary>
    public float GetLocalPingLocation(out SteamNetworkPingLocation_t result)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetLocalPingLocation(this.ptr, out result);
    }

    /// <summary>
    /// <para>
    /// Estimate the round-trip latency between two arbitrary locations, in<br/>
    /// milliseconds.  This is a conservative estimate, based on routing through<br/>
    /// the relay network.  For most basic relayed connections, this ping time<br/>
    /// will be pretty accurate, since it will be based on the route likely to<br/>
    /// be actually used.
    /// </para>
    ///
    /// <para>
    /// If a direct IP route is used (perhaps via NAT traversal), then the route<br/>
    /// will be different, and the ping time might be better.  Or it might actually<br/>
    /// be a bit worse!  Standard IP routing is frequently suboptimal!
    /// </para>
    ///
    /// <para>
    /// But even in this case, the estimate obtained using this method is a<br/>
    /// reasonable upper bound on the ping time.  (Also it has the advantage<br/>
    /// of returning immediately and not sending any packets.)
    /// </para>
    ///
    /// <para>
    /// In a few cases we might not able to estimate the route.  In this case<br/>
    /// a negative value is returned.  k_nSteamNetworkingPing_Failed means<br/>
    /// the reason was because of some networking difficulty.  (Failure to<br/>
    /// ping, etc)  k_nSteamNetworkingPing_Unknown is returned if we cannot<br/>
    /// currently answer the question for some other reason.
    /// </para>
    ///
    /// <para>
    /// Do you need to be able to do this from a backend/matchmaking server?<br/>
    /// You are looking for the "game coordinator" library.
    /// </para>
    /// </summary>
    public int EstimatePingTimeBetweenTwoLocations(in SteamNetworkPingLocation_t location1, in SteamNetworkPingLocation_t location2)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(this.ptr, location1, location2);
    }

    /// <summary>
    /// <para>
    /// Same as EstimatePingTime, but assumes that one location is the local host.<br/>
    /// This is a bit faster, especially if you need to calculate a bunch of<br/>
    /// these in a loop to find the fastest one.
    /// </para>
    ///
    /// <para>
    /// In rare cases this might return a slightly different estimate than combining<br/>
    /// GetLocalPingLocation with EstimatePingTimeBetweenTwoLocations.  That's because<br/>
    /// this function uses a slightly more complete set of information about what<br/>
    /// route would be taken.
    /// </para>
    /// </summary>
    public int EstimatePingTimeFromLocalHost(in SteamNetworkPingLocation_t remoteLocation)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(this.ptr, remoteLocation);
    }

    /// <summary>
    /// Convert a ping location into a text format suitable for sending over the wire.<br/>
    /// The format is a compact and human readable.  However, it is subject to change<br/>
    /// so please do not parse it yourself.  Your buffer must be at least<br/>
    /// k_cchMaxSteamNetworkingPingLocationString bytes.
    /// </summary>
    public string ConvertPingLocationToString(in SteamNetworkPingLocation_t location)
    {
        Span<byte> raw = stackalloc byte[MaxSteamNetworkingPingLocationString];
        Native.SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString(this.ptr, in location, raw, raw.Length);

        return Utf8StringHelper.NullTerminatedSpanToString(raw);
    }

    /// <summary>
    /// Parse back SteamNetworkPingLocation_t string.  Returns false if we couldn't understand<br/>
    /// the string.
    /// </summary>
    public bool ParsePingLocationString(string str, out SteamNetworkPingLocation_t result)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_ParsePingLocationString(this.ptr, str, out result);
    }

    /// <summary>
    /// <para>
    /// Check if the ping data of sufficient recency is available, and if<br/>
    /// it's too old, start refreshing it.
    /// </para>
    ///
    /// <para>
    /// Please only call this function when you *really* do need to force an<br/>
    /// immediate refresh of the data.  (For example, in response to a specific<br/>
    /// user input to refresh this information.)  Don't call it "just in case",<br/>
    /// before every connection, etc.  That will cause extra traffic to be sent<br/>
    /// for no benefit. The library will automatically refresh the information<br/>
    /// as needed.
    /// </para>
    ///
    /// <para>
    /// Returns true if sufficiently recent data is already available.
    /// </para>
    ///
    /// <para>
    /// Returns false if sufficiently recent data is not available.  In this<br/>
    /// case, ping measurement is initiated, if it is not already active.<br/>
    /// (You cannot restart a measurement already in progress.)
    /// </para>
    ///
    /// <para>
    /// You can use GetRelayNetworkStatus or listen for SteamRelayNetworkStatus_t<br/>
    /// to know when ping measurement completes.
    /// </para>
    /// </summary>
    public bool CheckPingDataUpToDate(float maxAgeSeconds)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_CheckPingDataUpToDate(this.ptr, maxAgeSeconds);
    }

    // List of Valve data centers, and ping times to them.  This might
    // be useful to you if you are use our hosting, or just need to measure
    // latency to a cloud data center where we are running relays.

    /// <summary>
    /// Fetch ping time of best available relayed route from this host to<br/>
    /// the specified data center.
    /// </summary>
    public int GetPingToDataCenter(SteamNetworkingPOPID popID, ref SteamNetworkingPOPID viaRelayPoP)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetPingToDataCenter(this.ptr, popID, ref viaRelayPoP);
    }

    /// <summary>
    /// Get *direct* ping time to the relays at the data center.
    /// </summary>
    public int GetDirectPingToPOP(SteamNetworkingPOPID popID)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetDirectPingToPOP(this.ptr, popID);
    }

    /// <summary>
    /// Get number of network points of presence in the config
    /// </summary>
    public int GetPOPCount()
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetPOPCount(this.ptr);
    }

    /// <summary>
    /// Get list of all POP IDs.  Returns the number of entries that were filled into<br/>
    /// your list.
    /// </summary>
    public int GetPOPList(Span<SteamNetworkingPOPID> list)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetPOPList(this.ptr, list, list.Length);
    }

    // Misc

    /// <summary>
    /// <para>
    /// Fetch current timestamp.  This timer has the following properties:
    /// </para>
    ///
    /// <para>
    /// - Monotonicity is guaranteed.<br/>
    /// - The initial value will be at least 24*3600*30*1e6, i.e. about<br/>
    /// 30 days worth of microseconds.  In this way, the timestamp value of<br/>
    /// 0 will always be at least "30 days ago".  Also, negative numbers<br/>
    /// will never be returned.<br/>
    /// - Wraparound / overflow is not a practical concern.
    /// </para>
    ///
    /// <para>
    /// If you are running under the debugger and stop the process, the clock<br/>
    /// might not advance the full wall clock time that has elapsed between<br/>
    /// calls.  If the process is not blocked from normal operation, the<br/>
    /// timestamp values will track wall clock time, even if you don't call<br/>
    /// the function frequently.
    /// </para>
    ///
    /// <para>
    /// The value is only meaningful for this run of the process.  Don't compare<br/>
    /// it to values obtained on another computer, or other runs of the same process.
    /// </para>
    /// </summary>
    public SteamNetworkingMicroseconds GetLocalTimestamp()
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetLocalTimestamp(this.ptr);
    }

    /// <summary>
    /// <para>
    /// Set a function to receive network-related information that is useful for debugging.<br/>
    /// This can be very useful during development, but it can also be useful for troubleshooting<br/>
    /// problems with tech savvy end users.  If you have a console or other log that customers<br/>
    /// can examine, these log messages can often be helpful to troubleshoot network issues.<br/>
    /// (Especially any warning/error messages.)
    /// </para>
    ///
    /// <para>
    /// The detail level indicates what message to invoke your callback on.  Lower numeric<br/>
    /// value means more important, and the value you pass is the lowest priority (highest<br/>
    /// numeric value) you wish to receive callbacks for.
    /// </para>
    ///
    /// <para>
    /// The value here controls the detail level for most messages.  You can control the<br/>
    /// detail level for various subsystems (perhaps only for certain connections) by<br/>
    /// adjusting the configuration values k_ESteamNetworkingConfig_LogLevel_Xxxxx.
    /// </para>
    ///
    /// <para>
    /// Except when debugging, you should only use k_ESteamNetworkingSocketsDebugOutputType_Msg<br/>
    /// or k_ESteamNetworkingSocketsDebugOutputType_Warning.  For best performance, do NOT<br/>
    /// request a high detail level and then filter out messages in your callback.  This incurs<br/>
    /// all of the expense of formatting the messages, which are then discarded.  Setting a high<br/>
    /// priority value (low numeric value) here allows the library to avoid doing this work.
    /// </para>
    ///
    /// <para>
    /// IMPORTANT: This may be called from a service thread, while we own a mutex, etc.<br/>
    /// Your output function must be threadsafe and fast!  Do not make any other<br/>
    /// Steamworks calls from within the handler.
    /// </para>
    /// </summary>
    public unsafe void SetDebugOutputFunction(ESteamNetworkingSocketsDebugOutputType detailLevel, FPtrSteamNetworkingSocketsDebugOutput func)
    {
        Native.SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction(this.ptr, detailLevel, func);
    }

    /// <summary>
    /// <para>
    /// Set a function to receive network-related information that is useful for debugging.<br/>
    /// This can be very useful during development, but it can also be useful for troubleshooting<br/>
    /// problems with tech savvy end users.  If you have a console or other log that customers<br/>
    /// can examine, these log messages can often be helpful to troubleshoot network issues.<br/>
    /// (Especially any warning/error messages.)
    /// </para>
    ///
    /// <para>
    /// The detail level indicates what message to invoke your callback on.  Lower numeric<br/>
    /// value means more important, and the value you pass is the lowest priority (highest<br/>
    /// numeric value) you wish to receive callbacks for.
    /// </para>
    ///
    /// <para>
    /// The value here controls the detail level for most messages.  You can control the<br/>
    /// detail level for various subsystems (perhaps only for certain connections) by<br/>
    /// adjusting the configuration values k_ESteamNetworkingConfig_LogLevel_Xxxxx.
    /// </para>
    ///
    /// <para>
    /// Except when debugging, you should only use k_ESteamNetworkingSocketsDebugOutputType_Msg<br/>
    /// or k_ESteamNetworkingSocketsDebugOutputType_Warning.  For best performance, do NOT<br/>
    /// request a high detail level and then filter out messages in your callback.  This incurs<br/>
    /// all of the expense of formatting the messages, which are then discarded.  Setting a high<br/>
    /// priority value (low numeric value) here allows the library to avoid doing this work.
    /// </para>
    ///
    /// <para>
    /// IMPORTANT: This may be called from a service thread, while we own a mutex, etc.<br/>
    /// Your output function must be threadsafe and fast!  Do not make any other<br/>
    /// Steamworks calls from within the handler.
    /// </para>
    /// </summary>
    public void SetDebugOutputFunction(ESteamNetworkingSocketsDebugOutputType detailLevel, FSteamNetworkingSocketsDebugOutput func)
    {
        Native.SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction(this.ptr, detailLevel, func);
    }

    // Fake IP
    //
    // Useful for interfacing with code that assumes peers are identified using an IPv4 address

    /// <summary>
    /// Return true if an IPv4 address is one that might be used as a "fake" one.<br/>
    /// This function is fast; it just does some logical tests on the IP and does<br/>
    /// not need to do any lookup operations.
    /// </summary>
    public bool IsFakeIPv4(uint ipv4)
    {
        return this.GetIPv4FakeIPType(ipv4) > ESteamNetworkingFakeIPType.NotFake;
    }

    public ESteamNetworkingFakeIPType GetIPv4FakeIPType(uint ipv4)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return ESteamNetworkingFakeIPType.NotFake;
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_GetIPv4FakeIPType(this.ptr, ipv4);
#endif
    }

    /// <summary>
    /// <para>
    /// Get the real identity associated with a given FakeIP.
    /// </para>
    ///
    /// <para>
    /// On failure, returns:<br/>
    /// - k_EResultInvalidParam: the IP is not a FakeIP.<br/>
    /// - k_EResultNoMatch: we don't recognize that FakeIP and don't know the corresponding identity.
    /// </para>
    ///
    /// <para>
    /// FakeIP's used by active connections, or the FakeIPs assigned to local identities,<br/>
    /// will always work.  FakeIPs for recently destroyed connections will continue to<br/>
    /// return results for a little while, but not forever.  At some point, we will forget<br/>
    /// FakeIPs to save space.  It's reasonably safe to assume that you can read back the<br/>
    /// real identity of a connection very soon after it is destroyed.  But do not wait<br/>
    /// indefinitely.
    /// </para>
    /// </summary>
    public EResult GetRealIdentityForFakeIP(in SteamNetworkingIPAddr fakeIP, ref SteamNetworkingIdentity outRealIdentity)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        // Not supported without Steam
        return EResult.Disabled;
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_GetRealIdentityForFakeIP(this.ptr, in fakeIP, ref outRealIdentity);
#endif
    }

    // Set and get configuration values, see ESteamNetworkingConfigValue for individual descriptions.

    /// <summary>
    /// Shortcuts for common cases.
    /// </summary>
    public bool SetGlobalConfigValueInt32(ESteamNetworkingConfigValue value, int val)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueInt32(this.ptr, value, val);
    }

    /// <summary>
    /// Shortcuts for common cases.
    /// </summary>
    public bool SetGlobalConfigValueFloat(ESteamNetworkingConfigValue value, float val)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueFloat(this.ptr, value, val);
    }

    /// <summary>
    /// Shortcuts for common cases.
    /// </summary>
    public bool SetGlobalConfigValueString(ESteamNetworkingConfigValue value, string val)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueString(this.ptr, value, val);
    }

    /// <summary>
    /// Shortcuts for common cases.
    /// </summary>
    public bool SetGlobalConfigValuePtr(ESteamNetworkingConfigValue value, IntPtr val)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValuePtr(this.ptr, value, val);
    }

    /// <summary>
    /// Shortcuts for common cases.
    /// </summary>
    public bool SetConnectionConfigValueInt32(HSteamNetConnection conn, ESteamNetworkingConfigValue value, int val)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueInt32(this.ptr, conn, value, val);
    }

    /// <summary>
    /// Shortcuts for common cases.
    /// </summary>
    public bool SetConnectionConfigValueFloat(HSteamNetConnection conn, ESteamNetworkingConfigValue value, float val)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueFloat(this.ptr, conn, value, val);
    }

    /// <summary>
    /// Shortcuts for common cases.
    /// </summary>
    public bool SetConnectionConfigValueString(HSteamNetConnection conn, ESteamNetworkingConfigValue value, string val)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueString(this.ptr, conn, value, val);
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public unsafe bool SetGlobalCallback_SteamNetConnectionStatusChanged(FnPtrSteamNetConnectionStatusChanged callback)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetConnectionStatusChanged(this.ptr, callback);
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public bool SetGlobalCallback_SteamNetConnectionStatusChanged(FnSteamNetConnectionStatusChanged callback)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetConnectionStatusChanged(this.ptr, callback);
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public unsafe bool SetGlobalCallback_SteamNetAuthenticationStatusChanged(FnPtrSteamNetAuthenticationStatusChanged callback)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetAuthenticationStatusChanged(this.ptr, callback);
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public bool SetGlobalCallback_SteamNetAuthenticationStatusChanged(FnSteamNetAuthenticationStatusChanged callback)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamNetAuthenticationStatusChanged(this.ptr, callback);
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public unsafe bool SetGlobalCallback_SteamRelayNetworkStatusChanged(FnPtrSteamRelayNetworkStatusChanged callback)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamRelayNetworkStatusChanged(this.ptr, callback);
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public bool SetGlobalCallback_SteamRelayNetworkStatusChanged(FnSteamRelayNetworkStatusChanged callback)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_SteamRelayNetworkStatusChanged(this.ptr, callback);
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public unsafe bool SetGlobalCallback_FakeIPResult(FnPtrSteamNetworkingFakeIPResult callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return this.SetGlobalConfigValuePtr(ESteamNetworkingConfigValue.Callback_FakeIPResult, (IntPtr)callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_FakeIPResult(this.ptr, callback);
#endif
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public bool SetGlobalCallback_FakeIPResult(FnSteamNetworkingFakeIPResult callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return this.SetGlobalConfigValuePtr(ESteamNetworkingConfigValue.Callback_FakeIPResult, Marshal.GetFunctionPointerForDelegate(callback));
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_FakeIPResult(this.ptr, callback);
#endif
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public unsafe bool SetGlobalCallback_MessagesSessionRequest(FnPtrSteamNetworkingMessagesSessionRequest callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return this.SetGlobalConfigValuePtr(ESteamNetworkingConfigValue.Callback_MessagesSessionRequest, (IntPtr)callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionRequest(this.ptr, callback);
#endif
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public bool SetGlobalCallback_MessagesSessionRequest(FnSteamNetworkingMessagesSessionRequest callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return this.SetGlobalConfigValuePtr(ESteamNetworkingConfigValue.Callback_MessagesSessionRequest, Marshal.GetFunctionPointerForDelegate(callback));
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionRequest(this.ptr, callback);
#endif
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public unsafe bool SetGlobalCallback_MessagesSessionFailed(FnPtrSteamNetworkingMessagesSessionFailed callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return this.SetGlobalConfigValuePtr(ESteamNetworkingConfigValue.Callback_MessagesSessionFailed, (IntPtr)callback);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionFailed(this.ptr, callback);
#endif
    }

    /// <summary>
    /// Set global callbacks.  If you do not want to use Steam's callback dispatch mechanism and you<br/>
    /// want to use the same callback on all (or most) listen sockets and connections, then<br/>
    /// simply install these callbacks first thing, and you are good to go.<br/>
    /// See ISteamNetworkingSockets::RunCallbacks
    /// </summary>
    public bool SetGlobalCallback_MessagesSessionFailed(FnSteamNetworkingMessagesSessionFailed callback)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return this.SetGlobalConfigValuePtr(ESteamNetworkingConfigValue.Callback_MessagesSessionFailed, Marshal.GetFunctionPointerForDelegate(callback));
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SetGlobalCallback_MessagesSessionFailed(this.ptr, callback);
#endif
    }

    /// <summary>
    /// Set a configuration value.<br/>
    /// - eValue: which value is being set<br/>
    /// - eScope: Onto what type of object are you applying the setting?<br/>
    /// - scopeArg: Which object you want to change?  (Ignored for global scope).  E.g. connection handle, listen socket handle, interface pointer, etc.<br/>
    /// - eDataType: What type of data is in the buffer at pValue?  This must match the type of the variable exactly!<br/>
    /// - pArg: Value to set it to.  You can pass NULL to remove a non-global setting at this scope,<br/>
    /// causing the value for that object to use global defaults.  Or at global scope, passing NULL<br/>
    /// will reset any custom value and restore it to the system default.<br/>
    /// NOTE: When setting pointers (e.g. callback functions), do not pass the function pointer directly.<br/>
    /// Your argument should be a pointer to a function pointer.
    /// </summary>
    public bool SetConfigValue(ESteamNetworkingConfigValue value, ESteamNetworkingConfigScope scopeType, IntPtr scopeObj, ESteamNetworkingConfigDataType dataType, ReadOnlySpan<byte> arg)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetConfigValue(this.ptr, value, scopeType, scopeObj, dataType, arg);
    }

    /// <summary>
    /// Set a configuration value, using a struct to pass the value.<br/>
    /// (This is just a convenience shortcut; see below for the implementation and<br/>
    /// a little insight into how SteamNetworkingConfigValue_t is used when<br/>
    /// setting config options during listen socket and connection creation.)
    /// </summary>
    public bool SetConfigValueStruct(in SteamNetworkingConfigValue_t opt, ESteamNetworkingConfigScope scopeType, IntPtr scopeObj)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_SetConfigValueStruct(this.ptr, in opt, scopeType, scopeObj);
    }

    /// <summary>
    /// Get a configuration value.<br/>
    /// - eValue: which value to fetch<br/>
    /// - eScopeType: query setting on what type of object<br/>
    /// - eScopeArg: the object to query the setting for<br/>
    /// - pOutDataType: If non-NULL, the data type of the value is returned.<br/>
    /// - pResult: Where to put the result.  Pass NULL to query the required buffer size.  (k_ESteamNetworkingGetConfigValue_BufferTooSmall will be returned.)<br/>
    /// - cbResult: IN: the size of your buffer.  OUT: the number of bytes filled in or required.
    /// </summary>
    public ESteamNetworkingGetConfigValueResult GetConfigValue(ESteamNetworkingConfigValue value, ESteamNetworkingConfigScope scopeType, IntPtr scopeObj, out ESteamNetworkingConfigDataType outDataType, Span<byte> result, ref SizeT resultSize)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_GetConfigValue(this.ptr, value, scopeType, scopeObj, out outDataType, result, ref resultSize);
    }

    /// <summary>
    /// Get info about a configuration value.  Returns the name of the value,<br/>
    /// or NULL if the value doesn't exist.  Other output parameters can be NULL<br/>
    /// if you do not need them.
    /// </summary>
    public string? GetConfigValueInfo(ESteamNetworkingConfigValue value, out ESteamNetworkingConfigDataType outDataType, out ESteamNetworkingConfigScope outScope)
    {
        string? result = null;
        IntPtr configPtr = Native.SteamAPI_ISteamNetworkingUtils_GetConfigValueInfo(this.ptr, value, out outDataType, out outScope);

        if (configPtr != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(configPtr);
        }

        return result;
    }

    /// <summary>
    /// <para>
    /// Iterate the list of all configuration values in the current environment that it might<br/>
    /// be possible to display or edit using a generic UI.  To get the first iterable value,<br/>
    /// pass k_ESteamNetworkingConfig_Invalid.  Returns k_ESteamNetworkingConfig_Invalid<br/>
    /// to signal end of list.
    /// </para>
    ///
    /// <para>
    /// The bEnumerateDevVars argument can be used to include "dev" vars.  These are vars that<br/>
    /// are recommended to only be editable in "debug" or "dev" mode and typically should not be<br/>
    /// shown in a retail environment where a malicious local user might use this to cheat.
    /// </para>
    /// </summary>
    public ESteamNetworkingConfigValue IterateGenericEditableConfigValues(ESteamNetworkingConfigValue current, bool enumerateDevVars)
    {
        return Native.SteamAPI_ISteamNetworkingUtils_IterateGenericEditableConfigValues(this.ptr, current, enumerateDevVars);
    }

    /// <summary>
    /// String conversions.  You'll usually access these using the respective<br/>
    /// inline methods.
    /// </summary>
    public string SteamNetworkingIPAddr_ToString(in SteamNetworkingIPAddr addr, bool withPort)
    {
        Span<byte> raw = stackalloc byte[SteamNetworkingIPAddr.MaxString];
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingIPAddr_ToString(in addr, raw, (SizeT)raw.Length, withPort);
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(this.ptr, in addr, raw, (uint)raw.Length, withPort);
#endif
        return Utf8StringHelper.NullTerminatedSpanToString(raw);
    }

    /// <summary>
    /// String conversions.  You'll usually access these using the respective<br/>
    /// inline methods.
    /// </summary>
    public bool SteamNetworkingIPAddr_ParseString(ref SteamNetworkingIPAddr addr, string str)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return Native.SteamNetworkingIPAddr_ParseString(ref addr, str);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(this.ptr, ref addr, str);
#endif
    }

    /// <summary>
    /// String conversions.  You'll usually access these using the respective<br/>
    /// inline methods.
    /// </summary>
    public ESteamNetworkingFakeIPType SteamNetworkingIPAddr_GetFakeIPType(in SteamNetworkingIPAddr addr)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return Native.SteamNetworkingIPAddr_GetFakeIPType(in addr);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType(this.ptr, in addr);
#endif
    }

    /// <summary>
    /// String conversions.  You'll usually access these using the respective<br/>
    /// inline methods.
    /// </summary>
    public string SteamNetworkingIdentity_ToString(in SteamNetworkingIdentity identity)
    {
        Span<byte> raw = stackalloc byte[SteamNetworkingIdentity.MaxString];
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamNetworkingIdentity_ToString(in identity, raw, (SizeT)raw.Length);
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(this.ptr, in identity, raw, (uint)raw.Length);
#endif
        return Utf8StringHelper.NullTerminatedSpanToString(raw);
    }

    /// <summary>
    /// String conversions.  You'll usually access these using the respective<br/>
    /// inline methods.
    /// </summary>
    public bool SteamNetworkingIdentity_ParseString(ref SteamNetworkingIdentity identity, string str)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        SizeT sizeofIdentity;
        unsafe
        {
            sizeofIdentity = (SizeT)sizeof(SteamNetworkingIdentity);
        }

        return Native.SteamNetworkingIdentity_ParseString(ref identity, sizeofIdentity, str);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(this.ptr, ref identity, str);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case SteamRelayNetworkStatus_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<SteamRelayNetworkStatus_t>();
                    this.SteamRelayNetworkStatusChanged?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamNetworkingUtils.OnDispatch()");
                break;
        }
    }

#endif
}
