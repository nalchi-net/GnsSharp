// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/* TODO: Port methods other than launch params */

/// <summary>
/// Exposes a wide range of information and actions for applications and <a href="https://partner.steamgames.com/doc/store/application/dlc">Downloadable Content (DLC)</a>.
/// </summary>
public class ISteamApps
{
    /// <summary>
    /// Max supported length of a legacy cd key.<br/>
    /// Only used internally in Steam.
    /// </summary>
    public const int AppProofOfPurchaseKeyMax = 240;

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamApps()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamApps");
#elif GNS_SHARP_STEAMWORKS_SDK
        this.ptr = Native.SteamAPI_SteamApps_v008();
#endif
    }

#pragma warning disable CS0067 // The event is never used

    /// <summary>
    /// Triggered after the current user gains ownership of DLC and that DLC is installed.
    /// </summary>
    public event Callback<DlcInstalled_t>? DlcInstalled;

    /// <summary>
    /// Posted after the user executes a steam url with command line or query parameters such as<br/>
    /// steam://run/(appid)//?param1=value1;param2=value2;param3=value3;<br/>
    /// while the game is already running.<br/>
    /// The new params can be queried with <see cref="GetLaunchCommandLine"/> and <see cref="GetLaunchQueryParam"/>.
    /// </summary>
    public event Callback<NewUrlLaunchParameters_t>? NewUrlLaunchParameters;

    /// <summary>
    /// Only used internally in Steam.
    /// </summary>
    public event Callback<AppProofOfPurchaseKeyResponse_t>? AppProofOfPurchaseKeyResponse;

    /// <summary>
    /// Called after requesting the details of a specific file.
    /// </summary>
    public event Callback<FileDetailsResult_t>? FileDetailsResult;

    /// <summary>
    /// Sent every minute when a appID is owned via a timed trial.
    /// </summary>
    public event Callback<TimedTrialStatus_t>? TimedTrialStatus;

#pragma warning restore CS0067

    public static ISteamApps? User { get; internal set; }

    public IntPtr Ptr => this.ptr;

    /// <summary>
    /// <para>
    /// Gets the command line if the game was launched via Steam URL, e.g. steam://run/(appid)//(command line)/.<br/>
    /// This method of passing a connect string (used when joining via rich presence, accepting an invite, etc)<br/>
    /// is preferable to launching with a command line via the operating system, which can be a security risk.<br/>
    /// In order for rich presence joins to go through this and not be placed on the OS command line,<br/>
    /// you must enable "Use launch command line" from the Installation &gt; General page on your app.<br/>
    /// Or you must set a value in your app's configuration on Steam.  Ask Valve for help with this.
    /// </para>
    /// <para>
    /// If game was already running and launched again, the <see cref="NewUrlLaunchParameters"/> will be fired.
    /// </para>
    /// </summary>
    /// <param name="commandLine">The string that the command line will be copied into.</param>
    /// <param name="commandLineSize">The size in UTF-8 bytes of the <paramref name="commandLine"/>.</param>
    /// <returns><see cref="int"/> Returns the command line as a string into the buffer provided in <paramref name="commandLine"/> and returns the number of bytes that were copied into that buffer.</returns>
    public int GetLaunchCommandLine(out string commandLine, int commandLineSize)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamApps");
#elif GNS_SHARP_STEAMWORKS_SDK
        Span<byte> span = stackalloc byte[commandLineSize];
        int result = Native.SteamAPI_ISteamApps_GetLaunchCommandLine(this.ptr, span, span.Length);

        commandLine = Utf8StringHelper.NullTerminatedSpanToString(span);
        return result;
#endif
    }

    /// <summary>
    /// <para>
    /// Gets the associated launch parameter if the game is run via steam://run/(appid)/?param1=value1;param2=value2;param3=value3 etc.
    /// </para>
    /// <para>
    /// Parameter names starting with the character '@' are reserved for internal use and will always return an empty string.<br/>
    /// Parameter names starting with an underscore '_' are reserved for steam features -- they can be queried by the game,<br/>
    /// but it is advised that you not param names beginning with an underscore for your own features.
    /// </para>
    /// <para>
    /// Check for new launch parameters on <see cref="NewUrlLaunchParameters"/> callback.
    /// </para>
    /// </summary>
    /// <param name="key">The launch key to test for. Ex: param1</param>
    /// <returns>The value associated with the key provided. Returns an empty string (<c>""</c>) if the specified key does not exist.</returns>
    public string? GetLaunchQueryParam(string key)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamApps");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamApps_GetLaunchQueryParam(this.ptr, key);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case DlcInstalled_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<DlcInstalled_t>();
                    this.DlcInstalled?.Invoke(ref data);
                    break;
                }

            case NewUrlLaunchParameters_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<NewUrlLaunchParameters_t>();
                    this.NewUrlLaunchParameters?.Invoke(ref data);
                    break;
                }

            case AppProofOfPurchaseKeyResponse_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<AppProofOfPurchaseKeyResponse_t>();
                    this.AppProofOfPurchaseKeyResponse?.Invoke(ref data);
                    break;
                }

            case FileDetailsResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FileDetailsResult_t>();
                    this.FileDetailsResult?.Invoke(ref data);
                    break;
                }

            case TimedTrialStatus_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<TimedTrialStatus_t>();
                    this.TimedTrialStatus?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamApps.OnDispatch()");
                break;
        }
    }

#endif
}
