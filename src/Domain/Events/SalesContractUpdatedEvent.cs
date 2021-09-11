using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class SalesContractUpdatedEvent : DomainEvent
    {
        public SalesContractUpdatedEvent(SalesContract item)
        {
            Item = item;
        }

        public SalesContract Item { get; }
    }
}
