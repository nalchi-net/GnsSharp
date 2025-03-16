// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public enum EServerMode : int
{
    /// <summary>
    /// DO NOT USE
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// Don't authenticate user logins and don't list on the server list
    /// </summary>
    NoAuthentication = 1,

    /// <summary>
    /// Authenticate users, list on the server list, don't run VAC on clients that connect
    /// </summary>
    Authentication = 2,

    /// <summary>
    /// Authenticate users, list on the server list and VAC protect clients
    /// </summary>
    AuthenticationAndSecure = 3,
}
