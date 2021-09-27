using Application.Interfaces;
using Application.Response.ApprovalLog;
using AutoMapper;
using Domain.Entities;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Service
{
   public class AprovalProcessLog: IApprovalProcessLog
    {

        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;

        /// <param name="applicationDb"></param>
        public AprovalProcessLog(UserManager<User> userManager, RoleManager<Group> roleManager,IApprovalProcessLog Iaproval,IApplicationDbContext applicationDb, IMapper mapper/*, IGeneratorService generator*/)

        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            // this.generator = generator;

        }


        //Task<ApprovalLog> LogToDb(ApprovalLogResponse logResponse, CancellationToken cancellationToken)
        //{
        //    var map = mapper.Map<ApprovalLog>(logResponse);
        //    var Log = applicationDb.ApprovalLog.AddAsync(map, cancellationToken);


        //    return Log;
        //}
      
        public bool LogToDb(ApprovalLogResponse logResponse, CancellationToken cancellationToken)
        {
            var map = mapper.Map<ApprovalLog>(logResponse);

            //get Groupbname and level
            var getuser = userManager.FindByIdAsync(map.Datas.UserId);
            var getroles = userManager.GetRolesAsync(getuser.Result);
            //var getGrup = roleManager.FindByIdAsync(getroles.Result);
            map.ApprovalGroupName = getroles.Result.ToArray();


            var Log = applicationDb.ApprovalLog.AddAsync(map, cancellationToken);

            return true;
        }
    }
}
