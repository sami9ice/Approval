using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Data: BaseEntity
    {
       // public string Id { get; set; } = Guid.NewGuid().ToString();
        public string  firstName { get; set; }
        public string lastName { get; set; }
        public string FormId { get; set; }
        public string PhoneNo { get; set; }
        public string EmialAddress { get; set; }
        public string CustomerId { get; set; }
        public string NotificationMethod { get; set; }
        public string AccountCategory { get; set; }
        public string AccountType { get; set; }
        public string ProductName { get; set; }
        public string Cost { get; set; }
        public string BillingCycle { get; set; }
        public string RenewalPattern { get; set; }
        public string AcctManagergName { get; set; }
        public string AcctManagergPhoneNo { get; set; }
        public string ICCID { get; set; }
        public string MDN { get; set; }
        public string UserId { get; set; }
       
        public string levelId { get; set; }

        public string LevelName { get; set; }

         public ApprovalLevel level { get; set; }


        public DateTime DateRaised { get; set; }
        public bool status { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime dateCreated { get; set; }





    }
}
