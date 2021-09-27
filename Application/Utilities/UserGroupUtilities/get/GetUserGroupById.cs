using Application.Features.ApprovalFeatures.Utilities.DTOs;
using Application.Interfaces;
using Application.Request.UserGroupMappingRequest;
using Application.Response.UserGroupResponse;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
   public class GetUserGroupById : IRequestHandler<GetUserGroupByIdRequest, GetUserGroupByIdResponse>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public GetUserGroupById(IApplicationDbContext applicationDb, IMapper mapper, UserManager<User> userManager, RoleManager<Group> roleManager)

        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            // this.generator = generator;

        }



        //public Task<GetUserByIdResponse[]> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<GetUserGroupByIdResponse> Handle(GetUserGroupByIdRequest request, CancellationToken cancellationToken)
        {
            return await applicationDb.UserGroups.Include(x => x.group).Include(a => a.User).Where(d => d.IsDeleted == false && d.Id==request.Id).OrderByDescending(a => a.DateCreated)
              .Select(a => new GetUserGroupByIdResponse
              {
                  Id = a.Id,

                  GroupdId = a.GroupdId,
                  UserId = a.UserId,
                 User = userManager.Users.Where(s => s.Id == a.UserId).ToArray().Select(y => new UserDTO { Id = a.User.Id, Email = a.User.Email, UserName = a.User.UserName }).ToArray(),
                    // group = new GroupDTO { Id=a.group.Id,Name=a.group.Name,GroupEmail=a.group.GroupEmail} 
                    group = roleManager.Roles.Where(s => s.Id == a.GroupdId).ToArray().Select(y => new GroupDTO { Id = y.Id, Name = y.Name, GroupEmail = y.GroupEmail }).ToArray(),

                    // group=  userManager.GetRolesAsync(a.User).Result.ToList()
                }).FirstOrDefaultAsync();


            //return await applicationDb.UserGroups.Where(d => d.IsDeleted == false)
            //     .ProjectTo<GetUserGroupByIdResponse>(mapper.ConfigurationProvider)
            //     .FirstOrDefaultAsync(x => x.Id.Equals(request.Id),
            //         cancellationToken);
           


        }

    }
}
