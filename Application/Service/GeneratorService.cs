using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace Application.Service
{


    /// <summary>
    /// 
    /// </summary>
    public class GeneratorService : IGeneratorService
    {
        private readonly IApplicationDbContext applicationDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorService"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public GeneratorService(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

       public bool SendMailForCreateUser(string ApprovedBy, string Email, string body)
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

        public bool SendMail(string FormId, string ApprovedBy, string Email, string body)
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





        /// <summary>
        /// Generates the asset identifier.
        /// </summary>
        /// <returns></returns>
        //public async Task<string> GenerateAssetId()
        //{
        //    Settings settings = await applicationDbContext.Settings.FirstOrDefaultAsync();
        //    Counter lastNumber = await applicationDbContext.Counters.FirstOrDefaultAsync();
        //    string uniqueId;
        //    if (lastNumber != null)
        //    {
        //        lastNumber.LastNumber++;
        //        uniqueId = $"{settings?.AssetIdPrefix}-{lastNumber.LastNumber:D8}";


        //    }
        //    else
        //    {
        //        await applicationDbContext.Counters.AddAsync(new Counter() { LastNumber = 1, Id = Guid.NewGuid().ToString() });
        //        uniqueId = $"{settings?.AssetIdPrefix}-{1:D8}";
        //    }

        //    await applicationDbContext.SaveChangesAsync();

        //    return uniqueId;
        //}
        /// <summary>
        /// configuration code
        /// </summary>
        /// <param name="initial"></param>
        /// <returns></returns>
        //public async Task<string> GenerateId(string initial)
        //{
        //    Counter lastNumber = await applicationDbContext.Counters.FirstOrDefaultAsync();
        //    string uniqueId;
        //    //takes the first 3 letters of configuration code
        //    var ConfigCode = initial.Substring(0, 3).ToUpper();
        //    if (lastNumber != null)
        //    {
        //        lastNumber.LastNumber++;
        //        uniqueId = $"{ConfigCode}-{lastNumber.LastNumber:D8}";


        //    }
        //    else
        //    {
        //        await applicationDbContext.Counters.AddAsync(new Counter() { LastNumber = 1, Id = Guid.NewGuid().ToString() });
        //        uniqueId = $"{ConfigCode}-{1:D8}";
        //    }

        //    await applicationDbContext.SaveChangesAsync();

        //    return uniqueId;
        //}
    }
}