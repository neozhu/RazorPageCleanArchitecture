using CleanArchitecture.Razor.Domain.Common;

namespace CleanArchitecture.Razor.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
