// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// The result of a call to <see cref="ISteamRemoteStorage.FileShare"/>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStorageFileShareResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 7;

    /// <summary>
    /// The result of the operation
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The handle that can be shared with users and features
    /// </summary>
    public UGCHandle_t File;

    private Array260<byte> fileName;

    public static int CallbackParamId => CallbackId;

    /// <summary>
    /// The name of the file that was shared
    /// </summary>
    public readonly string? FileName => Utf8StringHelper.NullTerminatedSpanToString(this.fileName);
}
