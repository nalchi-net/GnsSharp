// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

#pragma warning disable SA1202 // Elements should be ordered by access

[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStorageDownloadUGCResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 17;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The handle to the file that was attempted to be downloaded.
    /// </summary>
    public UGCHandle_t File;

    /// <summary>
    /// ID of the app that created this file.
    /// </summary>
    public AppId_t AppID;

    /// <summary>
    /// The size of the file that was downloaded, in bytes.
    /// </summary>
    public int SizeInBytes;

    private Array260<byte> fileName;

    /// <summary>
    /// Steam ID of the user who created this content.
    /// </summary>
    public ulong SteamIDOwner;

    public static int CallbackParamId => CallbackId;

    /// <summary>
    /// The name of the file that was downloaded.
    /// </summary>
    public readonly string? FileName => Utf8StringHelper.NullTerminatedSpanToString(this.fileName);
}
