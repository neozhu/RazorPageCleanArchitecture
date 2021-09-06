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
    public class PurchaseContractDetail : AuditableEntity, IHasDomainEvent
    {
        public int Id {  get; set; }
        public int PurchaseContractId { get; set; }
        public PurchaseContract PurchaseContract { get; set; }
        public decimal ContractAmount { get; set; }
        public string Terms { get; set; }
        public decimal Ratio { get; set; }
        public decimal RatioAmount { get; set; }
        public DateTime PlanedPaidDate { get; set; }

        public decimal PaidAmount { get; set; }
        public decimal PaidRatio { get; set; }
        public DateTime? PaidDate { get; set; }
        public string InviceNo { get; set; }
        public decimal? TaxRate { get; set; }
        public bool IsSpecial { get; set; }
        public decimal Balance { get; set; }
        public string Comments { get; set; }


        public List<DomainEvent> DomainEvents { get ; set; }= new List<DomainEvent>();
    }
}
