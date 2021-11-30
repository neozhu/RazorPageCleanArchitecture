// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MappingRules.DTOs;


public class MappingRuleDto : IMapFrom<MappingRule>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MappingRule, MappingRuleDto>()
                 .ForMember(x => x.ProjectName, y => y.MapFrom(z => z.MigrationProject.Name));

        profile.CreateMap<MappingRuleDto, MappingRule>()
                .ForMember(x => x.MigrationProject, y => y.Ignore());

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string LegacyField1 { get; set; }
    public string ImportParameterField1 { get; set; }
    public string LegacyDescription1 { get; set; }
    public string LegacyField2 { get; set; }
    public string ImportParameterField2 { get; set; }
    public string LegacyDescription2 { get; set; }
    public string LegacyField3 { get; set; }
    public string ImportParameterField3 { get; set; }
    public string LegacyDescription3 { get; set; }
    public string LegacyField4 { get; set; }
    public string ImportParameterField4 { get; set; }
    public string LegacyDescription4 { get; set; }
    public string NewValueField { get; set; }
    public string ExportParameterField { get; set; }
    public string NewValueFieldDescription { get; set; }
    public bool IsMock { get; set; }
    public string LegacySystem { get; set; }
    public string ProjectName { get; set; }
    public int MigrationProjectId { get; set; }
    public string RelevantObjects { get; set; }
    public string Team { get; set; }
    public string Comments { get; set; }
    public string TemplateFile { get; set; }
    public string TemplateDescription { get; set; }
    public TrackingState TrackingState { get; set; }

    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }

}

