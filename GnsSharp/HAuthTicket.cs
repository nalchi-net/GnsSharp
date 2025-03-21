// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle to an user authentication ticket.<br/>
/// Return type of <see cref="ISteamUser.GetAuthSessionTicket"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HAuthTicket(uint handle) : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
{
    public static readonly HAuthTicket Invalid = new(0);

    public uint Handle = handle;

    public static implicit operator HAuthTicket(uint handle) => new(handle);

    public static implicit operator uint(HAuthTicket socket) => socket.Handle;

    public static bool operator ==(HAuthTicket conn1, HAuthTicket conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HAuthTicket conn1, HAuthTicket conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(HAuthTicket conn1, HAuthTicket conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(HAuthTicket conn1, HAuthTicket conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(HAuthTicket conn1, HAuthTicket conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(HAuthTicket conn1, HAuthTicket conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(HAuthTicket other) => this == other;

    public override readonly bool Equals(object? obj) => obj is HAuthTicket conn && this == conn;

    public readonly int CompareTo(HAuthTicket other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
