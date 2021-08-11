
using CleanArchitecture.Razor.Domain.Common;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Domain.Events
{
    public class KeyValueCreatedEvent : DomainEvent
    {
        public KeyValueCreatedEvent(KeyValue item)
        {
            Item = item;
        }

        public KeyValue Item { get; }
    }
}
