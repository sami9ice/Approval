using Application.FeaturesNotification.Notifications;
using Application.Interfaces;
using Application.Request.CustomerData;
using Application.Response.CustomerData;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.CustomerDataUtilities
{
    public class ApprovalFlow : IRequestHandler<ApprovalFlowRequest, (bool Succeed, string Message, ApprovalFlowResponse)>
    {

        //A.get level wit formid from data

        //                                                                                                                                B.use level to get gruop and den users.
        //                                                                                                                                C.use pin to get user.
        //																										if B contains C
        //                                                                                                                                update log
        //                                                                                                                                increament Level+1

        //                                                                                                                                find level+1 from leveldb
        //																										if true{
        //    update Data

        //                                                                                                                                Send mail}
        //																										else

        private readonly IApplicationDbContext applicationDb;
        private readonly RoleManager<Domain.User.Group> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IMediator mediator;



        public ApprovalFlow(IMediator mediator, IApplicationDbContext applicationDb, RoleManager<Domain.User.Group> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            this.applicationDb = applicationDb;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
            this.mediator = mediator;
        }


        public async Task<(bool Succeed, string Message, ApprovalFlowResponse)> Handle(ApprovalFlowRequest request, CancellationToken cancellationToken)
        {
            var response = new ApprovalFlowResponse(); /*mapper.Map<ApprovalFlowResponse>(request);*/
            response.FormId = request.FormId;
         
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(request.PIN);
            var encodedData = System.Convert.ToBase64String(plainTextBytes);
            request.PIN = encodedData.ToString();

            var GetUser = await userManager.Users.FirstOrDefaultAsync(a => a.PIN == request.PIN);
            if (GetUser == null) return (false, "User does not exists", response);
            //get level with formid from data
            var level = await applicationDb.Data.FirstOrDefaultAsync(a => a.FormId == request.FormId);
            if (level == null) return (false, "Form Id does not exists", null);
            // use level to get group then get user where request.PIN==user.PIN 
            var Users = await (from a in applicationDb.ApprovalLevel
                         where a.LevelName == level.LevelName
                         join g in roleManager.Roles on a.Id equals g.ApprovalLevelId
                         join ug in applicationDb.UserGroups on g.Id equals ug.GroupdId
                         join u in userManager.Users on ug.UserId equals u.Id
                         select new User
                         {
                             PIN = u.PIN,
                         }).Where(a => a.PIN == GetUser.PIN).FirstOrDefaultAsync();
            if (Users == null) return (false, "User can not approve at this level", response);

            ApprovalLog log = new ApprovalLog();
            log.ApprovedBy = GetUser.UserName;
            log.ApprovalLevelName = level.LevelName;
            log.ApprovalGroupName = userManager.GetRolesAsync(GetUser).Result.ToArray();
            log.Status = true;
            log.Reason = request.Reason;
            log.FormId = request.FormId;
            log.Datas = level;
            log.UserId = GetUser.Id;
            ///Update log table
            await applicationDb.ApprovalLog.AddAsync(log, cancellationToken);
            string GetDigitFromLevelname = new String(level.LevelName.Where(Char.IsDigit).ToArray());
            var integer = Convert.ToInt32(GetDigitFromLevelname);
            integer = integer + 1;
            var inte = integer.ToString();
            var NewLevel = Regex.Replace(level.LevelName, @"[\d-]", inte);
            //check if this level exist
            var CheckLevel = await applicationDb.ApprovalLevel.Where(a => a.LevelName == NewLevel).FirstOrDefaultAsync();
            if (CheckLevel == null) return (true, "Approval Succeeded", response);
            level.LevelName = NewLevel;
            level.levelId = CheckLevel.Id;
            level.CreatedBy = GetUser.UserName;
            level.UserId = GetUser.Id;
            //update Data with the next level to approve
           

            var EmailAddress = await (from a in applicationDb.ApprovalLevel
                                where a.LevelName == NewLevel
                                join g in roleManager.Roles on a.Id equals g.ApprovalLevelId
                                join ug in applicationDb.UserGroups on g.Id equals ug.GroupdId
                                join u in userManager.Users on ug.UserId equals u.Id
                                select new string(u.Email)).ToListAsync();

            //var group = roleManager.Roles.Where(a => a.ApprovalLevelId == CheckLevel.Id).FirstOrDefault();
            //var user = userManager.GetUsersInRoleAsync(group.GroupName).Result.ToList();
            //var usersinroles = userManager.GetUsersInRoleAsync(group.GroupName).Result.Select(a => a.Email).ToList();


            //var usersinroless = userManager.GetUsersInRoleAsync(CheckLevel.Group.GroupName).Result.Select(a => a.Email).ToList();

            NotificationMessage notificationMessage = new NotificationMessage();
            notificationMessage.Data = level;
            notificationMessage.UserEmailAdd = EmailAddress;
            notificationMessage.NotificationType = NotificationType.Approval;
            notificationMessage.NotificationActionType = NotificationActionType.DataCreated;


            await mediator.Publish(notificationMessage, cancellationToken);
            //send mail
            applicationDb.Data.Attach(level);
            _ = applicationDb.SaveChangesAsync();
            return (true, "Approval Succeeded", response);


        }
    }
}
