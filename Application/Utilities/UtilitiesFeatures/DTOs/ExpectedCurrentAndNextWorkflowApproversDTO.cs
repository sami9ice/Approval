namespace Application.Features.ApprovalFeatures.Utilities.DTOs
{
    /// <summary>
    ///
    /// </summary>
    public class ExpectedCurrentAndNextWorkflowApproversDTO
    {
        /// <summary>
        /// Gets or sets the current workflow approvers.
        /// </summary>
        /// <value>
        /// The current workflow approvers.
        /// </value>
        public WorkflowApproversDTO CurrentWorkflowApprovers { get; set; }

        /// <summary>
        /// Gets or sets the current workflow approver.
        /// </summary>
        /// <value>
        /// The current workflow approver.
        /// </value>
        public UserDTO CurrentWorkflowApprover { get; set; }

        /// <summary>
        /// Gets or sets the current role.
        /// </summary>
        /// <value>
        /// The current role.
        /// </value>
        public GroupDTO CurrentRole { get; set; }

        /// <summary>
        /// Gets or sets the next workflow approvers.
        /// </summary>
        /// <value>
        /// The next workflow approvers.
        /// </value>
        public WorkflowApproversDTO NextWorkflowApprovers { get; set; }

        //new
        //public int CurrentWorkflowStage { get; set; }
        //public int NextWorkflowStage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [no more approver].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no more approver]; otherwise, <c>false</c>.
        /// </value>
        public bool NoMoreApprover { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        //end
    }
}