using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.DTOs
{
    public class PurchaseContractDetailDto:IMapFrom<PurchaseContractDetail>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PurchaseContractDetail, PurchaseContractDetailDto>()
                 .ForMember(x => x.ContractNo, y => y.MapFrom(z => z.PurchaseContract.ContractNo))
                 .ForMember(x => x.ProjectName, y => y.MapFrom(z => z.PurchaseContract.Project.Name))
                 .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.PurchaseContract.Customer.Name));
            profile.CreateMap<PurchaseContractDetailDto, PurchaseContractDetail>()
                .ForMember(x => x.PurchaseContract, y => y.Ignore());
          

        }
        public int Id { get; set; }
        public int PurchaseContractId { get; set; }
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public decimal ContractAmount { get; set; }
        public string Terms { get; set; }
        public decimal Ratio { get; set; }
        public decimal RatioAmount { get; set; }
        public DateTime PlanedPaidDate { get; set; }

        public decimal? PaidAmount { get; set; }
        public decimal? PaidRatio { get; set; }
        public DateTime? PaidDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? TaxRate { get; set; }
        public bool IsSpecial { get; set; }
        public string Comments { get; set; }
    }
}
