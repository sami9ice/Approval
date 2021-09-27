using Application.Features.ApprovalFeatures.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.UserGroupResponse
{
   public class GetAllUserGroupResponse
    {
        public string Id { get; set; }
        public string GroupId { get; set; }


        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
      public GroupDTO[] groups { get; set; }
      //  public string[] group { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
       // public UserDTO user { get; set; }
        public UserDTO[] user { get; set; }


        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>

    }
}
