using Application.FeaturesNotification.Notifications;
using Application.Interfaces;
using Application.Request.User;
using Application.Response.User;
using Application.Utilities.UtilitiesFeatures;
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

namespace Application.Utilities.UseUtility
{
    public class ResetPassword : IRequestHandler<ForgotPasswordRequest, (bool succeed, string message)>
    {

        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;
        private readonly IMediator mediator;
        private readonly IGeneratePassword getpassword;
        private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public ResetPassword(IGeneratePassword getpassword,IApplicationDbContext applicationDb, IMapper mapper, RoleManager<Group> roleManager, UserManager<User> userManager, IMediator mediator)

        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.userManager = userManager;
            this.mediator = mediator;
            this.roleManager = roleManager;
            this.getpassword = getpassword;

            this.generator = generator;

        }


        public async Task<(bool succeed, string message)> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {



            var EmialAddressExist = await userManager.FindByEmailAsync(request.Email);

            if (EmialAddressExist == null)
                return (false, "This Email Does Not Exist");


            //var CheckGroup=  await roleManager.FindByIdAsync(user.GroupId);

            //if (CheckGroup == null)
            //    return (false, "The group selected does not exist", null);



            //perform insert 
          //  EmialAddressExist.DateCreated = DateTime.Now;
            EmialAddressExist.DateModified = DateTime.Now;
            //  user.CreatedBy = "Test";
            EmialAddressExist.status = "Pending";
            EmialAddressExist.EmailConfirmed =false ;
            EmialAddressExist.ComfirmPin = false;
            EmialAddressExist.PIN = await getpassword.CreateRandomPassword();


            // user.PIN = await getpassword.GeneratePassword();
            var password = EmialAddressExist.PIN;
            //byte[] encData_byte = new byte[user.PIN.Length];
            //encData_byte = System.Text.Encoding.UTF8.GetBytes(user.PIN);
            //string encodedData = Convert.ToBase64String(encData_byte);



            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(EmialAddressExist.PIN);
            var encodedData = System.Convert.ToBase64String(plainTextBytes);
            EmialAddressExist.PIN = encodedData.ToString();
            //    
            await userManager.UpdateAsync(EmialAddressExist);
            //add Groupmto userGroup table
            //await userManager.AddToRoleAsync(user, CheckGroup.GroupName);
            //await userManager.Users.sa(cancellationToken);
            CreateUserResponse response = new CreateUserResponse();
            response.Id = EmialAddressExist.Id;
            response.firstName = EmialAddressExist.FirstName;
            response.lastName = EmialAddressExist.LastName;
            response.emialAddress = EmialAddressExist.Email;
            response.UserName = EmialAddressExist.UserName;
           // response.groupId = user.GroupId;
            response.Department = EmialAddressExist.Department;
            response.PhoneNo = EmialAddressExist.PhoneNo;
            NotificationMessage notificationMessage = new NotificationMessage();
            notificationMessage.User = EmialAddressExist;
            notificationMessage.NotificationType = NotificationType.ResetPassword;
            notificationMessage.NotificationActionType = NotificationActionType.PasswordReset;
         /*   string Status=*/   await mediator.Publish(notificationMessage, cancellationToken);
            //StringBuilder SbBody = new StringBuilder();
            //SbBody.Append("Name - " + user.UserName + "<br/>");
            //SbBody.Append("Group - " + CheckGroup.GroupName + "<br/>");
            //SbBody.Append("Email - " + user.Email + "<br/>");
            //SbBody.Append("CreatedBy - " + user.CreatedBy + "<br/>");
            //SbBody.Append("PIN - " + user.PIN + "<br/>");
            //SbBody.Append("Login with your user name and password in this link - https://localhost:44305/api/v1/UserAccount/ResetPassword < br/>");

            //SbBody.Append("<hr/>");
            //getpassword.SendMailForCreateUser(user.CreatedBy, user.Email, SbBody.ToString());
            //Send mail pin generated to user for them to Reset
            //reset updates data.EmailConfirmed=true and Pin
            //send mail(User Info) to ic&A to approval
           // if(Status==true) return (true, "A mail has been sent for you to reset your PIN");

            return (true, "A mail has been sent for you to reset your PIN");
        }
    }
}
