// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

internal interface ICallTask
{
#if GNS_SHARP_STEAMWORKS_SDK
    void SetResultFrom(HSteamPipe pipe, ref SteamAPICallCompleted_t callCompleted);
#endif
}
