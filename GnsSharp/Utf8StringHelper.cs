// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

using System;
using System.Text;

namespace GnsSharp;

internal static class Utf8StringHelper
{
    public static string NullTerminatedSpanToString(ReadOnlySpan<byte> span)
    {
        int nullIndex = span.IndexOf((byte)0);
        span = span[..nullIndex];

        return Encoding.UTF8.GetString(span);
    }
}
