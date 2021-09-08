using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class SalesContractConfiguration : IEntityTypeConfiguration<SalesContract>
    {
        public void Configure(EntityTypeBuilder<SalesContract> builder)
        {
            builder.Ignore(e => e.DomainEvents);
        }
    }
}
