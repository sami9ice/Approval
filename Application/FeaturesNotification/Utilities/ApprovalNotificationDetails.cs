using Application.Features.ApprovalFeatures.Utilities.DTOs;
using Domain.Common;

namespace Application.FeaturesNotification.Utilities
{
    public class ApprovalNotificationDetails
    {
        public ExpectedCurrentAndNextWorkflowApproversDTO ExpectedCurrentAndNextWorkflowApprovers { get; set; }
       // public WorkflowApprovalStatus WorkflowApprovalStatus { get; set; }

        public string CurrentApprovalStage { get; set; }
        public string CurrentApprovers { get; set; }
        //public ApprovalTypeCode ApprovalTypeCode { get; set; }
        public string WorkflowItemId { get; set; }
        public string ApprovalTypeId { get; set; }
    }
}