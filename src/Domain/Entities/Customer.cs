// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Enums;

namespace CleanArchitecture.Razor.Domain.Entities
{
    public partial class Customer : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameOfEnglish { get; set; }
        public string GroupName { get; set; }
        public PartnerType PartnerType { get; set; }

        public string Region { get; set; }
        public string Sales { get; set; }
        public string RegionSalesDirector { get; set; }

        public string Address { get; set; }

        public string AddressOfEnglish { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Contact2 { get; set; }

        public string Email2 { get; set; }

        public string PhoneNumber2 { get; set; }
        public string TaxNo { get; set; }
        public string Bank { get; set; }
        public string AccountNo { get; set; }

        public string Fax { get; set; }
        public string Comments { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
