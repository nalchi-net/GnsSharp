// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// These values are passed as parameters to the store with <see cref="ISteamFriends.ActivateGameOverlayToStore"/> and modify the behavior when the page opens.
/// </summary>
public enum EOverlayToStoreFlag : int
{
    /// <summary>
    /// No
    /// </summary>
    None = 0,

    /// <summary>
    /// Deprecated, now works the same as <see cref="AddToCartAndShow"/>.
    /// </summary>
    AddToCart = 1,

    /// <summary>
    /// Add the specified app ID to the users cart and show the store page.
    /// </summary>
    AddToCartAndShow = 2,
}
