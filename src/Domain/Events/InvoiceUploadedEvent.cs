using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class InvoiceUploadedEvent : DomainEvent
    {
        public InvoiceUploadedEvent(Invoice item)
        {
            Item = item;
        }

        public Invoice Item { get; }
    }
}
