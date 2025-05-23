// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// The floating on-screen keyboard has been closed
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FloatingGamepadTextInputDismissed_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 38;

    public static int CallbackParamId => CallbackId;
}
