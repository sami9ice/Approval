using Domain.Common;
using Domain.User;

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserGroup:BaseEntity
    {

        //public string UserId { get; set; } 
        //public int UserGroupId { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
       public User.User User { get; set; }
        public string GroupdId { get; set; }
       public Group group { get; set; }


        //public string CreatedBy { get; set; }



    }
}
