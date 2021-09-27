namespace Application.Features.ApprovalFeatures.Utilities
{
    public class WorkflowApprovalSettings
    {
        public bool UseApprovalFlowAtPointOfInitiation { get; set; }
        public bool UseLocationBasedApproval { get; set; }
        public string DefaultEmployeePassword { get; set; }
        public string DefaultEmployeeRole { get; set; }
    }
}