using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class SalesContractDeletedEvent : DomainEvent
    {
        public SalesContractDeletedEvent(SalesContract item)
        {
            Item = item;
        }

        public SalesContract Item { get; }
    }
}
