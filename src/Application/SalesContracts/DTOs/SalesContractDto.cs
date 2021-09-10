using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Application.SalesContracts.DTOs
{
    public class SalesContractDto:IMapFrom<SalesContract>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesContract, SalesContractDto>()
                .ForMember(x=>x.CustomerName,y=>y.MapFrom(z=>z.Customer.Name))
                .ForMember(x=>x.ProjectName, y=>y.MapFrom(z=>z.Project.Name));
            profile.CreateMap<SalesContractDto, SalesContract>()
                .ForMember(x => x.Project, y => y.Ignore())
                .ForMember(x => x.Customer, y => y.Ignore());

        }
        public int Id { get; set; }
        public string Status { get; set; }
        public string ContractNo { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string OrderNo { get; set; }
        public decimal ContractAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal ContractIncAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal Balance { get; set; }
        public string Comments { get; set; }
    }
}
