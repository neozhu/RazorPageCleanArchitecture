// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.KeyValues.Caching
{
    public static class KeyValueCacheKey
    {
        public const string GetAllCacheKey = "all-keyvalues";
        public static string GetCacheKey(string name)
        {
            return $"{name}-keyvalues";
        }
    }
}
