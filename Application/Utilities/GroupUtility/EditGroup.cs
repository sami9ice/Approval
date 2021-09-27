using Application.Interfaces;
using Application.Request.Group;
using Application.Response.Group;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.GroupUtility
{
  public  class EditGroup : IRequestHandler<EditGroupRequest, (bool succeed, string message, EditGroupResponse response)>
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
        public EditGroup(UserManager<User> userManager, RoleManager<Group> roleManager,
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
        public async Task<(bool succeed, string message, EditGroupResponse response)> Handle(EditGroupRequest request, CancellationToken cancellationToken)
        {
            //var location = await applicationDbContext.UserGroups.FindAsync(request.LocationId);
            //if (location == null) return (false, "Location cannot be found", null);

           // var currentUser = contextAccessor?.HttpContext?.User?.Identity?.Name;
            var existingUser = await roleManager.FindByIdAsync(request.GroupId);
            if (existingUser == null)
            {
                return (false, "User does not exist", null);
            }
            if (!string.IsNullOrEmpty(request.GroupName)) existingUser.GroupName = request.GroupName;
            if (!string.IsNullOrEmpty(request.GroupEmail)) existingUser.GroupEmail = request.GroupEmail;
            if (!string.IsNullOrEmpty(request.ApprovalLevelId))
            {
                var check = roleManager.Roles.Where(a => a.ApprovalLevelId == request.ApprovalLevelId).Count();
                if(check>0) return (false, "Approval Level Already Exist For Another Group", null);
                existingUser.ApprovalLevelId = request.ApprovalLevelId;
            } 
    
                  existingUser.Status = request.Status;



            var userUpdated = await roleManager.UpdateAsync(existingUser);

            if (!userUpdated.Succeeded)
                return (false, string.Join(",", userUpdated.Errors.Select(x => x.Description)), null);


            /// send mail to user
            //end
            return (true, "Group edited successfully", new EditGroupResponse
            {
                Id = existingUser.Id,
                GroupEmail = existingUser.GroupEmail,
                GroupName = existingUser.GroupName,
                LevelId = existingUser.ApprovalLevelId,
                //LevelName = existingUser.level.LevelName,
               
            });
        }
    }
}
