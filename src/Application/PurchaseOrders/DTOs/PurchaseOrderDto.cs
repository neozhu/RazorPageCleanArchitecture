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
            profile.CreateMap<PurchaseOrder, PurchaseOrderDto>().ReverseMap();

        }
  
        public int Id { get; set; }
        public string Status { get; set; }
        public string PO { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal Amount { get; set; }
        public string InviceNo { get; set; }
        public decimal? TaxRate { get; set; }
        public bool IsSpecial { get; set; }
    }
}
