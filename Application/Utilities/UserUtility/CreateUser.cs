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
    public class CreateUser : IRequestHandler<CreateUserRequest, (bool Succeed, string Message, CreateUserResponse Response)>
    {

        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;
        private readonly IMediator mediator;
        private readonly IGeneratePassword getpassword;
        private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public CreateUser(IGeneratePassword getpassword,IApplicationDbContext applicationDb, IMapper mapper, RoleManager<Group> roleManager, UserManager<User> userManager, IMediator mediator)

        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.userManager = userManager;
            this.mediator = mediator;
            this.roleManager = roleManager;
            this.getpassword = getpassword;

            this.generator = generator;

        }


        public async Task<(bool Succeed, string Message, CreateUserResponse Response)> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            //var data = mapper.Map<User>(request);

            var user = new User
            {
                Fullname= request.FirstName+"."+request.LastName,
                FirstName=request.FirstName,
                LastName=request.LastName,
                //EmialAddress=request.EmialAddress,
                PhoneNo=request.PhoneNo,
                
               // GroupId=request.GroupId,
               // PIN=request.PIN,
                UserName= request.FirstName + "." + request.LastName,
                Email=request.EmialAddress,
                Signature=request.Signature,
                CreatedBy=request.CreatedBy,
                Department=request.Department,
                PhoneNumber=request.PhoneNo
                  
            };
            //user.Fullname=request.Fullname


           //  var EmialAddressExist = await userManager.Users.FirstOrDefaultAsync(x => x.Email.Equals(request.EmialAddress), cancellationToken);
            var EmialAddressExist = await userManager.FindByEmailAsync(user.Email);

            if (EmialAddressExist != null)
                return (false, "The User Already Exist", null);
            var UserNameExist = await userManager.FindByNameAsync(user.UserName);

            if (UserNameExist != null)
                return (false, "The User Already Exist", null);
            var Signature = await userManager.Users.Where(a=>a.Signature==user.Signature).FirstOrDefaultAsync();

            if (Signature != null)
                return (false, "The User with the Signature Already Exist", null);
            //var CheckGroup=  await roleManager.FindByIdAsync(user.GroupId);

            //if (CheckGroup == null)
            //    return (false, "The group selected does not exist", null);
           


            //perform insert 
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
          //  user.CreatedBy = "Test";
            user.status = "Pending";
            user.EmailConfirmed =false ;
            user.ComfirmPin = false;
            user.PIN = await getpassword.CreateRandomPassword();


            // user.PIN = await getpassword.GeneratePassword();
            var password = user.PIN;
            //byte[] encData_byte = new byte[user.PIN.Length];
            //encData_byte = System.Text.Encoding.UTF8.GetBytes(user.PIN);
            //string encodedData = Convert.ToBase64String(encData_byte);



            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(user.PIN);
            var encodedData = System.Convert.ToBase64String(plainTextBytes);
            user.PIN = encodedData.ToString();

            
            //    
              await userManager.CreateAsync(user);
            //add Groupmto userGroup table

            //await userManager.AddToRoleAsync(user, CheckGroup.GroupName);

            //await userManager.Users.sa(cancellationToken);
            CreateUserResponse response = new CreateUserResponse();
            response.Id = user.Id;
            response.firstName = user.FirstName;
            response.lastName = user.LastName;
            response.emialAddress = user.Email;
            response.UserName = user.UserName;
           // response.groupId = user.GroupId;
            response.Department = user.Department;
            response.PhoneNo = request.PhoneNo;
            NotificationMessage notificationMessage = new NotificationMessage();
            notificationMessage.User = user;
            notificationMessage.NotificationType = NotificationType.Authentication;
            notificationMessage.NotificationActionType = NotificationActionType.UserCreated;
            await mediator.Publish(notificationMessage, cancellationToken);
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

            return (true, "User Created successfully", response);
        }
    }
}
