// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Workflow.Approval.Data;
using CleanArchitecture.Razor.Application.Workflow.Approval.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CleanArchitecture.Razor.Application.Workflow.Approval
{
    public class DocmentApprovalWorkflow : IWorkflow<ApprovalData>
    {
        public string Id => nameof(DocmentApprovalWorkflow);

        public int Version => 1;

        public void Build(IWorkflowBuilder<ApprovalData> builder)
        {
            builder
                 .StartWith<InitialStep>()
                        .Input(step=>step.To,data=>data.Approver)
                        .Input(step => step.DocumentName, data => data.DocumentName)
                        .Input(step => step.DocumentId, data => data.DocumentId)
                        .Output(data=>data.WorkId,step=> step.WorkId)
                 .UserTask("Do you approve", data => data.Approver)
                     .WithOption("yes", "I approve").Do(then => then
                         .StartWith<ApprovedStep>()
                         .Input(step=>step.Approver,data=>data.Approver)
                         .Input(step => step.DocumentName, data => data.DocumentName)
                         .Input(step=>step.To,data=>data.Applicant)
                     )
                     .WithOption("no", "I do not approve").Do(then => then
                         .StartWith<RejectedStep>()
                         .Input(step => step.Approver, data => data.Approver)
                         .Input(step => step.DocumentName, data => data.DocumentName)
                         .Input(step => step.To, data => data.Applicant)
                     )
                     .WithEscalation(x => TimeSpan.FromMinutes(1), x => x.Applicant, action => action
                         .StartWith<NotificationStep>()
                         .Input(step=>step.To,data=>data.Applicant)
                         .Input(step=>step.Subject,data=>"Timeout,Cancel the request")
                         .Input(step=>step.Body,data=>"Your request document approval has been timeout.")
                         )
                          
                 .Then(context => Console.WriteLine("end"));
        }
    }
}
