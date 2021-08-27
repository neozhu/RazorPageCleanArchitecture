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

namespace CleanArchitecture.Razor.Workflow.Approval.Steps
{
    public class SendMailStep : StepBodyAsync
    {
        private readonly IMailService _mailService;
        private readonly ILogger<SendMailStep> _logger;

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body {  get; set; }
        public SendMailStep(IMailService mailService,
            ILogger<SendMailStep> logger
            )
        {
            _mailService = mailService;
            _logger = logger;
        }
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var request = new MailRequest();
            request.To = To;
            request.Subject = Subject;
            request.Body = Body;
            await _mailService.SendAsync(request);
            Console.WriteLine($"Send Mail to {To},{Body}");
            _logger.LogInformation($"Send Mail to {To},{Body}");
            return ExecutionResult.Next();
        }
    }
}
