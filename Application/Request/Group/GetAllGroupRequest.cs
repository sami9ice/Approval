using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Group
{
   public class GetAllGroupRequest : IRequest<GetAllGroupResponse[]>
    {
        public string GroupId { get; set; }

        public string GroupName { get; set; }
        public string GroupEmail { get; set; }
        public bool Status { get; set; }

        public string ApprovalLevelId { get; set; }
        public string ApprovalLevelName { get; set; }

    }
}
