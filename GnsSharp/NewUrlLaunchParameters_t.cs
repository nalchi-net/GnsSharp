// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Posted after the user executes a steam url with command line or query parameters such as<br/>
/// steam://run/(appid)//?param1=value1;param2=value2;param3=value3;<br/>
/// while the game is already running.<br/>
/// The new params can be queried with <see cref="ISteamApps.GetLaunchCommandLine"/> and <see cref="ISteamApps.GetLaunchQueryParam"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct NewUrlLaunchParameters_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamAppsCallbacks + 14;

    public static int CallbackParamId => CallbackId;
}
