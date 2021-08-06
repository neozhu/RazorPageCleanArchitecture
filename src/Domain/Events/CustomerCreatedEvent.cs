
using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class CustomerCreatedEvent : DomainEvent
    {
        public CustomerCreatedEvent(Customer item)
        {
            Item = item;
        }

        public Customer Item { get; }
    }
}
