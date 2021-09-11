using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class InvoiceDetailDeletedEvent : DomainEvent
    {
        public InvoiceDetailDeletedEvent(InvoiceDetail item)
        {
            Item = item;
        }

        public InvoiceDetail Item { get; }
    }
}
