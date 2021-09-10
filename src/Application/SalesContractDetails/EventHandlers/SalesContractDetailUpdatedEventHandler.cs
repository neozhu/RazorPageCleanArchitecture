using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.EventHandlers
{
    public class SalesContractDetailUpdatedEventHandler : INotificationHandler<DomainEventNotification<SalesContractDetailUpdatedEvent>>
    {
        private readonly ILogger<SalesContractDetailUpdatedEventHandler> _logger;

        public SalesContractDetailUpdatedEventHandler(
            ILogger<SalesContractDetailUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<SalesContractDetailUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}