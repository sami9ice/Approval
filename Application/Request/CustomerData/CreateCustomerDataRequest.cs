using Application.Interfaces;
using Application.Response.CustomerData;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Request.CustomerData
{
    public class CreateCustomerDataRequest:IRequest<(bool Succeed, string Message, CreateCustomerDataResponse Response)>, IMapFrom<Domain.Entities.Data>
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string FormId { get; set; }
        public string PhoneNo { get; set; }
        public string EmialAddress { get; set; }
        public string NotificationMethod { get; set; }
        public string AccountCategory { get; set; }
        public string AccountType { get; set; }
        public string ProductName { get; set; }
        public string Cost { get; set; }
        public string BillingCycle { get; set; }
        public string RenewalPattern { get; set; }
        public string AcctManagergName { get; set; }
        public string AcctManagergPhoneNo { get; set; }
        public string AcctManagergSignature { get; set; }
        public string ICCID { get; set; }
        public string MDN { get; set; }
        public string CustomerId { get; set; }

        public DateTime dateRaised { get; set; }

        public string CreatedBy { get; set; }
        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date Modified.
        /// </summary>
        /// <value>
        /// The date Modified.
        /// </value>
        public DateTimeOffset DateModified { get; set; }
      //  public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
     //   public string DeletedBy { get; set; }
        //public DateTimeOffset DateDeleted { get; set; }


    }
}
