using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Ignore(e => e.ImgString);
            builder.HasMany(x => x.InvoiceRawData)
                .WithOne(x=>x.Invoice)
                .HasForeignKey(x=>x.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
