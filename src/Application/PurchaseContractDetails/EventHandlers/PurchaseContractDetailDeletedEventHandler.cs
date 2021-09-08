using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.EventHandlers
{
    public class PurchaseContractDetailDeletedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractDetailDeletedEvent>>
    {
        private readonly ILogger<PurchaseContractDetailDeletedEventHandler> _logger;

        public PurchaseContractDetailDeletedEventHandler(
            ILogger<PurchaseContractDetailDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseContractDetailDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}