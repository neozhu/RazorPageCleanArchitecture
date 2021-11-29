// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationProjects.EventHandlers;

    public class MigrationProjectUpdatedEventHandler : INotificationHandler<DomainEventNotification<MigrationProjectUpdatedEvent>>
    {
        private readonly ILogger<MigrationProjectUpdatedEventHandler> _logger;

        public MigrationProjectUpdatedEventHandler(
            ILogger<MigrationProjectUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationProjectUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
