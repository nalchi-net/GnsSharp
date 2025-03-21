// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle used when asynchronously writing to Steam Cloud.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct UGCFileWriteStreamHandle_t(ulong handle) : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
{
    public static readonly UGCFileWriteStreamHandle_t Invalid = new(0xfffffffffffffffful);

    public ulong Handle = handle;

    public static implicit operator UGCFileWriteStreamHandle_t(ulong handle) => new(handle);

    public static implicit operator ulong(UGCFileWriteStreamHandle_t socket) => socket.Handle;

    public static bool operator ==(UGCFileWriteStreamHandle_t conn1, UGCFileWriteStreamHandle_t conn2) => conn1.Handle == conn2.Handle;

    public static bool operator !=(UGCFileWriteStreamHandle_t conn1, UGCFileWriteStreamHandle_t conn2) => conn1.Handle != conn2.Handle;

    public static bool operator <(UGCFileWriteStreamHandle_t conn1, UGCFileWriteStreamHandle_t conn2) => conn1.Handle < conn2.Handle;

    public static bool operator >(UGCFileWriteStreamHandle_t conn1, UGCFileWriteStreamHandle_t conn2) => conn1.Handle > conn2.Handle;

    public static bool operator <=(UGCFileWriteStreamHandle_t conn1, UGCFileWriteStreamHandle_t conn2) => conn1.Handle <= conn2.Handle;

    public static bool operator >=(UGCFileWriteStreamHandle_t conn1, UGCFileWriteStreamHandle_t conn2) => conn1.Handle >= conn2.Handle;

    public readonly bool Equals(UGCFileWriteStreamHandle_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is UGCFileWriteStreamHandle_t conn && this == conn;

    public readonly int CompareTo(UGCFileWriteStreamHandle_t other) => this.Handle.CompareTo(other.Handle);

    public override readonly int GetHashCode() => this.Handle.GetHashCode();

    public override readonly string ToString() => this.Handle.ToString();
}
