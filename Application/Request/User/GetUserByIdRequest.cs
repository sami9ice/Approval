using Application.Response.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Request.User
{
   public class GetUserByIdRequest: IRequest<GetUserByIdResponse> 
    {
        public string Id { get; set; }
        public string Pin { get; set; }

    }
}
