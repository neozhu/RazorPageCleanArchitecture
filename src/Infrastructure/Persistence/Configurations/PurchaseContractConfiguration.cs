using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class PurchaseContractConfiguration : IEntityTypeConfiguration<PurchaseContract>
    {
        public void Configure(EntityTypeBuilder<PurchaseContract> builder)
        {
            builder.Ignore(e => e.DomainEvents);
        }
    }
}
