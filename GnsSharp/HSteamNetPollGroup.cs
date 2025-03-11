// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct HSteamNetPollGroup : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
{
    public uint Handle;

    public HSteamNetPollGroup(uint handle)
    {
        this.Handle = handle;
    }

    public static bool operator ==(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle != conn2.Handle;

    public readonly bool Equals(HSteamNetPollGroup other) => this == other;

    public override readonly bool Equals(object? obj)
    {
        return obj is HSteamNetPollGroup conn && this == conn;
    }

    public int CompareTo(HSteamNetPollGroup other)
    {
        return this.Handle.CompareTo(other.Handle);
    }

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
