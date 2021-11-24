// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.DTOs;


public class MigrationObjectDto : IMapFrom<MigrationObject>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProjectName { get; set; }
    public string Team { get; set; }
    public string Description { get; set; }
    public TrackingState TrackingState { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}

