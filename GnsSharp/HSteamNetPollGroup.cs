// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle used to identify a poll group, used to query many<br/>
/// connections at once efficiently.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HSteamNetPollGroup(uint handle) : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
{
    public static readonly HSteamNetPollGroup Invalid = new(0);

    public uint Handle = handle;

    public static explicit operator HSteamNetPollGroup(uint handle) => new(handle);

    public static explicit operator uint(HSteamNetPollGroup socket) => socket.Handle;

    public static bool operator ==(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(HSteamNetPollGroup conn1, HSteamNetPollGroup conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(HSteamNetPollGroup other) => this == other;

    public override readonly bool Equals(object? obj) => obj is HSteamNetPollGroup conn && this == conn;

    public readonly int CompareTo(HSteamNetPollGroup other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
