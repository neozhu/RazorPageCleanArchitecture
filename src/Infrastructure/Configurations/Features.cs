// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Infrastructure.Configurations
{
    public class Features
    {
        public bool AppSidebar { get; set; }
        public bool AppHeader { get; set; }
        public bool AppLayoutShortcut { get; set; }
        public bool AppFooter { get; set; }
        public bool ShortcutMenu { get; set; }
        public bool GoogleAnalytics { get; set; }
        public bool ChatInterface { get; set; }
        public bool LayoutSettings { get; set; }
    }
}
