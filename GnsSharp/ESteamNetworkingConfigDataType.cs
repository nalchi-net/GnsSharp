// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Different configuration values have different data types
/// </summary>
public enum ESteamNetworkingConfigDataType : int
{
    Int32 = 1,
    Int64 = 2,
    Float = 3,
    String = 4,
    Ptr = 5,
}
