// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// Purpose: interface to user independent utility functions
/// </summary>
public class ISteamUtils
{
    private const int DefaultUtf8StringSize = 1024;

#if GNS_SHARP_STEAMWORKS_SDK
    private Dictionary<SteamAPICall_t, ICallTask> asyncCallTasks = [];
    private object asyncCallTasksLock = new();
#endif

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamUtils(bool isGameServer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        if (isGameServer)
        {
            this.ptr = Native.SteamAPI_SteamGameServerUtils_v010();
        }
        else
        {
            this.ptr = Native.SteamAPI_SteamUtils_v010();
        }
#endif
    }

#pragma warning disable CS0067 // The event is never used

    /// <summary>
    /// Called when the big picture gamepad text input has been closed.
    /// </summary>
    public event Callback<GamepadTextInputDismissed_t>? GamepadTextInputDismissed;

    /// <summary>
    /// Called when the floating keyboard invoked from <see cref="ShowFloatingGamepadTextInput"/> has been closed.
    /// </summary>
    public event Callback<FloatingGamepadTextInputDismissed_t>? FloatingGamepadTextInputDismissed;

    /// <summary>
    /// <para>
    /// Called when the country of the user changed. The country should be updated with <see cref="GetIPCountry"/>.
    /// </para>
    ///
    /// <para>
    /// This callback has no fields.
    /// </para>
    /// </summary>
    public event Callback<IPCountry_t>? IPCountryChanged;

    /// <summary>
    /// Called when running on a laptop and less than 10 minutes of battery is left, and then fires then every minute afterwards.
    /// </summary>
    public event Callback<LowBatteryPower_t>? LowBatteryPower;

    /// <summary>
    /// <para>
    /// Sent after the device returns from sleep/suspend mode.
    /// </para>
    ///
    /// <para>
    /// This callback has no fields.
    /// </para>
    /// </summary>
    public event Callback<AppResumingFromSuspend_t>? AppResumingFromSuspend;

    /// <summary>
    /// <para>
    /// Called when Steam wants to shutdown.
    /// </para>
    ///
    /// <para>
    /// This callback has no fields.
    /// </para>
    /// </summary>
    public event Callback<SteamShutdown_t>? SteamShutdown;

    /// <summary>
    /// callback for CheckFileSignature
    /// </summary>
    public event Callback<CheckFileSignature_t>? CheckFileSignatureCallback;

    /// <summary>
    /// The text filtering dictionary has changed
    /// </summary>
    public event Callback<FilterTextDictionaryChanged_t>? FilterTextDictionaryChanged;

#pragma warning restore CS0067

    public static ISteamUtils? User { get; internal set; }

    public static ISteamUtils? GameServer { get; internal set; }

    /// <summary>
    /// Returns the number of seconds since the application was active.
    /// </summary>
    public uint GetSecondsSinceAppActive()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetSecondsSinceAppActive(this.ptr);
#endif
    }

    /// <summary>
    /// Returns the number of seconds since the user last moved the mouse.
    /// </summary>
    public uint GetSecondsSinceComputerActive()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetSecondsSinceComputerActive(this.ptr);
#endif
    }

    /// <summary>
    /// the universe this client is connecting to
    /// </summary>
    public EUniverse GetConnectedUniverse()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetConnectedUniverse(this.ptr);
#endif
    }

    /// <summary>
    /// Steam server time.  Number of seconds since January 1, 1970, GMT (i.e unix time)
    /// </summary>
    public uint GetServerRealTime()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetServerRealTime(this.ptr);
#endif
    }

    /// <summary>
    /// returns the 2 digit ISO 3166-1-alpha-2 format country code this client is running in (as looked up via an IP-to-location database)<br/>
    /// e.g "US" or "UK".
    /// </summary>
    public string? GetIPCountry()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamUtils_GetIPCountry(this.ptr);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// returns true if the image exists, and valid sizes were filled out
    /// </summary>
    public bool GetImageSize(int image, out uint width, out uint height)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetImageSize(this.ptr, image, out width, out height);
#endif
    }

    /// <summary>
    /// returns true if the image exists, and the buffer was successfully filled out<br/>
    /// results are returned in RGBA format<br/>
    /// the destination buffer size should be 4 * height * width * sizeof(char)
    /// </summary>
    public bool GetImageRGBA(int image, Span<byte> dest)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetImageRGBA(this.ptr, image, dest, (int)dest.Length);
#endif
    }

    /// <summary>
    /// return the amount of battery power left in the current system in % [0..100], 255 for being on AC power
    /// </summary>
    public byte GetCurrentBatteryPower()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetCurrentBatteryPower(this.ptr);
#endif
    }

    /// <summary>
    /// returns the appID of the current process
    /// </summary>
    public uint GetAppID()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetAppID(this.ptr);
#endif
    }

    /// <summary>
    /// Sets the position where the overlay instance for the currently calling game should show notifications.<br/>
    /// This position is per-game and if this function is called from outside of a game context it will do nothing.
    /// </summary>
    public void SetOverlayNotificationPosition(ENotificationPosition notificationPosition)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUtils_SetOverlayNotificationPosition(this.ptr, notificationPosition);
#endif
    }

    /// <summary>
    /// API asynchronous call results<br/>
    /// can be used directly, but more commonly used via the callback dispatch API (see steam_api.h)
    /// </summary>
    public bool IsAPICallCompleted(SteamAPICall_t steamAPICall, out bool failed)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_IsAPICallCompleted(this.ptr, steamAPICall, out failed);
#endif
    }

    public ESteamAPICallFailure GetAPICallFailureReason(SteamAPICall_t steamAPICall)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetAPICallFailureReason(this.ptr, steamAPICall);
#endif
    }

    public bool GetAPICallResult(SteamAPICall_t steamAPICall, Span<byte> callback, int callbackExpected, out bool failed)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetAPICallResult(this.ptr, steamAPICall, callback, callback.Length, callbackExpected, out failed);
#endif
    }

    /// <summary>
    /// returns the number of IPC calls made since the last time this function was called<br/>
    /// Used for perf debugging so you can understand how many IPC calls your game makes per frame<br/>
    /// Every IPC call is at minimum a thread context switch if not a process one so you want to rate<br/>
    /// control how often you do them.
    /// </summary>
    public uint GetIPCCallCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetIPCCallCount(this.ptr);
#endif
    }

    /// <summary>
    /// API warning handling<br/>
    /// 'int' is the severity; 0 for msg, 1 for warning<br/>
    /// 'const char *' is the text of the message<br/>
    /// callbacks will occur directly after the API function is called that generated the warning or message
    /// </summary>
    public void SetWarningMessageHook(SteamAPIWarningMessageHook_t function)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUtils_SetWarningMessageHook(this.ptr, function);
#endif
    }

    /// <summary>
    /// Returns true if the overlay is running &amp; the user can access it. The overlay process could take a few seconds to<br/>
    /// start &amp; hook the game process, so this function will initially return false while the overlay is loading.
    /// </summary>
    public bool IsOverlayEnabled()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_IsOverlayEnabled(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Normally this call is unneeded if your game has a constantly running frame loop that calls the<br/>
    /// D3D Present API, or OGL SwapBuffers API every frame.
    /// </para>
    ///
    /// <para>
    /// However, if you have a game that only refreshes the screen on an event driven basis then that can break<br/>
    /// the overlay, as it uses your Present/SwapBuffers calls to drive it's internal frame loop and it may also<br/>
    /// need to Present() to the screen any time an even needing a notification happens or when the overlay is<br/>
    /// brought up over the game by a user.  You can use this API to ask the overlay if it currently need a present<br/>
    /// in that case, and then you can check for this periodically (roughly 33hz is desirable) and make sure you<br/>
    /// refresh the screen with Present or SwapBuffers to allow the overlay to do it's work.
    /// </para>
    /// </summary>
    public bool BOverlayNeedsPresent()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_BOverlayNeedsPresent(this.ptr);
#endif
    }

    /// <summary>
    /// Asynchronous call to check if an executable file has been signed using the public key set on the signing tab<br/>
    /// of the partner site, for example to refuse to load modified executable files.<br/>
    /// The result is returned in CheckFileSignature_t.<br/>
    ///   k_ECheckFileSignatureNoSignaturesFoundForThisApp - This app has not been configured on the signing tab of the partner site to enable this function.<br/>
    ///   k_ECheckFileSignatureNoSignaturesFoundForThisFile - This file is not listed on the signing tab for the partner site.<br/>
    ///   k_ECheckFileSignatureFileNotFound - The file does not exist on disk.<br/>
    ///   k_ECheckFileSignatureInvalidSignature - The file exists, and the signing tab has been set for this file, but the file is either not signed or the signature does not match.<br/>
    ///   k_ECheckFileSignatureValidSignature - The file is signed and the signature is valid.
    /// </summary>
    public CallTask<CheckFileSignature_t>? CheckFileSignature(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = this.SafeSteamAPICall<IntPtr, string, CheckFileSignature_t>(Native.SteamAPI_ISteamUtils_CheckFileSignature, this.ptr, fileName);

        return task;
#endif
    }

    /// <summary>
    /// Activates the full-screen text input dialog which takes a initial text string and returns the text the user has typed
    /// </summary>
    public bool ShowGamepadTextInput(EGamepadTextInputMode inputMode, EGamepadTextInputLineMode lineInputMode, string description, uint charMax, string existingText)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_ShowGamepadTextInput(this.ptr, inputMode, lineInputMode, description, charMax, existingText);
#endif
    }

    /// <summary>
    /// Returns previously entered text &amp; length
    /// </summary>
    public uint GetEnteredGamepadTextLength()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetEnteredGamepadTextLength(this.ptr);
#endif
    }

    public bool GetEnteredGamepadTextInput(out string? text, int utf8StringSize = DefaultUtf8StringSize)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        Span<byte> raw = stackalloc byte[utf8StringSize];
        bool result = Native.SteamAPI_ISteamUtils_GetEnteredGamepadTextInput(this.ptr, raw, (uint)raw.Length);

        if (result)
        {
            text = Utf8StringHelper.NullTerminatedSpanToString(raw);
        }
        else
        {
            text = null;
        }

        return result;
#endif
    }

    /// <summary>
    /// returns the language the steam client is running in, you probably want ISteamApps::GetCurrentGameLanguage instead, this is for very special usage cases
    /// </summary>
    public string? GetSteamUILanguage()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamUtils_GetSteamUILanguage(this.ptr);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    /// <summary>
    /// returns true if Steam itself is running in VR mode
    /// </summary>
    public bool IsSteamRunningInVR()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_IsSteamRunningInVR(this.ptr);
#endif
    }

    /// <summary>
    /// Sets the inset of the overlay notification from the corner specified by SetOverlayNotificationPosition.
    /// </summary>
    public void SetOverlayNotificationInset(int horizontalInset, int verticalInset)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUtils_SetOverlayNotificationInset(this.ptr, horizontalInset, verticalInset);
#endif
    }

    /// <summary>
    /// returns true if Steam &amp; the Steam Overlay are running in Big Picture mode<br/>
    /// Games much be launched through the Steam client to enable the Big Picture overlay. During development,<br/>
    /// a game can be added as a non-steam game to the developers library to test this feature
    /// </summary>
    public bool IsSteamInBigPictureMode()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_IsSteamInBigPictureMode(this.ptr);
#endif
    }

    /// <summary>
    /// ask SteamUI to create and render its OpenVR dashboard
    /// </summary>
    public void StartVRDashboard()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUtils_StartVRDashboard(this.ptr);
#endif
    }

    /// <summary>
    /// Returns true if the HMD content will be streamed via Steam Remote Play
    /// </summary>
    public bool IsVRHeadsetStreamingEnabled()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled(this.ptr);
#endif
    }

    /// <summary>
    /// Set whether the HMD content will be streamed via Steam Remote Play<br/>
    /// If this is set to true, then the scene in the HMD headset will be streamed, and remote input will not be allowed.<br/>
    /// If this is set to false, then the application window will be streamed instead, and remote input will be allowed.<br/>
    /// The default is true unless "VRHeadsetStreaming" "0" is in the extended appinfo for a game.<br/>
    /// (this is useful for games that have asymmetric multiplayer gameplay)
    /// </summary>
    public void SetVRHeadsetStreamingEnabled(bool enabled)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled(this.ptr, enabled);
#endif
    }

    /// <summary>
    /// Returns whether this steam client is a Steam China specific client, vs the global client.
    /// </summary>
    public bool IsSteamChinaLauncher()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_IsSteamChinaLauncher(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Initializes text filtering, loading dictionaries for the language the game is running in.<br/>
    ///   unFilterOptions are reserved for future use and should be set to 0<br/>
    /// Returns false if filtering is unavailable for the game's language, in which case FilterText() will act as a passthrough.
    /// </para>
    ///
    /// <para>
    /// Users can customize the text filter behavior in their Steam Account preferences:<br/>
    /// https://store.steampowered.com/account/preferences#CommunityContentPreferences
    /// </para>
    /// </summary>
    public bool InitFilterText(uint filterOptions = 0)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_InitFilterText(this.ptr, filterOptions);
#endif
    }

    /// <summary>
    /// Filters the provided input message and places the filtered result into pchOutFilteredText, using legally required filtering and additional filtering based on the context and user settings<br/>
    ///   eContext is the type of content in the input string<br/>
    ///   sourceSteamID is the Steam ID that is the source of the input string (e.g. the player with the name, or who said the chat text)<br/>
    ///   pchInputText is the input string that should be filtered, which can be ASCII or UTF-8<br/>
    ///   pchOutFilteredText is where the output will be placed, even if no filtering is performed<br/>
    ///   nByteSizeOutFilteredText is the size (in bytes) of pchOutFilteredText, should be at least strlen(pchInputText)+1<br/>
    /// Returns the number of characters (not bytes) filtered
    /// </summary>
    public int FilterText(ETextFilteringContext context, CSteamID sourceSteamID, string inputMessage, out string outFilteredText)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        int byteSizeOutFilteredText = Encoding.UTF8.GetByteCount(inputMessage) + 1;
        Span<byte> raw = stackalloc byte[byteSizeOutFilteredText];

        int result = Native.SteamAPI_ISteamUtils_FilterText(this.ptr, context, sourceSteamID, inputMessage, raw, (uint)raw.Length);

        outFilteredText = Utf8StringHelper.NullTerminatedSpanToString(raw);
        return result;
#endif
    }

    /// <summary>
    /// Return what we believe your current ipv6 connectivity to "the internet" is on the specified protocol.<br/>
    /// This does NOT tell you if the Steam client is currently connected to Steam via ipv6.
    /// </summary>
    public ESteamIPv6ConnectivityState GetIPv6ConnectivityState(ESteamIPv6ConnectivityProtocol protocol)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_GetIPv6ConnectivityState(this.ptr, protocol);
#endif
    }

    /// <summary>
    /// returns true if currently running on the Steam Deck device
    /// </summary>
    public bool IsSteamRunningOnSteamDeck()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_IsSteamRunningOnSteamDeck(this.ptr);
#endif
    }

    /// <summary>
    /// Opens a floating keyboard over the game content and sends OS keyboard keys directly to the game.<br/>
    /// The text field position is specified in pixels relative the origin of the game window and is used to position the floating keyboard in a way that doesn't cover the text field
    /// </summary>
    public bool ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode keyboardMode, int textFieldXPosition, int textFieldYPosition, int textFieldWidth, int textFieldHeight)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_ShowFloatingGamepadTextInput(this.ptr, keyboardMode, textFieldXPosition, textFieldYPosition, textFieldWidth, textFieldHeight);
#endif
    }

    /// <summary>
    /// In game launchers that don't have controller support you can call this to have Steam Input translate the controller input into mouse/kb to navigate the launcher
    /// </summary>
    public void SetGameLauncherMode(bool launcherMode)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamUtils_SetGameLauncherMode(this.ptr, launcherMode);
#endif
    }

    /// <summary>
    /// Dismisses the floating keyboard.
    /// </summary>
    public bool DismissFloatingGamepadTextInput()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_DismissFloatingGamepadTextInput(this.ptr);
#endif
    }

    /// <summary>
    /// Dismisses the full-screen text input dialog.
    /// </summary>
    public bool DismissGamepadTextInput()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamUtils");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamUtils_DismissGamepadTextInput(this.ptr);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    /* No variadic generics in C#?  Meh... */

    internal CallTask<TResult>? SafeSteamAPICall<T, TResult>(Func<T, SteamAPICall_t> nativeCall, T param)
            where TResult : unmanaged, ICallbackParam
            where T : allows ref struct
    {
        var task = new CallTask<TResult>();

        lock (this.asyncCallTasksLock)
        {
            SteamAPICall_t handle = nativeCall(param);

            if (handle == SteamAPICall_t.Invalid)
            {
                return null;
            }

            this.asyncCallTasks.Add(handle, task);
        }

        return task;
    }

    internal CallTask<TResult>? SafeSteamAPICall<T1, T2, TResult>(Func<T1, T2, SteamAPICall_t> nativeCall, T1 param1, T2 param2)
            where TResult : unmanaged, ICallbackParam
            where T1 : allows ref struct
            where T2 : allows ref struct
    {
        var task = new CallTask<TResult>();

        lock (this.asyncCallTasksLock)
        {
            SteamAPICall_t handle = nativeCall(param1, param2);

            if (handle == SteamAPICall_t.Invalid)
            {
                return null;
            }

            this.asyncCallTasks.Add(handle, task);
        }

        return task;
    }

    internal CallTask<TResult>? SafeSteamAPICall<T1, T2, T3, TResult>(Func<T1, T2, T3, SteamAPICall_t> nativeCall, T1 param1, T2 param2, T3 param3)
            where TResult : unmanaged, ICallbackParam
            where T1 : allows ref struct
            where T2 : allows ref struct
            where T3 : allows ref struct
    {
        var task = new CallTask<TResult>();

        lock (this.asyncCallTasksLock)
        {
            SteamAPICall_t handle = nativeCall(param1, param2, param3);

            if (handle == SteamAPICall_t.Invalid)
            {
                return null;
            }

            this.asyncCallTasks.Add(handle, task);
        }

        return task;
    }

    internal CallTask<TResult>? SafeSteamAPICall<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, SteamAPICall_t> nativeCall, T1 param1, T2 param2, T3 param3, T4 param4)
            where TResult : unmanaged, ICallbackParam
            where T1 : allows ref struct
            where T2 : allows ref struct
            where T3 : allows ref struct
            where T4 : allows ref struct
    {
        var task = new CallTask<TResult>();

        lock (this.asyncCallTasksLock)
        {
            SteamAPICall_t handle = nativeCall(param1, param2, param3, param4);

            if (handle == SteamAPICall_t.Invalid)
            {
                return null;
            }

            this.asyncCallTasks.Add(handle, task);
        }

        return task;
    }

    internal CallTask<TResult>? SafeSteamAPICall<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, SteamAPICall_t> nativeCall, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            where TResult : unmanaged, ICallbackParam
            where T1 : allows ref struct
            where T2 : allows ref struct
            where T3 : allows ref struct
            where T4 : allows ref struct
            where T5 : allows ref struct
    {
        var task = new CallTask<TResult>();

        lock (this.asyncCallTasksLock)
        {
            SteamAPICall_t handle = nativeCall(param1, param2, param3, param4, param5);

            if (handle == SteamAPICall_t.Invalid)
            {
                return null;
            }

            this.asyncCallTasks.Add(handle, task);
        }

        return task;
    }

    internal CallTask<TResult>? SafeSteamAPICall<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, SteamAPICall_t> nativeCall, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
            where TResult : unmanaged, ICallbackParam
            where T1 : allows ref struct
            where T2 : allows ref struct
            where T3 : allows ref struct
            where T4 : allows ref struct
            where T5 : allows ref struct
            where T6 : allows ref struct
    {
        var task = new CallTask<TResult>();

        lock (this.asyncCallTasksLock)
        {
            SteamAPICall_t handle = nativeCall(param1, param2, param3, param4, param5, param6);

            if (handle == SteamAPICall_t.Invalid)
            {
                return null;
            }

            this.asyncCallTasks.Add(handle, task);
        }

        return task;
    }

    internal void OnDispatch(HSteamPipe pipe, ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case SteamAPICallCompleted_t.CallbackId:
                this.HandleCallCompletedResult(pipe, ref msg);
                break;

            case GamepadTextInputDismissed_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<GamepadTextInputDismissed_t>();
                    this.GamepadTextInputDismissed?.Invoke(ref data);
                    break;
                }

            case FloatingGamepadTextInputDismissed_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FloatingGamepadTextInputDismissed_t>();
                    this.FloatingGamepadTextInputDismissed?.Invoke(ref data);
                    break;
                }

            case IPCountry_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<IPCountry_t>();
                    this.IPCountryChanged?.Invoke(ref data);
                    break;
                }

            case LowBatteryPower_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<LowBatteryPower_t>();
                    this.LowBatteryPower?.Invoke(ref data);
                    break;
                }

            case AppResumingFromSuspend_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<AppResumingFromSuspend_t>();
                    this.AppResumingFromSuspend?.Invoke(ref data);
                    break;
                }

            case SteamShutdown_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<SteamShutdown_t>();
                    this.SteamShutdown?.Invoke(ref data);
                    break;
                }

            case CheckFileSignature_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<CheckFileSignature_t>();
                    this.CheckFileSignatureCallback?.Invoke(ref data);
                    break;
                }

            case FilterTextDictionaryChanged_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<FilterTextDictionaryChanged_t>();
                    this.FilterTextDictionaryChanged?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamUtils.OnDispatch()");
                break;
        }
    }

    private void HandleCallCompletedResult(HSteamPipe pipe, ref CallbackMsg_t msg)
    {
        ref var callCompleted = ref msg.GetCallbackParamAs<SteamAPICallCompleted_t>();

        ICallTask? task;

        lock (this.asyncCallTasksLock)
        {
            if (this.asyncCallTasks.TryGetValue(callCompleted.AsyncCall, out task))
            {
                this.asyncCallTasks.Remove(callCompleted.AsyncCall);
            }
        }

        if (task != null)
        {
            task.SetResultFrom(pipe, ref callCompleted);
        }
        else
        {
            Debug.WriteLine($"Got unexpected call result #{callCompleted.AsyncCall}, Id = {callCompleted.AsyncCallbackId}");
        }
    }

#endif
}
