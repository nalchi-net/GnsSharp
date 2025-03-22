// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Deprecated - Only used with the deprecated RemoteStorage based Workshop API.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct PublishedFileId_t(ulong id) : IEquatable<PublishedFileId_t>, IComparable<PublishedFileId_t>
{
    public static readonly PublishedFileId_t Invalid = new(0);

    public ulong Id = id;

    public static implicit operator PublishedFileId_t(ulong handle) => new(handle);

    public static implicit operator ulong(PublishedFileId_t socket) => socket.Id;

    public static bool operator ==(PublishedFileId_t conn1, PublishedFileId_t conn2) => conn1.Id == conn2.Id;

    public static bool operator !=(PublishedFileId_t conn1, PublishedFileId_t conn2) => conn1.Id != conn2.Id;

    public static bool operator <(PublishedFileId_t conn1, PublishedFileId_t conn2) => conn1.Id < conn2.Id;

    public static bool operator >(PublishedFileId_t conn1, PublishedFileId_t conn2) => conn1.Id > conn2.Id;

    public static bool operator <=(PublishedFileId_t conn1, PublishedFileId_t conn2) => conn1.Id <= conn2.Id;

    public static bool operator >=(PublishedFileId_t conn1, PublishedFileId_t conn2) => conn1.Id >= conn2.Id;

    public readonly bool Equals(PublishedFileId_t other) => this == other;

    public override readonly bool Equals(object? obj) => obj is PublishedFileId_t conn && this == conn;

    public readonly int CompareTo(PublishedFileId_t other) => this.Id.CompareTo(other.Id);

    public override readonly int GetHashCode() => this.Id.GetHashCode();

    public override readonly string ToString() => this.Id.ToString();
}
