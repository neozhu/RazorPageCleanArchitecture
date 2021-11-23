// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.FieldValueMappings.Caching;

    public static class FieldValueMappingCacheKey
    {
        public const string GetAllCacheKey = "all-FieldValueMappings";
        public static string GetPagtionCacheKey(string parameters) {
            return "FieldValueMappingsWithPaginationQuery,{parameters}";
        }
    }

