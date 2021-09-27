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
    public class AssetActionsNotificationMessageHandler : INotificationHandler<NotificationMessage>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly RoleManager<Group> roleManager;
        private readonly IOptionsSnapshot<AssetURLS> assetURLS;
        private readonly UserManager<User> userManager;

        public AssetActionsNotificationMessageHandler(IApplicationDbContext applicationDbContext, IMediator mediator, RoleManager<Group> roleManager, IOptionsSnapshot<AssetURLS> assetURLS, UserManager<User> userManager)
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

            //if (notification.NotificationActionType == NotificationActionType.AssetCreated)
            //{
            //    bool generated = await generateEmail.AssetCreated(notification);
            //}
            //if (notification.NotificationActionType == NotificationActionType.AssetCheckOut)
            //{
            //    bool generated = await generateEmail.AssetCheckOut(notification);
            //}
            //if (notification.NotificationActionType == NotificationActionType.AssetService)
            //{
            //    bool generated = await generateEmail.AssetService(notification);
            //}
            //if (notification.NotificationActionType == NotificationActionType.AssetReserved)
            //{
            //    bool generated = await generateEmail.AssetReservation(notification);
            //}
            //if (notification.NotificationActionType == NotificationActionType.AssetCheckIn)
            //{
            //    bool generated = await generateEmail.AssetCheckIn(notification);
            //}
        }
    }
}