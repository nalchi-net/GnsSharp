// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// The globally unique identifier for Steam Games.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CGameID(ulong id) : IEquatable<CGameID>, IComparable<CGameID>
{
    public static readonly CGameID Invalid = new(0);

    public ulong Id = id;

    public static implicit operator CGameID(ulong handle) => new(handle);

    public static implicit operator ulong(CGameID socket) => socket.Id;

    public static bool operator ==(CGameID conn1, CGameID conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(CGameID conn1, CGameID conn2) => conn1.Id != conn2.Id;

    public static bool operator <(CGameID conn1, CGameID conn2) => conn1.Id < conn2.Id;

    public static bool operator >(CGameID conn1, CGameID conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(CGameID conn1, CGameID conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(CGameID conn1, CGameID conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(CGameID other) => this == other;

    public override readonly bool Equals(object? obj) => obj is CGameID conn && this == conn;

    public readonly int CompareTo(CGameID other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
