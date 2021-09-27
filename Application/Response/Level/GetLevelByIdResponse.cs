using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Level
{
   public class GetLevelByIdResponse :  IMapFrom<Domain.Entities.ApprovalLevel>
    {
        public string Id { get; set; }
        public string LevelName { get; set; }
        public bool Status { get; set; }


    }
}
