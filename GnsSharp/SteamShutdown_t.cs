// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// called when Steam wants to shutdown
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamShutdown_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 4;

    public static int CallbackParamId => CallbackId;
}
