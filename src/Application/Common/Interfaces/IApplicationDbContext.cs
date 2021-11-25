// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Domain.Entities.Audit;
using CleanArchitecture.Razor.Domain.Entities.Log;
using CleanArchitecture.Razor.Domain.Entities.Worflow;

namespace CleanArchitecture.Razor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Logger> Loggers { get; set; }
    DbSet<AuditTrail> AuditTrails { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<DocumentType> DocumentTypes { get; set; }
    DbSet<Document> Documents { get; set; }
    DbSet<KeyValue> KeyValues { get; set; }
    DbSet<ApprovalData> ApprovalDatas { get; set; }

    DbSet<Product> Products { get; set; }
    DbSet<MigrationObject> MigrationObjects { get; set; }
    DbSet<ObjectField> ObjectFields { get; set; }
    DbSet<FieldValueMapping> FieldValueMappings { get; set; }
    DbSet<MigrationTemplateFile> MigrationTemplateFiles { get; set; }

    DbSet<MappingRule> MappingRules { get; set; }
    DbSet<FieldMappingValue> FieldMappingValues { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
