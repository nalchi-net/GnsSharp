// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamAPICallCompleted_t
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 3;

    public SteamAPICall_t AsyncCall;
    public int Callback;
    public uint Param;
}
