// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// Store an IP and port.  IPv6 is always used; IPv4 is represented using
/// "IPv4-mapped" addresses: IPv4 aa.bb.cc.dd => IPv6 ::ffff:aabb:ccdd
/// (RFC 4291 section 2.5.5.2.)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SteamNetworkingIPAddr : IEquatable<SteamNetworkingIPAddr>
{
    public IpUnion Ip;

    public ushort Port;

    /// <summary>
    /// Max length of the buffer needed to hold IP formatted using ToString, including '\0'
    /// ([0123:4567:89ab:cdef:0123:4567:89ab:cdef]:12345)
    /// </summary>
    private const int MaxString = 48;

    /// <summary>
    /// See if two addresses are identical
    /// </summary>
    public static bool operator ==(in SteamNetworkingIPAddr addr1, in SteamNetworkingIPAddr addr2)
    {
        return Native.SteamAPI_SteamNetworkingIPAddr_IsEqualTo(in addr1, in addr2);
    }

    /// <summary>
    /// See if two addresses are not identical
    /// </summary>
    public static bool operator !=(in SteamNetworkingIPAddr addr1, in SteamNetworkingIPAddr addr2) => !(addr1 == addr2);

    /// <summary>
    /// See if two addresses are identical
    /// </summary>
    public readonly bool Equals(SteamNetworkingIPAddr other)
    {
        return this == other;
    }

    /// <summary>
    /// See if two objects are identical
    /// </summary>
    public override readonly bool Equals(object? obj)
    {
        return obj is SteamNetworkingIPAddr addr && this == addr;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(this.Ip, this.Port);
    }

    /// <summary>
    /// Set everything to zero.  E.g. [::]:0
    /// </summary>
    public void Clear()
    {
        Native.SteamAPI_SteamNetworkingIPAddr_Clear(ref this);
    }

    /// <returns>Return true if the IP is ::0.  (Doesn't check port.)</returns>
    public bool IsIPv6AllZeros()
    {
        return Native.SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref this);
    }

    /// <summary>
    /// Set IPv6 address.  IP is interpreted as bytes, so there are no endian issues.  (Same as inaddr_in6.)  The IP can be a mapped IPv4 address
    /// </summary>
    public void SetIPv6(ReadOnlySpan<byte> ipv6, ushort port)
    {
        Native.SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref this, ipv6, port);
    }

    /// <summary>
    /// Sets to IPv4 mapped address.  IP and port are in host byte order.
    /// </summary>
    public void SetIPv4(uint ip, ushort port)
    {
        Native.SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref this, ip, port);
    }

    /// <returns>Return true if IP is mapped IPv4</returns>
    public bool IsIPv4()
    {
        return Native.SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref this);
    }

    /// <returns>Returns IP in host byte order (e.g. aa.bb.cc.dd as 0xaabbccdd).  Returns 0 if IP is not mapped IPv4.</returns>
    public uint GetIPv4()
    {
        return Native.SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref this);
    }

    /// <summary>
    /// Set to the IPv6 localhost address ::1, and the specified port.
    /// </summary>
    public void SetIPv6LocalHost(ushort port = 0)
    {
        Native.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref this, port);
    }

    /// <returns>Return true if this identity is localhost.  (Either IPv6 ::1, or IPv4 127.0.0.1)</returns>
    public bool IsLocalHost()
    {
        return Native.SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref this);
    }

    /// <summary>
    /// Print to a string, with or without the port.  Mapped IPv4 addresses are printed
    /// as dotted decimal (12.34.56.78), otherwise this will print the canonical
    /// form according to RFC5952.  If you include the port, IPv6 will be surrounded by
    /// brackets, e.g. [::1:2]:80.  Your buffer should be at least k_cchMaxString bytes
    /// to avoid truncation
    ///
    /// See also SteamNetworkingIdentityRender
    /// </summary>
    public string ToString(bool withPort)
    {
        Span<byte> raw = stackalloc byte[MaxString];
#if GNS_SHARP_OPENSOURCE_GNS
        Native.SteamAPI_SteamNetworkingIPAddr_ToString(in this, raw, (UIntPtr)raw.Length, withPort);
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_SteamNetworkingIPAddr_ToString(ref this, raw, (uint)raw.Length, withPort);
#endif

        int nullIndex = raw.IndexOf((byte)0);
        ReadOnlySpan<byte> trunc = raw.Slice(0, nullIndex);

        return Encoding.UTF8.GetString(trunc);
    }

    /// <summary>
    /// Print to a string, with the port.  Mapped IPv4 addresses are printed
    /// as dotted decimal (12.34.56.78), otherwise this will print the canonical
    /// form according to RFC5952.  If you include the port, IPv6 will be surrounded by
    /// brackets, e.g. [::1:2]:80.  Your buffer should be at least k_cchMaxString bytes
    /// to avoid truncation
    ///
    /// See also SteamNetworkingIdentityRender
    /// </summary>
    public override string ToString()
    {
        return this.ToString(true);
    }

    /// <summary>
    /// Parse an IP address and optional port.  If a port is not present, it is set to 0.
    /// (This means that you cannot tell if a zero port was explicitly specified.)
    /// </summary>
    public bool ParseString(string str)
    {
        return Native.SteamAPI_SteamNetworkingIPAddr_ParseString(ref this, str);
    }

    /// <summary>
    /// Classify address as FakeIP.  This function never returns
    /// k_ESteamNetworkingFakeIPType_Invalid.
    /// </summary>
    public ESteamNetworkingFakeIPType GetFakeIPType()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        return Native.SteamNetworkingIPAddr_GetFakeIPType(in this);
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref this);
#endif
    }

    /// <returns>Return true if we are a FakeIP</returns>
    public bool IsFakeIP()
    {
        return this.GetFakeIPType() > ESteamNetworkingFakeIPType.NotFake;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IPv4MappedAddress
    {
        public ulong EightZeros;
        public ushort Two0000;
        public ushort TwoFFFF;

        /// <summary>
        /// NOTE: As bytes, i.e. network byte order
        /// </summary>
        public Array4<byte> Ip;

        [InlineArray(4)]
        public struct Array4<T>
        {
            private T elem;
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct IpUnion
    {
        [FieldOffset(0)]
        public Array16<byte> Ipv6;

        [FieldOffset(0)]
        public IPv4MappedAddress Ipv4Mapped;

        public override int GetHashCode()
        {
            return this.Ipv6.GetHashCode();
        }

        [InlineArray(16)]
        public struct Array16<T>
        {
            private T elem;
        }
    }
}
