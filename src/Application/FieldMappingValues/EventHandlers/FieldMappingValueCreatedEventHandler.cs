// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.EventHandlers;

    public class FieldMappingValueCreatedEventHandler : INotificationHandler<DomainEventNotification<FieldMappingValueCreatedEvent>>
    {
        private readonly ILogger<FieldMappingValueCreatedEventHandler> _logger;

        public FieldMappingValueCreatedEventHandler(
            ILogger<FieldMappingValueCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<FieldMappingValueCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
