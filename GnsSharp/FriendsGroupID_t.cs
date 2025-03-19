// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Friends group (tags) identifier.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct FriendsGroupID_t(short id) : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
{
    /// <summary>
    /// Invalid friends group identifier.
    /// </summary>
    public static readonly FriendsGroupID_t Invalid = new(-1);

    public short Id = id;

    public static explicit operator FriendsGroupID_t(short handle) => new(handle);

    public static explicit operator short(FriendsGroupID_t socket) => socket.Id;

    public static bool operator ==(FriendsGroupID_t conn1, FriendsGroupID_t conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(FriendsGroupID_t conn1, FriendsGroupID_t conn2) => conn1.Id != conn2.Id;

    public static bool operator <(FriendsGroupID_t conn1, FriendsGroupID_t conn2) => conn1.Id < conn2.Id;

    public static bool operator >(FriendsGroupID_t conn1, FriendsGroupID_t conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(FriendsGroupID_t conn1, FriendsGroupID_t conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(FriendsGroupID_t conn1, FriendsGroupID_t conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(FriendsGroupID_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is FriendsGroupID_t conn && this == conn;

    public readonly int CompareTo(FriendsGroupID_t other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
