using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public  interface IApplicationDbContext
    {
        DbSet<Data> Data { get; set; }
        // DbSet<User> User { get; set; }
         DbSet<UserGroup> UserGroups { get; set; }
        // DbSet<Group> Group { get; set; }
         DbSet<ApprovalLevel> ApprovalLevel { get; set; }
         DbSet<ApprovalLog> ApprovalLog { get; set; }
        DbSet<MailConfigurations> MailConfigurations { get; set; }
        DbSet<NotificationMessages> NotificationMessages { get; set; }
        DbSet<NotificationMessageTemplate> NotificationMessageTemplates { get; set; }
        DbSet<NotificationReceiver> NotificationReceivers { get; set; }
        DbSet<Configurations> Configuration { get; set; }




        Task<int> SaveChangesAsync(CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

    }
}
