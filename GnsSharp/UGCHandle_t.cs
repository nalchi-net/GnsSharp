// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// A unique handle to a piece of user generated content.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct UGCHandle_t(ulong handle) : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
{
    public static readonly UGCHandle_t Invalid = new(0xfffffffffffffffful);

    public ulong Handle = handle;

    public static implicit operator UGCHandle_t(ulong handle) => new(handle);

    public static implicit operator ulong(UGCHandle_t socket) => socket.Handle;

    public static bool operator ==(UGCHandle_t conn1, UGCHandle_t conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(UGCHandle_t conn1, UGCHandle_t conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(UGCHandle_t conn1, UGCHandle_t conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(UGCHandle_t conn1, UGCHandle_t conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(UGCHandle_t conn1, UGCHandle_t conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(UGCHandle_t conn1, UGCHandle_t conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(UGCHandle_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is UGCHandle_t conn && this == conn;

    public readonly int CompareTo(UGCHandle_t other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
