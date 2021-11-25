// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MappingRules.EventHandlers;

    public class MappingRuleDeletedEventHandler : INotificationHandler<DomainEventNotification<MappingRuleDeletedEvent>>
    {
        private readonly ILogger<MappingRuleDeletedEventHandler> _logger;

        public MappingRuleDeletedEventHandler(
            ILogger<MappingRuleDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MappingRuleDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
