using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class PurchaseContractDetailUpdatedEvent : DomainEvent
    {
        public PurchaseContractDetailUpdatedEvent(PurchaseContractDetail item)
        {
            Item = item;
        }

        public PurchaseContractDetail Item { get; }
    }
}
