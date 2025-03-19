// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// A server was added/removed from the favorites list, you should refresh now.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FavoritesListChanged_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamMatchmakingCallbacks + 2;

    /// <summary>
    /// An IP of 0 means reload the whole list, any other value means just one server.<br/>
    /// This is host order, i.e 127.0.0.1 == 0x7f000001.
    /// </summary>
    public uint Ip;

    /// <summary>
    /// If <see cref="Ip"/> is set then this is the new servers query port, in host order.
    /// </summary>
    public uint QueryPort;

    /// <summary>
    /// If <see cref="Ip"/> is set then this is the new servers connection port, in host order.
    /// </summary>
    public uint ConnPort;

    /// <summary>
    /// If <see cref="Ip"/> is set then this is the App ID the game server belongs to.
    /// </summary>
    public uint AppID;

    /// <summary>
    /// If <see cref="Ip"/> is set then this returns whether the the server is on the favorites list or the history list.<br/>
    /// See k_unFavoriteFlagNone for more information.
    /// </summary>
    public EFavoriteFlags Flags;

    /// <summary>
    /// If <see cref="Ip"/> is set then this is whether the server was added to the list (<c>true</c>) or removed (<c>false</c>) from it.
    /// </summary>
    public bool Add;

    public AccountID_t AccountId;

    public static int CallbackParamId => CallbackId;
}
