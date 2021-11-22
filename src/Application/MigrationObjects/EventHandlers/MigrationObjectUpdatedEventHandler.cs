// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.EventHandlers;

    public class MigrationObjectUpdatedEventHandler : INotificationHandler<DomainEventNotification<MigrationObjectUpdatedEvent>>
    {
        private readonly ILogger<MigrationObjectUpdatedEventHandler> _logger;

        public MigrationObjectUpdatedEventHandler(
            ILogger<MigrationObjectUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationObjectUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
