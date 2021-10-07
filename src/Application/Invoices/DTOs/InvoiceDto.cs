using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Application.Invoices.DTOs
{
    public class InvoiceDto:IMapFrom<Invoice>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Invoice, InvoiceDto>().ReverseMap();

        }
        public int Id { get; set; }
        public string Status { get; set; }
        public string InvoiceNo { get; set; }
        public string Title { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? Tax { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
