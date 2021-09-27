using Application.Interfaces;
using Application.Request.ApprovalLog;
using Application.Response.ApprovalLog;
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

namespace Application.Utilities.Log
{
   public class GetLog : IRequestHandler<GetApprovalLog, ApprovalLogResponse>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public GetLog(IApplicationDbContext applicationDb, IMapper mapper, UserManager<User> userManager, RoleManager<Group> roleManager)

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


        public async Task<ApprovalLogResponse> Handle(GetApprovalLog request, CancellationToken cancellationToken)
        {

            return await applicationDb.ApprovalLog.Where(d => d.IsDeleted == false)
                 .ProjectTo<ApprovalLogResponse>(mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(x => x.FormId.Equals(request.FormId),
                     cancellationToken);
        }

    }
}
