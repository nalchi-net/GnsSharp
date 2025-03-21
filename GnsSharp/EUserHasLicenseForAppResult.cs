// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Result of <see cref="ISteamUser.UserHasLicenseForApp"/>.
/// </summary>
public enum EUserHasLicenseForAppResult : int
{
    /// <summary>
    /// The user has a license for specified app.
    /// </summary>
    HasLicense = 0,

    /// <summary>
    /// The user does not have a license for the specified app.
    /// </summary>
    DoesNotHaveLicense = 1,

    /// <summary>
    /// The user has not been authenticated.
    /// </summary>
    NoAuth = 2,
}
