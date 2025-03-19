// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Unique identifier for an app.<br/>
/// For more information see the <a href="https://partner.steamgames.com/doc/store/application">Applications</a> documentation.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct AppId_t(uint id) : IEquatable<AppId_t>, IComparable<AppId_t>
{
    /// <summary>
    /// An Invalid App ID.
    /// </summary>
    public static readonly AppId_t Invalid = new(0);

    public uint Id = id;

    public static explicit operator AppId_t(uint handle) => new(handle);

    public static explicit operator uint(AppId_t socket) => socket.Id;

    public static bool operator ==(AppId_t conn1, AppId_t conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(AppId_t conn1, AppId_t conn2) => conn1.Id != conn2.Id;

    public static bool operator <(AppId_t conn1, AppId_t conn2) => conn1.Id < conn2.Id;

    public static bool operator >(AppId_t conn1, AppId_t conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(AppId_t conn1, AppId_t conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(AppId_t conn1, AppId_t conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(AppId_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is AppId_t conn && this == conn;

    public readonly int CompareTo(AppId_t other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
