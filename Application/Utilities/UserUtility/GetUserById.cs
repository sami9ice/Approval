using Application.Interfaces;
using Application.Request.User;
using Application.Response.User;
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

namespace Application.Utilities.UserUtility
{
    public class GetUserById : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public GetUserById(IApplicationDbContext applicationDb, IMapper mapper, UserManager<User> userManager, RoleManager<Group> roleManager)

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


        public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {

            if (!string.IsNullOrEmpty(request.Id)){
                return await userManager.Users.Where(d => d.IsDeleted == false)
                 .ProjectTo<GetUserByIdResponse>(mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(x => x.Id.Equals(request.Id),
                     cancellationToken);
            }
            else
            {
                return await userManager.Users.Where(d => d.IsDeleted == false)
             .ProjectTo<GetUserByIdResponse>(mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(x => x.PIN.Equals(request.Pin),
                 cancellationToken);
            }
            
        
        }

    }
}
