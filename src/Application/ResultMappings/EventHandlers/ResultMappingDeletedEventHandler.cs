// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ResultMappings.EventHandlers;

    public class ResultMappingDeletedEventHandler : INotificationHandler<DomainEventNotification<ResultMappingDeletedEvent>>
    {
        private readonly ILogger<ResultMappingDeletedEventHandler> _logger;

        public ResultMappingDeletedEventHandler(
            ILogger<ResultMappingDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ResultMappingDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
