using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.EventHandlers
{
    public class SalesContractDetailCreatedEventHandler : INotificationHandler<DomainEventNotification<SalesContractDetailCreatedEvent>>
    {
        private readonly ILogger<SalesContractDetailCreatedEventHandler> _logger;

        public SalesContractDetailCreatedEventHandler(
            ILogger<SalesContractDetailCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<SalesContractDetailCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}