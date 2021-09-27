using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response.User
{
   public class ResetPasswordRequestCommand : IRequest<(bool success, string message)>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        public string OldPin { get; set; }
        public string PIN { get; set; }


    }
}
