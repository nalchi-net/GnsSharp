// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// handle to single instance of a steam user
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HSteamUser(int handle) : IEquatable<HSteamUser>, IComparable<HSteamUser>
{
    public int Handle = handle;

    public static implicit operator HSteamUser(int handle) => new(handle);

    public static implicit operator int(HSteamUser socket) => socket.Handle;

    public static bool operator ==(HSteamUser conn1, HSteamUser conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamUser conn1, HSteamUser conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(HSteamUser conn1, HSteamUser conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(HSteamUser conn1, HSteamUser conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(HSteamUser conn1, HSteamUser conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(HSteamUser conn1, HSteamUser conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(HSteamUser other) => this == other;

    public override readonly bool Equals(object? obj) => obj is HSteamUser conn && this == conn;

    public readonly int CompareTo(HSteamUser other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
