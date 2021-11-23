// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Caching;

    public  sealed class MigrationTemplateFileCacheTokenSource
    {
        static MigrationTemplateFileCacheTokenSource() {
            ResetCacheToken = new CancellationTokenSource();
        }
        public static CancellationTokenSource ResetCacheToken { get; private set; }
    }

