// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Tells Steam where to place the browser window inside the overlay
/// </summary>
public enum EActivateGameOverlayToWebPageMode : int
{
    /// <summary>
    /// Browser will open next to all other windows that the user has open in the overlay.<br/>
    /// The window will remain open, even if the user closes then re-opens the overlay.
    /// </summary>
    Default = 0,

    /// <summary>
    /// Browser will be opened in a special overlay configuration which hides all other windows that the user has open in the overlay.<br/>
    /// When the user closes the overlay, the browser window will also close.<br/>
    /// When the user closes the browser window, the overlay will automatically close.
    /// </summary>
    Modal = 1,
}
