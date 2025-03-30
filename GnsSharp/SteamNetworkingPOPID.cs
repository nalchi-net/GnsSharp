// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Identifier used for a network location point of presence.  (E.g. a Valve data center.)<br/>
/// Typically you won't need to directly manipulate these.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamNetworkingPOPID(uint id) : IEquatable<SteamNetworkingPOPID>, IComparable<SteamNetworkingPOPID>
{
    /// <summary>
    /// The POPID "dev" is used in non-production environments for testing.
    /// </summary>
    public static readonly SteamNetworkingPOPID Dev = 6579574u;

    public uint Id = id;

    public static implicit operator SteamNetworkingPOPID(uint id) => new(id);

    public static implicit operator uint(SteamNetworkingPOPID socket) => socket.Id;

    public static bool operator ==(SteamNetworkingPOPID conn1, SteamNetworkingPOPID conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(SteamNetworkingPOPID conn1, SteamNetworkingPOPID conn2) => conn1.Id != conn2.Id;

    public static bool operator <(SteamNetworkingPOPID conn1, SteamNetworkingPOPID conn2) => conn1.Id < conn2.Id;

    public static bool operator >(SteamNetworkingPOPID conn1, SteamNetworkingPOPID conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(SteamNetworkingPOPID conn1, SteamNetworkingPOPID conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(SteamNetworkingPOPID conn1, SteamNetworkingPOPID conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(SteamNetworkingPOPID other) => this == other;

    public override readonly bool Equals(object? obj) => obj is SteamNetworkingPOPID conn && this == conn;

    public readonly int CompareTo(SteamNetworkingPOPID other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
