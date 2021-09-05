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
   public class Project: AuditableEntity
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Status { get; set; }
        public string Description {  get; set; }
        public DateTime BeginDateTime {  get; set; }
        public DateTime? EndDateTime {  get; set; }
        public decimal? Cost { get; set; }
        public decimal? ContractAmount { get;set; }
        public decimal? ReceiptAmount {  get; set; }
        public decimal? GrossMargin { get; set; }

    }
}
