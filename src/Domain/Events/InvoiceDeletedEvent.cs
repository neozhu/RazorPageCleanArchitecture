using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class InvoiceDeletedEvent : DomainEvent
    {
        public InvoiceDeletedEvent(Invoice item)
        {
            Item = item;
        }

        public Invoice Item { get; }
    }
}
