using Application.FeaturesNotification.NotificationGenerators;
using Application.FeaturesNotification.Notifications;
using Application.FeaturesNotification.Utilities;
using Application.Interfaces;
using Domain.Common;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.FeaturesNotification.NotificationHandlers
{
    public class ApprovalActionsNotificationMessageHandler : INotificationHandler<NotificationMessage>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly RoleManager<Group> roleManager;
        private IOptionsSnapshot<AssetURLS> assetURLS;
        private readonly UserManager<User> userManager;

        public ApprovalActionsNotificationMessageHandler(IApplicationDbContext applicationDbContext, IMediator mediator, RoleManager<Group> roleManager, IOptionsSnapshot<AssetURLS> assetURLS, UserManager<User> userManager)
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

            //if (notification.NotificationType == NotificationType.Approval)
            //{
            //    ApprovalTypeCode approvalTypeCode = notification.SubmitApprovalCommand.ApprovalTypeCode;
            //    int approvalTypeCodeValue = (int)approvalTypeCode;
            //    if (approvalTypeCodeValue >= 100 && approvalTypeCodeValue < 200)
            //    {
            //        //Asset Approval
            //        if (notification.SubmitApprovalCommand.ApprovalTypeCode == ApprovalTypeCode.NewAsset)
            //        {
            //            bool generated = await GenerateAssetApproval(notification);
            //        }
            //        if (notification.SubmitApprovalCommand.ApprovalTypeCode == ApprovalTypeCode.AssetCheckOut)
            //        {
            //            bool generated = await GenerateAssetCheckOutApproval(notification);
            //        }
            //        if (notification.SubmitApprovalCommand.ApprovalTypeCode == ApprovalTypeCode.AssetCheckIn)
            //        {
            //            bool generated = await GenerateAssetCheckInApproval(notification);
            //        }
            //        if (notification.SubmitApprovalCommand.ApprovalTypeCode == ApprovalTypeCode.AssetReservation)
            //        {
            //            bool generated = await GenerateAssetReservationApproval(notification);
            //        }
            //        if (notification.SubmitApprovalCommand.ApprovalTypeCode == ApprovalTypeCode.AssetService)
            //        {
            //            bool generated = await GenerateAssetServiceApproval(notification);
            //        }
            //        if (notification.SubmitApprovalCommand.ApprovalTypeCode == ApprovalTypeCode.AssetServiceCompletion)
            //        {
            //            bool generated = await GenerateAssetServiceCompletionApproval(notification);
            //        }
            //    }
            //}
        }

        //private async Task<bool> GenerateAssetApproval(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var asset = await applicationDbContext.Assets.Where(x => x.Id == notification.SubmitApprovalCommand.WorkflowItemId)
        //            .FirstOrDefaultAsync();

        //        notification.MainEntity = asset;

        //        notification.NotificationActionType = NotificationActionType.AssetCreated;

        //        GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetURLS, userManager);
        //        generated = await generateEmail.AssetCreated(notification);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //private async Task<bool> GenerateAssetCheckOutApproval(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var assetCheckout = await applicationDbContext.AssetCheckOuts
        //            .Where(x => x.Id == notification.SubmitApprovalCommand.WorkflowItemId).FirstOrDefaultAsync();
        //        var asset = await applicationDbContext.Assets.Where(x => x.Id == assetCheckout.AssetId)
        //            .FirstOrDefaultAsync();

        //        notification.MainEntity = asset;
        //        notification.SubEntity = assetCheckout;

        //        notification.NotificationActionType = NotificationActionType.AssetCheckOut;

        //        GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetURLS, userManager);
        //        generated = await generateEmail.AssetCheckOut(notification);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //private async Task<bool> GenerateAssetCheckInApproval(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var assetCheckin = await applicationDbContext.AssetCheckIns
        //            .Where(x => x.Id == notification.SubmitApprovalCommand.WorkflowItemId).FirstOrDefaultAsync();
        //        var asset = await applicationDbContext.Assets.Where(x => x.Id == assetCheckin.AssetId)
        //            .FirstOrDefaultAsync();

        //        notification.MainEntity = asset;
        //        notification.SubEntity = assetCheckin;

        //        notification.NotificationActionType = NotificationActionType.AssetCheckIn;

        //        GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetURLS, userManager);
        //        generated = await generateEmail.AssetCheckIn(notification);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //private async Task<bool> GenerateAssetReservationApproval(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var assetReservation = await applicationDbContext.ReservedAssets
        //            .Where(x => x.Id == notification.SubmitApprovalCommand.WorkflowItemId).FirstOrDefaultAsync();
        //        var asset = await applicationDbContext.Assets.Where(x => x.Id == assetReservation.AssetId)
        //            .FirstOrDefaultAsync();

        //        notification.MainEntity = asset;
        //        notification.SubEntity = assetReservation;

        //        notification.NotificationActionType = NotificationActionType.AssetReserved;

        //        GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetURLS, userManager);
        //        generated = await generateEmail.AssetReservation(notification);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //private async Task<bool> GenerateAssetServiceApproval(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var assetService = await applicationDbContext.ServiceAssets
        //            .Where(x => x.Id == notification.SubmitApprovalCommand.WorkflowItemId).FirstOrDefaultAsync();
        //        var asset = await applicationDbContext.Assets.Where(x => x.Id == assetService.AssetId)
        //            .FirstOrDefaultAsync();

        //        notification.MainEntity = asset;
        //        notification.SubEntity = assetService;

        //        notification.NotificationActionType = NotificationActionType.AssetService;

        //        GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetURLS, userManager);
        //        generated = await generateEmail.AssetService(notification);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //private async Task<bool> GenerateAssetServiceCompletionApproval(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var assetService = await applicationDbContext.ServiceAssets
        //            .Where(x => x.Id == notification.SubmitApprovalCommand.WorkflowItemId).FirstOrDefaultAsync();
        //        var asset = await applicationDbContext.Assets.Where(x => x.Id == assetService.AssetId)
        //            .FirstOrDefaultAsync();

        //        notification.MainEntity = asset;
        //        notification.SubEntity = assetService;

        //        notification.NotificationActionType = NotificationActionType.AssetServiceCompleted;

        //        GenerateEmail generateEmail = new GenerateEmail(applicationDbContext, mediator, roleManager, assetURLS, userManager);
        //        generated = await generateEmail.AssetService(notification);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}
    }
}