using Application.Request.Group;
using Application.Response.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Request.User
{
    public class CreateUserRequest:IRequest<(bool succeed, string message, CreateUserResponse userResponse)>
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone No is required")]

        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "EmailAddress is required")]

        public string EmialAddress { get; set; }
        // public string customerId { get; set; }
        //public string PIN { get; set; }
        public string Department { get; set; }

        [Required(ErrorMessage = "Signature is required")]
        public string Signature { get; set;}
        //Pending or Reject Or Accept
        //public string status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        //public bool ComfirmPin { get; set; }

        // public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date Modified.
        /// </summary>
        /// <value>
        /// The date Modified.
        /// </value>
        public DateTimeOffset DateModified { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        //public string DeletedBy { get; set; }
       // public string GroupId { get; set; }
        // public Group group { get; set; }

       // public ICollection<CreateGroupRequest> Groups { get; set; }

        //public GroupProfile group { get; set; }

    }
}
