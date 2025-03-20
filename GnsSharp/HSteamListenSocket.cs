// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle used to identify a "listen socket".  Unlike traditional<br/>
/// Berkeley sockets, a listen socket and a connection are two<br/>
/// different abstractions.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HSteamListenSocket(uint handle) : IEquatable<HSteamListenSocket>, IComparable<HSteamListenSocket>
{
    public static readonly HSteamListenSocket Invalid = new(0);

    public uint Handle = handle;

    public static implicit operator HSteamListenSocket(uint handle) => new(handle);

    public static implicit operator uint(HSteamListenSocket socket) => socket.Handle;

    public static bool operator ==(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(HSteamListenSocket conn1, HSteamListenSocket conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(HSteamListenSocket other) => this == other;

    public override readonly bool Equals(object? obj) => obj is HSteamListenSocket conn && this == conn;

    public readonly int CompareTo(HSteamListenSocket other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
