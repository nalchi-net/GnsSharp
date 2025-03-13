// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

internal static class Utf8StringHelper
{
    public static string NullTerminatedSpanToString(ReadOnlySpan<byte> span)
    {
        int nullIndex = span.IndexOf((byte)0);
        span = span[..nullIndex];

        return Encoding.UTF8.GetString(span);
    }

    public static IntPtr AllocHGlobalString(string str, out int lengthIncludingNull)
    {
        // Allocate enough space for the converted UTF-8 unmanaged string.
        int utf8BytesCount = Encoding.UTF8.GetByteCount(str);
        IntPtr unmanagedPtr = Marshal.AllocHGlobal(utf8BytesCount + 1);

        // Get the span of unmanaged space.
        Span<byte> unmanagedSpan;
        unsafe
        {
            unmanagedSpan = new Span<byte>((void*)unmanagedPtr, utf8BytesCount + 1);
        }

        // Marshal to this unmanaged span directly.
        int bytesWritten = Encoding.UTF8.GetBytes(str.AsSpan(), unmanagedSpan);
        unmanagedSpan[bytesWritten++] = (byte)0;

        lengthIncludingNull = bytesWritten;

        return unmanagedPtr;
    }

    public static IntPtr AllocHGlobalString(string str)
    {
        // Allocate enough space for the converted UTF-8 unmanaged string.
        int utf8BytesCount = Encoding.UTF8.GetByteCount(str);
        IntPtr unmanagedPtr = Marshal.AllocHGlobal(utf8BytesCount + 1);

        // Get the span of unmanaged space.
        Span<byte> unmanagedSpan;
        unsafe
        {
            unmanagedSpan = new Span<byte>((void*)unmanagedPtr, utf8BytesCount + 1);
        }

        // Marshal to this unmanaged span directly.
        int bytesWritten = Encoding.UTF8.GetBytes(str.AsSpan(), unmanagedSpan);
        unmanagedSpan[bytesWritten++] = (byte)0;

        return unmanagedPtr;
    }
}
