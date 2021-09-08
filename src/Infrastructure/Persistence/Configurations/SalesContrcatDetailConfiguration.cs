using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class SalesContrcatDetailConfiguration : IEntityTypeConfiguration<SalesContrcatDetail>
    {
        public void Configure(EntityTypeBuilder<SalesContrcatDetail> builder)
        {
            builder.Ignore(e => e.DomainEvents);
           
        }
    }
}
