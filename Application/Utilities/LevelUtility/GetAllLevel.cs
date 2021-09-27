using Application.Interfaces;
using Application.Request.Level;
using Application.Response.Level;
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
   public class GetAllLevel : IRequestHandler<GetAllLevelRequest, GetAllLevelResponse[]>
    {

        private readonly IApplicationDbContext applicationDbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public GetAllLevel(IApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<Group> roleManager)
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
        public async Task<GetAllLevelResponse[]> Handle(GetAllLevelRequest request, CancellationToken cancellationToken)
        {


            var level = (from u in applicationDbContext.ApprovalLevel
                         where !u.IsDeleted
                         orderby u.DateDeleted descending
                         select new GetAllLevelResponse
                         {
                             Id = u.Id,
                             LevelName = u.LevelName,
                             Status = u.status
                         });



            return await level.ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
