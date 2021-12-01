// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.DTOs;


public class ObjectFieldDto : IMapFrom<ObjectField>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ObjectField, ObjectFieldDto>()
                 .ForMember(x => x.ProjectName, y => y.MapFrom(z => z.MigrationProject.Name));

        profile.CreateMap<ObjectFieldDto, ObjectField>()
                .ForMember(x => x.MigrationProject, y => y.Ignore());

    }
    public int Id { get; set; }
    public string Name { get; set; }
 
    public string Description { get; set; }
    public string ProjectName { get; set; }
    public int MigrationProjectId { get; set; }
    public TrackingState TrackingState { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}

