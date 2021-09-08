using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.EventHandlers
{
    public class PurchaseContractDeletedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractDeletedEvent>>
    {
        private readonly ILogger<PurchaseContractDeletedEventHandler> _logger;

        public PurchaseContractDeletedEventHandler(
            ILogger<PurchaseContractDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<PurchaseContractDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}