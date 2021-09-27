using Application.Interfaces;
using Application.Request.ApprovalLog;
using Application.Request.User;
using Application.Response.User;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.Log
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{GetUsersRequest, CreateUserResponse[]}" />
    public class GetLogsQueryHandler : IRequestHandler<GetLogRequest, GetlogResponse>
    {

        private readonly IApplicationDbContext applicationDbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;
        private readonly IMapper mapper;




        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public GetLogsQueryHandler(IMapper mapper,IApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<Group> roleManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
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
       

        public async Task<GetlogResponse> Handle(GetLogRequest request, CancellationToken cancellationToken)
        {
            return  applicationDbContext.ApprovalLog.Where(a=>a.FormId!=null)
                .ProjectTo<GetlogResponse>(mapper.ConfigurationProvider).FirstOrDefault()
                 ;

        }
    }
}