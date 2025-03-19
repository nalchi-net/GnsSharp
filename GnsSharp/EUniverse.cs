// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Steam universes.  Each universe is a self-contained Steam instance.
/// </summary>
public enum EUniverse : int
{
    /// <summary>
    /// Invalid.
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// The standard public universe.
    /// </summary>
    Public = 1,

    /// <summary>
    /// Beta universe used inside Valve.
    /// </summary>
    Beta = 2,

    /// <summary>
    /// Internal universe used inside Valve.
    /// </summary>
    Internal = 3,

    /// <summary>
    /// Dev universe used inside Valve.
    /// </summary>
    Dev = 4,
}
