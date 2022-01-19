// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.MigrationObjects.Caching;

    public static class MigrationObjectCacheKey
    {
        public const string GetAllCacheKey = "all-MigrationObjects";
        public static string GetPagtionCacheKey(string parameters) {
            return "MigrationObjectsWithPaginationQuery,{parameters}";
        }
    static MigrationObjectCacheKey()
    {
        ResetCacheToken = new CancellationTokenSource();
        MemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            .AddExpirationToken(new CancellationChangeToken(ResetCacheToken.Token));
    }
    public static CancellationTokenSource ResetCacheToken { get; private set; }

    public static MemoryCacheEntryOptions MemoryCacheEntryOptions { get; private set; }
}

