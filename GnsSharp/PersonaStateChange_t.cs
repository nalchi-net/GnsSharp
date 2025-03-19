// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called whenever a friends' status changes.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct PersonaStateChange_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 4;

    /// <summary>
    /// Steam ID of the user who changed.
    /// </summary>
    public ulong SteamID;

    /// <summary>
    /// A bit-wise union of <see cref="EPersonaChange"/> values.
    /// </summary>
    public EPersonaChange ChangeFlags;

    public static int CallbackParamId => CallbackId;
}
