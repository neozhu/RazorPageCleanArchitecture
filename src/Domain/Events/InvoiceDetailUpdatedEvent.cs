using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class InvoiceDetailUpdatedEvent : DomainEvent
    {
        public InvoiceDetailUpdatedEvent(InvoiceDetail item)
        {
            Item = item;
        }

        public InvoiceDetail Item { get; }
    }
}
