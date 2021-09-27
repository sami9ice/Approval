using Application.Interfaces;
using Application.Request.Group;
using Application.Response.Group;
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

namespace Application.Utilities.GroupUtility
{
   public class GetGroupById : IRequestHandler<GetGroupByIdRequest, GetGroupByIdResponse>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public GetGroupById(IApplicationDbContext applicationDb, IMapper mapper, UserManager<User> userManager, RoleManager<Group> roleManager)

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


        public async Task<GetGroupByIdResponse> Handle(GetGroupByIdRequest request, CancellationToken cancellationToken)
        {

                return await roleManager.Roles.Where(d=> d.Id==request.Id)
                 .ProjectTo<GetGroupByIdResponse>(mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(x => x.Status.Equals(true),
                 cancellationToken);
        }

    }
}
