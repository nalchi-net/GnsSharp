// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Purpose: Result of our request to create a Lobby<br/>
/// m_eResult == k_EResultOK on success<br/>
/// at this point, the lobby has been joined and is ready for use<br/>
/// a LobbyEnter_t callback will also be received (since the local user is joining their own lobby)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FavoritesListAccountsUpdated_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 16;

    public EResult Result;

    public static int CallbackParamId => CallbackId;
}
