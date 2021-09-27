using Domain.Entities;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response.User
{
    public class GetlogResponse 
    {
        public string ApprovedBy { get; set; }
        public string ApprovalLevelName { get; set; }
        public string ApprovalGroupName { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public string FormId { get; set; }
        public string DataId { get; set; }
        public Data Data { get; set; }

    }
   

}
