// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationObjects.EventHandlers;

    public class MigrationObjectDeletedEventHandler : INotificationHandler<DomainEventNotification<MigrationObjectDeletedEvent>>
    {
        private readonly ILogger<MigrationObjectDeletedEventHandler> _logger;

        public MigrationObjectDeletedEventHandler(
            ILogger<MigrationObjectDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationObjectDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
