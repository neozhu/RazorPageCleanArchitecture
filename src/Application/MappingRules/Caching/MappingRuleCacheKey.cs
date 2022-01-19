// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.MappingRules.Caching;

public static class MappingRuleCacheKey
{
    public const string GetAllCacheKey = "all-MappingRules";
    public static string GetPagtionCacheKey(string parameters)
    {
        return "MappingRulesWithPaginationQuery,{parameters}";
    }
    static MappingRuleCacheKey()
    {
        ResetCacheToken = new CancellationTokenSource();
        MemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            .AddExpirationToken(new CancellationChangeToken(ResetCacheToken.Token));
    }
    public static CancellationTokenSource ResetCacheToken { get; private set; }

    public static MemoryCacheEntryOptions MemoryCacheEntryOptions { get; private set; }
}

