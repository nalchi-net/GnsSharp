// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Posted when the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a> activates or deactivates.<br/>
/// The game can use this to pause or resume single player games.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct GameOverlayActivated_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 31;

    /// <summary>
    /// <c>1</c> if it's just been activated, otherwise <c>0</c>.
    /// </summary>
    public bool Active;

    /// <summary>
    /// <c>true</c> if the user asked for the overlay to be activated/deactivated
    /// </summary>
    public bool UserInitiated;

    /// <summary>
    /// the appID of the game (should always be the current game)
    /// </summary>
    public AppId_t AppID;

    /// <summary>
    /// used internally
    /// </summary>
    private uint overlayPID;

    public static int CallbackParamId => CallbackId;
}
