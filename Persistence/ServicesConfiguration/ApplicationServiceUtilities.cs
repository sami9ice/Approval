
using Application.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Domain.User;

namespace Application
{
    /// <summary>
    /// Register Dependency for application layer
    /// </summary>
    public static class ApplicationServiceUtilities
    {
        /// <summary>
        /// Extension method to register application dependency
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationServiceUtilities(this IServiceCollection services, IConfiguration configuration)
        {
            //add repos
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //add factory
           // services.AddScoped(typeof(IGenericObjectFactory<,>), typeof(GenericObjectFactory<,>));

            services.AddScoped<IdentityDbContext<User, Group, string>, ApplicationDbContext>();
            //add automapper
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            //configure Workflow settings
            var mySection = configuration.GetSection("WorkflowApprovalSettings");
            //services.Configure<WorkflowApprovalSettings>(w => mySection.Bind(w));
            //services.Configure<WorkflowApprovalSettings>(Configuration.GetSection("WorkflowApprovalSettings"));
            //end
        }
    }
}