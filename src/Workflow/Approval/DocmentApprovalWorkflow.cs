// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Workflow.Approval.Data;
using WorkflowCore.Interface;

namespace CleanArchitecture.Razor.Workflow.Approval
{
    public class DocmentApprovalWorkflow : IWorkflow<ApprovalData>
    {
        public string Id => "DocmentApprovalWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<ApprovalData> builder)
        {
            throw new NotImplementedException();
        }
    }
}
