using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class PurchaseContractDetailDeletedEvent : DomainEvent
    {
        public PurchaseContractDetailDeletedEvent(PurchaseContractDetail item)
        {
            Item = item;
        }

        public PurchaseContractDetail Item { get; }
    }
}
