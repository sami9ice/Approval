using Application.Common.Passwordgenerator;
using Application.FeaturesNotification.Notifications;
using Application.Interfaces;
using Application.Response.User;
using AutoMapper;
using Domain.Common;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.UserUtility
{
    public class ForgotPassword: IRequestHandler<ResetPasswordRequestCommand, (bool succeed, string message)>
    {
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        private readonly ILogger<ForgotPassword> logger;
       private readonly IGeneratePassword getpassword;


        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ForgotPassword(IMapper mapper,UserManager<User> userManager, IMediator mediator, ILogger<ForgotPassword> logger, IGeneratePassword getpassword)
        {
            this.userManager = userManager;
            this.mediator = mediator;
            this.logger = logger;
            this.mapper = mapper;
            this.getpassword = getpassword;
        }
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<(bool succeed, string message)> Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            //var rnd = new Random();
            // var rnd =  await getpassword.GeneratePassword();

            var NewPin = request.PIN;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(request.PIN);
            var encodedData = System.Convert.ToBase64String(plainTextBytes);

            request.PIN = encodedData;
            //var plainTextBytess = System.Text.Encoding.UTF8.GetBytes(request.OldPin);
            //var encodedDatas = System.Convert.ToBase64String(plainTextBytes);

            //request.OldPin = encodedDatas;

           




            var PINameExist = await userManager.FindByNameAsync(request.PIN);

            if (PINameExist != null)
                return (false, "This Pin/Password cannot be used");

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                logger.LogInformation($"User not found");
                return (false, "The user cannot be found");
            }
            if(user.PIN==request.PIN) return (false, "This Pin/Password cannot be used");
            var base64EncodedBytes = System.Convert.FromBase64String(user.PIN);
            var decodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var checkPin = decodedData;

            
            if (checkPin != request.OldPin)
            {
                return (false, "The old password is not correct");
            }
            if (request.OldPin == NewPin)
            {
                //logger.LogInformation($"User not found");
                return (false, "The new password cannot be the same as the old password");
            }

            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            //var password = rnd.Next(10000, 1000000);
            //  var password = rnd;
            user.PIN = request.PIN;
            user.ComfirmPin = true;
            user.EmailConfirmed = true;


            logger.LogInformation($"Password token generated {resetToken}");
            logger.LogInformation($"Password generated { NewPin}");

            var passCode = await userManager.ResetPasswordAsync(user, resetToken, NewPin);

            if (passCode.Succeeded)
            {
                // send mail
                //Notification Message
                //NotificationMessage notificationMessage = new NotificationMessage
                //{
                //    User = user,
                //    NotificationType = NotificationType.Authentication,
                //    NotificationActionType = NotificationActionType.PasswordReset,
                //    NewUserPassword = request.PIN
                //};
                //await mediator.Publish(notificationMessage, cancellationToken);



                return (true, "You have successfully reset your password. login with your username and password ");
            }

            return (false, string.Join(" ", passCode.Errors.Select(x => x.Description)));
        }

    }
}