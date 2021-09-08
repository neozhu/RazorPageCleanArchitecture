using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.EventHandlers
{
    public class PurchaseContractUpdatedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractUpdatedEvent>>
    {
        private readonly ILogger<PurchaseContractUpdatedEventHandler> _logger;

        public PurchaseContractUpdatedEventHandler(
            ILogger<PurchaseContractUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseContractUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}