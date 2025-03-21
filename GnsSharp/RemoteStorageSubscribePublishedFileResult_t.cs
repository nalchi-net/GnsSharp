// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the user has subscribed to a piece of UGC. Result from <see cref="ISteamUGC.SubscribeItem"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStorageSubscribePublishedFileResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 13;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The workshop item that the user subscribed to.
    /// </summary>
    public PublishedFileId_t PublishedFileId;

    public static int CallbackParamId => CallbackId;
}
