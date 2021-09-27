using Application.Interfaces;
using Application.Request.CustomerData;
using Application.Response.CustomerData;
using AutoMapper;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.CustomerDataUtilities
{
    public class UpdateCustomerInfo : IRequestHandler<UpdateCustomerInfoRequest, (bool Succeed, string Message, UpdateCustomerInfoResponse Response)>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;


        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="applicationDbContext"></param>
        public UpdateCustomerInfo(IMapper mapper,UserManager<User> userManager, RoleManager<Group> roleManager,
            IApplicationDbContext applicationDbContext, IMediator mediator, IHttpContextAccessor contextAccessor
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationDbContext = applicationDbContext;
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
        }

        public async Task<(bool Succeed, string Message, UpdateCustomerInfoResponse Response)> Handle(UpdateCustomerInfoRequest request, CancellationToken cancellationToken)
        {
            var DataInfo = await applicationDbContext.Data.FindAsync(request.FormId);


            if (DataInfo.FormId == request.FormId)
            {
                if (request.status != null) DataInfo.status = request.status;
                if (request.ICCID != null) DataInfo.ICCID = request.ICCID;
                if (request.firstName != null) DataInfo.firstName = request.firstName;
                if (request.lastName != null) DataInfo.lastName = request.lastName;
                if (request.EmialAddress != null) DataInfo.EmialAddress = request.EmialAddress;
                if (request.CustomerId != null) DataInfo.CustomerId = request.CustomerId;


                await applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return (false, $"The Customer with form id {request.FormId} does not exist", null);
            }

            var response = mapper.Map<UpdateCustomerInfoResponse>(request);


            return (true, "Customer edited successfully", response);
           
        }
    }
}
