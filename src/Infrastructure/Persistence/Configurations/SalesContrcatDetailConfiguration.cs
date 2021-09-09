using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class SalesContractDetailConfiguration : IEntityTypeConfiguration<SalesContractDetail>
    {
        public void Configure(EntityTypeBuilder<SalesContractDetail> builder)
        {
            builder.Ignore(e => e.DomainEvents);
           
        }
    }
}
