using Application.Interfaces;
using Application.Request.Level;
using Application.ResponseLevel;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.LevelUtility
{
    public class EditLevel : IRequestHandler<EditLevelRequest, (bool succeed, string message, EditLevelResponse response)>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="applicationDbContext"></param>
        public EditLevel(UserManager<User> userManager, RoleManager<Group> roleManager,
            IApplicationDbContext applicationDbContext, IMediator mediator
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationDbContext = applicationDbContext;
            this.mediator = mediator;
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
        public async Task<(bool succeed, string message, EditLevelResponse response)> Handle(EditLevelRequest request, CancellationToken cancellationToken)
        {
            //var location = await applicationDbContext.UserGroups.FindAsync(request.LocationId);
            //if (location == null) return (false, "Location cannot be found", null);

            var existingUser = await applicationDbContext.ApprovalLevel.FirstOrDefaultAsync(a => a.Id == request.Id,cancellationToken);
            if (existingUser == null)
            {
                existingUser = await applicationDbContext.ApprovalLevel.FirstOrDefaultAsync(a => a.LevelName == request.LevelName,cancellationToken);
                if (existingUser == null) return (false, "Level does not exist", null);


            }
            if (!string.IsNullOrEmpty(request.LevelName)) existingUser.LevelName = request.LevelName;
             existingUser.status = request.Status;
             existingUser.ModifiedBy = request.DoneBy;
                 await applicationDbContext.SaveChangesAsync(cancellationToken);


            //var userUpdated =  applicationDbContext.ApprovalLevel.Attach(existingUser);
                             // applicationDbContext.SaveChangesAsync(cancellationToken);

            /// send mail to user
            //end
            return (true, "Level edited successfully", new EditLevelResponse
            {
                Id = existingUser.Id,
                LevelName = existingUser.LevelName,

                Status = request.Status
            });
        }
    }
}
