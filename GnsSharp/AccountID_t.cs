// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// This is used internally in <see cref="CSteamID"/> to represent a specific user account without caring about what steam universe it's in.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct AccountID_t(uint id) : IEquatable<AccountID_t>, IComparable<AccountID_t>
{
    public static readonly AccountID_t Invalid = new(0);

    public uint Id = id;

    public static implicit operator AccountID_t(uint handle) => new(handle);

    public static implicit operator uint(AccountID_t socket) => socket.Id;

    public static bool operator ==(AccountID_t conn1, AccountID_t conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(AccountID_t conn1, AccountID_t conn2) => conn1.Id != conn2.Id;

    public static bool operator <(AccountID_t conn1, AccountID_t conn2) => conn1.Id < conn2.Id;

    public static bool operator >(AccountID_t conn1, AccountID_t conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(AccountID_t conn1, AccountID_t conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(AccountID_t conn1, AccountID_t conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(AccountID_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is AccountID_t conn && this == conn;

    public readonly int CompareTo(AccountID_t other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
