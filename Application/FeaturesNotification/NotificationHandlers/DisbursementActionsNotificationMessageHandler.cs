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
    public class DisbursementActionsNotificationMessageHandler : INotificationHandler<NotificationMessage>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly RoleManager<Group> roleManager;
        private readonly IOptionsSnapshot<AssetURLS> assetURLS;
        private readonly UserManager<User> userManager;

        public DisbursementActionsNotificationMessageHandler(IApplicationDbContext applicationDbContext, IMediator mediator, RoleManager<Group> roleManager, IOptionsSnapshot<AssetURLS> assetURLS, UserManager<User> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.mediator = mediator;
            this.roleManager = roleManager;
            this.assetURLS = assetURLS;
            this.userManager = userManager;
        }

        public async Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
        {
            //GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetURLS, userManager);

            //if (notification.NotificationActionType == NotificationActionType.AssetCheckOutDisbursed)
            //{
            //    bool generated = await generateEmail.AssetCheckOutDisbursement(notification);
            //}
            //if (notification.NotificationActionType == NotificationActionType.AssetReservationDisbursed)
            //{
            //    bool generated = await generateEmail.AssetReservationDisbursement(notification);
            //}
        }
    }
}