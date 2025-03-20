// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when we have received the group status of a user.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct GSClientGroupStatus_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamGameServerCallbacks + 8;

    /// <summary>
    /// The user whose group status we queried.
    /// </summary>
    public CSteamID SteamIDUser;

    /// <summary>
    /// The group that we queried.
    /// </summary>
    public CSteamID SteamIDGroup;

    /// <summary>
    /// Is the user a member of the group (<c>true</c>) or not (<c>false</c>)?
    /// </summary>
    public bool Member;

    /// <summary>
    /// Is the user an officer in the group (<c>true</c>) or not (<c>false</c>)?<br/>
    /// This will never be true if <see cref="Member"/> is false.
    /// </summary>
    public bool Officer;

    public static int CallbackParamId => CallbackId;
}
