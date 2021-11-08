// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Infrastructure.Configurations
{
    public class Theme
    {
        public string ThemeVersion { get; set; }
        public string IconPrefix { get; set; }
        public string Logo { get; set; }
        public string User { get; set; }
        public string Role { get; set; }  
        public string Email { get; set; }
        public string Twitter { get; set; }
        public string Avatar { get; set; }
    }
}
