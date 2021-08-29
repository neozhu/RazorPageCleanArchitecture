using System;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Entities.Worflow;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs
{
    public class ApprovalDataDto:IMapFrom<ApprovalData>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApprovalData, ApprovalDataDto>().ReverseMap();

        }
        public string WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public string Status { get; set; }
        public string DocumentName { get; set; }
        public int DocumentId { get; set; }
        public string Url { get; set; }
        public string Applicant { get; set; }
        public string Approver { get; set; }
        public string Outcome { get; set; }
        public string Comments { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public DateTime RequestDateTime { get; set; }
    }
}
