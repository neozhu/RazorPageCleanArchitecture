// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Features.AuditTrails.DTOs
{
    public  class AuditTrailDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AuditType { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; } 
        public string NewValues { get; set; } 
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
    }
}
