using Application.Interfaces;
using Application.Response.ApprovalLog;
using AutoMapper;
using Domain.Entities;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Passwordgenerator
{
    public class GeneratePasword : IGeneratePassword
    {
        private readonly UserManager<User> userManager;
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;



        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateUser"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="applicationDbContext"></param>
        public GeneratePasword(UserManager<User> userManager, IMapper mapper, IApplicationDbContext applicationDb)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.applicationDb = applicationDb;

        }

        public async Task<string> CreateRandomPassword()
        {
            int length = 15;
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public async Task<string> GeneratePassword()
        {
            var options = userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

        public async Task<bool> SendMailForCreateUser(string ApprovedBy, string Email, string body)
        {
            bool send = false;
            try
            {
                string Password = "Billing123";
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("no-reply@cyberspace.net.ng", Password);
                MailMessage mm = new MailMessage("no-reply@cyberspace.net.ng", Email, ApprovedBy, body);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.Body = body;
                mm.IsBodyHtml = true;

                client.Send(mm);
                send = true;
                return send;
            }
            catch (Exception ex)
            {
                return send = false;
            }
        }

        public async Task<bool> SendMail(string FormId, string ApprovedBy, string Email, string body)
        {
            bool send = false;
            try
            {
                string Password = "Billing123";
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("no-reply@cyberspace.net.ng", Password);
                MailMessage mm = new MailMessage("no-reply@cyberspace.net.ng", Email, ApprovedBy, body);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.Body = body;
                mm.IsBodyHtml = true;

                client.Send(mm);
                send = true;
                return send;
            }
            catch
            {
                return send = false;
            }
        }


        public async Task<bool> LogToDb(ApprovalLogResponse logResponse, CancellationToken cancellationToken)
        {
            //var map = mapper.Map<ApprovalLog>(logResponse);
            var map = new ApprovalLog
            {
                ApprovalGroupName = logResponse.ApprovalGroupName,
                ApprovalLevelName = logResponse.ApprovalLevelName,
                ApprovedBy = logResponse.ApprovedBy,
                Datas = logResponse.Data,
                DataId = logResponse.DataId,
                FormId = logResponse.FormId,
                Status = logResponse.Status,
                UserId = logResponse.UserId,
                Reason = logResponse.Reason
            };
            //get Groupbname and level
            var getuser = await userManager.FindByIdAsync(map.UserId);
            var getroles = await userManager.GetRolesAsync(getuser);
            //var getGrup = roleManager.FindByIdAsync(getroles.Result);
            map.ApprovalGroupName = getroles.ToArray();
          

            await applicationDb.ApprovalLog.AddAsync(map, cancellationToken);
            await applicationDb.SaveChangesAsync(cancellationToken);

            return true;
        }



    }
}
