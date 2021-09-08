using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;

namespace CleanArchitecture.Razor.Application.Products.DTOs
{
    public class ProductDto:IMapFrom<Product>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>().ReverseMap();
        }
        //TODO:Define field properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TrackingState TrackingState { get; set; }
    }
}
