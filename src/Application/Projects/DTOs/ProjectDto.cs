using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;

namespace CleanArchitecture.Razor.Application.Projects.DTOs
{
    public class ProjectDto:IMapFrom<Project>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDto>().ReverseMap();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime? EndDateTime { get; set; } 
        public decimal? EstimatedCost { get; set; } 
        public decimal? ActualCost { get; set; }
        public decimal? ContractAmount { get; set; } 
        public decimal? ReceiptAmount { get; set; } 
        public decimal? GrossMargin { get; set; }
        public TrackingState TrackingState { get; set; }
    }
}
