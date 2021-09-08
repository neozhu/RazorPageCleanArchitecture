using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class PurchaseContractDetailCreatedEvent : DomainEvent
    {
        public PurchaseContractDetailCreatedEvent(PurchaseContractDetail item)
        {
            Item = item;
        }

        public PurchaseContractDetail Item { get; }
    }
}
