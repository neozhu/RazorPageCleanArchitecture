using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Entities.Audit;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AuditTrail> AuditTrails { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<Document> Documents { get; set; }
        DbSet<KeyValue> KeyValues { get; set; }
        DbSet<ApprovalData> ApprovalDatas { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
