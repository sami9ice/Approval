using Application.Response.ApprovalLog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.ApprovalLog
{
   public class GetApprovalLog: IRequest<ApprovalLogResponse>
    {
        public string FormId { get; set; }
    }
}
