using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.EventHandlers
{
    public class PurchaseContractDetailCreatedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractDetailCreatedEvent>>
    {
        private readonly ILogger<PurchaseContractDetailCreatedEventHandler> _logger;

        public PurchaseContractDetailCreatedEventHandler(
            ILogger<PurchaseContractDetailCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseContractDetailCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}