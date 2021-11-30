// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.DTOs;


public class ObjectFieldDto : IMapFrom<ObjectField>
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string ParameterName { get; set; }
    public string Direct { get; set; }
    public string AssociatedType { get; set; }
    public string DataType { get; set; }
    public int? Length { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string SourceTemplateName { get; set; }
    public TrackingState TrackingState { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}

