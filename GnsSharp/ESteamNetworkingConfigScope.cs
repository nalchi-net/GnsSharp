// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

/// <summary>
/// Configuration values can be applied to different types of objects.
/// </summary>
public enum ESteamNetworkingConfigScope : int
{
    /// <summary>
    /// Get/set global option, or defaults.  Even options that apply to more specific scopes<br/>
    /// have global scope, and you may be able to just change the global defaults.  If you<br/>
    /// need different settings per connection (for example), then you will need to set those<br/>
    /// options at the more specific scope.
    /// </summary>
    Global = 1,

    /// <summary>
    /// Some options are specific to a particular interface.  Note that all connection<br/>
    /// and listen socket settings can also be set at the interface level, and they will<br/>
    /// apply to objects created through those interfaces.
    /// </summary>
    SocketsInterface = 2,

    /// <summary>
    /// Options for a listen socket.  Listen socket options can be set at the interface layer,<br/>
    /// if  you have multiple listen sockets and they all use the same options.<br/>
    /// You can also set connection options on a listen socket, and they set the defaults<br/>
    /// for all connections accepted through this listen socket.  (They will be used if you don't<br/>
    /// set a connection option.)
    /// </summary>
    ListenSocket = 3,

    /// <summary>
    /// Options for a specific connection.
    /// </summary>
    Connection = 4,
}
