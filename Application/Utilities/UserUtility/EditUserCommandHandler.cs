using Application.FeaturesNotification.Notifications;
using Application.Interfaces;
using Application.Request.User;
using Application.Response.User;
using Domain.Common;
using Domain.Entities;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.UserUtility
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         MediatR.IRequestHandler{Application.Request.User.EditUserRequest, (System.Boolean succeed, System.String
    ///         message, Application.Response.User.EditUserResponse response)}
    ///     </cref>
    /// </seealso>
    public class EditUserCommandHandler : IRequestHandler<EditUserRequest, (bool succeed, string message, EditUserResponse response)>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="applicationDbContext"></param>
        public EditUserCommandHandler(UserManager<User> userManager, RoleManager<Group> roleManager,
            IApplicationDbContext applicationDbContext, IMediator mediator, IHttpContextAccessor contextAccessor
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationDbContext = applicationDbContext;
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<(bool succeed, string message, EditUserResponse response)> Handle(EditUserRequest request, CancellationToken cancellationToken)
        {
            //var location = await applicationDbContext.UserGroups.FindAsync(request.LocationId);
            //if (location == null) return (false, "Location cannot be found", null);

            var currentUser = contextAccessor?.HttpContext?.User?.Identity?.Name;
            var existingUser = await userManager.FindByIdAsync(request.Id);
            if (existingUser == null)
            {
                return (false, "User does not exist", null);
            }
            if(!string.IsNullOrEmpty(request.Email)) existingUser.Email = request.Email;
            if (!string.IsNullOrEmpty(request.FullName)) existingUser.Fullname = request.FullName;
            if (!string.IsNullOrEmpty(request.UserName)) existingUser.UserName = request.UserName;
            if (!string.IsNullOrEmpty(request.Phone)) existingUser.PhoneNumber = request.Phone;
            if (!string.IsNullOrEmpty(request.status)) existingUser.status = request.status;



            var userUpdated = await userManager.UpdateAsync(existingUser);

            if (!userUpdated.Succeeded)
                return (false, string.Join(",", userUpdated.Errors.Select(x => x.Description)), null);

            //var existingUserRoles = await userManager.GetRolesAsync(existingUser) as List<string>;
            //var userRoleRemoved = await userManager.RemoveFromRolesAsync(existingUser, existingUserRoles);

            //if (!userRoleRemoved.Succeeded)
            //    return (false, string.Join(",", userRoleRemoved.Errors.Select(x => x.Description)), null);

            //remove user role location mapping
            //var existingUserMappingList = await applicationDbContext.UserGroups
            //          .Where(u => u.User.Email == request.Email
            //          && u.UserId == request.).ToListAsync();
            //if (existingUserMappingList != null && existingUserMappingList?.Count > 0)
            //{
            //    var userMappingToUpdateList = new List<UserRoleLocationMapping>();

            //    foreach (var userLocMapping in existingUserMappingList)
            //    {
            //        userLocMapping.IsDeleted = true;
            //        userMappingToUpdateList.Add(userLocMapping);
            //    }
            //    applicationDbContext.UserRoleLocationMappings.UpdateRange(userMappingToUpdateList);
            //    int updated = await applicationDbContext.SaveChangesAsync();
            //}
            ////end
            foreach (var GroupName in request.Roles)
            {
                if (string.IsNullOrWhiteSpace(GroupName))
                {
                    continue;
                }
                if (await roleManager.RoleExistsAsync(GroupName))
                {
                    var userAddedToNewRole = await userManager.AddToRoleAsync(existingUser, GroupName);
                    //check if maping exist and add if it doesn't
                    var existingUserMapping = applicationDbContext.UserGroups
                        .Where(u => u.group.Name == GroupName && u.User.Email == request.Email).FirstOrDefault();
                    if (existingUserMapping == null)
                    {
                        var userExistingRole = await roleManager.FindByNameAsync(GroupName);
                        // await userManager.AddToRoleAsync(existingUser, GroupName);
                        applicationDbContext.UserGroups.Add(new UserGroup { group = userExistingRole, User = existingUser });
                        await applicationDbContext.SaveChangesAsync(cancellationToken);
                    }
                    //end
                }
                else
                {
                    var userRole = new Group() { Name = GroupName };
                    var newRole = await roleManager.CreateAsync(userRole);
                    if (newRole.Succeeded)
                    {
                        var userAddedToNewRole = await userManager.AddToRoleAsync(existingUser, GroupName);

                        applicationDbContext.UserGroups.Add(new UserGroup {  group = userRole, User = existingUser });
                        await applicationDbContext.SaveChangesAsync(cancellationToken);
                    }
                }
                if (!userUpdated.Succeeded) break;
            }

            if (!userUpdated.Succeeded)
            {
                // delete user
                return (false, string.Join(",", userUpdated.Errors), null);
            }
            //Notification Message
            NotificationMessage notificationMessage = new NotificationMessage();
            notificationMessage.User = existingUser;
            notificationMessage.NotificationType = NotificationType.Configuration;
            notificationMessage.NotificationActionType = NotificationActionType.UserEditedCreated;
            await mediator.Publish(notificationMessage, cancellationToken);

            /// send mail to user
            //end
            return (true, "User edited successfully", new EditUserResponse
            {
                Id = existingUser.Id,
                Email = existingUser.Email,
                FullName = existingUser.Fullname,
                Phone = existingUser.PhoneNumber,
                UserName = existingUser.UserName,
                Group = request.Roles,
                status= request.status
            });
        }
    }
}