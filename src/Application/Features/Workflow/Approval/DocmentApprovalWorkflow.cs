// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Workflow.Approval.Steps;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using WorkflowCore.Interface;

namespace CleanArchitecture.Razor.Application.Workflow.Approval;

public class DocmentApprovalWorkflow : IWorkflow<ApprovalData>
{
    public string Id => nameof(DocmentApprovalWorkflow);

    public int Version => 1;

    public void Build(IWorkflowBuilder<ApprovalData> builder)
    {
        builder
             .StartWith<InitialStep>()
                    .Input(step => step.To, data => data.Approver)
                    .Input(step => step.DocumentName, data => data.DocumentName)
                    .Input(step => step.DocumentId, data => data.DocumentId)
                    .Output(data => data.WorkflowId, step => step.WorkId)
             .UserTask("Do you approve", data => data.Approver)
                 .WithOption("Approved", "I approve").Do(then => then
                     .StartWith<ApprovedStep>()
                     .Input(step => step.DocumentName, data => data.DocumentName)
                     .Input(step => step.To, data => data.Applicant)
                 )
                 .WithOption("Rejected", "I do not approve").Do(then => then
                     .StartWith<RejectedStep>()
                     .Input(step => step.DocumentName, data => data.DocumentName)
                     .Input(step => step.To, data => data.Applicant)
                 )
                 .WithEscalation(x => TimeSpan.FromMinutes(1), x => x.Applicant, action => action
                     .StartWith<CancelStep>()
                     .Input(step => step.To, data => data.Applicant)
                     .Input(step => step.DocumentName, data => data.DocumentName)
                     )

             .Then(context => Console.WriteLine("end"));
    }
}
