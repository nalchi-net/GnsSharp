// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when a SteamAPICall_t has completed (or failed)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamAPICallCompleted_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 3;

    /// <summary>
    /// The handle of the Steam API Call that completed.
    /// </summary>
    public SteamAPICall_t AsyncCall;

    /// <summary>
    /// This is the k_iCallback constant which uniquely identifies the completed callback.
    /// </summary>
    public int AsyncCallbackId;

    /// <summary>
    /// The size in bytes of the completed callback.
    /// </summary>
    public uint ParamSize;

    public static int CallbackParamId => CallbackId;
}
