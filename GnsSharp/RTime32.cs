// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// RTime32.  Seconds elapsed since Jan 1 1970, i.e. unix timestamp.<br/>
/// It's the same as time_t, but it is always 32-bit and unsigned.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RTime32(uint seconds) : IEquatable<RTime32>, IComparable<RTime32>
{
    public uint Seconds = seconds;

    public static implicit operator RTime32(uint seconds) => new(seconds);

    public static implicit operator uint(RTime32 socket) => socket.Seconds;

    public static bool operator ==(RTime32 conn1, RTime32 conn2) => conn1.Seconds == conn2.Seconds;

    public static bool operator !=(RTime32 conn1, RTime32 conn2) => conn1.Seconds != conn2.Seconds;

    public static bool operator <(RTime32 conn1, RTime32 conn2) => conn1.Seconds < conn2.Seconds;

    public static bool operator >(RTime32 conn1, RTime32 conn2) => conn1.Seconds > conn2.Seconds;

    public static bool operator <=(RTime32 conn1, RTime32 conn2) => conn1.Seconds <= conn2.Seconds;

    public static bool operator >=(RTime32 conn1, RTime32 conn2) => conn1.Seconds >= conn2.Seconds;

    public readonly bool Equals(RTime32 other) => this == other;

    public override readonly bool Equals(object? obj) => obj is RTime32 conn && this == conn;

    public readonly int CompareTo(RTime32 other) => this.Seconds.CompareTo(other.Seconds);

    public override readonly int GetHashCode() => this.Seconds.GetHashCode();

    public override readonly string ToString() => this.Seconds.ToString();
}
