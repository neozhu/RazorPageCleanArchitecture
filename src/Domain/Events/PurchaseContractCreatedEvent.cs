using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class PurchaseContractCreatedEvent : DomainEvent
    {
        public PurchaseContractCreatedEvent(PurchaseContract item)
        {
            Item = item;
        }

        public PurchaseContract Item { get; }
    }
}
