// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/// <summary>
/// Internal structure used in manual callback dispatch
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct CallbackMsg_t
{
    /// <summary>
    /// Specific user to whom this callback applies.
    /// </summary>
    public HSteamUser SteamUser;

    /// <summary>
    /// Callback identifier.  (Corresponds to the k_iCallback enum in the callback structure.)
    /// </summary>
    public int CallbackId;

    /// <summary>
    /// Points to the callback structure
    /// </summary>
    public IntPtr Param;

    /// <summary>
    /// Size of the data pointed to by <see cref="Param"/>
    /// </summary>
    public int ParamSize;

    public ref T GetCallbackParamAs<T>()
        where T : unmanaged
    {
        Debug.Assert(Unsafe.SizeOf<T>() == this.ParamSize, $"Param size was {this.ParamSize}, yet tried to get as {typeof(T)} whose size is {Unsafe.SizeOf<T>()}");

        unsafe
        {
            return ref Unsafe.AsRef<T>(this.Param.ToPointer());
        }
    }
}
