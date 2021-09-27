using Application.Response.ApprovalLog;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApprovalProcessLog
    {
        bool LogToDb(ApprovalLogResponse logResponse, CancellationToken cancellationToken);
       
    }
}
