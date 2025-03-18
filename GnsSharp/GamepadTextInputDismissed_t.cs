// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Full Screen gamepad text input has been closed
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GamepadTextInputDismissed_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUtilsCallbacks + 14;

    /// <summary>
    /// true if user entered and accepted text (Call ISteamUtils::GetEnteredGamepadTextInput() for text),<br/>
    /// false if canceled input
    /// </summary>
    public bool Submitted;

    /// <summary>
    /// Contains the length in bytes if there was text submitted.
    /// </summary>
    public uint SubmittedText;
    public AppId_t AppID;

    public static int CallbackParamId => CallbackId;
}
