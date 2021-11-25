// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.EventHandlers;

    public class FieldMappingValueUpdatedEventHandler : INotificationHandler<DomainEventNotification<FieldMappingValueUpdatedEvent>>
    {
        private readonly ILogger<FieldMappingValueUpdatedEventHandler> _logger;

        public FieldMappingValueUpdatedEventHandler(
            ILogger<FieldMappingValueUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<FieldMappingValueUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
