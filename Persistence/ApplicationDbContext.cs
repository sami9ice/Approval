using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Group, string>, IApplicationDbContext
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<ApplicationDbContext> logger;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            this.contextAccessor = contextAccessor;
            this.logger = logger;

        }


        public DbSet<Data> Data { get; set ; }
       // public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
       // public DbSet<Group> Group{ get; set; }
        public DbSet<ApprovalLevel> ApprovalLevel { get; set; }
        public DbSet<ApprovalLog> ApprovalLog { get; set; }
        public DbSet<MailConfigurations> MailConfigurations { get; set; }
        public DbSet<NotificationMessages> NotificationMessages { get; set; }
        public DbSet<NotificationMessageTemplate> NotificationMessageTemplates { get; set; }
        public DbSet<NotificationReceiver> NotificationReceivers { get; set; }
        public DbSet<Configurations> Configuration { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(new User
            {
                Id = "3dae749f-5550-46db-9d82-409b76a67554",
                FirstName = "LastName",
                LastName = "Software",
                CreatedBy = "System",
                Email = "user@gmail.com",
                PIN="",
                Department="Marketer",
                
                //Signature="",

                

            });
            builder.Entity<Group>().HasData(new Group
            {
                Id = "5dcb05a3-4924-46b8-b561-0211d7473471",
                GroupName = "Account marketer",
                GroupEmail = "Account@gmail.com",
                CreatedBy = "System",
                ApprovalLevelId = "6de30bc2-8152-42f3-8765-2fc0f6c05cda",

            });
            builder.Entity<ApprovalLevel>().HasData(new ApprovalLevel
            {
                Id = "6de30bc2-8152-42f3-8765-2fc0f6c05cda",
                LevelName = "Level1",
                CreatedBy = "System",

            });
            builder.Entity<MailConfigurations>().HasData(
                new List<MailConfigurations>() {
                    new MailConfigurations() {
                        Id = "376e968d-ed6b-406e-abcb-f7561f68372d",
                        ApiKey = "5b8a6fd9-8e07-4748-affa-0dcc39bf6754",
                        DateCreated = DateTime.UtcNow,
                        ProviderType = EmailProviderType.ELASTICMAIL,

                        IsDefault = false, From = "Account@cyberspace.net.ng", FromName = "Marketer", MailProvider = "Elastic Mail", Password = "",
                        Url = "https://api.elasticemail.com/v2/email/send"
                    },
                    new MailConfigurations() {
                        Id = "e03fb776-6a0a-4423-a8b7-24c57570be24",
                        ApiKey = "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",
                        DateCreated = DateTime.UtcNow,
                        ProviderType = EmailProviderType.SENDGRID,
                        IsDefault = true, From = "Account@cyberspace.net.ng", FromName = "Marketer", MailProvider = "Elastic Mail", Password = "",
                        Url = "https://api.elasticemail.com/v2/email/send"
                    }
                });

        }

        public async Task<int> SaveChangesAsync()
        {
            //Set current user and datetime just before saving to db
            int saved = 0;
            var currentUser = "";
            if (contextAccessor != null && contextAccessor.HttpContext != null && contextAccessor.HttpContext.User != null)
            {
                currentUser = contextAccessor.HttpContext.User.Identity.Name;
            }
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {

                        entry.Entity.DateCreated = currentDate;
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;


                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;
                        if (entry.Entity.IsDeleted == true)
                        {
                            var IsDeletedOriginalValue = entry.OriginalValues.GetValue<bool>("IsDeleted");
                            //If the avlues changed then item was just marked for deletion
                            if (IsDeletedOriginalValue != entry.Entity.IsDeleted)
                            {
                                entry.Entity.DateDeleted = currentDate;
                                entry.Entity.DeletedBy = currentUser;
                            }

                        }
                    }
                }
                saved = await base.SaveChangesAsync();
            }
            catch (Exception Ex)
            {

            }

            return saved;
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //Set current user and datetime just before saving to db
            int saved = 0;
            var currentUser = "";
            if (contextAccessor != null && contextAccessor.HttpContext != null && contextAccessor.HttpContext.User != null)
            {
                currentUser = contextAccessor.HttpContext.User.Identity.Name;
            }
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {

                    if (entry.State == EntityState.Added)
                    {

                        entry.Entity.DateCreated = currentDate;
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;


                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;
                        if (entry.Entity.IsDeleted == true)
                        {
                            var IsDeletedOriginalValue = entry.OriginalValues.GetValue<bool>("IsDeleted");
                            //If the avlues changed then item was just marked for deletion
                            if (IsDeletedOriginalValue != entry.Entity.IsDeleted)
                            {
                                entry.Entity.DateDeleted = currentDate;
                                entry.Entity.DeletedBy = currentUser;
                            }

                        }
                    }
                }
                saved = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            return saved;
        }



    }
}
