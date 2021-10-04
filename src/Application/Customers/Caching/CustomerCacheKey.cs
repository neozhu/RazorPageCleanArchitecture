// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Customers.Caching
{
    public static class CustomerCacheKey
    {
        public const string GetAllCacheKey = "all-customers";
        public static string GetPaginationCacheKey(string parameters) {
            return $"CustomersWithPaginationQuery,{parameters}";
        }
    }
}
