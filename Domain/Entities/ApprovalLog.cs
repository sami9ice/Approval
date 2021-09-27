using Domain.Common;
using Domain.User;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class ApprovalLog:BaseEntity
    {
        //public int LogId { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovalLevelName { get; set; }
        [NotMapped]
        public string[] ApprovalGroupName { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public string FormId { get; set; }
        public string DataId { get; set; }
        public Data Datas { get; set; }

        public string UserId { get; set; }
        
        //public User User { get; set; }




    }
}
