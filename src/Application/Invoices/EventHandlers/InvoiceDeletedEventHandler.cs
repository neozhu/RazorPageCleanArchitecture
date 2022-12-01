namespace CleanArchitecture.Razor.Application.Invoices.EventHandlers
{
    public class InvoiceDeletedEventHandler : INotificationHandler<DomainEventNotification<InvoiceDeletedEvent>>
    {
        private readonly ILogger<InvoiceDeletedEventHandler> _logger;

        public InvoiceDeletedEventHandler(
            ILogger<InvoiceDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<InvoiceDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}