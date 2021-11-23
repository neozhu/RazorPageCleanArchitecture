// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;


public class FieldValueMappingDto : IMapFrom<FieldValueMapping>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FieldValueMapping, FieldValueMappingDto>()
                    .ForMember(x => x.FieldName, y => y.MapFrom(z => z.ObjectField.Name));
        profile.CreateMap<FieldValueMappingDto, FieldValueMapping>()
                .ForMember(x => x.ObjectField, y => y.Ignore());
    }
    public int Id { get; set; }
    public int ObjectFieldId { get; set; }
    public string FieldName { get; set; }
    public string Stage { get; set; }
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

