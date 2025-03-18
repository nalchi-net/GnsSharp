// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public enum EFloatingGamepadTextInputMode : int
{
    /// <summary>
    /// Enter dismisses the keyboard
    /// </summary>
    ModeSingleLine = 0,

    /// <summary>
    /// User needs to explictly close the keyboard
    /// </summary>
    ModeMultipleLines = 1,

    /// <summary>
    /// Keyboard layout is email, enter dismisses the keyboard
    /// </summary>
    ModeEmail = 2,

    /// <summary>
    /// Keyboard layout is numeric, enter dismisses the keyboard
    /// </summary>
    ModeNumeric = 3,
}
