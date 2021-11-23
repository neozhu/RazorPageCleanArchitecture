// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;


    public class MigrationTemplateFileDto:IMapFrom<MigrationTemplateFile>
    {

    public int Id { get; set; }
    public string Name { get; set; }
    public string ObjectField { get; set; }
    public string Legacy1Field { get; set; }
    public string Legacy2Field { get; set; }
    public string Legacy3Field { get; set; }
    public string NewValueField { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public string LegacySystem { get; set; }

    public string Comments { get; set; }
    public TrackingState TrackingState { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}

