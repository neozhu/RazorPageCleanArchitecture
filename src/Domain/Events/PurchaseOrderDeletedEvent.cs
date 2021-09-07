using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class PurchaseOrderDeletedEvent : DomainEvent
    {
        public PurchaseOrderDeletedEvent(PurchaseOrder item)
        {
            Item = item;
        }

        public PurchaseOrder Item { get; }
    }
}
