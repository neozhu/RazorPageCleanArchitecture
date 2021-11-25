// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MappingRules.EventHandlers;

    public class MappingRuleUpdatedEventHandler : INotificationHandler<DomainEventNotification<MappingRuleUpdatedEvent>>
    {
        private readonly ILogger<MappingRuleUpdatedEventHandler> _logger;

        public MappingRuleUpdatedEventHandler(
            ILogger<MappingRuleUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MappingRuleUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
