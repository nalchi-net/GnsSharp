// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Unique identifier for a depot.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DepotId_t(uint id) : IEquatable<DepotId_t>, IComparable<DepotId_t>
{
    public static readonly DepotId_t Invalid = new(0);

    public uint Id = id;

    public static implicit operator DepotId_t(uint handle) => new(handle);

    public static implicit operator uint(DepotId_t socket) => socket.Id;

    public static bool operator ==(DepotId_t conn1, DepotId_t conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(DepotId_t conn1, DepotId_t conn2) => conn1.Id != conn2.Id;

    public static bool operator <(DepotId_t conn1, DepotId_t conn2) => conn1.Id < conn2.Id;

    public static bool operator >(DepotId_t conn1, DepotId_t conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(DepotId_t conn1, DepotId_t conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(DepotId_t conn1, DepotId_t conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(DepotId_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is DepotId_t conn && this == conn;

    public readonly int CompareTo(DepotId_t other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
