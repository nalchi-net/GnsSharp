// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct HSteamListenSocket : IEquatable<HSteamListenSocket>, IComparable<HSteamListenSocket>
{
    public uint Handle;

    public HSteamListenSocket(uint handle)
    {
        this.Handle = handle;
    }

    public static bool operator ==(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle != conn2.Handle;

    public readonly bool Equals(HSteamListenSocket other) => this == other;

    public override readonly bool Equals(object? obj)
    {
        return obj is HSteamListenSocket conn && this == conn;
    }

    public int CompareTo(HSteamListenSocket other)
    {
        return this.Handle.CompareTo(other.Handle);
    }

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
