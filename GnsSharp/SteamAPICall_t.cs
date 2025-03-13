// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// handle to a Steam API call
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamAPICall_t(ulong handle) : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
{
    public static readonly SteamAPICall_t Invalid = new(0);

    public ulong Handle = handle;

    public static explicit operator SteamAPICall_t(ulong handle) => new(handle);

    public static explicit operator ulong(SteamAPICall_t socket) => socket.Handle;

    public static bool operator ==(SteamAPICall_t conn1, SteamAPICall_t conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(SteamAPICall_t conn1, SteamAPICall_t conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(SteamAPICall_t conn1, SteamAPICall_t conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(SteamAPICall_t conn1, SteamAPICall_t conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(SteamAPICall_t conn1, SteamAPICall_t conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(SteamAPICall_t conn1, SteamAPICall_t conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(SteamAPICall_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is SteamAPICall_t conn && this == conn;

    public readonly int CompareTo(SteamAPICall_t other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
