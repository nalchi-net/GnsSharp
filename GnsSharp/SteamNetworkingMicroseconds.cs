// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// A local timestamp.  You can subtract two timestamps to get the number of elapsed<br/>
/// microseconds.  This is guaranteed to increase over time during the lifetime<br/>
/// of a process, but not globally across runs.  You don't need to worry about<br/>
/// the value wrapping around.  Note that the underlying clock might not actually have<br/>
/// microsecond resolution.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamNetworkingMicroseconds(long usec) : IEquatable<SteamNetworkingMicroseconds>, IComparable<SteamNetworkingMicroseconds>
{
    public long Usec = usec;

    public static implicit operator SteamNetworkingMicroseconds(long usec) => new(usec);

    public static implicit operator long(SteamNetworkingMicroseconds socket) => socket.Usec;

    public static bool operator ==(SteamNetworkingMicroseconds conn1, SteamNetworkingMicroseconds conn2) => conn1.Usec == conn2.Usec;

    public static bool operator !=(SteamNetworkingMicroseconds conn1, SteamNetworkingMicroseconds conn2) => conn1.Usec != conn2.Usec;

    public static bool operator <(SteamNetworkingMicroseconds conn1, SteamNetworkingMicroseconds conn2) => conn1.Usec < conn2.Usec;

    public static bool operator >(SteamNetworkingMicroseconds conn1, SteamNetworkingMicroseconds conn2) => conn1.Usec > conn2.Usec;

    public static bool operator <=(SteamNetworkingMicroseconds conn1, SteamNetworkingMicroseconds conn2) => conn1.Usec <= conn2.Usec;

    public static bool operator >=(SteamNetworkingMicroseconds conn1, SteamNetworkingMicroseconds conn2) => conn1.Usec >= conn2.Usec;

    public readonly bool Equals(SteamNetworkingMicroseconds other) => this == other;

    public override readonly bool Equals(object? obj) => obj is SteamNetworkingMicroseconds conn && this == conn;

    public readonly int CompareTo(SteamNetworkingMicroseconds other) => this.Usec.CompareTo(other.Usec);

    public override readonly int GetHashCode() => this.Usec.GetHashCode();

    public override readonly string ToString() => this.Usec.ToString();
}
