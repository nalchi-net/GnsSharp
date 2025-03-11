// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct HSteamNetConnection : IEquatable<HSteamNetConnection>
{
    public uint Id;

    public HSteamNetConnection(uint id)
    {
        this.Id = id;
    }

    public static bool operator ==(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(HSteamNetConnection conn1, HSteamNetConnection conn2) => conn1.Id != conn2.Id;

    public readonly bool Equals(HSteamNetConnection other) => this == other;

    public override readonly bool Equals(object? obj)
    {
        return obj is HSteamNetConnection conn && this == conn;
    }

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
