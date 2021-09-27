using Application.Response.Level;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Level
{
   public class GetAllLevelRequest: IRequest<GetAllLevelResponse[]>
    {
        public string LevelId { get; set; }

        public string LevelName { get; set; }
        public bool Status { get; set; }
    }
}
