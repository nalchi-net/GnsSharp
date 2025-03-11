// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Different methods of describing the identity of a network host.
/// </summary>
public enum ESteamNetworkingIdentityType : int
{
    /// <summary>
    /// Dummy/empty/invalid.<br/>
    /// Please note that if we parse a string that we don't recognize<br/>
    /// but that appears reasonable, we will NOT use this type.  Instead<br/>
    /// we'll use ESteamNetworkingIdentityType.UnknownType.
    /// </summary>
    Invalid = 0,

    // Basic platform-specific identifiers.

    /// <summary>
    /// 64-bit CSteamID.
    /// </summary>
    SteamID = 16,

    /// <summary>
    /// Publisher-specific user identity, as string.
    /// </summary>
    XboxPairwiseID = 17,

    /// <summary>
    /// 64-bit ID.
    /// </summary>
    SonyPSN = 18,

    // Special identifiers.

    /// <summary>
    /// <para>
    /// Use their IP address (and port) as their "identity".<br/>
    /// These types of identities are always unauthenticated.<br/>
    /// They are useful for porting plain sockets code, and other<br/>
    /// situations where you don't care about authentication.  In this<br/>
    /// case, the local identity will be "localhost",<br/>
    /// and the remote address will be their network address.
    /// </para>
    ///
    /// <para>
    /// We use the same type for either IPv4 or IPv6, and<br/>
    /// the address is always store as IPv6.  We use IPv4<br/>
    /// mapped addresses to handle IPv4.
    /// </para>
    /// </summary>
    IPAddress = 1,

    /// <summary>
    /// Generic string blobs.  It's up to your app to interpret this.<br/>
    /// This library can tell you if the remote host presented a certificate<br/>
    /// signed by somebody you have chosen to trust, with this identity on it.<br/>
    /// It's up to you to ultimately decide what this identity means.
    /// </summary>
    GenericString = 2,

    /// <summary>
    /// Generic binary blobs.  It's up to your app to interpret this.<br/>
    /// This library can tell you if the remote host presented a certificate<br/>
    /// signed by somebody you have chosen to trust, with this identity on it.<br/>
    /// It's up to you to ultimately decide what this identity means.
    /// </summary>
    GenericBytes = 3,

    /// <summary>
    /// This identity type is used when we parse a string that looks like is a<br/>
    /// valid identity, just of a kind that we don't recognize.  In this case, we<br/>
    /// can often still communicate with the peer!  Allowing such identities<br/>
    /// for types we do not recognize useful is very useful for forward<br/>
    /// compatibility.
    /// </summary>
    UnknownType = 4,
}
