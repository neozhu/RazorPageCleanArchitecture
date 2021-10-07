using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class InvoiceUpdatedEvent : DomainEvent
    {
        public InvoiceUpdatedEvent(Invoice item)
        {
            Item = item;
        }

        public Invoice Item { get; }
    }
}
