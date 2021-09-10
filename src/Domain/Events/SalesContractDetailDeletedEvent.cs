using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class SalesContractDetailDeletedEvent : DomainEvent
    {
        public SalesContractDetailDeletedEvent(SalesContractDetail item)
        {
            Item = item;
        }

        public SalesContractDetail Item { get; }
    }
}
