// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;

namespace CleanArchitecture.Razor.Application.Customers.DTOs
{
    public partial class CustomerDto:IMapFrom<Customer>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerDto>()
                .ForMember(x => x.PartnerType, y => y.MapFrom(z => z.PartnerType.ToString()));
            profile.CreateMap<CustomerDto, Customer>()
                .ForMember(x => x.PartnerType, y => y.MapFrom(z => Enum.Parse<PartnerType>(z.PartnerType)))
                .ForMember(x => x.DomainEvents, y => y.Ignore())
                .ForMember(x => x.Created, y => y.Ignore())
                .ForMember(x => x.CreatedBy, y => y.Ignore())
                .ForMember(x => x.LastModified, y => y.Ignore())
                .ForMember(x => x.LastModifiedBy, y => y.Ignore());

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameOfEnglish { get; set; }
        public string GroupName { get; set; }
        public string PartnerType { get; set; }
        public string Region { get; set; }
        public string Sales { get; set; }
        public string RegionSalesDirector { get; set; }
        public string Address { get; set; }
        public string AddressOfEnglish { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Comments { get; set; }
    }
}
