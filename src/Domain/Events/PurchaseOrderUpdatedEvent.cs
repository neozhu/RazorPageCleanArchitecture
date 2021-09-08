using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class PurchaseOrderUpdatedEvent : DomainEvent
    {
        public PurchaseOrderUpdatedEvent(PurchaseOrder item)
        {
            Item = item;
        }

        public PurchaseOrder Item { get; }
    }
}
