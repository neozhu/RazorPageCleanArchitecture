namespace CleanArchitecture.Razor.Application.Invoices.EventHandlers
{
    public class InvoiceCreatedEventHandler : INotificationHandler<DomainEventNotification<InvoiceCreatedEvent>>
    {
        private readonly ILogger<InvoiceCreatedEventHandler> _logger;

        public InvoiceCreatedEventHandler(
            ILogger<InvoiceCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<InvoiceCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}