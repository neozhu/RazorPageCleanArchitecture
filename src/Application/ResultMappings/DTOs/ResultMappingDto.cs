// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ResultMappings.DTOs;


public class ResultMappingDto : IMapFrom<ResultMapping>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ResultMapping, ResultMappingDto>().ReverseMap();

    }
    public int Id { get; set; }
    public int MigrationProjectId { get; set; }
    public virtual MigrationProject MigrationProject { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string RelevantMock { get; set; }
    public string LegacySystem { get; set; }
    public string ProjectName { get; set; }
    public string RelevantObjects { get; set; }
    public string Team { get; set; }
    public string MigrationApproach { get; set; }
    public string TemplateFile { get; set; }
    public string TemplateDescription { get; set; }
    public List<FieldParameter> FieldParameters { get; set; } = new List<FieldParameter>();
    //public virtual ICollection<ResultMappingData> ResultMappingDatas { get; set; } = new HashSet<ResultMappingData>();
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
    public int Verified { get; set; }
    public int Total { get; set; }
}

