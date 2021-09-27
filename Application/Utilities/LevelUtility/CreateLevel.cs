using Application.Interfaces;
using Application.Request.Level;
using Application.Response.Level;
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

namespace Application.Utilities.LevelUtility
{
   public class CreateLevel : IRequestHandler<CreateLevelRequest, (bool Succeed, string Message, CreateLevelResponse Response)>
    {

        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly RoleManager<Group> roleManager;

        // private readonly IGeneratorService generator;


        /// <param name="applicationDb"></param>
        public CreateLevel(IApplicationDbContext applicationDb, IMapper mapper, RoleManager<Group> roleManager/*, IGeneratorService generator*/)

        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.roleManager = roleManager;
            // this.generator = generator;

        }


        public async Task<(bool Succeed, string Message, CreateLevelResponse Response)> Handle(CreateLevelRequest request, CancellationToken cancellationToken)
        {
            var data = mapper.Map<ApprovalLevel>(request);

            var NameExist = await applicationDb.ApprovalLevel.Where(a => a.LevelName == request.LevelName).FirstOrDefaultAsync() ;
            if (NameExist != null)
                return (false, "The Level Name Already Exist", null);
           
            //perform insert 
            data.DateCreated = DateTime.Now;
            data.DateModified = DateTime.Now;
            data.CreatedBy = request.CreatedBy;
            data.status = true;
            data.LevelName = request.LevelName;




            await  applicationDb.ApprovalLevel.AddAsync(data,cancellationToken);

            await applicationDb.SaveChangesAsync(cancellationToken);
            CreateLevelResponse response = new CreateLevelResponse();
            response.Id = data.Id;
            response.LevelName = data.LevelName;
            response.LevelStatus = data.status;
            
            // Next is to map group with level

            return (true, "Level Created successfully", response);
        }
    }
}
