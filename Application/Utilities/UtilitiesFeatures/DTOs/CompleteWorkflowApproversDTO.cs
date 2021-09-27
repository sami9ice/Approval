using Domain.Entities;
using System.Collections.Generic;

namespace Application.Features.ApprovalFeatures.Utilities.DTOs
{
    public class CompleteWorkflowApproversDTO
    {
       // public ApprovalType Workflow { get; set; }
        public List<WorkflowApproversDTO> WorkflowApprovers { get; set; }

        public CompleteWorkflowApproversDTO()
        {
            WorkflowApprovers = new List<WorkflowApproversDTO>();
        }
    }
}