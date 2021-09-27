using Application.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Response.User
{
    /// <summary>
    ///
    /// </summary>
    public class GetUserByIdResponse: IMapFrom<Domain.User.User>
    {
        public string Id { get; set; }

        public string Fullname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        // public string customerId { get; set; }
        public string PIN { get; set; }
        public string Department { get; set; }


        public string Signature { get; set; }
        //Pending or Reject Or Accept
        public string status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool ComfirmPin { get; set; }

        public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date Modified.
        /// </summary>
        /// <value>
        /// The date Modified.
        /// </value>
        public DateTimeOffset DateModified { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        public string DeletedBy { get; set; }
        public DateTimeOffset DateDeleted { get; set; }
        public string GroupId { get; set; }



    }
}