// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// handle to a communication pipe to the Steam client
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HSteamPipe(int handle) : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
{
    public int Handle = handle;

    public static explicit operator HSteamPipe(int handle) => new(handle);

    public static explicit operator int(HSteamPipe socket) => socket.Handle;

    public static bool operator ==(HSteamPipe conn1, HSteamPipe conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HSteamPipe conn1, HSteamPipe conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(HSteamPipe conn1, HSteamPipe conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(HSteamPipe conn1, HSteamPipe conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(HSteamPipe conn1, HSteamPipe conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(HSteamPipe conn1, HSteamPipe conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(HSteamPipe other) => this == other;

    public override readonly bool Equals(object? obj) => obj is HSteamPipe conn && this == conn;

    public readonly int CompareTo(HSteamPipe other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
