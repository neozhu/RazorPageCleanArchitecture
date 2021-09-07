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
    public class PurchaseOrder : AuditableEntity, IHasDomainEvent
    {
        public int Id {  get; set; }
        public string Status { get; set; }
        public string PO { get; set; }
        public int ProductId {  get; set; }
        public virtual Product Product {  get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer {  get; set; } 
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? TaxRate { get; set; }
        public bool IsSpecial { get; set; }


        public List<DomainEvent> DomainEvents { get; set; }=new List<DomainEvent>();
    }
}
