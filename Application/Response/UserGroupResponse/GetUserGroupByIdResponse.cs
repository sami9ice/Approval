using Application.Features.ApprovalFeatures.Utilities.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.UserGroupResponse
{
    public  class GetUserGroupByIdResponse : IMapFrom<Domain.Entities.UserGroup>
    {
        public string Id { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public UserDTO[] User { get; set; }
        public string GroupdId { get; set; }
        public GroupDTO[] group { get; set; }
    }
}
