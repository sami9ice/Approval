using Application.Interfaces;
using Application.Request.Group;
using Application.Request.User;
using Application.Response.User;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.UseUtility
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{GetUsersRequest, CreateUserResponse[]}" />
    public class GetUsersQueryHandler : IRequestHandler<GetUsersRequest, GetUserResponse[]>
    {

        private readonly IApplicationDbContext applicationDbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public GetUsersQueryHandler(IApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<Group> roleManager)
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
        public async Task<GetUserResponse[]> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {

                     
            var user = (from u in userManager.Users
                        where !u.IsDeleted
                        orderby u.DateDeleted descending
                        select new GetUserResponse
                        {
                            Id = u.Id,
                            emialAddress = u.Email,
                            firstName = $"{u.FirstName} -> ({u.Email})",
                            lastName = $"{u.LastName} -> ({u.Email})",
                            PhoneNo = u.PhoneNumber,
                            FullName = u.UserName,
                            //GroupId=u.GroupId,
                            GroupbelongedTo = /*(from a in*/ userManager.GetRolesAsync(u).Result.ToArray()
                                          //where l.Id == u.GroupId
                                          //select new GetgroupResponse { GroupId=a}
                               /* ).ToArray()*/,
                            LevelbelongedTo = (from a in roleManager.Roles.Include(a=>a.level) where a.Id==applicationDbContext.UserGroups.Where(a=>a.User.Id==u.Id).Select(a=>a.GroupdId).FirstOrDefault()  select new Level {Name=a.level.LevelName,Id=a.ApprovalLevelId }).ToArray()
                        });


            //var zzz = applicationDbContext.UserGroups.Where(a => a.User.Id == user.FirstOrDefault().Id).Select(a => a.GroupdId).FirstOrDefault();


            return await user.ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}