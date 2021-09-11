using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.SalesContracts.EventHandlers
{
    public class SalesContractCreatedEventHandler : INotificationHandler<DomainEventNotification<SalesContractCreatedEvent>>
    {
        private readonly ILogger<SalesContractCreatedEventHandler> _logger;

        public SalesContractCreatedEventHandler(
            ILogger<SalesContractCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<SalesContractCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}