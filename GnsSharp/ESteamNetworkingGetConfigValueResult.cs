// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Return value of ISteamNetworkintgUtils::GetConfigValue
/// </summary>
public enum ESteamNetworkingGetConfigValueResult : int
{
    /// <summary>
    /// No such configuration value
    /// </summary>
    BadValue = -1,

    /// <summary>
    /// Bad connection handle, etc
    /// </summary>
    BadScopeObj = -2,

    /// <summary>
    /// Couldn't fit the result in your buffer
    /// </summary>
    BufferTooSmall = -3,

    OK = 1,

    /// <summary>
    /// A value was not set at this level, but the effective (inherited) value was returned.
    /// </summary>
    OKInherited = 2,
}
