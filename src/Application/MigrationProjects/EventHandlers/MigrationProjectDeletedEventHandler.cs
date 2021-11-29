// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationProjects.EventHandlers;

    public class MigrationProjectDeletedEventHandler : INotificationHandler<DomainEventNotification<MigrationProjectDeletedEvent>>
    {
        private readonly ILogger<MigrationProjectDeletedEventHandler> _logger;

        public MigrationProjectDeletedEventHandler(
            ILogger<MigrationProjectDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationProjectDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
