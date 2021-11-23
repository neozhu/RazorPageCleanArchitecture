// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.DTOs;


public class ObjectFieldDto : IMapFrom<ObjectField>
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string MasterDataRelevant { get; set; }
    public string TechMockMasterData { get; set; }
    public string Team { get; set; }
    public string Status { get; set; }
    public string Link { get; set; }
    public string LegacySystem { get; set; }
    public string IsUsedAK1 { get; set; }
    public string MajorTable { get; set; }
    public string Cases { get; set; }
    public string Numbers { get; set; }
    public string RelevantObjects { get; set; }
    public string Check { get; set; }
    public string Comments { get; set; }
    public string MigrationTemplate { get; set; }
    public TrackingState TrackingState { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}

