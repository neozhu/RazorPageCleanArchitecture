using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class SalesContractCreatedEvent : DomainEvent
    {
        public SalesContractCreatedEvent(SalesContract item)
        {
            Item = item;
        }

        public SalesContract Item { get; }
    }
}
