// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// An abstract way to represent the identity of a network host.  All identities can<br/>
/// be represented as simple string.  Furthermore, this string representation is actually<br/>
/// used on the wire in several places, even though it is less efficient, in order to<br/>
/// facilitate forward compatibility.  (Old client code can handle an identity type that<br/>
/// it doesn't understand.)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SteamNetworkingIdentity : IEquatable<SteamNetworkingIdentity>
{
    /// <summary>
    /// Type of identity.
    /// </summary>
    public ESteamNetworkingIdentityType Type;

    /// <summary>
    /// <para>
    /// Internal representation.  Don't access this directly, use the accessors!
    /// </para>
    ///
    /// <para>
    /// Number of bytes that are relevant below.  This MUST ALWAYS be<br/>
    /// set.  (Use the accessors!)  This is important to enable old code to work<br/>
    /// with new identity types.
    /// </para>
    /// </summary>
    public int Size;

    /// <summary>
    /// Internal representation.  Don't access this directly, use the accessors.
    /// </summary>
    public IdentityUnion Identity;

    /// <summary>
    /// Max length of the buffer needed to hold any identity, formatted in string format by ToString.
    /// </summary>
    internal const int MaxString = 128;

    /// <summary>
    /// Max length of the string for generic string identities.  Including terminating '\0'.
    /// </summary>
    private const int MaxGenericString = 32;

    /// <summary>
    /// Including terminating '\0'.
    /// </summary>
    private const int MaxXboxPairwiseID = 33;

    private const int MaxGenericBytes = 32;

    /// <summary>
    /// See if two identities are identical
    /// </summary>
    public static bool operator ==(in SteamNetworkingIdentity ident1, in SteamNetworkingIdentity ident2)
    {
        return Native.SteamAPI_SteamNetworkingIdentity_IsEqualTo(in ident1, in ident2);
    }

    /// <summary>
    /// See if two identities are not identical
    /// </summary>
    public static bool operator !=(in SteamNetworkingIdentity ident1, in SteamNetworkingIdentity ident2) => !(ident1 == ident2);

    /// <summary>
    /// See if two identities are identical
    /// </summary>
    public bool Equals(SteamNetworkingIdentity other)
    {
        return this == other;
    }

    /// <summary>
    /// See if two objects are identical
    /// </summary>
    public override readonly bool Equals(object? obj)
    {
        return obj is SteamNetworkingIdentity identity && this == identity;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Type, this.Size, this.Identity);
    }

    /// <summary>
    /// Clears the identity.
    /// </summary>
    public void Clear()
    {
        Native.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
    }

    /// <returns>Returns true if we are the invalid type.</returns>
    public bool IsInvalid()
    {
        return Native.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
    }

    public void SetSteamID(ulong steamID)
    {
        Native.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, steamID);
    }

    /// <returns>Returns Black CSteamID (!IsValid()) if identity is not a SteamID.</returns>
    public ulong GetSteamID()
    {
        return Native.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
    }

    /// <param name="steamID">SteamID as raw 64-bit number</param>
    public void SetSteamID64(ulong steamID)
    {
        Native.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
    }

    /// <returns>Returns 0 if identity is not SteamID</returns>
    public ulong GetSteamID64()
    {
        return Native.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
    }

    /// <returns>Returns false if invalid length</returns>
    public bool SetXboxPairwiseID(string str)
    {
        return Native.SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref this, str);
    }

    /// <returns> Returns null if not Xbox ID</returns>
    public string? GetXboxPairwiseID()
    {
        string? result = null;

        IntPtr raw = Native.SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref this);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
    }

    public void SetPSNID(ulong id)
    {
        this.Type = ESteamNetworkingIdentityType.SonyPSN;
        this.Size = sizeof(PsnIdType);
        this.Identity.PsnID = id;
    }

    /// <returns>Returns 0 if not PSN.</returns>
    public ulong GetPSNID()
    {
        return this.Type == ESteamNetworkingIdentityType.SonyPSN ? this.Identity.PsnID : 0;
    }

    /// <summary>
    /// Set to specified IP:port
    /// </summary>
    public void SetIPAddr(in SteamNetworkingIPAddr addr)
    {
        Native.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, in addr);
    }

    /// <returns>Returns an empty span if we are not an IP address.</returns>
    public ReadOnlySpan<SteamNetworkingIPAddr> GetIPAddr()
    {
        var result = ReadOnlySpan<SteamNetworkingIPAddr>.Empty;

        IntPtr raw = Native.SteamAPI_SteamNetworkingIdentity_GetIPAddr(ref this);
        unsafe
        {
            if (raw != IntPtr.Zero)
            {
                result = new ReadOnlySpan<SteamNetworkingIPAddr>((void*)raw, 1);
            }
        }

        return result;
    }

    /// <summary>
    /// Set to specified IPv4:port
    /// </summary>
    public void SetIPv4Addr(uint ipv4, ushort port)
    {
        this.Type = ESteamNetworkingIdentityType.IPAddress;
        unsafe
        {
            this.Size = sizeof(SteamNetworkingIPAddr);
        }

        this.Identity.Ip.SetIPv4(ipv4, port);
    }

    /// <returns>Returns 0 if we are not an IPv4 address.</returns>
    public uint GetIPv4()
    {
        return this.Type == ESteamNetworkingIdentityType.IPAddress ? this.Identity.Ip.GetIPv4() : 0;
    }

    public ESteamNetworkingFakeIPType GetFakeIPType()
    {
        return this.Type == ESteamNetworkingIdentityType.IPAddress ? this.Identity.Ip.GetFakeIPType() : ESteamNetworkingFakeIPType.Invalid;
    }

    public bool IsFakeIP()
    {
        return this.GetFakeIPType() > ESteamNetworkingFakeIPType.NotFake;
    }

    /// <summary>
    /// "localhost" is equivalent for many purposes to "anonymous."  Our remote<br/>
    /// will identify us by the network address we use.<br/>
    /// Set to localhost.  (We always use IPv6 ::1 for this, not 127.0.0.1)
    /// </summary>
    public void SetLocalHost()
    {
        Native.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
    }

    /// <returns>Return true if this identity is localhost.</returns>
    public bool IsLocalHost()
    {
        return Native.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
    }

    /// <returns>Returns false if invalid length.</returns>
    public bool SetGenericString(string str)
    {
        return Native.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, str);
    }

    /// <returns>Returns null if not generic string type.</returns>
    public string? GetGenericString()
    {
        string? result = null;

        IntPtr raw = Native.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
    }

    /// <returns>Returns false if invalid size.</returns>
    public bool SetGenericBytes(ReadOnlySpan<byte> data, uint len)
    {
        return Native.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, len);
    }

    /// <returns>Returns an empty span if not generic bytes type.</returns>
    public ReadOnlySpan<byte> GetGenericBytes(ref int len)
    {
        var result = ReadOnlySpan<byte>.Empty;

        IntPtr raw = Native.SteamAPI_SteamNetworkingIdentity_GetGenericBytes(ref this, ref len);
        unsafe
        {
            if (raw != IntPtr.Zero)
            {
                result = new ReadOnlySpan<byte>((void*)raw, len);
            }
        }

        return result;
    }

    /// <summary>
    /// <para>
    /// Print to a human-readable string.  This is suitable for debug messages<br/>
    /// or any other time you need to encode the identity as a string.  It has a<br/>
    /// URL-like format (type:(type-data)).  Your buffer should be at least<br/>
    /// k_cchMaxString bytes big to avoid truncation.
    /// </para>
    ///
    /// <para>
    /// See also SteamNetworkingIPAddrRender
    /// </para>
    /// </summary>
    public override string ToString()
    {
        Span<byte> raw = stackalloc byte[MaxString];
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamAPI_SteamNetworkingIdentity_ToString(in this, raw, (SizeT)raw.Length);
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_SteamNetworkingIdentity_ToString(ref this, raw, (uint)raw.Length);
#endif
        return Utf8StringHelper.NullTerminatedSpanToString(raw);
    }

    /// <summary>
    /// Parse back a string that was generated using ToString.  If we don't understand the<br/>
    /// string, but it looks "reasonable" (it matches the pattern type:(type-data) and doesn't<br/>
    /// have any funky characters, etc), then we will return true, and the type is set to<br/>
    /// k_ESteamNetworkingIdentityType_UnknownType.  false will only be returned if the string<br/>
    /// looks invalid.
    /// </summary>
    public bool ParseString(string str)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        SizeT sizeofIdentity;
        unsafe
        {
            sizeofIdentity = (SizeT)sizeof(SteamNetworkingIdentity);
        }

        return Native.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, sizeofIdentity, str);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, str);
#endif
    }

    /// <summary>
    /// Internal representation.  Don't access this directly, use the accessors.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct IdentityUnion
    {
        [FieldOffset(0)]
        public ulong SteamID64;

        [FieldOffset(0)]
        public PsnIdType PsnID;

        [FieldOffset(0)]
        public Array32<byte> GenericString;

        [FieldOffset(0)]
        public Array33<byte> XboxPairwiseID;

        [FieldOffset(0)]
        public Array32<byte> GenericBytes;

        [FieldOffset(0)]
        public Array128<byte> UnknownRawString;

        [FieldOffset(0)]
        public SteamNetworkingIPAddr Ip;

        /// <summary>
        /// Pad structure to leave easy room for future expansion.
        /// </summary>
        [FieldOffset(0)]
        private Array32<uint> reserved;

        public override readonly int GetHashCode()
        {
            return this.UnknownRawString.GetHashCode();
        }
    }
}
