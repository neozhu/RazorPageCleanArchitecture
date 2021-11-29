// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.MigrationProjects.Caching;

    public static class MigrationProjectCacheKey
    {
        public const string GetAllCacheKey = "all-MigrationProjects";
        public static string GetPagtionCacheKey(string parameters) {
            return "MigrationProjectsWithPaginationQuery,{parameters}";
        }
    }

