// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle used to identify a connection to a remote host.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HSteamNetConnection(uint handle) : IEquatable<HSteamNetConnection>, IComparable<HSteamNetConnection>
{
    public static readonly HSteamNetConnection Invalid = new(0);

    public uint Handle = handle;

    public static implicit operator HSteamNetConnection(uint handle) => new(handle);

    public static implicit operator uint(HSteamNetConnection socket) => socket.Handle;

    public static bool operator ==(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(HSteamNetConnection other) => this == other;

    public override readonly bool Equals(object? obj) => obj is HSteamNetConnection conn && this == conn;

    public readonly int CompareTo(HSteamNetConnection other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
