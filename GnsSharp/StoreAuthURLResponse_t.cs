// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Response when we have recieved the authentication URL after a call to <see cref="ISteamUser.RequestStoreAuthURL"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct StoreAuthURLResponse_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 65;

    private Array512<byte> url;

    public static int CallbackParamId => CallbackId;

    /// <summary>
    /// The authentication URL.
    /// </summary>
    public readonly string? Url => Utf8StringHelper.NullTerminatedSpanToString(this.url);
}
