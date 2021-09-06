using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.EventHandlers
{
    public class PurchaseOrderUpdatedEventHandler : INotificationHandler<DomainEventNotification<PurchaseOrderUpdatedEvent>>
    {
        private readonly ILogger<PurchaseOrderUpdatedEventHandler> _logger;

        public PurchaseOrderUpdatedEventHandler(
            ILogger<PurchaseOrderUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseOrderUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}