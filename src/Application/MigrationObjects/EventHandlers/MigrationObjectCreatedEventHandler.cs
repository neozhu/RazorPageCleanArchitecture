// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.EventHandlers;

    public class MigrationObjectCreatedEventHandler : INotificationHandler<DomainEventNotification<MigrationObjectCreatedEvent>>
    {
        private readonly ILogger<MigrationObjectCreatedEventHandler> _logger;

        public MigrationObjectCreatedEventHandler(
            ILogger<MigrationObjectCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationObjectCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
