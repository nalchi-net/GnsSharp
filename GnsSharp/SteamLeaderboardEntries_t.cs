// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Handle to a list of downloaded entries in a leaderboard.<br/>
/// This is returned by <see cref="LeaderboardScoresDownloaded_t"/> and can be used to iterate through all of the entries with <see cref="ISteamUserStats.GetDownloadedLeaderboardEntry"/>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamLeaderboardEntries_t(ulong id) : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
{
    public ulong Id = id;

    public static implicit operator SteamLeaderboardEntries_t(ulong handle) => new(handle);

    public static implicit operator ulong(SteamLeaderboardEntries_t socket) => socket.Id;

    public static bool operator ==(SteamLeaderboardEntries_t conn1, SteamLeaderboardEntries_t conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(SteamLeaderboardEntries_t conn1, SteamLeaderboardEntries_t conn2) => conn1.Id != conn2.Id;

    public static bool operator <(SteamLeaderboardEntries_t conn1, SteamLeaderboardEntries_t conn2) => conn1.Id < conn2.Id;

    public static bool operator >(SteamLeaderboardEntries_t conn1, SteamLeaderboardEntries_t conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(SteamLeaderboardEntries_t conn1, SteamLeaderboardEntries_t conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(SteamLeaderboardEntries_t conn1, SteamLeaderboardEntries_t conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(SteamLeaderboardEntries_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is SteamLeaderboardEntries_t conn && this == conn;

    public readonly int CompareTo(SteamLeaderboardEntries_t other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
