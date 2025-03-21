// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// The globally unique identifier for all Steam accounts, Steam groups, Lobbies and Chat rooms.
/// See <see cref="EAccountType"/> and <see cref="EUniverse"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CSteamID(ulong id) : IEquatable<CSteamID>, IComparable<CSteamID>
{
    public static readonly CSteamID Nil = new(0);
    public static readonly CSteamID NotInitYetGS = new(1);
    public static readonly CSteamID NonSteamGS = new(2);

    public ulong Id = id;

    public static implicit operator CSteamID(ulong handle) => new(handle);

    public static implicit operator ulong(CSteamID socket) => socket.Id;

    public static bool operator ==(CSteamID conn1, CSteamID conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(CSteamID conn1, CSteamID conn2) => conn1.Id != conn2.Id;

    public static bool operator <(CSteamID conn1, CSteamID conn2) => conn1.Id < conn2.Id;

    public static bool operator >(CSteamID conn1, CSteamID conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(CSteamID conn1, CSteamID conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(CSteamID conn1, CSteamID conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(CSteamID other) => this == other;

    public override readonly bool Equals(object? obj) => obj is CSteamID conn && this == conn;

    public readonly int CompareTo(CSteamID other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
