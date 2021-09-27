using Application.Interfaces;
using Application.Request.Group;
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

namespace Application.Utilities.GroupUtility
{
    public class GetAllGroup : IRequestHandler<GetAllGroupRequest, GetAllGroupResponse[]>
    {

        private readonly IApplicationDbContext applicationDbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public GetAllGroup(IApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<Group> roleManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
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
        public async Task<GetAllGroupResponse[]> Handle(GetAllGroupRequest request, CancellationToken cancellationToken)
        {


            var group = (from u in roleManager.Roles
                         where !u.IsDeleted
                         orderby u.DateDeleted descending
                         select new GetAllGroupResponse
                         {
                             Id = u.Id,
                             GroupName = u.Name,
                             GroupEmail = u.GroupEmail,
                             LevelId = u.ApprovalLevelId,
                             LevelName = applicationDbContext.ApprovalLevel.Where(a => a.Id == u.ApprovalLevelId).Select(a => a.LevelName).FirstOrDefault()
                         });



            return await group.ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
