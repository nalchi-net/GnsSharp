// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public static class GnsSharp
{
    public static void SetupInterfaces()
    {
        ISteamNetworkingSockets.Setup();
        ISteamNetworkingUtils.Setup();
    }
}
