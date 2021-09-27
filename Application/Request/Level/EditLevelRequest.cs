using Application.ResponseLevel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Level
{
   public class EditLevelRequest: IRequest<(bool succeed, string message, EditLevelResponse levelResponse)>
    {
        public string Id { get; set; }

        public string LevelName { get; set; }
        public string DoneBy { get; set; }

        public bool Status { get; set; }

    }
}
