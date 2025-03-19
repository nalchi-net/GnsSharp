// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// lobby search filter tools
/// </summary>
public enum ELobbyComparison : int
{
    EqualToOrLessThan = -2,
    LessThan = -1,
    Equal = 0,
    GreaterThan = 1,
    EqualToOrGreaterThan = 2,
    NotEqual = 3,
}
