using Application.Response.CustomerData;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.CustomerData
{
    public class ApprovalFlowRequest: IRequest<(bool Succeed, string Message, ApprovalFlowResponse)>
    {
        public string FormId { get; set; }
        public string PIN { get; set; }
        public string Reason { get; set; }


    }
}
