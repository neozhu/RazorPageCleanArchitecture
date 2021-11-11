// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Workflow.Approval;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using WorkflowCore.Interface;

namespace CleanArchitecture.Razor.Application.Documents.EventHandlers
{
    public class DocumentCreatedEventHandler : INotificationHandler<DomainEventNotification<DocumentCreatedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<DocumentCreatedEventHandler> _logger;
        private readonly IWorkflowHost _workflowHost;

        public DocumentCreatedEventHandler(
            IApplicationDbContext context,
            ILogger<DocumentCreatedEventHandler> logger,
            IWorkflowHost  workflowHost
            )
        {
            _context = context;
            _logger = logger;
            _workflowHost = workflowHost;
        }
        public async Task Handle(DomainEventNotification<DocumentCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var data = new ApprovalData() {
                Applicant = "neozhu@126.com",
                Approver = "new163@163.com",
                DocumentId = notification.DomainEvent.Item.Id,
                DocumentName = notification.DomainEvent.Item.Title,
                Status = "Pending",
                Url = notification.DomainEvent.Item.URL,
                RequestDateTime = DateTime.Now,
                WorkflowName= nameof(DocmentApprovalWorkflow)
            };
          
            var workid = await _workflowHost.StartWorkflow(nameof(DocmentApprovalWorkflow), data);
            data.WorkflowId= workid;
            _context.ApprovalDatas.Add(data);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"start new workflow:{workid}");
        }
    }
}
