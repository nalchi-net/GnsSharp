// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Purpose: The country of the user changed
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct IPCountry_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 1;

    public static int CallbackParamId => CallbackId;
}
