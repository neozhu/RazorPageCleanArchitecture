// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Settings;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CleanArchitecture.Razor.Application.Workflow.Approval.Steps
{
    public class ApprovedStep : StepBodyAsync
    {
        //private readonly IApplicationDbContext _context;
        private readonly IMailService _mailService;
        private readonly ILogger<ApprovedStep> _logger;
        public string WorkId { get; set; }
        public string DocumentName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string Approver { get; set; }
        public ApprovedStep(
            //IApplicationDbContext context,
            IMailService mailService,
            ILogger<ApprovedStep> logger)
        {
            //_context = context;
            _mailService = mailService;
            _logger = logger;
        }
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            WorkId = context.Workflow.Id;
            Body = $"Your request document has been approved! DocumentName:{DocumentName}";
            Subject = $"Your request has been approved.";
            var request = new MailRequest();
            request.To = To;
            request.Subject = Subject;
            request.Body = Body;
            await _mailService.SendAsync(request);
            Console.WriteLine($"Your request document has been approved by {Approver}! DocumentName:{DocumentName},Send mail to:{To}");
            _logger.LogInformation($"Your request document has been approved by {Approver}! DocumentName:{DocumentName},Send mail to:{To}");

            //var approval = _context.ApprovalDatas.FirstOrDefault(x => x.WorkflowId == WorkId);
            //if (approval != null)
            //{
            //    approval.Status = "Finished";
            //    approval.Outcome = "Approved";
            //    approval.ApprovedDateTime = DateTime.Now;
            //    approval.Comments = "";
            //    await _context.SaveChangesAsync(default);
            //}

            return ExecutionResult.Next();
        }
    }
}
