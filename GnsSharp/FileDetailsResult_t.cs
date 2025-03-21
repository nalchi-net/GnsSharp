// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Called after requesting the details of a specific file.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct FileDetailsResult_t : ICallbackParam
{
    public const int CallbackId = Constants.SteamAppsCallbacks + 23;

    /// <summary>
    /// Was the call successful? <see cref="EResult.OK"/> if it was;<br/>
    /// otherwise, <see cref="EResult.FileNotFound"/> if the file was not found.<br/>
    /// None of the other fields are filled out if the call was not successful.
    /// </summary>
    public EResult Result;

    /// <summary>
    /// The original file size in bytes.
    /// </summary>
    public ulong FileSize;

    /// <summary>
    /// The original file SHA1 hash.
    /// </summary>
    public Array20<byte> FileSHA;

    public uint Flags;

    public static int CallbackParamId => CallbackId;
}
