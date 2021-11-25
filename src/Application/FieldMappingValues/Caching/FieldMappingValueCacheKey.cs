// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.FieldMappingValues.Caching;

    public static class FieldMappingValueCacheKey
    {
        public const string GetAllCacheKey = "all-FieldMappingValues";
        public static string GetPagtionCacheKey(string parameters) {
            return "FieldMappingValuesWithPaginationQuery,{parameters}";
        }
    }

