// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called when the callback system for this client is in an error state (and has flushed pending callbacks)<br/>
/// When getting this message the client should disconnect from Steam, reset any stored Steam state and reconnect.<br/>
/// This usually occurs in the rare event the Steam client has some kind of fatal error.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct IPCFailure_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamUserCallbacks + 17;

    public EFailureType FailureType;

    public enum EFailureType : byte
    {
        FlushedCallbackQueue,
        PipeFail,
    }

    public static int CallbackParamId => CallbackId;
}
