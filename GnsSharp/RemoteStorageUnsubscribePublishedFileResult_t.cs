// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the user has unsubscribed from a piece of UGC. Result from <see cref="ISteamUGC.UnsubscribeItem"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStorageUnsubscribePublishedFileResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 15;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The workshop item that the user unsubscribed from.
    /// </summary>
    public PublishedFileId_t PublishedFileId;

    public static int CallbackParamId => CallbackId;
}
