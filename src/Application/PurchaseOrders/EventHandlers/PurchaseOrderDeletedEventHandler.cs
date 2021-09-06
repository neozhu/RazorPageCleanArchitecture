using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.EventHandlers
{
    public class PurchaseOrderDeletedEventHandler : INotificationHandler<DomainEventNotification<PurchaseOrderDeletedEvent>>
    {
        private readonly ILogger<PurchaseOrderDeletedEventHandler> _logger;

        public PurchaseOrderDeletedEventHandler(
            ILogger<PurchaseOrderDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseOrderDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}