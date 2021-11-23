// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldValueMappings.EventHandlers;

    public class FieldValueMappingDeletedEventHandler : INotificationHandler<DomainEventNotification<FieldValueMappingDeletedEvent>>
    {
        private readonly ILogger<FieldValueMappingDeletedEventHandler> _logger;

        public FieldValueMappingDeletedEventHandler(
            ILogger<FieldValueMappingDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<FieldValueMappingDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
