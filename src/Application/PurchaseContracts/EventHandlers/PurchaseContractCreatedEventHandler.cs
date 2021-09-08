using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.EventHandlers
{
    public class PurchaseContractCreatedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractCreatedEvent>>
    {
        private readonly ILogger<PurchaseContractCreatedEventHandler> _logger;

        public PurchaseContractCreatedEventHandler(
            ILogger<PurchaseContractCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseContractCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}