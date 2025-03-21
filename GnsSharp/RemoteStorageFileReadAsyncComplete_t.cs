// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Response when reading a file asyncrounously with <see cref="ISteamRemoteStorage.FileReadAsync"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStorageFileReadAsyncComplete_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 32;

    /// <summary>
    /// Call handle of the async read which was made, must be passed to <see cref="ISteamRemoteStorage.FileReadAsyncComplete"/> to get the data.
    /// </summary>
    public SteamAPICall_t FileReadAsync;

    /// <summary>
    /// The result of the operation.
    /// If the local read was successful this will be <see cref="EResult.OK"/>, you can then call <see cref="ISteamRemoteStorage.FileReadAsyncComplete"/> to get the data.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// Offset into the file this read was at.
    /// </summary>
    public uint Offset;

    /// <summary>
    /// Amount of bytes read - will be the &lt;= the amount requested.
    /// </summary>
    public uint ReadSize;

    public static int CallbackParamId => CallbackId;
}
