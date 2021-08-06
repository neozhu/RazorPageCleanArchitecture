using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
