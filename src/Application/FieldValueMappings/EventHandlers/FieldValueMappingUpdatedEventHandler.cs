// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldValueMappings.EventHandlers;

    public class FieldValueMappingUpdatedEventHandler : INotificationHandler<DomainEventNotification<FieldValueMappingUpdatedEvent>>
    {
        private readonly ILogger<FieldValueMappingUpdatedEventHandler> _logger;

        public FieldValueMappingUpdatedEventHandler(
            ILogger<FieldValueMappingUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<FieldValueMappingUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
