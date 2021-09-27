using Application.Features.ApprovalFeatures.Utilities.DTOs;
using Application.Interfaces;
using Application.Request.CreateUserGroupMappingRequest;

namespace Application.Response.UserGroupResponse
{
    /// <summary>
    ///
    /// </summary>
    public class CreateUserGroupResponse : IMapFrom<CreateUserGroupMappingRequest>
    {
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
       // public string RoleId { get; set; }
        public string GroupId { get; set; }


        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
       // public GroupDTO group { get; set; }

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
      //  public UserDTO User { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
       
    }
}