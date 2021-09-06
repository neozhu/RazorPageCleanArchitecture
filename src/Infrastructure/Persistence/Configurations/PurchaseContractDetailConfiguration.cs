using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class PurchaseContractDetailConfiguration : IEntityTypeConfiguration<PurchaseContractDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseContractDetail> builder)
        {
            builder.Ignore(e => e.DomainEvents);
        }
    }
}
