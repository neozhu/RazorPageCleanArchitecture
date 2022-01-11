// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Domain.Entities.Worflow;
using WorkflowCore.Interface;

namespace CleanArchitecture.Razor.Application.Documents.EventHandlers
{
    public class DocumentCreatedEventHandler : INotificationHandler<DomainEventNotification<DocumentCreatedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<DocumentCreatedEventHandler> _logger;

        public DocumentCreatedEventHandler(
            IApplicationDbContext context,
            ILogger<DocumentCreatedEventHandler> logger,
            IWorkflowHost  workflowHost
            )
        {
            _context = context;
            _logger = logger;
  
        }
        public async Task Handle(DomainEventNotification<DocumentCreatedEvent> notification, CancellationToken cancellationToken)
        {
           
           
        }
    }
}
