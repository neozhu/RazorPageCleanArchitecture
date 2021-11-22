// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.EventHandlers;

    public class ObjectFieldDeletedEventHandler : INotificationHandler<DomainEventNotification<ObjectFieldDeletedEvent>>
    {
        private readonly ILogger<ObjectFieldDeletedEventHandler> _logger;

        public ObjectFieldDeletedEventHandler(
            ILogger<ObjectFieldDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ObjectFieldDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
