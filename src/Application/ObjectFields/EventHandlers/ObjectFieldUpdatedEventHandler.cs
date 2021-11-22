// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.EventHandlers;

    public class ObjectFieldUpdatedEventHandler : INotificationHandler<DomainEventNotification<ObjectFieldUpdatedEvent>>
    {
        private readonly ILogger<ObjectFieldUpdatedEventHandler> _logger;

        public ObjectFieldUpdatedEventHandler(
            ILogger<ObjectFieldUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ObjectFieldUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
