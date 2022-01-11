// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class ResultMappingDataConfiguration : IEntityTypeConfiguration<ResultMappingData>
{
    public void Configure(EntityTypeBuilder<ResultMappingData> builder)
    {

        builder.Property(e => e.FieldData)
           .HasConversion(
                 v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                 v => JsonSerializer.Deserialize<Dictionary<string,string>>(v, (JsonSerializerOptions)null)
                );

    
    }
}
