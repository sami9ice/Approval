using Application.Response.UserGroupResponse;
using Application.ResponseLevel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.UserGroupMappingRequest
{
   public class EditUserGroupRequest : IRequest<(bool succeed, string message, EditUserGroupResponse userGroupResponse)>
    {
        public string UserId { get; set; }
        public string GroupId { get; set; }

    }
}
