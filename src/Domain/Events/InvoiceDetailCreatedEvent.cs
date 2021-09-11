using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class InvoiceDetailCreatedEvent : DomainEvent
    {
        public InvoiceDetailCreatedEvent(InvoiceDetail item)
        {
            Item = item;
        }

        public InvoiceDetail Item { get; }
    }
}
