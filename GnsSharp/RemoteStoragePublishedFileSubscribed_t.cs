// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// User subscribed to a file for the app (from within the app or on the web)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStoragePublishedFileSubscribed_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 21;

    /// <summary>
    /// The published file id
    /// </summary>
    public PublishedFileId_t PublishedFileId;

    /// <summary>
    /// ID of the app that will consume this file.
    /// </summary>
    public AppId_t AppID;

    public static int CallbackParamId => CallbackId;
}
