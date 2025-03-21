// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System.Runtime.CompilerServices;

#pragma warning disable SA1649 // File name should match first type name

[InlineArray(4)]
public struct Array4<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(8)]
public struct Array8<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(10)]
public struct Array10<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(16)]
public struct Array16<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(28)]
public struct Array28<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(32)]
public struct Array32<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(33)]
public struct Array33<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(50)]
public struct Array50<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(63)]
public struct Array63<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(64)]
public struct Array64<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(128)]
public struct Array128<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(256)]
public struct Array256<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(512)]
public struct Array512<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(1024)]
public struct Array1024<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(2048)]
public struct Array2048<T>
    where T : unmanaged
{
    private T elem;
}

[InlineArray(2560)]
public struct Array2560<T>
    where T : unmanaged
{
    private T elem;
}

#pragma warning restore SA1649
