// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

public enum GnsBackendKind
{
    None,

    /// <summary>
    /// Using stand-alone open source GameNetworkingSockets as a backend.
    /// </summary>
    OpenSource,

    /// <summary>
    /// Using Steamworks SDK version of GNS as a backend.
    /// </summary>
    Steamworks,
}

/// <summary>
/// Check the current backend kind with <see cref="Kind"/>.
/// </summary>
public static class GnsBackend
{
#if GNS_SHARP_OPENSOURCE_GNS
    public const GnsBackendKind Kind = GnsBackendKind.OpenSource;
#elif GNS_SHARP_STEAMWORKS_SDK
    public const GnsBackendKind Kind = GnsBackendKind.Steamworks;
#else
    public const GnsBackendKind Kind = GnsBackendKind.None;
#endif
}
