using Application.Response.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Group
{
   public class EditGroupRequest: IRequest<(bool succeed, string message, EditGroupResponse groupResponse)>
    {
        public string GroupId { get; set; } 

        public string GroupName { get; set; }
        public string GroupEmail { get; set; }
        public bool Status { get; set; }

        public string ApprovalLevelId { get; set; }
    }
}
