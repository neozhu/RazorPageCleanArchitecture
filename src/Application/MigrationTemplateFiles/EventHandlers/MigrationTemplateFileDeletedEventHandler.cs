// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.EventHandlers;

    public class MigrationTemplateFileDeletedEventHandler : INotificationHandler<DomainEventNotification<MigrationTemplateFileDeletedEvent>>
    {
        private readonly ILogger<MigrationTemplateFileDeletedEventHandler> _logger;

        public MigrationTemplateFileDeletedEventHandler(
            ILogger<MigrationTemplateFileDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationTemplateFileDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
