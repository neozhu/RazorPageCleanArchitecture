using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.EventHandlers
{
    public class PurchaseContractDetailUpdatedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractDetailUpdatedEvent>>
    {
        private readonly ILogger<PurchaseContractDetailUpdatedEventHandler> _logger;

        public PurchaseContractDetailUpdatedEventHandler(
            ILogger<PurchaseContractDetailUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseContractDetailUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}