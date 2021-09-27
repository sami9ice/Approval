using Application.FeaturesNotification.NotificationGenerators;
using Application.FeaturesNotification.Notifications;
using Application.FeaturesNotification.Utilities;
using Application.Interfaces;
using Domain.Common;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Application.FeaturesNotification.NotificationHandlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>MediatR.INotificationHandler{Application.FeaturesNotification.Notifications.NotificationMessage}</cref>
    /// </seealso>
    public class ConfigurationActionsNotificationMessageHandler : INotificationHandler<NotificationMessage>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly RoleManager<Group> roleManager;
        private readonly IOptionsSnapshot<AssetURLS> assetUrls;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationActionsNotificationMessageHandler"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="assetUrls">The asset urls.</param>
        /// <param name="userManager">The user manager.</param>
        public ConfigurationActionsNotificationMessageHandler(IApplicationDbContext applicationDbContext, IMediator mediator, RoleManager<Group> roleManager, IOptionsSnapshot<AssetURLS> assetUrls, UserManager<User> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.mediator = mediator;
            this.roleManager = roleManager;
            this.assetUrls = assetUrls;
            this.userManager = userManager;
        }
        /// <summary>
        /// Handles a notification
        /// </summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
        {
            //GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetUrls, userManager);

            //if (notification.NotificationActionType == NotificationActionType.EmployeeCreated)
            //{
            //    await generateEmail.EmployeeCreated(notification);
            //}
        }
    }
}