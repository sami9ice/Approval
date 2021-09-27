using Application.Interfaces;
using Application.Request.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response.Group
{
   public class CreateGroupResponse: IMapFrom<CreateGroupRequest>
    {
        public string Id { get; set; }

    }
}
