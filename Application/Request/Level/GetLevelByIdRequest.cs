using Application.Response.Level;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Level
{
    public   class GetLevelByIdRequest: IRequest<GetLevelByIdResponse>
    {
        public string Id { get; set; }
       
    }
}
