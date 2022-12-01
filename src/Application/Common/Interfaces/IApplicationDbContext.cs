using CleanArchitecture.Razor.Domain.Entities.Audit;
using CleanArchitecture.Razor.Domain.Entities.Log;
using CleanArchitecture.Razor.Domain.Entities.Worflow;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Serilog> Serilogs { get; set; }
        DbSet<AuditTrail> AuditTrails { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<Document> Documents { get; set; }
        DbSet<KeyValue> KeyValues { get; set; }
        DbSet<ApprovalData> ApprovalDatas { get; set; }
        DbSet<Invoice> Invoices {  get; set; }
        DbSet<InvoiceRawData> InvoiceRawDatas { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
