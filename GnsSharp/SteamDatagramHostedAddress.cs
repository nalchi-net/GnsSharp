// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Describe the address of a gameserver in a known PoP that is part<br/>
/// of the Steam Datagram relay network.  This is an intentionally<br/>
/// opaque byte blob.  The relays know how to use this to forward it on<br/>
/// to the intended destination, but otherwise clients really should not<br/>
/// need to know what's inside.  (Indeed, we don't really want them to<br/>
/// know, as it could reveal information useful to an attacker.  For that<br/>
/// reason, in production use cases the address will be encrypted with a<br/>
/// key that relays know how to decrypt.)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamDatagramHostedAddress
{
    /// <summary>
    /// Size of data blob.
    /// </summary>
    public int Size;

    /// <summary>
    /// Opaque data
    /// </summary>
    public Array128<byte> Data;

    // Reset to empty state
    public void Clear()
    {
        this = default;
    }

    // TODO... Maybe?

    /// <summary>
    /// Parse the data center out of the blob.
    /// </summary>
    // SteamNetworkingPOPID GetPopID() const
    // {
    //     return CalculateSteamNetworkingPOPIDFromString( m_data );
    // }

    /// <summary>
    /// Set a dummy routing blob with a hardcoded IP:port.  You should only use<br/>
    /// this in a dev environment, since the address is in plaintext!<br/>
    /// In production this information should come from the server,<br/>
    /// using ISteamNetworkingSockets::GetHostedDedicatedServerAddress
    /// </summary>
    // void SetDevAddress(uint32 nIP, uint16 nPort, SteamNetworkingPOPID popid = 0)
    // {
    //     GetSteamNetworkingLocationPOPStringFromID(popid, m_data);
    //     m_cbSize = 4;
    //     m_data[m_cbSize++] = 1;
    //     m_data[m_cbSize++] = 1;
    //     m_data[m_cbSize++] = char(nPort);
    //     m_data[m_cbSize++] = char(nPort >> 8);
    //     m_data[m_cbSize++] = char(nIP);
    //     m_data[m_cbSize++] = char(nIP >> 8);
    //     m_data[m_cbSize++] = char(nIP >> 16);
    //     m_data[m_cbSize++] = char(nIP >> 24);
    // }

    /// <summary>
    /// Convert to/from std::string (or anything that acts like it).<br/>
    /// Useful for interfacing with google protobuf.  It's a template<br/>
    /// mainly so that we don't have to include "string" in the header.<br/>
    /// Note: by "string", we don't mean that it's text.  It's a binary<br/>
    /// blob, and it might have zeros in it.  (std::string can handle that.)
    /// </summary>

    // template <typename T> bool SetFromString(string str)
    // {
    //     if (str.length() >= sizeof(m_data))
    //     {
    //         m_cbSize = 0;
    //         return false;
    //     }
    //     m_cbSize = (int)str.length();
    //     memcpy(m_data, str.c_str(), m_cbSize);
    //     return true;
    // }

    // template <typename T> void GetAsStdString( T *str ) const
    // {
    //     str->assign( m_data, m_cbSize );
    // }
}
