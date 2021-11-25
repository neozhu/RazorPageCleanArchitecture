// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.EventHandlers;

    public class FieldMappingValueDeletedEventHandler : INotificationHandler<DomainEventNotification<FieldMappingValueDeletedEvent>>
    {
        private readonly ILogger<FieldMappingValueDeletedEventHandler> _logger;

        public FieldMappingValueDeletedEventHandler(
            ILogger<FieldMappingValueDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<FieldMappingValueDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
