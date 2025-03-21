// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Sent in response to <see cref="ISteamUser.GetMarketEligibility"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct MarketEligibilityResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 66;

    public bool Allowed;
    public EMarketNotAllowedReasonFlags NotAllowedReason;
    public RTime32 AllowedAtTime;

    /// <summary>
    /// The number of days any user is required to have had Steam Guard before they can use the market
    /// </summary>
    public int SteamGuardRequiredDays;

    /// <summary>
    /// The number of days after initial device authorization a user must wait before using the market on that device
    /// </summary>
    public int NewDeviceCooldown;

    public static int CallbackParamId => CallbackId;
}
