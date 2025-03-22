// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle that you will receive when requesting server list.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HServerListRequest(IntPtr handle) : IEquatable<HServerListRequest>, IComparable<HServerListRequest>
{
    public static readonly HServerListRequest Invalid = new(0);

    public IntPtr Handle = handle;

    public static implicit operator HServerListRequest(IntPtr handle) => new(handle);

    public static implicit operator IntPtr(HServerListRequest socket) => socket.Handle;

    public static bool operator ==(HServerListRequest conn1, HServerListRequest conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(HServerListRequest conn1, HServerListRequest conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(HServerListRequest conn1, HServerListRequest conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(HServerListRequest conn1, HServerListRequest conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(HServerListRequest conn1, HServerListRequest conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(HServerListRequest conn1, HServerListRequest conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(HServerListRequest other) => this == other;

    public override readonly bool Equals(object? obj) => obj is HServerListRequest conn && this == conn;

    public readonly int CompareTo(HServerListRequest other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
