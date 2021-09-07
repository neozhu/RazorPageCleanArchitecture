using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.DTOs
{
    public class PurchaseOrderDto:IMapFrom<PurchaseOrder>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PurchaseOrderDto, PurchaseOrder>()
                .ForMember(x => x.Customer, s => s.Ignore())
                .ForMember(x => x.Product, s => s.Ignore());
            profile.CreateMap<PurchaseOrder, PurchaseOrderDto>()
                 .ForMember(y => y.ProductName, z => z.MapFrom(x => x.Product.Name))
                 .ForMember(y => y.CustomerName, z => z.MapFrom(x => x.Customer.Name));
        }
        public int Id { get; set; }
        public string Status { get; set; }
        public string PO { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CustomerId { get; set; }
        public string  CustomerName { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? TaxRate { get; set; }
        public bool IsSpecial { get; set; } = false;
    }
}
