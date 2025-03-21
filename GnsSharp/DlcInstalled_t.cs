// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Triggered after the current user gains ownership of DLC and that DLC is installed.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct DlcInstalled_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamAppsCallbacks + 5;

    /// <summary>
    /// App ID of the DLC that was installed.
    /// </summary>
    public AppId_t AppID;

    public static int CallbackParamId => CallbackId;
}
