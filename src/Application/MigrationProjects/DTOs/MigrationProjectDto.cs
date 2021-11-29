// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationProjects.DTOs;


public class MigrationProjectDto : IMapFrom<MigrationProject>
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime BeginDateTime { get; set; }
    public DateTime? FinishedDateTime { get; set; }
    public int Progress { get; set; }
    public string Description { get; set; }
    public TrackingState TrackingState { get; set; }
}

