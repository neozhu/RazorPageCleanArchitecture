// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;


public class FieldMappingValueDto : IMapFrom<FieldMappingValue>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FieldMappingValue, FieldMappingValueDto>()
                 .ForMember(x => x.MappingRule, y => y.MapFrom(z => z.MappingRule.Name))
                 .ForMember(x => x.LegacySystem, y => y.MapFrom(z => z.MappingRule.LegacySystem));
        profile.CreateMap<FieldMappingValueDto, FieldMappingValue>()
                .ForMember(x => x.MappingRule, y => y.Ignore());

    }
    public int Id { get; set; }
    public int MappingRuleId { get; set; }
    public string MappingRule { get; set; }
    public string Mock { get; set; }
    public string Legacy1 { get; set; }
    public string Legacy2 { get; set; }
    public string Legacy3 { get; set; }
    public string Legacy4 { get; set; }
    public string NewValue { get; set; }
    public string LegacySystem { get; set; }
    public string Description { get; set; }
    public string Team { get; set; }
    public string Check { get; set; }
    public string Comments { get; set; }

    public TrackingState TrackingState { get; set; }

    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}

