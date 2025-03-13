// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// A struct used to describe a "fake IP" we have been assigned to<br/>
/// use as an identifier.  This callback is posted when<br/>
/// ISteamNetworkingSoockets::BeginAsyncRequestFakeIP completes.<br/>
/// See also ISteamNetworkingSockets::GetFakeIP
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamNetworkingFakeIPResult_t
{
    public const int CallbackId = Constants.SteamNetworkingSocketsCallbacks + 3;

    /// <summary>
    /// <para>
    /// Status/result of the allocation request.  Possible failure values are:<br/>
    /// - k_EResultBusy - you called GetFakeIP but the request has not completed.<br/>
    /// - k_EResultInvalidParam - you called GetFakeIP with an invalid port index<br/>
    /// - k_EResultLimitExceeded - You asked for too many ports, or made an<br/>
    ///   additional request after one had already succeeded<br/>
    /// - k_EResultNoMatch - GetFakeIP was called, but no request has been made
    /// </para>
    ///
    /// <para>
    /// Note that, with the exception of k_EResultBusy (if you are polling),<br/>
    /// it is highly recommended to treat all failures as fatal.
    /// </para>
    /// </summary>
    public EResult Result;

    /// <summary>
    /// Local identity of the ISteamNetworkingSockets object that made<br/>
    /// this request and is assigned the IP.  This is needed in the callback<br/>
    /// in the case where there are multiple ISteamNetworkingSockets objects.<br/>
    /// (E.g. one for the user, and another for the local gameserver).
    /// </summary>
    public SteamNetworkingIdentity Identity;

    /// <summary>
    /// Fake IPv4 IP address that we have been assigned.  NOTE: this<br/>
    /// IP address is not exclusively ours!  Steam tries to avoid sharing<br/>
    /// IP addresses, but this may not always be possible.  The IP address<br/>
    /// may be currently in use by another host, but with different port(s).<br/>
    /// The exact same IP:port address may have been used previously.<br/>
    /// Steam tries to avoid reusing ports until they have not been in use for<br/>
    /// some time, but this may not always be possible.
    /// </summary>
    public uint Ip;

    /// <summary>
    /// <para>
    /// Port number(s) assigned to us.  Only the first entries will contain<br/>
    /// nonzero values.  Entries corresponding to ports beyond what was<br/>
    /// allocated for you will be zero.
    /// </para>
    ///
    /// <para>
    /// (NOTE: At the time of this writing, the maximum number of ports you may<br/>
    /// request is 4.)
    /// </para>
    /// </summary>
    public Array8<ushort> Ports;
}
