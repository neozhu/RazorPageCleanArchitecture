// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.EventHandlers;

    public class ObjectFieldCreatedEventHandler : INotificationHandler<DomainEventNotification<ObjectFieldCreatedEvent>>
    {
        private readonly ILogger<ObjectFieldCreatedEventHandler> _logger;

        public ObjectFieldCreatedEventHandler(
            ILogger<ObjectFieldCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ObjectFieldCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
