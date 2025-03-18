// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// callback for CheckFileSignature
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct CheckFileSignature_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 5;

    public ECheckFileSignature CheckFileSignature;

    public static int CallbackParamId => CallbackId;
}
