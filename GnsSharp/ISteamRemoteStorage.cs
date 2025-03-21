// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// Provides functions for reading, writing, and accessing files which can be stored remotely in the Steam Cloud.
/// </para>
/// <para>
/// See <a href="https://partner.steamgames.com/doc/features/cloud">Steam Cloud</a> for more information.
/// </para>
/// </summary>
public class ISteamRemoteStorage
{
    /// <summary>
    /// Defines the largest allowed file size.<br/>
    /// Cloud files cannot be written in a single chunk over 100MB (and cannot be over 200MB total.)
    /// </summary>
    public const int MaxCloudFileChunkSize = 100 * 1024 * 1024;

    public const int FilenameMax = 260;

    private IntPtr ptr = IntPtr.Zero;

    internal ISteamRemoteStorage()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        this.ptr = Native.SteamAPI_SteamRemoteStorage_v016();
#endif
    }

#pragma warning disable CS0067 // The event is never used

    public event Callback<RemoteStorageDownloadUGCResult_t>? RemoteStorageDownloadUGCResult;

    /// <summary>
    /// Response when reading a file asyncrounously with <see cref="FileReadAsync"/>.
    /// </summary>
    public event Callback<RemoteStorageFileReadAsyncComplete_t>? RemoteStorageFileReadAsyncComplete;

    /// <summary>
    /// The result of a call to <see cref="FileShare"/>
    /// </summary>
    public event Callback<RemoteStorageFileShareResult_t>? RemoteStorageFileShareResult;

    /// <summary>
    /// Response when writing a file asyncrounously with <see cref="FileWriteAsync"/>.
    /// </summary>
    public event Callback<RemoteStorageFileWriteAsyncComplete_t>? RemoteStorageFileWriteAsyncComplete;

    /// <summary>
    /// If a Steam app is flagged for supporting dynamic Steam Cloud sync, and a sync occurs, this callback will be posted to the app if any local files changed.
    /// </summary>
    public event Callback<RemoteStorageLocalFileChange_t>? RemoteStorageLocalFileChange;

    /// <summary>
    /// User subscribed to a file for the app (from within the app or on the web)
    /// </summary>
    public event Callback<RemoteStoragePublishedFileSubscribed_t>? RemoteStoragePublishedFileSubscribed;

    /// <summary>
    /// User unsubscribed from a file for the app (from within the app or on the web)
    /// </summary>
    public event Callback<RemoteStoragePublishedFileUnsubscribed_t>? RemoteStoragePublishedFileUnsubscribed;

    /// <summary>
    /// Called when the user has subscribed to a piece of UGC. Result from <see cref="ISteamUGC.SubscribeItem"/>.
    /// </summary>
    public event Callback<RemoteStorageSubscribePublishedFileResult_t>? RemoteStorageSubscribePublishedFileResult;

    /// <summary>
    /// Called when the user has unsubscribed from a piece of UGC. Result from <see cref="ISteamUGC.UnsubscribeItem"/>.
    /// </summary>
    public event Callback<RemoteStorageUnsubscribePublishedFileResult_t>? RemoteStorageUnsubscribePublishedFileResult;

#pragma warning restore CS0067

    public static ISteamRemoteStorage? User { get; internal set; }

    // NOTE
    //
    // Filenames are case-insensitive, and will be converted to lowercase automatically.
    // So "foo.bar" and "Foo.bar" are the same file, and if you write "Foo.bar" then
    // iterate the files, the filename returned will be "foo.bar".
    //

    // file operations

    /// <summary>
    /// Creates a new file, writes the bytes to the file, and then closes the file.<br/>
    /// If the target file already exists, it is overwritten.
    /// </summary>
    /// <remarks>
    /// NOTE: This is a synchronous call and as such is a will block your calling thread on the disk IO, and will also block the SteamAPI, which can cause other threads in your application to block.<br/>
    /// To avoid "hitching" due to a busy disk on the client machine using <see cref="FileWriteAsync"/>, the asynchronous version of this API is recommended.
    /// </remarks>
    /// <param name="fileName">The name of the file to write to.</param>
    /// <param name="data">The bytes to write to the file.</param>
    /// <returns>
    /// <para>
    /// <see cref="bool"/> <c>true</c> if the write was successful.
    /// </para>
    /// <para>
    /// Otherwise, <c>false</c> under the following conditions:<br/>
    /// * The file you're trying to write is larger than 100MiB as defined by <see cref="MaxCloudFileChunkSize"/>.<br/>
    /// * <paramref name="data"/> is empty.<br/>
    /// * You tried to write to an invalid path or filename. Because Steam Cloud is cross platform the files need to have valid names on all supported OSes and file systems. See Microsoft's documentation on <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365247(v=vs.85).aspx">Naming Files, Paths, and Namespaces</a>.<br/>
    /// * The current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.<br/>
    /// * Steam could not write to the disk, the location might be read-only.
    /// </para>
    /// </returns>
    public bool FileWrite(string fileName, ReadOnlySpan<byte> data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileWrite(this.ptr, fileName, data, data.Length);
#endif
    }

    /// <summary>
    /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
    /// </summary>
    /// <remarks>
    /// NOTE: This is a synchronous call and as such is a will block your calling thread on the disk IO, and will also block the SteamAPI, which can cause other threads in your application to block.<br/>
    /// To avoid "hitching" due to a busy disk on the client machine using <see cref="FileReadAsync"/>, the asynchronous version of this API is recommended.
    /// </remarks>
    /// <param name="fileName">The name of the file to read from.</param>
    /// <param name="data">The buffer that the file will be read into.<br/>
    /// The amount of bytes to read is generally obtained from <see cref="GetFileSize"/> or <see cref="GetFileTimestamp"/>.</param>
    /// <returns>
    /// <para>
    /// <see cref="int"/> The number of bytes read.
    /// </para>
    /// <para>
    /// Returns <c>0</c> if the file doesn't exist or the read fails.
    /// </para>
    /// </returns>
    public int FileRead(string fileName, Span<byte> data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileRead(this.ptr, fileName, data, data.Length);
#endif
    }

    /// <summary>
    /// Creates a new file and asynchronously writes the raw byte data to the Steam Cloud, and then closes the file.<br/>
    /// If the target file already exists, it is overwritten.
    /// </summary>
    /// <param name="fileName">The name of the file to write to.</param>
    /// <param name="data">The bytes to write to the file.</param>
    /// <returns>
    /// <see cref="CallTask&lt;RemoteStorageFileWriteAsyncComplete_t&gt;"/> that will return <see cref="RemoteStorageFileWriteAsyncComplete_t"/> when awaited.<br/>
    /// Returns <c>null</c> under the following conditions:<br/>
    /// * The file you're trying to write is larger than 100MiB as defined by <see cref="MaxCloudFileChunkSize"/>.<br/>
    /// * <paramref name="data"/> is empty.<br/>
    /// * You tried to write to an invalid path or filename. Because Steam Cloud is cross platform the files need to have valid names on all supported OSes and file systems. See Microsoft's documentation on <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365247(v=vs.85).aspx">Naming Files, Paths, and Namespaces</a>.<br/>
    /// * The current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.<br/>
    /// </returns>
    public CallTask<RemoteStorageFileWriteAsyncComplete_t>? FileWriteAsync(string fileName, ReadOnlySpan<byte> data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, string, ReadOnlySpan<byte>, uint, RemoteStorageFileWriteAsyncComplete_t>(
        Native.SteamAPI_ISteamRemoteStorage_FileWriteAsync, this.ptr, fileName, data, (uint)data.Length);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Starts an asynchronous read from a file.
    /// </para>
    /// <para>
    /// The offset and amount to read should be valid for the size of the file, as indicated by <see cref="GetFileSize"/> or <see cref="GetFileTimestamp"/>.
    /// </para>
    /// </summary>
    /// <param name="fileName">The name of the file to read from.</param>
    /// <param name="offset">The offset in bytes into the file where the read will start from. <c>0</c> if you're reading the whole file in one chunk.</param>
    /// <param name="sizeToRead">The amount of bytes to read starting from <paramref name="offset"/>.</param>
    /// <returns>
    /// <para>
    /// <see cref="CallTask&lt;RemoteStorageFileReadAsyncComplete_t&gt;"/> that will return <see cref="RemoteStorageFileReadAsyncComplete_t"/> when awaited.<br/>
    /// Returns <c>null</c> under the following conditions:<br/>
    /// * You tried to write to an invalid path or filename. Because Steam Cloud is cross platform the files need to have valid names on all supported OSes and file systems. See Microsoft's documentation on <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365247(v=vs.85).aspx">Naming Files, Paths, and Namespaces</a>.<br/>
    /// * The file doesn't exist.<br/>
    /// * <paramref name="sizeToRead"/> is &lt;= 0 bytes. You need to be able to read something!<br/>
    /// * The combination of <paramref name="offset"/> and <paramref name="sizeToRead"/> would read past the end of the file.<br/>
    /// * You have an async read in progress on this file already.
    /// </para>
    /// <para>
    /// Upon completion of the read request you will receive the call result, if the value of <see cref="RemoteStorageFileReadAsyncComplete_t.Result"/> is <see cref="EResult.OK"/><br/>
    /// you can then call <see cref="FileReadAsyncComplete"/> to read the requested data into your buffer.<br/>
    /// The <c>readCall</c> parameter should match the return value of this function, and the amount to read should generally be equal to the amount requested as indicated by <paramref name="offset"/> and <paramref name="sizeToRead"/>.
    /// </para>
    /// </returns>
    public CallTask<RemoteStorageFileReadAsyncComplete_t>? FileReadAsync(string fileName, uint offset, uint sizeToRead)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, string, uint, uint, RemoteStorageFileReadAsyncComplete_t>(
Native.SteamAPI_ISteamRemoteStorage_FileReadAsync, this.ptr, fileName, offset, sizeToRead);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Copies the bytes from a file which was asynchronously read with <see cref="FileReadAsync"/> into a byte array.
    /// </para>
    /// <para>
    /// This should never be called outside of the context of a <see cref="RemoteStorageFileReadAsyncComplete_t"/> call result.
    /// </para>
    /// </summary>
    /// <param name="readCall">The call result handle obtained from <see cref="RemoteStorageFileReadAsyncComplete_t"/>.</param>
    /// <param name="buffer">The buffer that the file will be read into. The length should usually be the <see cref="RemoteStorageFileReadAsyncComplete_t.ReadSize"/></param>
    /// <returns>
    /// <para>
    /// <see cref="bool"/> <c>true</c> if the file was successfully read.
    /// </para>
    /// <para>
    /// Otherwise, <c>false</c> under the following conditions:<br/>
    /// * The handle passed to <paramref name="readCall"/> is invalid.<br/>
    /// * The read failed as indicated by <see cref="RemoteStorageFileReadAsyncComplete_t.Result"/>, you shouldn't have called this.<br/>
    /// * The <paramref name="buffer"/> isn't big enough.
    /// </para>
    /// </returns>
    public bool FileReadAsyncComplete(SteamAPICall_t readCall, Span<byte> buffer)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete(this.ptr, readCall, buffer, (uint)buffer.Length);
#endif
    }

    /// <summary>
    /// <para>
    /// Deletes the file from remote storage, but leaves it on the local disk and remains accessible from the API.
    /// </para>
    /// <para>
    /// When you are out of Cloud space, this can be used to allow calls to FileWrite to keep working without needing to make the user delete files.
    /// </para>
    /// <para>
    /// How you decide which files to forget are up to you. It could be a simple Least Recently Used (LRU) queue or something more complicated.
    /// </para>
    /// <para>
    /// Requiring the user to manage their Cloud-ized files for a game, while is possible to do, it is never recommended.<br/>
    /// For instance, "Which file would you like to delete so that you may store this new one?" removes a significant advantage of using the Cloud in the first place: its transparency.
    /// </para>
    /// <para>
    /// Once a file has been deleted or forgotten, calling <see cref="FileWrite"/> will resynchronize it in the Cloud.<br/>
    /// Rewriting a forgotten file is the only way to make it persisted again.
    /// </para>
    /// </summary>
    /// <param name="fileName">The name of the file that will be forgotten.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the file exists and has been successfully forgotten; otherwise, <c>false</c>.</returns>
    public bool FileForget(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileForget(this.ptr, fileName);
#endif
    }

    /// <summary>
    /// <para>
    /// Deletes a file from the local disk, and propagates that delete to the cloud.
    /// </para>
    /// <para>
    /// This is meant to be used when a user actively deletes a file.<br/>
    /// Use <see cref="FileForget"/> if you want to remove a file from the Steam Cloud but retain it on the users local disk.
    /// </para>
    /// <para>
    /// When a file has been deleted it can be re-written with <see cref="FileWrite"/> to reupload it to the Steam Cloud.
    /// </para>
    /// </summary>
    /// <param name="fileName">The name of the file that will be deleted.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the file exists and has been successfully deleted; otherwise, <c>false</c> if the file did not exist.</returns>
    public bool FileDelete(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileDelete(this.ptr, fileName);
#endif
    }

    /// <returns><see cref="CallTask&lt;RemoteStorageFileShareResult_t&gt;"/> that will return <see cref="RemoteStorageFileShareResult_t"/> when awaited.</returns>
    public CallTask<RemoteStorageFileShareResult_t>? FileShare(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, string, RemoteStorageFileShareResult_t>(
        Native.SteamAPI_ISteamRemoteStorage_FileShare, this.ptr, fileName);

        return task;
#endif
    }

    /// <summary>
    /// <para>
    /// Allows you to specify which operating systems a file will be synchronized to.
    /// </para>
    /// <para>
    /// Use this if you have a multiplatform game but have data which is incompatible between platforms.
    /// </para>
    /// <para>
    /// Files default to <see cref="ERemoteStoragePlatform.All"/> when they are first created.<br/>
    /// You can use the bitwise OR operator, <c>"|"</c> to specify multiple platforms.
    /// </para>
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="remoteStoragePlatform">The platforms that the file will be syncronized to.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the file exists, otherwise <c>false</c>.</returns>
    public bool SetSyncPlatforms(string fileName, ERemoteStoragePlatform remoteStoragePlatform)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_SetSyncPlatforms(this.ptr, fileName, remoteStoragePlatform);
#endif
    }

    // file operations that cause network IO

    /// <summary>
    /// <para>
    /// Creates a new file output stream allowing you to stream out data to the Steam Cloud file in chunks.<br/>
    /// If the target file already exists, it is not overwritten until <see cref="FileWriteStreamClose"/> has been called.
    /// </para>
    /// <para>
    /// To write data out to this stream you can use <see cref="FileWriteStreamWriteChunk"/>, and then to close or cancel you use <see cref="FileWriteStreamClose"/> and <see cref="FileWriteStreamCancel"/> respectively.
    /// </para>
    /// </summary>
    /// <param name="fileName">The name of the file to write to.</param>
    /// <returns>
    /// <see cref="UGCFileWriteStreamHandle_t"/> Returns <see cref="UGCFileWriteStreamHandle_t.Invalid"/> under the following conditions:<br/>
    /// * You tried to write to an invalid path or filename. Because Steam Cloud is cross platform the files need to have valid names on all supported OSes and file systems. See Microsoft's documentation on <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365247(v=vs.85).aspx">Naming Files, Paths, and Namespaces</a>.<br/>
    /// * The current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.
    /// </returns>
    public UGCFileWriteStreamHandle_t FileWriteStreamOpen(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen(this.ptr, fileName);
#endif
    }

    /// <summary>
    /// Writes a blob of data to the file write stream.
    /// </summary>
    /// <param name="writeHandle">The file write stream to write to.</param>
    /// <param name="data">The data to write to the stream.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the data was successfully written to the file write stream.<br/>
    /// <c>false</c> if <paramref name="writeHandle"/> is not a valid file write stream, Size of <paramref name="data"/> is larger than <see cref="MaxCloudFileChunkSize"/>, or the current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.
    /// </returns>
    public bool FileWriteStreamWriteChunk(UGCFileWriteStreamHandle_t writeHandle, ReadOnlySpan<byte> data)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk(this.ptr, writeHandle, data, data.Length);
#endif
    }

    /// <summary>
    /// <para>
    /// Closes a file write stream that was started by <see cref="FileWriteStreamOpen"/>.
    /// </para>
    /// <para>
    /// This flushes the stream to the disk, overwriting the existing file if there was one.
    /// </para>
    /// </summary>
    /// <param name="writeHandle">The file write stream to close.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the file write stream was successfully closed, the file has been committed to the disk.<br/>
    /// <c>false</c> if writeHandle is not a valid file write stream.
    /// </returns>
    public bool FileWriteStreamClose(UGCFileWriteStreamHandle_t writeHandle)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamClose(this.ptr, writeHandle);
#endif
    }

    /// <summary>
    /// <para>
    /// Cancels a file write stream that was started by <see cref="FileWriteStreamOpen"/>.
    /// </para>
    /// <para>
    /// This trashes all of the data written and closes the write stream, but if there was an existing file with this name, it remains untouched.
    /// </para>
    /// </summary>
    /// <param name="writeHandle">The file write stream to cancel.</param>
    public bool FileWriteStreamCancel(UGCFileWriteStreamHandle_t writeHandle)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel(this.ptr, writeHandle);
#endif
    }

    // file information

    /// <summary>
    /// Checks whether the specified file exists.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns><see cref="bool"/> <c>true</c> if the file exists; otherwise, <c>false</c>.</returns>
    public bool FileExists(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FileExists(this.ptr, fileName);
#endif
    }

    /// <summary>
    /// Checks if a specific file is persisted in the steam cloud.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>
    /// <see cref="bool"/> <c>true</c> if the file exists and the file is persisted in the Steam Cloud.<br/>
    /// <c>false</c> if FileForget was called on it and is only available locally.
    /// </returns>
    public bool FilePersisted(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_FilePersisted(this.ptr, fileName);
#endif
    }

    /// <summary>
    /// Gets the specified files size in bytes.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns><see cref="int"/> The size of the file in bytes. Returns <c>0</c> if the file does not exist.</returns>
    public int GetFileSize(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetFileSize(this.ptr, fileName);
#endif
    }

    /// <summary>
    /// Gets the specified file's last modified timestamp in Unix epoch format (seconds since Jan 1st 1970).
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns><see cref="long"/> The last modified timestamp in Unix epoch format (seconds since Jan 1st 1970).</returns>
    public long GetFileTimestamp(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetFileTimestamp(this.ptr, fileName);
#endif
    }

    /// <summary>
    /// Obtains the platforms that the specified file will syncronize to.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns><see cref="ERemoteStoragePlatform"/> Bitfield containing the platforms that the file was set to with <see cref="SetSyncPlatforms"/>.</returns>
    public ERemoteStoragePlatform GetSyncPlatforms(string fileName)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetSyncPlatforms(this.ptr, fileName);
#endif
    }

    // iteration

    /// <summary>
    /// <para>
    /// Gets the total number of local files synchronized by Steam Cloud.
    /// </para>
    /// <para>
    /// Used for enumeration with <see cref="GetFileNameAndSize"/>.
    /// </para>
    /// </summary>
    /// <returns><see cref="int"/> The number of files present for the current user, including files in subfolders.</returns>
    public int GetFileCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetFileCount(this.ptr);
#endif
    }

    /// <summary>
    /// Gets the file name and size of a file from the index.
    /// </summary>
    /// <remarks>
    /// NOTE: You must call <see cref="GetFileCount"/> first to get the number of files.
    /// </remarks>
    /// <param name="fileIndex">The index of the file, this should be between <c>0</c> and <see cref="GetFileCount"/>.</param>
    /// <param name="fileSizeInBytes">Returns the file size in bytes.</param>
    /// <returns><see cref="string"/> The name of the file at the specified index, if it exists. Returns an empty string (<c>""</c>) if the file doesn't exist.</returns>
    public string? GetFileNameAndSize(int fileIndex, out int fileSizeInBytes)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamRemoteStorage_GetFileNameAndSize(this.ptr, fileIndex, out fileSizeInBytes);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    // configuration management

    /// <summary>
    /// Gets the number of bytes available, and used on the users Steam Cloud storage.
    /// </summary>
    /// <param name="totalBytes">Returns the total amount of bytes the user has access to.</param>
    /// <param name="availableBytes">Returns the number of bytes available.</param>
    /// <returns><see cref="bool"/> This function always returns true.</returns>
    public bool GetQuota(out ulong totalBytes, out ulong availableBytes)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetQuota(this.ptr, out totalBytes, out availableBytes);
#endif
    }

    /// <summary>
    /// <para>
    /// Checks if the account wide Steam Cloud setting is enabled for this user; or if they disabled it in the Settings->Cloud dialog.
    /// </para>
    /// <para>
    /// Ensure that you are also checking <see cref="IsCloudEnabledForApp"/>, as these two options are mutually exclusive.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if Steam Cloud is enabled for this account; otherwise, <c>false</c>.</returns>
    public bool IsCloudEnabledForAccount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Checks if the per game Steam Cloud setting is enabled for this user; or if they disabled it in the Game Properties->Update dialog.
    /// </para>
    /// <para>
    /// Ensure that you are also checking <see cref="IsCloudEnabledForAccount"/>, as these two options are mutually exclusive.
    /// </para>
    /// <para>
    /// It's generally recommended that you allow the user to toggle this setting within your in-game options, you can toggle it with SetCloudEnabledForApp.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if Steam Cloud is enabled for this app; otherwise, <c>false</c>.</returns>
    public bool IsCloudEnabledForApp()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// Toggles whether the Steam Cloud is enabled for your application.
    /// </para>
    /// <para>
    /// This setting can be queried with <see cref="IsCloudEnabledForApp"/>.
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: This must only ever be called as the direct result of the user explicitly requesting that it's enabled or not.<br/>
    /// This is typically accomplished with a checkbox within your in-game options.
    /// </remarks>
    /// <param name="enabled">Enable (<c>true</c>) or disable (<c>false</c>) the Steam Cloud for this application?</param>
    public void SetCloudEnabledForApp(bool enabled)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        Native.SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp(this.ptr, enabled);
#endif
    }

    // user generated content

    /// <summary>
    /// Downloads a UGC file.  A priority value of <c>0</c> will download the file immediately,<br/>
    /// otherwise it will wait to download the file until all downloads with a lower priority<br/>
    /// value are completed.  Downloads with equal priority will occur simultaneously.
    /// </summary>
    /// <returns><see cref="CallTask&lt;RemoteStorageDownloadUGCResult_t&gt;"/> that will return <see cref="RemoteStorageDownloadUGCResult_t"/> when awaited.</returns>
    public CallTask<RemoteStorageDownloadUGCResult_t>? UGCDownload(UGCHandle_t content, uint priority)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, UGCHandle_t, uint, RemoteStorageDownloadUGCResult_t>(
        Native.SteamAPI_ISteamRemoteStorage_UGCDownload, this.ptr, content, priority);

        return task;
#endif
    }

    /// <summary>
    /// Gets the amount of data downloaded so far for a piece of content. <paramref name="bytesExpected"/> can be <c>0</c> if function returns <c>false</c><br/>
    /// or if the transfer hasn't started yet, so be careful to check for that before dividing to get a percentage
    /// </summary>
    public bool GetUGCDownloadProgress(UGCHandle_t content, out int bytesDownloaded, out int bytesExpected)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress(this.ptr, content, out bytesDownloaded, out bytesExpected);
#endif
    }

    /// <summary>
    /// Gets metadata for a file after it has been downloaded.<br/>
    /// This is the same metadata given in the <see cref="RemoteStorageDownloadUGCResult_t"/> call result
    /// </summary>
    public bool GetUGCDetails(UGCHandle_t content, out AppId_t appID, Span<IntPtr> name, out int fileSizeInBytes, out CSteamID steamIDOwner)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetUGCDetails(this.ptr, content, out appID, name, out fileSizeInBytes, out steamIDOwner);
#endif
    }

    /// <summary>
    /// After download, gets the content of the file.<br/>
    /// Small files can be read all at once by calling this function with an offset of <c>0</c> and <paramref name="data.Length"/> equal to the size of the file.<br/>
    /// Larger files can be read in chunks to reduce memory usage (since both sides of the IPC client and the game itself must allocate<br/>
    /// enough memory for each chunk).  Once the last byte is read, the file is implicitly closed and further calls to <see cref="UGCRead"/> will fail<br/>
    /// unless <see cref="UGCDownload"/> is called again.<br/>
    /// For especially large files (anything over 100MB) it is a requirement that the file is read in chunks.
    /// </summary>
    public int UGCRead(UGCHandle_t content, Span<byte> data, uint offset, EUGCReadAction action)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_UGCRead(this.ptr, content, data, data.Length, offset, action);
#endif
    }

    /* Functions to iterate through UGC that has finished downloading but has not yet been read via UGCRead() */

    public int GetCachedUGCCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetCachedUGCCount(this.ptr);
#endif
    }

    public UGCHandle_t GetCachedUGCHandle(int cachedContentIndex)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle(this.ptr, cachedContentIndex);
#endif
    }

    /// <returns><see cref="CallTask&lt;RemoteStorageDownloadUGCResult_t&gt;"/> that will return <see cref="RemoteStorageDownloadUGCResult_t"/> when awaited.</returns>
    public CallTask<RemoteStorageDownloadUGCResult_t>? UGCDownloadToLocation(UGCHandle_t content, string location, uint priority)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        var task = ISteamUtils.User!.SafeSteamAPICall<IntPtr, UGCHandle_t, string, uint, RemoteStorageDownloadUGCResult_t>(
        Native.SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation, this.ptr, content, location, priority);

        return task;
#endif
    }

    // Cloud dynamic state change notification

    /// <summary>
    /// When your application receives a <see cref="RemoteStorageLocalFileChange_t"/>, use this method to get the number of changes (file updates and file deletes) that have been made.<br/>
    /// You can then iterate the changes using <see cref="GetLocalFileChange"/>.
    /// </summary>
    /// <remarks>
    /// Note: only applies to applications flagged as supporting dynamic Steam Cloud sync.
    /// </remarks>
    /// <returns><see cref="int"/> The number of local file changes that have occurred.</returns>
    public int GetLocalFileChangeCount()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_GetLocalFileChangeCount(this.ptr);
#endif
    }

    /// <summary>
    /// <para>
    /// After calling <see cref="GetLocalFileChangeCount"/>, use this method to iterate over the changes.<br/>
    /// The changes described have already been made to local files.<br/>
    /// Your application should take appropriate action to reload state from disk, and possibly notify the user.
    /// </para>
    /// <para>
    /// For example: The local system had been suspended, during which time the user played elsewhere and uploaded changes to the Steam Cloud.<br/>
    /// On resume, Steam downloads those changes to the local system before resuming the application.<br/>
    /// The application receives an <see cref="RemoteStorageLocalFileChange_t"/>, and uses <see cref="GetLocalFileChangeCount"/> and <see cref="GetLocalFileChange"/> to iterate those changes.<br/>
    /// Depending on the application structure and the nature of the changes, the application could:<br/>
    /// * Re-load game progress to resume at exactly the point where the user was when they exited the game on the other device<br/>
    /// * Notify the user of any synchronized changes that don't require reloading<br/>
    /// * etc
    /// </para>
    /// </summary>
    /// <param name="fileIndex">Zero-based index of the change</param>
    /// <param name="changeType">What happened to this file</param>
    /// <param name="filePathType">Type of path to the file returned</param>
    /// <returns><see cref="string"/> The file name or full path of the file affected by this change. See comments on <paramref name="filePathType"/> above for more detail.</returns>
    public string? GetLocalFileChange(int fileIndex, out ERemoteStorageLocalFileChange changeType, out ERemoteStorageFilePathType filePathType)
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK

        string? result = null;

        IntPtr raw = Native.SteamAPI_ISteamRemoteStorage_GetLocalFileChange(this.ptr, fileIndex, out changeType, out filePathType);
        if (raw != IntPtr.Zero)
        {
            result = Marshal.PtrToStringUTF8(raw);
        }

        return result;
#endif
    }

    // Indicate to Steam the beginning / end of a set of local file
    // operations - for example, writing a game save that requires updating two files.

    /// <summary>
    /// <para>
    /// Use this along with <see cref="EndFileWriteBatch"/> to wrap a set of local file writes/deletes that should be considered part of one single state change.<br/>
    /// For example, if saving game progress requires updating both <c>savegame1.dat</c> and <c>maxprogress.dat</c>,<br/>
    /// wrap those operations with calls to <see cref="BeginFileWriteBatch"/> and <see cref="EndFileWriteBatch"/>.
    /// </para>
    /// <para>
    /// These functions provide a hint to Steam which will help it manage the app's Cloud files. Using these functions is optional, however it will provide better reliability.
    /// </para>
    /// <para>
    /// Note that the functions may be used whether the writes are done using the <see cref="ISteamRemoteStorage"/> API, or done directly to local disk (where AutoCloud is used).
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if the write batch was begun, <c>false</c> if there was a batch already in progress.</returns>
    public bool BeginFileWriteBatch()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_BeginFileWriteBatch(this.ptr);
#endif
    }

    /// <summary>
    /// Use this along with <see cref="BeginFileWriteBatch"/> - see that documentation for more details.
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if the write batch was ended, <c>false</c> if there was no batch already in progress.</returns>
    public bool EndFileWriteBatch()
    {
#if GNS_SHARP_OPENSOURCE_GNS
        throw new NotImplementedException("Open source GNS doesn't have ISteamRemoteStorage");
#elif GNS_SHARP_STEAMWORKS_SDK
        return Native.SteamAPI_ISteamRemoteStorage_EndFileWriteBatch(this.ptr);
#endif
    }

#if GNS_SHARP_STEAMWORKS_SDK

    internal void OnDispatch(ref CallbackMsg_t msg)
    {
        switch (msg.CallbackId)
        {
            case RemoteStorageDownloadUGCResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStorageDownloadUGCResult_t>();
                    this.RemoteStorageDownloadUGCResult?.Invoke(ref data);
                    break;
                }

            case RemoteStorageFileReadAsyncComplete_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStorageFileReadAsyncComplete_t>();
                    this.RemoteStorageFileReadAsyncComplete?.Invoke(ref data);
                    break;
                }

            case RemoteStorageFileShareResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStorageFileShareResult_t>();
                    this.RemoteStorageFileShareResult?.Invoke(ref data);
                    break;
                }

            case RemoteStorageFileWriteAsyncComplete_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStorageFileWriteAsyncComplete_t>();
                    this.RemoteStorageFileWriteAsyncComplete?.Invoke(ref data);
                    break;
                }

            case RemoteStorageLocalFileChange_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStorageLocalFileChange_t>();
                    this.RemoteStorageLocalFileChange?.Invoke(ref data);
                    break;
                }

            case RemoteStoragePublishedFileSubscribed_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStoragePublishedFileSubscribed_t>();
                    this.RemoteStoragePublishedFileSubscribed?.Invoke(ref data);
                    break;
                }

            case RemoteStoragePublishedFileUnsubscribed_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStoragePublishedFileUnsubscribed_t>();
                    this.RemoteStoragePublishedFileUnsubscribed?.Invoke(ref data);
                    break;
                }

            case RemoteStorageSubscribePublishedFileResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStorageSubscribePublishedFileResult_t>();
                    this.RemoteStorageSubscribePublishedFileResult?.Invoke(ref data);
                    break;
                }

            case RemoteStorageUnsubscribePublishedFileResult_t.CallbackId:
                {
                    ref var data = ref msg.GetCallbackParamAs<RemoteStorageUnsubscribePublishedFileResult_t>();
                    this.RemoteStorageUnsubscribePublishedFileResult?.Invoke(ref data);
                    break;
                }

            default:
                Debug.WriteLine($"Unsupported callback = {msg.CallbackId} on ISteamUser.OnDispatch()");
                break;
        }
    }

#endif
}
