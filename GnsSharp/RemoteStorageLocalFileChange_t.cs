// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// If a Steam app is flagged for supporting dynamic Steam Cloud sync, and a sync occurs, this callback will be posted to the app if any local files changed.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct RemoteStorageLocalFileChange_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamRemoteStorageCallbacks + 33;

    public static int CallbackParamId => CallbackId;
}
