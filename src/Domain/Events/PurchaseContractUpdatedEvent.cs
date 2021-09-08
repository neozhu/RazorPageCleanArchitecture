using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class PurchaseContractUpdatedEvent : DomainEvent
    {
        public PurchaseContractUpdatedEvent(PurchaseContract item)
        {
            Item = item;
        }

        public PurchaseContract Item { get; }
    }
}
