using System.Collections.Generic;

namespace Application.Features.ApprovalFeatures.Utilities.DTOs
{
    public class WorkflowApproversDTO
    {
        public bool CurrentApprovers { get; set; }
        public List<UserDTO> Approvers { get; set; }

       // public ApprovalConfigDTO WorkflowStage { get; set; }

        public WorkflowApproversDTO()
        {
            Approvers = new List<UserDTO>();
        }
    }
}