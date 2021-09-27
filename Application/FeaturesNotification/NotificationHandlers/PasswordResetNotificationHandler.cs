using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.FeaturesNotification.Notifications;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FeaturesNotification.NotificationHandlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>MediatR.INotificationHandler{Application.FeaturesNotification.Notifications.NotificationMessage}</cref>
    /// </seealso>
    public class PasswordResetNotificationHandler : INotificationHandler<NotificationMessage>
    {
        private readonly IApplicationDbContext applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordResetNotificationHandler"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public PasswordResetNotificationHandler(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        /// <summary>
        /// Handles a notification
        /// </summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
        {
            if (notification.NotificationActionType == NotificationActionType.PasswordReset)
            {
                var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
                    .Where(n => n.NotificationActionType == notification.NotificationActionType && !n.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);
                string template = notificationMessageTemplate?.MessageTemplate;
                if (!string.IsNullOrWhiteSpace(template))
                {
                    var itemActionName = "Reset Password";

                    if (notification.NotificationType == NotificationType.Approval)
                    {
                        itemActionName += " Reset Password";
                    }

                    if (template.Contains(@"{ItemActionName}"))
                    {
                        template = template.Replace(@"{ItemActionName}", itemActionName);
                    }
                    if (template.Contains(@"{RecipientFullName}"))
                    {
                        template = template.Replace(@"{RecipientFullName}", notification.User?.Fullname);

                    }
                    if (template.Contains(@"{UserEmail}"))
                    {
                        template = template.Replace(@"{UserEmail}", notification.User?.Email);

                    }

                    if (template.Contains(@"{Password}"))
                    {
                        template = template.Replace(@"{Password}", notification.NewUserPassword);

                    }

                    var message = new NotificationMessages
                    {
                        To = notification.User?.Email,
                        Body = template,
                        NotificationActionType = notification.NotificationActionType,
                        Template = template,

                    };
                    applicationDbContext.NotificationMessages.Add(message);
                    await applicationDbContext.SaveChangesAsync(cancellationToken);

                }
            }

        }
    }
}