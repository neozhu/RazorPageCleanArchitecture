// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace CleanArchitecture.Razor.Domain.Entities.Worflow
{
    public  class ApprovalData:AuditableEntity
    {
        public string WorkflowId { get; set; }
        public string WorkflowName { get; set; }
      
        public string Status { get; set; }
        public string DocumentName { get; set; }
        public int DocumentId { get; set; }
        public string Url { get; set; }
        public string Applicant { get; set; }
        public string Approver { get; set; }
        public string Outcome { get; set; }
        public string Comments { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public DateTime RequestDateTime { get; set; }
    }
}
