// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Domain.Common;

namespace CleanArchitecture.Razor.Domain.Entities
{
    public class InoviceRawData: AuditableEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice {  get; set; }
        public string Text { get; set; }
        public decimal Confidence { get; set; }
        public string Text_Region { get; set; }
        public string Label { get; set; }
    }
}
