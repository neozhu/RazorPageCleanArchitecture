namespace CleanArchitecture.Razor.Application.Invoices.EventHandlers
{
    public class InvoiceUpdatedEventHandler : INotificationHandler<DomainEventNotification<InvoiceUpdatedEvent>>
    {
        private readonly ILogger<InvoiceUpdatedEventHandler> _logger;

        public InvoiceUpdatedEventHandler(
            ILogger<InvoiceUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<InvoiceUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}