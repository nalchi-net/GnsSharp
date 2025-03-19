// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Interface to access information about individual users and interact with the <a href="https://partner.steamgames.com/doc/features/overlay">Steam Overlay</a>.
/// </summary>
public class ISteamFriends
{
    /// <summary>
    /// Maximum size in bytes that chat room, lobby, or chat/lobby member metadata may have.
    /// </summary>
    public const int ChatMetadataMax = 8192;
}
