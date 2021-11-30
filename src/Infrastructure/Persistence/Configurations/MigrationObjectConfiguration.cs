
using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Razor.Infrastructure.Persistence.Configurations;

public class MigrationObjectConfiguration : IEntityTypeConfiguration<MigrationObject>
{
    public void Configure(EntityTypeBuilder<MigrationObject> builder)
    {
        builder.HasOne(d => d.MigrationProject)
               .WithMany(p => p.MigrationObjects)
               .HasForeignKey(d => d.MigrationProjectId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}