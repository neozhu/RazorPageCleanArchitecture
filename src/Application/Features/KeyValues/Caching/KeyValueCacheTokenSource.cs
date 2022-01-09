// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.Features.KeyValues.Caching;

public sealed class KeyValueCacheTokenSource
{
    static KeyValueCacheTokenSource()
    {
        ResetCacheToken = new CancellationTokenSource();
    }
    public static CancellationTokenSource ResetCacheToken { get; private set; }
}
