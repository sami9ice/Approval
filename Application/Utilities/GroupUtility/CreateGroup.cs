using Application.Interfaces;
using Application.Request.Group;
using Application.Response.Group;
using AutoMapper;
using Domain.Entities;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.GroupUtility
{
    public class CreateGroup : IRequestHandler<CreateGroupRequest, (bool Succeed, string Message, CreateGroupResponse Response)>
    {

        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly RoleManager<Group> roleManager;

        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public CreateGroup(IApplicationDbContext applicationDb, IMapper mapper, RoleManager<Group> roleManager/*, IGeneratorService generator*/)

        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.roleManager = roleManager;
            // this.generator = generator;

        }


        public async Task<(bool Succeed, string Message, CreateGroupResponse Response)> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            var data = mapper.Map<Group>(request);
            if (request.ApprovalLevelId == null)
                return (false, "Group must be assigned to a level", null);
            var NameExist = await roleManager.FindByNameAsync(request.GroupName);
            if (NameExist != null)
                return (false, "The Group Name Already Exist", null);
            var EmialExist = await roleManager.Roles.FirstOrDefaultAsync(x => x.GroupEmail.Equals(request.GroupEmail), cancellationToken);
            if (EmialExist != null)
                return (false, "The Group Email Already Exist", null);
            var Level = await roleManager.Roles.FirstOrDefaultAsync(x => x.ApprovalLevelId.Equals(request.ApprovalLevelId), cancellationToken);
            if (Level != null)
                return (false, "This level has already been assigned to a group", null);
            //perform insert 
            data.DateCreated = DateTime.Now;
            data.DateModified = DateTime.Now;
            data.CreatedBy = "Test";
            data.Status = true;
            data.GroupName = data.GroupName;
            data.Name = data.GroupName;
            data.GroupEmail = data.GroupEmail;
            data.ApprovalLevelId = request.ApprovalLevelId;


            await roleManager.CreateAsync(data);

            //await applicationDb.SaveChangesAsync(cancellationToken);
            CreateGroupResponse response = new CreateGroupResponse();
            response.Id = data.Id;
           // Next is to map group with level

            return (true, "Group Created successfully", response);
        }
    }
}
