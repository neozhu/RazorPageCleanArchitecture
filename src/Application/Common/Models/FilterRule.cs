// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Common.Models
{
    public class FilterRule
    {
        public string field { get; set; }
        public string op { get; set; }
        public string value { get; set; }
    }
}
