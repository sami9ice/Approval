using Application.Interfaces;
using Application.Request.Level;
using Application.Response.Level;
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

namespace Application.Utilities.LevelUtility
{
   public class GetLevelById : IRequestHandler<GetLevelByIdRequest, GetLevelByIdResponse>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public GetLevelById(IApplicationDbContext applicationDb, IMapper mapper, UserManager<User> userManager, RoleManager<Group> roleManager)

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


        public async Task<GetLevelByIdResponse> Handle(GetLevelByIdRequest request, CancellationToken cancellationToken)
        {


            return await applicationDb.ApprovalLevel.Where(d => d.IsDeleted == false)
             .ProjectTo<GetLevelByIdResponse>(mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(x => x.Id.Equals(request.Id),
                 cancellationToken);




        }

       
    }
}
