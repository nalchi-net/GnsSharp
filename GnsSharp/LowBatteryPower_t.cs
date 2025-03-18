// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Purpose: Fired when running on a handheld PC or laptop with less than 10 minutes of battery is left, fires then every minute
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct LowBatteryPower_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 2;

    /// <summary>
    /// The estimated amount of battery life left in minutes.
    /// </summary>
    public byte MinutesBatteryLeft;

    public static int CallbackParamId => CallbackId;
}
