// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct AppResumingFromSuspend_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 36;

    public static int CallbackParamId => CallbackId;
}
