using Application.FeaturesNotification.Notifications;
using Application.Interfaces;
using Application.Request.CustomerData;
using Application.Response.ApprovalLog;
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
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.CustomerDataUtilities
{
   public class CreateCustomerCommand: IRequestHandler<CreateCustomerDataRequest, (bool Succeed, string Message, CreateCustomerDataResponse Response)>
    {

        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly RoleManager<Group> roleManager;
        private readonly UserManager<User> userManager;

        //private readonly IGeneratorService Log;
        private readonly IGeneratePassword getpassword;
        private readonly IMediator mediator;


        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public CreateCustomerCommand(UserManager<User> userManager,IMediator mediator,RoleManager<Group> roleManager,IApplicationDbContext applicationDb, IMapper mapper,IGeneratePassword getpassword /*IApprovalProcessLog Log,*/ /*IGeneratorService generator*/)

        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.getpassword = getpassword;
            this.roleManager = roleManager;
            this.mediator = mediator;
            this.userManager = userManager;
            //this.generator = generator;

        }
        public async Task<(bool Succeed, string Message, CreateCustomerDataResponse Response)> Handle(CreateCustomerDataRequest request, CancellationToken cancellationToken)
        {

            var data = mapper.Map<Data>(request);
            if (data.UserId == null)
            {
                return (false, "The Creator Id can not be null", null);
            }
            if (data.CreatedBy == null)
            {
                return (false, "The Creator name can not be null", null);
            }
            var EmialAddressExist = await applicationDb.Data.FirstOrDefaultAsync(x => x.EmialAddress.Equals(request.EmialAddress), cancellationToken);
            if (EmialAddressExist != null)
                return (false, "The csutomer with the EmialAddress Exist", null);
            //perform insert 
            data.DateCreated = DateTime.Now;
            data.DateModified = DateTime.Now;
            //data.CreatedBy = request.CreatedBy;
            data.status = true;
            data.FormId = DateTime.Now.Ticks.ToString();
           var level= await applicationDb.ApprovalLevel.Include(a=>a.Group).Where(a => a.LevelName== "Level1" ).FirstOrDefaultAsync();
            data.LevelName = level.LevelName;
            data.levelId = level.Id;
            await applicationDb.Data.AddAsync(data, cancellationToken);

            await applicationDb.SaveChangesAsync(cancellationToken);
            CreateCustomerDataResponse createNew = new CreateCustomerDataResponse();
            createNew.Id = data.Id;
            createNew.Name = data.FormId;
            // mapper can be used here
            var response = mapper.Map<CreateCustomerDataResponse>(createNew);
            // return response object 
            response.Id = data.Id;
            response.Name = $"{data.FormId}.    CustomerName: {data.firstName} {data.lastName}";
            //Update Log db
            var getuser = await userManager.FindByNameAsync(request.CreatedBy);
            var getroles =await userManager.GetRolesAsync(getuser);
            //var getGrup = roleManager.FindByIdAsync(getroles.Result);
           // map.ApprovalGroupName = getroles.Result.ToArray();
            ApprovalLogResponse logResponse = new ApprovalLogResponse();
            data.CreatedBy = getuser.UserName;
            logResponse.ApprovalGroupName = getroles.ToArray();
            logResponse.ApprovalLevelName = data.LevelName;
            logResponse.ApprovedBy = data.CreatedBy;
            logResponse.Data = data;
            logResponse.DataId = data.Id;
            logResponse.FormId = data.FormId;
            logResponse.Status = true;
            logResponse.UserId = data.UserId;
            logResponse.Reason = $"Newly Created by {data.CreatedBy}";




           await getpassword.LogToDb(logResponse, cancellationToken);

            var level2 = await applicationDb.ApprovalLevel.Include(a => a.Group).Where(a => a.LevelName=="Level2").FirstOrDefaultAsync();

            data.levelId = level2.Id;
            data.LevelName = level2.LevelName;

             applicationDb.Data.Update(data);

            var usersinroles=  userManager.GetUsersInRoleAsync(level2.Group.GroupName).Result.Select(a=>a.Email).ToList();
            NotificationMessage notificationMessage = new NotificationMessage();
            notificationMessage.Data = data;
            notificationMessage.UserEmailAdd = usersinroles;
            notificationMessage.NotificationType = NotificationType.Data;
            notificationMessage.NotificationActionType = NotificationActionType.DataCreated;


            await mediator.Publish(notificationMessage, cancellationToken);
            //goto Group to get Group Email
            //Send mail


            return (true, "Created successfully", response);
        }
    }
   
}
