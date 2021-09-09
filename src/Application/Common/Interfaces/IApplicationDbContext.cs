using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<Document> Documents { get; set; }
        DbSet<KeyValue> KeyValues { get; set; }
        DbSet<ApprovalData> ApprovalDatas { get; set; }

        DbSet<Product> Products {  get; set; }
        DbSet<Project> Projects {  get; set; }
        DbSet<PurchaseContract> PurchaseContracts {  get; set;}
        DbSet<PurchaseContractDetail> PurchaseContractDetails {  get; set;}
        DbSet<PurchaseOrder> PurchaseOrders {  get; set; }
        DbSet<SalesContract> SalesContracts {  get; set; }
        DbSet<SalesContractDetail> SalesContractDetails {  get; set;}
        DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
