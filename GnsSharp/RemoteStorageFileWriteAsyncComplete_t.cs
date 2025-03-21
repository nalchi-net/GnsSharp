// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Response when writing a file asyncrounously with <see cref="ISteamRemoteStorage.FileWriteAsync"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStorageFileWriteAsyncComplete_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 31;

    /// <summary>
    /// The result of the operation.<br/>
    /// If the local write was successful then this will be <see cref="EResult.OK"/> - any other value likely indicates that the filename is invalid or the available quota would have been exceeded by the requested write.<br/>
    /// Any attempt to write files that exceed this size will return <see cref="EResult.InvalidParam"/>.<br/>
    /// Writing files to the cloud is limited to 100 MiB.
    /// </summary>
    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
