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
    public class InitialStep : StepBodyAsync
    {
        private readonly IApplicationDbContext _context;
        private readonly IMailService _mailService;
        private readonly ILogger<InitialStep> _logger;
        public string WorkId { get; set; }
        public string To { get; set; }
        public string From {  get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string DocumentName { get; set; }
        public int DocumentId { get; set; }
             
        public InitialStep(
            IApplicationDbContext context,
            IMailService mailService,
            ILogger<InitialStep> logger)
        {
            _context = context;
            _mailService = mailService;
            _logger = logger;
        }
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            WorkId = context.Workflow.Id;
            Body = $"Please,Approval this document:{DocumentName},URL:http://....";
            Subject = $"a new document approval request";
            var request = new MailRequest();
            request.To = To;
            request.Subject = Subject;
            request.Body = Body;
            await _mailService.SendAsync(request);
            Console.WriteLine($"Send Mail to {To},{Body}");
            _logger.LogInformation($"Send Mail to {To},{Body}");
            var approval = _context.ApprovalDatas.FirstOrDefault(x => x.WorkflowId == WorkId);
            if (approval != null)
            {
                approval.Status = "Pending";
                approval.RequestDateTime = DateTime.Now;
                await _context.SaveChangesAsync(default);
            }
            return ExecutionResult.Next();
        }
    }
}
