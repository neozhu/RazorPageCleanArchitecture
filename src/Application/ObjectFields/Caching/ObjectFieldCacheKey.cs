// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.ObjectFields.Caching;

    public static class ObjectFieldCacheKey
    {
        public const string GetAllCacheKey = "all-ObjectFields";
        public static string GetPagtionCacheKey(string parameters) {
            return "ObjectFieldsWithPaginationQuery,{parameters}";
        }
    static ObjectFieldCacheKey()
    {
        ResetCacheToken = new CancellationTokenSource();
        MemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            .AddExpirationToken(new CancellationChangeToken(ResetCacheToken.Token));
    }
    public static CancellationTokenSource ResetCacheToken { get; private set; }

    public static MemoryCacheEntryOptions MemoryCacheEntryOptions { get; private set; }
}

