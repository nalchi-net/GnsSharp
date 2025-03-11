# GnsSharp

GnsSharp is a C# binding for the [ValveSoftware/GameNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets).

It supports both the stand-alone open source GameNetworkingSockets, and the Steamworks SDK version of it,\
So you can switch between those without too much hassle.

## Why?

None of the existing C# bindings support switching between stand-alone GNS and Steamworks SDK version of it.\
So, I'm creating one for my own needs.

## Scope

Currently, I'm only interested in the APIs that are compatible for both the stand-alone and Steamworks versions.\
This includes [`ISteamNetworkingSockets`](https://partner.steamgames.com/doc/api/ISteamNetworkingSockets), [`ISteamNetworkingUtils`](https://partner.steamgames.com/doc/api/ISteamNetworkingUtils) and [`steamnetworkingtypes` header](https://partner.steamgames.com/doc/api/steamnetworkingtypes).

This means that there's no support for the Steamworks exclusive features (e.g. matchmaking, steam friends...).\
This might change in the future if I need those.

# Documentation

Most of the APIs are the same as the original GNS, so you can refer to the [official Steam Networking Docs](https://partner.steamgames.com/doc/features/multiplayer/networking) to figure out how to use them.

# License

GnsSharp is licensed under the [MIT License](LICENSE).

This project depends on either the stand-alone GameNetworkingSockets or the Steamworks SDK.
* [Stand-alone GameNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets) is licensed under the [BSD 3-Clause "New" or "Revised" License](https://github.com/ValveSoftware/GameNetworkingSockets/blob/master/LICENSE).
    * Refer to the GameNetworkingSockets' GitHub repo for other dependencies' licenses.
* Steamworks SDK is licensed under the [STEAMWORKS SDK license](https://partner.steamgames.com/documentation/sdk_access_agreement).
