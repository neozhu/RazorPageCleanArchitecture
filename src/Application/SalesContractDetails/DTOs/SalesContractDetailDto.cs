using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.DTOs
{
    public class SalesContractDetailDto:IMapFrom<SalesContractDetail>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesContractDetail, SalesContractDetailDto>()
                .ForMember(x => x.ContractNo, y => y.MapFrom(z => z.SalesContract.ContractNo));
            profile.CreateMap<SalesContractDetailDto, SalesContractDetail>()
                .ForMember(x => x.SalesContract, y => y.Ignore());

        }
        public int Id { get; set; }
        public int SalesContractId { get; set; }
        public string  ContractNo { get; set; }
        public decimal ContractAmount { get; set; }
        public string Terms { get; set; }
        public decimal Ratio { get; set; }
        public decimal RatioAmount { get; set; }
        public DateTime PlanedReceiptDate { get; set; }
        public decimal? ReceiptAmount { get; set; }
        public decimal? ReceiptRatio { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? DueTime { get; set; }
        public string Comments { get; set; }
    }
}
