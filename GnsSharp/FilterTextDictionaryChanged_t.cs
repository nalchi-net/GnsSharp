// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// The text filtering dictionary has changed
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FilterTextDictionaryChanged_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 39;

    /// <summary>
    /// One of ELanguage, or k_LegallyRequiredFiltering
    /// </summary>
    public int Language;

    public static int CallbackParamId => CallbackId;
}
