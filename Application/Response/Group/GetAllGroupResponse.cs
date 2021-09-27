using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Group
{
   public class GetAllGroupResponse
    {
        public string Id { get; set; }
        public string GroupName { get; set; }
        public string GroupEmail { get; set; }
        public string LevelId { get; set; }
        public string LevelName { get; set; }
    }
}
