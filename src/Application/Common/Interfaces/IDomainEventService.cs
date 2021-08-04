using CleanArchitecture.Razor.Domain.Common;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
