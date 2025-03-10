// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Different methods of describing the identity of a network host.
/// </summary>
public enum ESteamNetworkingIdentityType : int
{
    /// <summary>
    /// Dummy/empty/invalid.
    /// Please note that if we parse a string that we don't recognize
    /// but that appears reasonable, we will NOT use this type.  Instead
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
    /// Use their IP address (and port) as their "identity".
    /// These types of identities are always unauthenticated.
    /// They are useful for porting plain sockets code, and other
    /// situations where you don't care about authentication.  In this
    /// case, the local identity will be "localhost",
    /// and the remote address will be their network address.
    ///
    /// We use the same type for either IPv4 or IPv6, and
    /// the address is always store as IPv6.  We use IPv4
    /// mapped addresses to handle IPv4.
    /// </summary>
    IPAddress = 1,

    /// <summary>
    /// Generic string blobs.  It's up to your app to interpret this.
    /// This library can tell you if the remote host presented a certificate
    /// signed by somebody you have chosen to trust, with this identity on it.
    /// It's up to you to ultimately decide what this identity means.
    /// </summary>
    GenericString = 2,

    /// <summary>
    /// Generic binary blobs.  It's up to your app to interpret this.
    /// This library can tell you if the remote host presented a certificate
    /// signed by somebody you have chosen to trust, with this identity on it.
    /// It's up to you to ultimately decide what this identity means.
    /// </summary>
    GenericBytes = 3,

    /// <summary>
    /// This identity type is used when we parse a string that looks like is a
    /// valid identity, just of a kind that we don't recognize.  In this case, we
    /// can often still communicate with the peer!  Allowing such identities
    /// for types we do not recognize useful is very useful for forward
    /// compatibility.
    /// </summary>
    UnknownType = 4,
}
