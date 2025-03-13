// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// <para>
/// In a few places we need to set configuration options on listen sockets and connections, and<br/>
/// have them take effect *before* the listen socket or connection really starts doing anything.<br/>
/// Creating the object and then setting the options "immediately" after creation doesn't work<br/>
/// completely, because network packets could be received between the time the object is created and<br/>
/// when the options are applied.  To set options at creation time in a reliable way, they must be<br/>
/// passed to the creation function.  This structure is used to pass those options.
/// </para>
///
/// <para>
/// For the meaning of these fields, see <c>ISteamNetworkingUtils::SetConfigValue</c>.  Basically<br/>
/// when the object is created, we just iterate over the list of options and call<br/>
/// <c>ISteamNetworkingUtils::SetConfigValueStruct</c>, where the scope arguments are supplied by the<br/>
/// object being created.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SteamNetworkingConfigValue_t : IDisposable
{
    /// <summary>
    /// Which option is being set
    /// </summary>
    public ESteamNetworkingConfigValue OptionKind;

    /// <summary>
    /// Which field below did you fill in?
    /// </summary>
    public ESteamNetworkingConfigDataType DataType;

    /// <summary>
    /// Option value
    /// </summary>
    public OptionValueUnion OptionValue;

    /* Shortcut helpers to set the type and value in a single call */

    public void SetInt32(ESteamNetworkingConfigValue optionKind, int data)
    {
        this.Dispose();

        this.OptionKind = optionKind;
        this.DataType = ESteamNetworkingConfigDataType.Int32;
        this.OptionValue.Int32 = data;
    }

    public void SetInt64(ESteamNetworkingConfigValue optionKind, long data)
    {
        this.Dispose();

        this.OptionKind = optionKind;
        this.DataType = ESteamNetworkingConfigDataType.Int64;
        this.OptionValue.Int64 = data;
    }

    public void SetFloat(ESteamNetworkingConfigValue optionKind, float data)
    {
        this.Dispose();

        this.OptionKind = optionKind;
        this.DataType = ESteamNetworkingConfigDataType.Float;
        this.OptionValue.Float = data;
    }

    public void SetPtr(ESteamNetworkingConfigValue optionKind, IntPtr data)
    {
        this.Dispose();

        this.OptionKind = optionKind;
        this.DataType = ESteamNetworkingConfigDataType.Ptr;
        this.OptionValue.Ptr = data;
    }

    /// <summary>
    /// Unlike original GNS, this method allocates an unmanaged string inside of <see cref="SteamNetworkingConfigValue_t"/>.<br/>
    /// So, you MUST <see cref="Dispose"/> it to free the unmanaged string.
    /// </summary>
    public void SetString(ESteamNetworkingConfigValue optionKind, string str)
    {
        this.Dispose();

        this.OptionKind = optionKind;
        this.DataType = ESteamNetworkingConfigDataType.String;

        // Store the unmanaged pointer to converted UTF-8 string.
        IntPtr ptr = Utf8StringHelper.AllocHGlobalString(str);
        this.OptionValue.String = ptr;
    }

    /// <summary>
    /// Disposes the unmanaged string if preset
    /// </summary>
    public void Dispose()
    {
        if (this.DataType == ESteamNetworkingConfigDataType.String)
        {
            // Free the unmanaged string buffer.
            Marshal.FreeHGlobal(this.OptionValue.String);

            this.DataType = (ESteamNetworkingConfigDataType)0;
        }
    }

    /// <summary>
    /// Option value
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct OptionValueUnion
    {
        [FieldOffset(0)]
        public int Int32;

        [FieldOffset(0)]
        public long Int64;

        [FieldOffset(0)]
        public float Float;

        /// <summary>
        /// Points to your '\0'-terminated buffer
        /// </summary>
        [FieldOffset(0)]
        public IntPtr String;

        [FieldOffset(0)]
        public IntPtr Ptr;
    }
}
