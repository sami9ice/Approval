using Application.FeaturesNotification.Utilities;
using Domain.Common;
using Domain.Entities;
using Domain.User;
using MediatR;
using System.Collections.Generic;

namespace Application.FeaturesNotification.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MediatR.INotification" />
    public class NotificationMessage : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessage"/> class.
        /// </summary>
        public NotificationMessage()
        {
            NewUserRoles = new List<string>();
            UserEmailAdd = new List<string>();

        }

        /// <summary>
        /// Gets or sets the main entity.
        /// </summary>
        /// <value>
        /// The main entity.
        /// </value>
        public BaseEntity MainEntity { get; set; }
        /// <summary>
        /// Gets or sets the sub entity.
        /// </summary>
        /// <value>
        /// The sub entity.
        /// </value>
        public BaseEntity SubEntity { get; set; }

        /// <summary>
        /// Gets or sets the disbursement entity.
        /// </summary>
        /// <value>
        /// The disbursement entity.
        /// </value>
        public BaseEntity DisbursementEntity { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
        public Data Data { get; set; }

        /// <summary>
        /// Creates new userpassword.
        /// </summary>
        /// <value>
        /// The new user password.
        /// </value>
        public string NewUserPassword { get; set; }
        /// <summary>
        /// Creates new userroles.
        /// </summary>
        /// <value>
        /// The new user roles.
        /// </value>
        public List<string> NewUserRoles { get; set; }
        public List<string> UserEmailAdd { get; set; }

        /// <summary>
        /// Gets or sets the workflow item identifier.
        /// </summary>
        /// <value>
        /// The workflow item identifier.
        /// </value>
        public string WorkflowItemId { get; set; }
        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        /// <value>
        /// The type of the notification.
        /// </value>
        public NotificationType NotificationType { get; set; }
        /// <summary>
        /// Gets or sets the approval notification details.
        /// </summary>
        /// <value>
        /// The approval notification details.
        /// </value>
        public ApprovalNotificationDetails ApprovalNotificationDetails { get; set; }
        /// <summary>
        /// Gets or sets the type of the notification action.
        /// </summary>
        /// <value>
        /// The type of the notification action.
        /// </value>
        public NotificationActionType NotificationActionType { get; set; }

        ///// <summary>
        ///// Gets or sets the submit approval command response.
        ///// </summary>
        ///// <value>
        ///// The submit approval command response.
        ///// </value>
      //  public SubmitApprovalCommandResponse SubmitApprovalCommandResponse { get; set; }
        ///// <summary>
        ///// Gets or sets the submit approval command.
        ///// </summary>
        ///// <value>
        ///// The submit approval command.
        ///// </value>
        //public SubmitApprovalCommand SubmitApprovalCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [no more approval].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no more approval]; otherwise, <c>false</c>.
        /// </value>
        public bool NoMoreApproval { get; set; }

    }
}