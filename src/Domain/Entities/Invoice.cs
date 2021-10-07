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
    public class Invoice : AuditableEntity,
        IHasDomainEvent,
        IAuditTrial
    {
        public int Id {  get; set; }
        public string Status { get; set; }
        public string InvoiceNo { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Tax { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Description {  get; set; }
        public string Result { get; set; }
        public string AttachmentUrl { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new();
        public virtual ICollection<InoviceRawData> InoviceRawData { get; set; } = new HashSet<InoviceRawData>();
    }


}
