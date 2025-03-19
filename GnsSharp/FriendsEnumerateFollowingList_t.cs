// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;

#pragma warning disable SA1202 // Elements should be ordered by access

/// <summary>
/// Returns the result of EnumerateFollowingList.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FriendsEnumerateFollowingList_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamFriendsCallbacks + 46;

    /// <summary>
    /// The result of the operation.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The list of users that we are following.
    /// </summary>
    private Array50<CSteamID> steamID;

    /// <summary>
    /// The number of results returned in <see cref="SteamID"/>.
    /// </summary>
    public int ResultsReturned;

    /// <summary>
    /// The total number of people we are following.<br/>
    /// If this is greater than <see cref="ResultsReturned"/>, then you should make a subsequent call to <see cref="ISteamFriends.EnumerateFollowingList"/> with <see cref="ResultsReturned"/> as the index to get the next portion of followers.
    /// </summary>
    public int TotalResultCount;

    public static int CallbackParamId => CallbackId;

    public Span<CSteamID> SteamID => MemoryMarshal.CreateSpan(ref this.steamID[0], this.ResultsReturned);
}
