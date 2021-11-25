// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace CleanArchitecture.Razor.Application.FieldMappingValues.Caching;

    public  sealed class FieldMappingValueCacheTokenSource
    {
        static FieldMappingValueCacheTokenSource() {
            ResetCacheToken = new CancellationTokenSource();
        }
        public static CancellationTokenSource ResetCacheToken { get; private set; }
    }

