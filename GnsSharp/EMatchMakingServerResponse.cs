// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public enum EMatchMakingServerResponse : int
{
    ServerResponded = 0,
    ServerFailedToRespond,

    /// <summary>
    /// for the Internet query type, returned in response callback if no servers of this type match
    /// </summary>
    NoServersListedOnMasterServer,
}
