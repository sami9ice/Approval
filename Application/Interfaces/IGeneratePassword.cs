using Application.Response.ApprovalLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGeneratePassword
    {
        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <returns></returns>
        Task<string> GeneratePassword();
        Task<string> CreateRandomPassword();
        Task<bool> SendMail(string FormId, string ApprovedBy, string Email, string body);
        Task<bool> SendMailForCreateUser(string ApprovedBy, string Email, string body);
       Task< bool> LogToDb(ApprovalLogResponse logResponse, CancellationToken cancellationToken);


    }
}
