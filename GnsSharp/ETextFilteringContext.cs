// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// The context where text filtering is being done
/// </summary>
public enum ETextFilteringContext : int
{
    /// <summary>
    /// Unknown context
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Game content, only legally required filtering is performed
    /// </summary>
    GameContent = 1,

    /// <summary>
    /// Chat from another player
    /// </summary>
    Chat = 2,

    /// <summary>
    /// Character or item name
    /// </summary>
    Name = 3,
}
