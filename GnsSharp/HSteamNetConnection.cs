// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct HSteamNetConnection : IEquatable<HSteamNetConnection>, IComparable<HSteamNetConnection>
{
    public uint Handle;

    public HSteamNetConnection(uint handle)
    {
        this.Handle = handle;
    }

    public static bool operator ==(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Handle != conn2.Handle;

    public readonly bool Equals(HSteamNetConnection other) => this == other;

    public override readonly bool Equals(object? obj)
    {
        return obj is HSteamNetConnection conn && this == conn;
    }

    public int CompareTo(HSteamNetConnection other)
    {
        return this.Handle.CompareTo(other.Handle);
    }

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
