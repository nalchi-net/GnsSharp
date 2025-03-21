// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called whenever the users licenses (owned packages) changes.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LicensesUpdated_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 25;

    public static int CallbackParamId => CallbackId;
}
