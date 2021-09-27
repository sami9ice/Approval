using Application.Interfaces;
using Application.Response.Level;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Level
{
   public class CreateLevelRequest : IRequest<(bool Succeed, string Message, CreateLevelResponse Response)>, IMapFrom<Domain.Entities.ApprovalLevel>
    {
        public string LevelName { get; set; }
        public string CreatedBy { get; set; }

    }
}
