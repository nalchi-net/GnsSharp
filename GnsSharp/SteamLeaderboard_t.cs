// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle to a single leaderboard.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamLeaderboard_t(ulong id) : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
{
    public ulong Id = id;

    public static implicit operator SteamLeaderboard_t(ulong handle) => new(handle);

    public static implicit operator ulong(SteamLeaderboard_t socket) => socket.Id;

    public static bool operator ==(SteamLeaderboard_t conn1, SteamLeaderboard_t conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(SteamLeaderboard_t conn1, SteamLeaderboard_t conn2) => conn1.Id != conn2.Id;

    public static bool operator <(SteamLeaderboard_t conn1, SteamLeaderboard_t conn2) => conn1.Id < conn2.Id;

    public static bool operator >(SteamLeaderboard_t conn1, SteamLeaderboard_t conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(SteamLeaderboard_t conn1, SteamLeaderboard_t conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(SteamLeaderboard_t conn1, SteamLeaderboard_t conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(SteamLeaderboard_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is SteamLeaderboard_t conn && this == conn;

    public readonly int CompareTo(SteamLeaderboard_t other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
