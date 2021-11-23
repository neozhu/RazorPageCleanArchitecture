// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.EventHandlers;

    public class MigrationTemplateFileUpdatedEventHandler : INotificationHandler<DomainEventNotification<MigrationTemplateFileUpdatedEvent>>
    {
        private readonly ILogger<MigrationTemplateFileUpdatedEventHandler> _logger;

        public MigrationTemplateFileUpdatedEventHandler(
            ILogger<MigrationTemplateFileUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<MigrationTemplateFileUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
