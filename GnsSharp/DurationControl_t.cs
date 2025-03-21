// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// Sent for games with enabled anti indulgence / duration control, for enabled users.<br/>
/// Lets the game know whether the feature applies to the user, whether the user needs to exit the game soon, and the remaining daily playtime for the user.
/// </para>
///
/// <para>
/// This callback is fired asynchronously in response to timers triggering.<br/>
/// It is also fired in response to calls to GetDurationControl().
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct DurationControl_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 67;

    /// <summary>
    /// result of call (always k_EResultOK for asynchronous timer-based notifications)
    /// </summary>
    public EResult Result;

    /// <summary>
    /// appid generating playtime
    /// </summary>
    public AppId_t Appid;

    /// <summary>
    /// is duration control applicable to user + game combination
    /// </summary>
    public bool Applicable;

    /// <summary>
    /// playtime since most recent 5 hour gap in playtime,<br/>
    /// only counting up to regulatory limit of playtime, in seconds
    /// </summary>
    public int Last5h;

    /// <summary>
    /// recommended progress (either everything is fine, or please exit game)
    /// </summary>
    public EDurationControlProgress Progress;

    /// <summary>
    /// notification to show, if any (always <see cref="EDurationControlNotification.None"/> for API calls)
    /// </summary>
    public EDurationControlNotification Notification;

    /// <summary>
    /// playtime on current calendar day
    /// </summary>
    public int Today;

    /// <summary>
    /// playtime remaining until the user hits a regulatory limit
    /// </summary>
    public int Remaining;

    public static int CallbackParamId => CallbackId;
}
