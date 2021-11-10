using CleanArchitecture.Razor.Domain.Entities.Worflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class ApprovalDataonfiguration : IEntityTypeConfiguration<ApprovalData>
    {
        public void Configure(EntityTypeBuilder<ApprovalData> builder)
        {
            builder.HasKey(t => t.WorkflowId);
            builder.Property(t => t.WorkflowId)
                .HasMaxLength(50)
                .ValueGeneratedNever();
        }
    }
}
