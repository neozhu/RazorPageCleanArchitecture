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
    public class PurchaseContract : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ContractNo { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string OrderNo { get; set; }
        public decimal ContractExtAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal ContractAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string Comments { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
