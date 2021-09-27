using Application.Response.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Group
{
   public class GetGroupByIdRequest: IRequest<GetGroupByIdResponse>
    {
        public string Id { get; set; }

    }
}
