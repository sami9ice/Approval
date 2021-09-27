using NewCustomerActivationProcess.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenIddict.Core;
using MediatR;
using Application.Request.Group;
using Application.Request.User;
using Microsoft.Extensions.Logging;

namespace NewCustomerActivationProcess
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.IHostedService" />
    public class Worker : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;
        private class SystemConstants
        {
            public const string SuperAdmin = "superAdmin";
            public const string UserRole = "Account";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="configuration">The configuration.</param>
        public Worker(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }
        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Worker>>();
            var accountManager = scope.ServiceProvider.GetRequiredService<IMediator>();
            await context.Database.EnsureCreatedAsync(cancellationToken);
            //await context.Database.MigrateAsync(cancellationToken: cancellationToken);
            // Note: when using a custom entity or a custom key type, replace OpenIddictApplication by the appropriate type.
            var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();
            var scopeManager = scope.ServiceProvider.GetRequiredService<OpenIddictScopeManager<OpenIddictScope>>();
            var clientList = new List<Clients>();
            configuration.Bind("Clients", clientList);
            if (clientList.Any())
            {
                foreach (var item in clientList)
                {
                    if (await manager.FindByClientIdAsync(item.ClientId, cancellationToken) == null)
                    {
                        await manager.CreateAsync(new OpenIddictApplicationDescriptor
                        {
                            ClientId = item.ClientId,
                            ClientSecret = item.ClientSecret,
                            DisplayName = item.DisplayName,
                            Permissions =
                            {
                                OpenIddictConstants.Permissions.Endpoints.Token,
                                OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                                OpenIddictConstants.Permissions.Endpoints.Authorization,
                                OpenIddictConstants.Permissions.Endpoints.Logout,
                                OpenIddictConstants.Permissions.Endpoints.Introspection
                            },
                            RedirectUris = { new Uri(item.RedirectUris) }
                        }, cancellationToken);
                    }
                }
            }
            var clientResourceList = new List<ClientResource>();
            configuration.Bind("ApiResourceScope", clientResourceList);
            foreach (var item in clientResourceList)
            {
                if (await scopeManager.FindByNameAsync(item.Name, cancellationToken) == null)
                {
                    var descriptor = new OpenIddictScopeDescriptor
                    {
                        Name = item.Name,
                        Resources = { item.Resources }
                    };

                    await scopeManager.CreateAsync(descriptor, cancellationToken);
                }
            }
            //await EnsureRoleAsync(SystemConstants.UserRole, accountManager);

            //await EnsureRoleAsync(SystemConstants.SuperAdmin, "SuperAdmin@gmail.com", accountManager);
            //await EnsureRoleAsync(SystemConstants.UserRole, "UserRole@gmail.com", accountManager);

            //string superAdminUsername = "superAdmin";
            //string userName = "inbuiltUser";

            //if (!await context.Users.AnyAsync(x => string.Equals(x.UserName, superAdminUsername)))
            //{
            //    logger.LogInformation("Generating inbuilt accounts for super admin user");

            //    await CreateUserAsync(superAdminUsername, "Samuel Agwogie", "P@ssw0rd123", "Samuel.Agwogie@cyberspace.net.ng", "+234 8166207784", "f4a334ce-5af3-47ba-9196-e7d2d704e35e", accountManager, SystemConstants.SuperAdmin.ToString());

            //    logger.LogInformation("Inbuilt super admin account generation completed");
            //}
            //if (!await context.Users.AnyAsync(x => string.Equals(x.UserName, userName)))
            //{
            //    logger.LogInformation("Generating inbuilt accounts for user");

            //    await CreateUserAsync(userName, "Ola olamide", "P@ssw0rd123", "Olamide.Onakoya@cyberspace.net.ng", "+234 8166207784", "6de30bc2-8152-42f3-8765-2fc0f6c05cda", accountManager, SystemConstants.UserRole.ToString());

            //    logger.LogInformation("Inbuilt super admin account generation completed");
            //}
        }

        private async Task EnsureRoleAsync(string groupName,string Email, IMediator accountManager)
        {
            await accountManager.Send(new CreateGroupRequest { GroupName = groupName ,GroupEmail=Email});
        }

        //private async Task CreateUserAsync(string userName, string fullname, string password, string email, string phoneNumber, string GroupId, IMediator accountManager,string GroupName)
        //{
        //    var result = await accountManager.Send(new CreateUserRequest { FirstName = userName, Fullname = fullname, EmialAddress = email, PIN = password, groupId = GroupId });

        //    //if (!result.succeed)
        //    //    throw new System.Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.message)}");
        //}


        //var roles = configuration.GetValue<string[]>("Roles:Default") ?? new string[] { };
        //    foreach (string role in roles)
        //    {
        //        var roleStore = new RoleStore<Role>(context);

        //        if (!context.Roles.Any(r => r.Name == role))
        //        {
        //            await roleStore.CreateAsync(new Role() { Name = role, NormalizedName = role }, cancellationToken);
        //        }
        //    }
        //    await context.SaveChangesAsync(cancellationToken);
        //}

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns></returns>

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
