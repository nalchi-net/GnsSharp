// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.InteropServices;

/// <summary>
/// Ticket used to authenticate a client to talk to a specific gameserver.<br/>
/// Tickets are used for the connection/authentication flow where you want<br/>
/// your central matchmaking service ("game coordinator") to more tightly<br/>
/// control which clients can talk to which servers.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
public struct SteamDatagramRelayAuthTicket
{
    /// <summary>
    /// Identity of the gameserver we want to talk to.  This is required.
    /// </summary>
    public SteamNetworkingIdentity IdentityGameserver;

    /// <summary>
    /// Identity of the person who was authorized.  This is required.
    /// </summary>
    public SteamNetworkingIdentity IdentityAuthorizedClient;

    /// <summary>
    /// <para>
    /// SteamID is authorized to send from a particular public IP.  If this<br/>
    /// is 0, then the sender is not restricted to a particular IP.<br/>
    /// </para>
    ///
    /// <para>
    /// Recommend to leave this set to zero.
    /// </para>
    /// </summary>
    public uint PublicIp;

    /// <summary>
    /// <para>
    /// Time when the ticket expires.  Recommended: take the current<br/>
    /// time and add 6 hours, or maybe a bit longer if your gameplay<br/>
    /// sessions are longer.
    /// </para>
    ///
    /// <para>
    /// NOTE: relays may reject tickets with expiry times excessively<br/>
    /// far in the future, so contact us if you wish to use an expiry<br/>
    /// longer than, say, 24 hours.
    /// </para>
    /// </summary>
    public RTime32 RtimeTicketExpiry;

    /// <summary>
    /// <para>
    /// Routing information where the gameserver is listening for<br/>
    /// relayed traffic.  You should fill this in when generating<br/>
    /// a ticket.
    /// </para>
    ///
    /// <para>
    /// When generating tickets on your backend:<br/>
    /// - In production: The gameserver knows the proper routing<br/>
    ///   information, so you need to call<br/>
    ///   ISteamNetworkingSockets::GetHostedDedicatedServerAddress<br/>
    ///   and send the info to your backend.<br/>
    /// - In development, you will need to provide public IP<br/>
    ///   of the server using SteamDatagramServiceNetID::SetDevAddress.<br/>
    ///   Relays need to be able to send UDP<br/>
    ///   packets to this server.  Since it's very likely that<br/>
    ///   your server is behind a firewall/NAT, make sure that<br/>
    ///   the address is the one that the outside world can use.<br/>
    ///   The traffic from the relays will be "unsolicited", so<br/>
    ///   stateful firewalls won't work -- you will probably have<br/>
    ///   to set up an explicit port forward.<br/>
    /// On the client:<br/>
    /// - this field will always be blank.
    /// </para>
    /// </summary>
    public SteamDatagramHostedAddress Routing;

    /// <summary>
    /// App ID this is for.  This is required, and should be the<br/>
    /// App ID the client is running.  (Even if your gameserver<br/>
    /// uses a different App ID.)
    /// </summary>
    public uint AppID;

    /// <summary>
    /// <para>
    /// Restrict this ticket to be used for a particular virtual port?<br/>
    /// Set to -1 to allow any virtual port.
    /// </para>
    ///
    /// <para>
    /// This is useful as a security measure, and also so the client will<br/>
    /// use the right ticket (which might have extra fields that are useful<br/>
    /// for proper analytics), if the client happens to have more than one<br/>
    /// appropriate ticket.
    /// </para>
    ///
    /// <para>
    /// Note: if a client has more that one acceptable ticket, they will<br/>
    /// always use the one expiring the latest.
    /// </para>
    /// </summary>
    public int RestrictToVirtualPort;

    public int ExtraFields;
    public Array16<ExtraField> VecExtraFields;

    private const int MaxExtraFields = 16;

    public SteamDatagramRelayAuthTicket()
    {
        this.Clear();
    }

    /// <summary>
    /// Reset all fields
    /// </summary>
    public void Clear()
    {
        this = default;
        this.RestrictToVirtualPort = -1;
    }

    /// <summary>
    /// <para>
    /// Extra fields.
    /// </para>
    ///
    /// <para>
    /// These are collected for backend analytics.  For example, you might<br/>
    /// send a MatchID so that all of the records for a particular match can<br/>
    /// be located.  Or send a game mode field so that you can compare<br/>
    /// the network characteristics of different game modes.
    /// </para>
    ///
    /// <para>
    /// (At the time of this writing we don't have a way to expose the data<br/>
    /// we collect to partners, but we hope to in the future so that you can<br/>
    /// get visibility into network conditions.)
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = Native.PackSize)]
    public struct ExtraField
    {
        public EType Type;
        public Array28<byte> Name;

        public ValueUnion Value;

        public enum EType : int
        {
            String,

            /// <summary>
            /// For most small integral values.<br/>
            /// Uses google protobuf sint64, so it's small on the wire.<br/>
            /// WARNING: In some places this value may be transmitted in JSON,<br/>
            /// in which case precision may be lost in backend analytics.<br/>
            /// Don't use this for an "identifier", use it for a scalar quantity.
            /// </summary>
            Int,

            /// <summary>
            /// 64 arbitrary bits.  This value is treated as an "identifier".<br/>
            /// In places where JSON format is used, it will be serialized as a string.<br/>
            /// No aggregation / analytics can be performed on this value.
            /// </summary>
            Fixed64,
        }

        [StructLayout(LayoutKind.Explicit, Pack = Native.PackSize)]
        public struct ValueUnion
        {
            [FieldOffset(0)]
            public Array128<byte> StringValue;

            [FieldOffset(0)]
            public long IntValue;

            [FieldOffset(0)]
            public ulong Fixed64Value;
        }
    }

    // TODO... Maybe?

    // Helper to add an extra field in a single call
    // void AddExtraField_Int( const char* pszName, int64 val)
    // {
    //     ExtraField* p = AddExtraField(pszName, ExtraField::k_EType_Int);
    //     if (p)
    //         p->m_nIntValue = val;
    // }

    // Helper to add an extra field in a single call
    // void AddExtraField_Fixed64( const char* pszName, uint64 val)
    // {
    //     ExtraField* p = AddExtraField(pszName, ExtraField::k_EType_Fixed64);
    //     if (p)
    //         p->m_nFixed64Value = val;
    // }

    // Helper to add an extra field in a single call
    // void AddExtraField_String( const char* pszName, const char* val)
    // {
    //     ExtraField* p = AddExtraField(pszName, ExtraField::k_EType_String);
    //     if (p)
    //     {
    //         size_t l = strlen(val);
    //         if (l > sizeof(p->m_szStringValue) -1 )
    //             l = sizeof(p->m_szStringValue)-1;
    //         memcpy(p->m_szStringValue, val, l);
    //         p->m_szStringValue[l] = '\0';
    //     }
    // }

    // private ExtraField* AddExtraField( const char* pszName, ExtraField::EType eType)
    // {
    //     if (m_nExtraFields >= k_nMaxExtraFields)
    //     {
    //         assert(false);
    //         return NULL;
    //     }
    //     ExtraField* p = &m_vecExtraFields[m_nExtraFields++];
    //     p->m_eType = eType;
    //
    //     size_t l = strlen(pszName);
    //     if (l > sizeof(p->m_szName) -1 )
    //         l = sizeof(p->m_szName)-1;
    //     memcpy(p->m_szName, pszName, l);
    //     p->m_szName[l] = '\0';
    //     return p;
    // }
}
