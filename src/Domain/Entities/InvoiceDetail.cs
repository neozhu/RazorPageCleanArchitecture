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
    public class InvoiceDetail : AuditableEntity, IHasDomainEvent
    {
        public int Id {  get; set; }
        public int SalesContractId { get; set; }
        public SalesContract SalesContract {  get; set; }
        public decimal ContractAmount { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal TaxRate { get; set; }
        public bool HasPaid { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueTime { get; set; }
        public decimal Balance { get; set; }
        public string Comments { get; set; }

        public List<DomainEvent> DomainEvents { get ; set ; }= new List<DomainEvent>();
    }
}
