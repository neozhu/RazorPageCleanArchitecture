// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldValueMappings.EventHandlers;

    public class FieldValueMappingCreatedEventHandler : INotificationHandler<DomainEventNotification<FieldValueMappingCreatedEvent>>
    {
        private readonly ILogger<FieldValueMappingCreatedEventHandler> _logger;

        public FieldValueMappingCreatedEventHandler(
            ILogger<FieldValueMappingCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<FieldValueMappingCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
