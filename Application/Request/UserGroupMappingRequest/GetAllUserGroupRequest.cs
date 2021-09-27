using Application.Response.UserGroupResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.UserGroupMappingRequest
{
   public class GetAllUserGroupRequest: IRequest<GetAllUserGroupResponse[]>
    {
    }
}
