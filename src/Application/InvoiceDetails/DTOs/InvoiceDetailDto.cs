using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.DTOs
{
    public class InvoiceDetailDto:IMapFrom<InvoiceDetail>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<InvoiceDetail, InvoiceDetailDto>()
                .ForMember(x => x.ProjectName, y => y.MapFrom(z => z.SalesContract.Project.Name))
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.SalesContract.Customer.Name))
                .ForMember(x => x.ContractAmount, y => y.MapFrom(z => z.SalesContract.ContractIncAmount));
                //.ForMember(x => x.Balance, y => y.MapFrom(z => (z.SalesContract.ContractAmount - z.SalesContract.InvoiceAmount)));

            profile.CreateMap<InvoiceDetailDto, InvoiceDetail>()
                .ForMember(x => x.SalesContract, y => y.Ignore());
        }
        public int Id { get; set; }
        public int SalesContractId { get; set; }
        public string ContractNo { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public decimal ContractAmount { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal TaxRate { get; set; }
        public bool HasPaid { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueTime { get; set; }
        public decimal Balance { get; set; }
        public string Comments { get; set; }
    }
}
