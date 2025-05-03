// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

#if GNS_SHARP_STEAMWORKS_SDK

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

public class CallTask<T> : ICallTask, INotifyCompletion
    where T : unmanaged, ICallbackParam
{
    private object taskLock = new();

    private T result;
    private Action? continuation;
    private SynchronizationContext? syncContext = SynchronizationContext.Current;
    private bool isCompleted = false;
    private bool isFailed = true;

    public bool IsCompleted
    {
        get
        {
            lock (this.taskLock)
            {
                return this.isCompleted;
            }
        }
    }

    public void OnCompleted(Action continuation)
    {
        bool lockTaken = false;
        try
        {
            Monitor.Enter(this.taskLock, ref lockTaken);

            // Double-check if we got the result right before locking
            if (this.IsCompleted)
            {
                // Got the result, no need to lock now
                Monitor.Exit(this.taskLock);
                lockTaken = false;

                // Continue execution immediately
                continuation();
            }
            else
            {
                // Reserve the continuation so that it can be resumed from the `SetResultFrom()`
                this.continuation = continuation;
            }
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(this.taskLock);
            }
        }
    }

    public T? GetResult()
    {
        lock (this.taskLock)
        {
            if (this.isFailed)
            {
                return null;
            }

            return this.result;
        }
    }

    public void SetResultFrom(HSteamPipe pipe, ref SteamAPICallCompleted_t callCompleted)
    {
        Debug.Assert(T.CallbackParamId == callCompleted.AsyncCallbackId, $"Callback id mismatch (expected {T.CallbackParamId} for {typeof(T)}, got {callCompleted.AsyncCallbackId})");
        Debug.Assert(Unsafe.SizeOf<T>() == callCompleted.ParamSize, $"Callback param size mismatch (expected {Unsafe.SizeOf<T>()} for {typeof(T)}, got {callCompleted.ParamSize})");

        bool lockTaken = false;
        try
        {
            Monitor.Enter(this.taskLock, ref lockTaken);

            // Get the call result param directly into `result`
            Span<byte> resultRaw = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this.result, 1));
            bool gotResult = Native.SteamAPI_ManualDispatch_GetAPICallResult(pipe, callCompleted.AsyncCall, resultRaw, resultRaw.Length, callCompleted.AsyncCallbackId, out this.isFailed);
            Debug.Assert(gotResult, "There was no call result available");

            this.isCompleted = true;

            // If the continuation has been already reserved, this thread has the responsibility to call it
            if (this.continuation != null)
            {
                Monitor.Exit(this.taskLock);
                lockTaken = false;

                // If there existed a synchronization context on the thread that created this `CallTask`
                if (this.syncContext != null)
                {
                    // Post the continuation to that synchronization context
                    this.syncContext.Post(cont => ((Action)cont!).Invoke(), this.continuation);
                }
                else
                {
                    // Continue directly within this thread
                    this.continuation();
                }
            }
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(this.taskLock);
            }
        }
    }

    public CallTask<T> GetAwaiter()
    {
        return this;
    }
}

#elif GNS_SHARP_OPENSOURCE_GNS

public class CallTask<T> : ICallTask
{
}

#endif
