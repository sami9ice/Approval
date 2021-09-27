using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response.CustomerData
{
   public class GetCustomerDataByFormIDResponse: IMapFrom<Domain.Entities.Data>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string firstName { get; set; }
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
        public byte AcctManagergSignature { get; set; }
        public string AcctManagergId { get; set; }
        public DateTime dateRaised { get; set; }
        public bool status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime dateCreated { get; set; }

    }
}
