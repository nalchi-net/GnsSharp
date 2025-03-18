# GnsSharp

GnsSharp is a C# binding for the [ValveSoftware/GameNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets).

It supports both the stand-alone open source GameNetworkingSockets, and the Steamworks SDK version of it,\
So you can switch between those without too much hassle.

> This project is **heavily WIP**.\
> API might be missing, subject to change, and things might break or not work at all;\
> Use it at your own risk.

## Why?

None of the existing C# bindings support switching between stand-alone GNS and Steamworks SDK version of it.\
So, I'm creating one for my own needs.

## State of binding

Currently, I've only ported the APIs that are compatible for both the stand-alone GNS and Steamworks SDK.\
This means that there's **almost NO support** for the Steamworks exclusive features.

This might change in the future if I need those.

<details>
    <summary>Table of state</summary>

| Interfaces          | Ported? | Interfaces              | Ported? |
|---------------------|---------|-------------------------|---------|
| ISteamApps               | ❌ | ISteamNetworking        | ❌ |
| ISteamAppTicket          | ❌ | ISteamNetworkingSockets | ✔ |
| ISteamClient             | ❌ | ISteamNetworkingUtils   | ✔ |
| ISteamController         | ❌ | ISteamRemotePlay        | ❌ |
| ISteamFriends            | ❌ | ISteamRemoteStorage     | ❌ |
| ISteamGameCoordinator    | ❌ | ISteamScreenshots       | ❌ |
| ISteamGameServer         | ❌ | ISteamTimeline          | ❌ |
| ISteamGameServerStats    | ❌ | ISteamUGC               | ❌ |
| ISteamHTMLSurface        | ❌ | ISteamUser              | ❌ |
| ISteamHTTP               | ❌ | ISteamUserStats         | ❌ |
| ISteamInput              | ❌ | ISteamUtils             | ✔ |
| ISteamInventory          | ❌ | ISteamVideo             | ❌ |
| ISteamMatchmaking        | ❌ | SteamEncryptedAppTicket | ❌ |
| ISteamMatchmakingServers | ❌ | steam_api               | ✔ |
| ISteamMusic              | ❌ | steam_gameserver        | ✔ |
| ISteamMusicRemote        | ❌ | GameNetworkingSockets   | ✔ |
</details>

# Documentation

Most of the APIs are almost the same as the original GNS, so you can refer to the [official Steam Networking Docs](https://partner.steamgames.com/doc/features/multiplayer/networking) to figure out how to use them.

## Differences

### Steam Callbacks

Steam callbacks are implemented as C# events in the respective Steam interfaces.

```cs
// Subscribe an event to receive Steam callbacks associated with it.
ISteamNetworkingSockets.User.SteamNetAuthenticationStatusChanged
    += (ref SteamNetAuthenticationStatus_t data) {
        ...
    }
```

There's one exception to this, which is `FnSteamNetConnectionStatusChanged`:

```cs
// This delegate should be alive until the listen socket is closed.
FnSteamNetConnectionStatusChanged listenSocketStatusChanged = /* your callback handler */;

// Setup this delegate object as a listen socket's configuration.
Span<SteamNetworkingConfigValue_t> listenSocketConfigs = stackalloc SteamNetworkingConfigValue_t[1];
listenSocketConfigs[0].SetPtr(ESteamNetworkingConfigValue.Callback_ConnectionStatusChanged,
                        Marshal.GetFunctionPointerForDelegate(listenSocketStatusChanged));

// Start listening with specifying this configuration.
HSteamListenSocket listener
    = ISteamNetworkingSockets.User.CreateListenSocketIP(in address, listenSocketConfigs);
```

This is to allow the listen socket and the client connections to have a different callback set up.

### Steam CallResults

Steam CallResults will be implemented with C# async-await pattern.

It's not implemented yet, so no example for now.

## Build

### Stand-alone GameNetworkingSockets

1. Build the open source [GameNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets).
    * As of writing, the latest is [commit `725e273`](https://github.com/ValveSoftware/GameNetworkingSockets/tree/725e273c7442bac7a8bc903c0b210b1c15c34d92)
    * Refer to the [`BUILDING.md`](https://github.com/ValveSoftware/GameNetworkingSockets/blob/master/BUILDING.md) on GameNetworkingSockets for details.
        * If you're using *Developer Powershell for VS 2022* on Windows, do note that it defaults to x86 environment, which obviously doesn't work when building for the AMD64.\
          You need to switch to AMD64 environment manually with following:
          ```powershell
          Enter-VsDevShell -DevCmdArguments "-arch=x64 -host_arch=x64" -VsInstallPath "C:/Program Files/Microsoft Visual Studio/2022/Community" -SkipAutomaticLocation
          ```
1. Copy the native library files to your executable's build directory.
    * On Windows, you should copy all the dependent dlls along with the `GameNetworkingSockets.dll`.

### Steamworks SDK

1. Download the Steamworks SDK from the [Steamworks partner site](https://partner.steamgames.com/).
    * As of writing, the latest is [Steamworks SDK v1.62](https://partner.steamgames.com/downloads/steamworks_sdk_162.zip)
1. Copy the native library files from the `sdk/redistributable_bin/` to your executable's build directory.

## Basic Examples

<details>
    <summary>Self connect example</summary>

```cs
using GnsSharp;

using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS0162 // Unreachable code (because of `GnsSharpCore.Backend` check)

// Load the native library depending on your platform
string nativeLibraryPath = Path.Join(AppContext.BaseDirectory,
       "GameNetworkingSockets.dll"         /* Open source GNS for Windows */
    // "libGameNetworkingSockets.so"       /* Open source GNS for Linux */
    // "libGameNetworkingSockets.dylib"    /* Open source GNS for macOS */
    // "steam_api64.dll"                   /* Steamworks SDK for Windows (AMD64) */
    // "steam_api.dll"                     /* Steamworks SDK for Windows (x86) */
    // "libsteam_api.so"                   /* Steamworks SDK for Linux */
    // "libsteam_api.dylib"                /* Steamworks SDK for macOS */
);

IntPtr nativeLibrary = NativeLibrary.Load(nativeLibraryPath);

// Initialize GNS or SteamAPI
bool initialized = false;
string? errMsg = null;
if (GnsSharpCore.Backend == GnsSharpCore.BackendKind.OpenSource)
{
    initialized = GameNetworkingSockets.Init(out errMsg);
}
else if (GnsSharpCore.Backend == GnsSharpCore.BackendKind.Steamworks)
{
    // For test environment, write `480` in `steam_appid.txt`, and put it next to your executable.
    // And you must be running Steam client on your PC.
    initialized = (SteamAPI.InitEx(out errMsg) == ESteamAPIInitResult.OK);
}

if (!initialized)
{
    Console.WriteLine(errMsg!);
    throw new Exception(errMsg!);
}

// Run callbacks as a seperate task
CancellationTokenSource cancelTokenSrc = new();
CancellationToken cancelToken = cancelTokenSrc.Token;
Task callbackRunner;

if (GnsSharpCore.Backend == GnsSharpCore.BackendKind.OpenSource)
{
    callbackRunner = Task.Run(async () =>
    {
        while (!cancelToken.IsCancellationRequested)
        {
            ISteamNetworkingSockets.User!.RunCallbacks();
            await Task.Delay(16, cancelToken);
        }
    }, cancelToken);
}
else if (GnsSharpCore.Backend == GnsSharpCore.BackendKind.Steamworks)
{
    callbackRunner = Task.Run(async () =>
    {
        while (!cancelToken.IsCancellationRequested)
        {
            SteamAPI.RunCallbacks();
            await Task.Delay(16, cancelToken);
        }
    }, cancelToken);
}

// Setup the debug output delegate
// (For every callback delegate, including this one,
// it should be stored somewhere safe to prevent it from garbage collected.)
FSteamNetworkingSocketsDebugOutput debugOutput = (ESteamNetworkingSocketsDebugOutputType level, string msg) =>
{
    Console.WriteLine($"[{level}] {msg}");
};

ISteamNetworkingUtils.User!.SetDebugOutputFunction(ESteamNetworkingSocketsDebugOutputType.Everything, debugOutput);

// Setup listen address: IPv6 any address & port 43000
SteamNetworkingIPAddr addr = default;
addr.ParseString("[::]:43000");

int serverClosing = 0;

// Setup listen socket connection status changed callback
FnSteamNetConnectionStatusChanged listenStatusChanged = (ref SteamNetConnectionStatusChangedCallback_t status) =>
{
    switch (status.Info.State)
    {
        case ESteamNetworkingConnectionState.Connecting:
            ISteamNetworkingSockets.User!.AcceptConnection(status.Conn);
            Console.WriteLine("Server has accepted the connection from client!");
            break;

        case ESteamNetworkingConnectionState.ClosedByPeer:
        case ESteamNetworkingConnectionState.ProblemDetectedLocally:
            StringBuilder builder = new();
            builder.Append($"Server: #{status.Conn} disconnected");
            if (status.Info.EndDebug != null)
                builder.Append($": {status.Info.EndDebug}");
            Console.WriteLine(builder.ToString());

            // Server side also need to close the connection to clean up resources
            ISteamNetworkingSockets.User!.CloseConnection(status.Conn, 0, "Server's closing too!", false);

            Interlocked.Exchange(ref serverClosing, 1);
            break;
    }
};

Span<SteamNetworkingConfigValue_t> serverConfigs = stackalloc SteamNetworkingConfigValue_t[1];
serverConfigs[0].SetPtr(ESteamNetworkingConfigValue.Callback_ConnectionStatusChanged,
                        Marshal.GetFunctionPointerForDelegate(listenStatusChanged));

// Server: Start listening
HSteamListenSocket listener = ISteamNetworkingSockets.User!.CreateListenSocketIP(in addr, serverConfigs);

serverConfigs[0].Dispose(); // Dispose config after usage, actually not required unless `SetString()` is used

// On Steam, in order to properly listen, you need to wait for the authentication to complete
if (GnsSharpCore.Backend == GnsSharpCore.BackendKind.Steamworks)
{
    for (int i = 0; ; ++i)
    {
        ESteamNetworkingAvailability avail =
            ISteamNetworkingSockets.User.GetAuthenticationStatus(out SteamNetAuthenticationStatus_t auth);

        if (avail == ESteamNetworkingAvailability.Current)
            break;
        else if (avail == ESteamNetworkingAvailability.Failed)
        {
            Console.WriteLine($"Auth failed: {auth.DebugMsg}");
            throw new Exception(auth.DebugMsg);
        }

        Console.WriteLine($"Waiting for Steam authentication for {i} seconds... ({avail})");

        await Task.Delay(1000);
    }

    Console.WriteLine("Steam authentication succeeded!");
}

// Setup connect address: IPv6 loopback address & port 43000
addr.ParseString("[::1]:43000");

int clientConnected = 0;

// Setup connect client connection status changed callback
FnSteamNetConnectionStatusChanged clientStatusChanged = (ref SteamNetConnectionStatusChangedCallback_t status) =>
{
    switch (status.Info.State)
    {
        case ESteamNetworkingConnectionState.Connected:
            Console.WriteLine("Client successfully connected to the server!");
            Interlocked.Exchange(ref clientConnected, 1);
            break;

        case ESteamNetworkingConnectionState.ClosedByPeer:
        case ESteamNetworkingConnectionState.ProblemDetectedLocally:
            StringBuilder builder = new();
            builder.Append($"Client: #{status.Conn} disconnected");
            if (status.Info.EndDebug != null)
                builder.Append($": {status.Info.EndDebug}");
            Console.WriteLine(builder.ToString());

            ISteamNetworkingSockets.User.CloseConnection(status.Conn, 0, "Client closing lately?", false);
            break;
    }
};

Span<SteamNetworkingConfigValue_t> clientConfigs = stackalloc SteamNetworkingConfigValue_t[1];
clientConfigs[0].SetPtr(ESteamNetworkingConfigValue.Callback_ConnectionStatusChanged,
                        Marshal.GetFunctionPointerForDelegate(clientStatusChanged));

// Client: Connect to the server
HSteamNetConnection client = ISteamNetworkingSockets.User.ConnectByIPAddress(addr, clientConfigs);

clientConfigs[0].Dispose();

// Wait for the connection to complete
while (clientConnected == 0)
    await Task.Delay(16);

// Close from the client side
ISteamNetworkingSockets.User.CloseConnection(client, 0, "Client's closing!", false);

// Wait for the server side to close the connection
while (serverClosing == 0)
    await Task.Delay(16);

// Stop the callback loop task
cancelTokenSrc.Cancel();
try
{
    await callbackRunner;
}
catch (TaskCanceledException)
{
    Console.WriteLine("Callback loop task stopped!");
}

// De-initialize GNS or SteamAPI
if (GnsSharpCore.Backend == GnsSharpCore.BackendKind.OpenSource)
    GameNetworkingSockets.Kill();
else if (GnsSharpCore.Backend == GnsSharpCore.BackendKind.Steamworks)
    SteamAPI.Shutdown();

// Free the native library
NativeLibrary.Free(nativeLibrary);
```

</details>

# License

GnsSharp is licensed under the [MIT License](LICENSE).

This project depends on either the stand-alone GameNetworkingSockets or the Steamworks SDK.
* [Stand-alone GameNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets) is licensed under the [BSD 3-Clause "New" or "Revised" License](https://github.com/ValveSoftware/GameNetworkingSockets/blob/master/LICENSE).
    * Refer to the GameNetworkingSockets' GitHub repo for other dependencies' licenses.
* Steamworks SDK is licensed under the [STEAMWORKS SDK license](https://partner.steamgames.com/documentation/sdk_access_agreement).
