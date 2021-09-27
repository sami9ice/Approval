using Application.Interfaces;
using Application.Response.UserGroupResponse;
using MediatR;
using System;

namespace Application.Request.CreateUserGroupMappingRequest
{
    /// <summary>
    /// 1.Name (unique)
    /// 2.Website
    /// 3.Description
    /// 4.Email
    /// 5.PhoneNo
    /// 6.ContactPerson
    /// 7. Status(True or false)
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         MediatR.IRequest{(System.Boolean Succeed, System.String Message,
    ///         Application.Response.UserRoleLocationMapping.CreateUserRoleLocationMappingResponse Response)}
    ///     </cref>
    /// </seealso>
    /// <seealso>
    ///     <cref>Application.Interfaces.IMapFrom{Domain.Entities.UserRoleLocationMapping}</cref>
    /// </seealso>
    public class CreateUserGroupMappingRequest : IRequest<(bool Succeed, string Message, CreateUserGroupResponse Response)>, IMapFrom<Domain.Entities.UserGroup>
    {
        /// <summary>
        ///// Gets or sets the Group identifier.
        /// </summary>
        /// <value>
        /// The group identifier.
        /// </value>
        public string GroupdId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date Modified.
        /// </summary>
        /// <value>
        /// The date Modified.
        /// </value>
        public DateTimeOffset DateModified { get; set; }
        //public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
       // public string DeletedBy { get; set; }
        //public DateTimeOffset DateDeleted { get; set; }


    }
}