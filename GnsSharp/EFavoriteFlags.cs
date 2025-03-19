// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;

[Flags]
public enum EFavoriteFlags : uint
{
    /// <summary>
    /// This favorite game server has no flags set.
    /// </summary>
    None = 0,

    /// <summary>
    /// This favorite game server entry is for the favorites list.
    /// </summary>
    Favorite = 1,

    /// <summary>
    /// This favorite game server entry is for the history list.
    /// </summary>
    History = 2,
}
