// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Workflow.Approval;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;

namespace CleanArchitecture.Razor.Application.Documents.EventHandlers
{
    public class DocumentCreatedEventHandler : INotificationHandler<DomainEventNotification<DocumentCreatedEvent>>
    {
        private readonly ILogger<DocumentCreatedEventHandler> _logger;
        private readonly IWorkflowHost _workflowHost;

        public DocumentCreatedEventHandler(
            ILogger<DocumentCreatedEventHandler> logger,
            IWorkflowHost  workflowHost
            )
        {
            _logger = logger;
            _workflowHost = workflowHost;
        }
        public async Task Handle(DomainEventNotification<DocumentCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var data = new ApprovalData() {
                Applicant = "neozhu@126.net",
                Approver = "new163@163.com",
                DocumentId = notification.DomainEvent.Item.Id,
                DocumentName = notification.DomainEvent.Item.Title,
                Status = "Pending",
                Url = notification.DomainEvent.Item.URL,
                RequestDateTime = DateTime.Now,
                WorkflowName= nameof(DocmentApprovalWorkflow)
            };
            var workid = await _workflowHost.StartWorkflow(nameof(DocmentApprovalWorkflow), data);
            Thread.Sleep(1000);
            var openItems = _workflowHost.GetOpenUserActions(workid);
            _logger.LogInformation($"start new workflow:{workid}");
        }
    }
}
