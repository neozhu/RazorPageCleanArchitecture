// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.Razor.Application.Common.Interfaces.Caching
{
    public interface ICacheable
    {
        string CacheKey { get; }
        MemoryCacheEntryOptions Options { get; }
    }
}
