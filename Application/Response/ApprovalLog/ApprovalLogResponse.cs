using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.ApprovalLog
{
  public  class ApprovalLogResponse
    {
        //public string ApprovedBy { get; set; }
        //public string ApprovalLevelName { get; set; }
        //public string ApprovalGroupName { get; set; }
        //public bool Status { get; set; }
        //public string Reason { get; set; }
        //public string FormId { get; set; }
        //public string DataId { get; set; }
        //public Data Data { get; set; }

        public string ApprovedBy { get; set; }
        public string ApprovalLevelName { get; set; }
        public string[] ApprovalGroupName { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public string FormId { get; set; }
        public string DataId { get; set; }
        public Data Data { get; set; }

        public string UserId { get; set; }

    }
}
