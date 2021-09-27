using Application.Features.ApprovalFeatures.Utilities.DTOs;
using Application.Interfaces;
using Application.Request.UserGroupMappingRequest;
using Application.Response.UserGroupResponse;
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

namespace Application.Utilities.UserGroupUtilities.get
{
    public class GetAllUserGroup : IRequestHandler<GetAllUserGroupRequest, GetAllUserGroupResponse[]>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public GetAllUserGroup(IApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<Group> roleManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public async Task<GetAllUserGroupResponse[]> Handle(GetAllUserGroupRequest request, CancellationToken cancellationToken)
        {

            return await applicationDbContext.UserGroups.Include(x => x.group).Include(a => a.User).Where(d => d.IsDeleted == false).OrderByDescending(a => a.DateCreated)
                .Select(a => new GetAllUserGroupResponse
                {
                    Id = a.Id,

                    GroupId = a.GroupdId,
                    UserId = a.UserId,
                    user = userManager.Users.Where(s => s.Id == a.GroupdId).ToArray().Select(y => new UserDTO { Id = a.User.Id, Email = a.User.Email, UserName = a.User.UserName }).ToArray(),
                    // group = new GroupDTO { Id=a.group.Id,Name=a.group.Name,GroupEmail=a.group.GroupEmail} 
                    groups = roleManager.Roles.Where(s => s.Id == a.GroupdId).ToArray().Select(y => new GroupDTO { Id = y.Id, Name = y.Name, GroupEmail = y.GroupEmail }).ToArray(),

                    // group=  userManager.GetRolesAsync(a.User).Result.ToList()
                }).ToArrayAsync(cancellationToken);
            //var user = (from u in applicationDbContext.UserGroups

            //            where !u.IsDeleted



            //            orderby u.DateCreated descending
            //            select new GetAllUserGroupResponse
            //            {
            //                Id = u.Id,

            //                GroupId = u.GroupdId,
            //                UserId = u.UserId,
            //                //group= 

            //            });

            //var user = (from u in applicationDbContext.UserGroups

            //            where !u.IsDeleted
            //           join s in userManager.Users on u.UserId equals s.Id
            //            join x in roleManager.Roles on u.GroupdId equals x.Id


            //            orderby u.DateCreated descending
            //            select new GetAllUserGroupResponse
            //            {
            //                Id = u.Id,

            //                GroupId = u.GroupdId,
            //                UserId = u.UserId,
            //                user= new UserDTO { Id=s.Id,UserName=s.UserName},
            //               //group= 

            //            });



            //return await usergroup.ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
