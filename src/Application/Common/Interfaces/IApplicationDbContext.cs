// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Domain.Entities.Audit;
using CleanArchitecture.Razor.Domain.Entities.Log;


namespace CleanArchitecture.Razor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Logger> Loggers { get; set; }
    DbSet<AuditTrail> AuditTrails { get; set; }
    DbSet<KeyValue> KeyValues { get; set; }
    DbSet<MigrationObject> MigrationObjects { get; set; }
    DbSet<ObjectField> ObjectFields { get; set; }
    DbSet<MigrationTemplateFile> MigrationTemplateFiles { get; set; }
    DbSet<MigrationProject> MigrationProjects { get; set; }
    DbSet<MappingRule> MappingRules { get; set; }
    DbSet<FieldMappingValue> FieldMappingValues { get; set; }
    DbSet<ResultMapping> ResultMappings { get; set; }
    DbSet<ResultMappingData> ResultMappingDatas { get; set; }
    DbSet<ToDoItem> ToDoItems { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
