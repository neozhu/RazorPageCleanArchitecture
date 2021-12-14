// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ResultMappings.EventHandlers;

    public class ResultMappingCreatedEventHandler : INotificationHandler<DomainEventNotification<ResultMappingCreatedEvent>>
    {
        private readonly ILogger<ResultMappingCreatedEventHandler> _logger;

        public ResultMappingCreatedEventHandler(
            ILogger<ResultMappingCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ResultMappingCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
