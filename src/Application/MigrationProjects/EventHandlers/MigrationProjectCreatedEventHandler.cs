// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationProjects.EventHandlers;

    public class MigrationProjectCreatedEventHandler : INotificationHandler<DomainEventNotification<MigrationProjectCreatedEvent>>
    {
        private readonly ILogger<MigrationProjectCreatedEventHandler> _logger;

        public MigrationProjectCreatedEventHandler(
            ILogger<MigrationProjectCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationProjectCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
