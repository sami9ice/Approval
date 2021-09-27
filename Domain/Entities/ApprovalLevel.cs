using Domain.Common;
using Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
   public class ApprovalLevel: BaseEntity
    {
       //public int LevelId { get; set; }
        public string LevelName { get; set; }
        public Group Group { get; set; }


        public bool status { get; set; }
    }
}
