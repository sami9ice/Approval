using Application.Interfaces;
using Application.Request.CreateUserGroupMappingRequest;
using Application.Response.UserGroupResponse;
using AutoMapper;
using Domain.Entities;
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

namespace Application.Utilities.UserGroupUtilities.Post
{
    public class CreateUserGroupMappingPost : IRequestHandler<CreateUserGroupMappingRequest, (bool Succeed, string Message, CreateUserGroupResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public CreateUserGroupMappingPost(IApplicationDbContext applicationDb, IMapper mapper, UserManager<User> userManager,
            RoleManager<Group> roleManager)
        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<(bool Succeed, string Message, CreateUserGroupResponse Response)> Handle(CreateUserGroupMappingRequest request, CancellationToken cancellationToken)
        {
            var userGroupMapping = mapper.Map<UserGroup>(request);
           var CheckUserExist= await userManager.FindByIdAsync(request.UserId);
            if (CheckUserExist == null)
            {
                return (false, "User does not exists", null);
            }
            var CheckgroupExist = await roleManager.FindByIdAsync(request.GroupdId);
            if (CheckgroupExist == null)
            {
                return (false, "Group does not exists", null);
            }

            //check for existing mapping
            var existingUserGroupMapping = await applicationDb.UserGroups
                .FirstOrDefaultAsync(a => a.IsDeleted == false &&
                a.UserId == request.UserId &&
                a.GroupdId == request.GroupdId, cancellationToken
                );
                
            if (existingUserGroupMapping != null)
            {
                return (false, "User Group Mapping already exists", null);
            }

            //perform insert
            await applicationDb.UserGroups.AddAsync(userGroupMapping, cancellationToken);
            await userManager.AddToRoleAsync(CheckUserExist, CheckgroupExist.GroupName);

            await applicationDb.SaveChangesAsync(cancellationToken);
            //Add user to role
            //var user = await userManager.FindByIdAsync(userGroupMapping.UserId);
            // var group = await roleManager.FindByIdAsync(userGroupMapping.GroupdId);
            // var result = await userManager.AddToRoleAsync(user, group.Name);
            //end
            // mapper can be used here
            var response = new CreateUserGroupResponse {Id=userGroupMapping.Id,GroupId=userGroupMapping.GroupdId,UserId=userGroupMapping.UserId };
            //var response = mapper.Map<CreateUserGroupResponse>(request);
            // return response object
            //response.Id = userGroupMapping.Id;
            //send mail to the user
            return (true, "User Group Mapping Created Successfully", response);
        }
    }
}
