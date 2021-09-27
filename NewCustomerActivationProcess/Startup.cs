using Application;
using Application.Common.Passwordgenerator;
using Application.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewCustomerActivationProcess.Exception;
using Persistence;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace NewCustomerActivationProcess
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var path = @"C:\\DataStore";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var directory = new DirectoryInfo(path);
            // var file = $"{directory.FullName}\\publiciteCert.pfx";
            // var password = "S)C1@lc)G@dm1n";
            services.AddDataProtection()
                   //.ProtectKeysWithCertificate(new X509Certificate2(file,password))
                   .PersistKeysToFileSystem(directory);
            //.ProtectKeysWithDpapiNG();
            services.AddDistributedMemoryCache();
            services.AddControllers().AddFluentValidation(a=> { a.AutomaticValidationEnabled = false; });
            services.AddApplication();
           // services.AddTransient<IGeneratePassword, GeneratePasword>();

            services.AddApplicationServiceUtilities(Configuration);
            services.AddPersistence(Configuration);
            services.AddApiVersion();
            services.AddDocSwagger();
            services.AddSerilogLogger(Configuration);
            services.AddIdentity();
            services.AddCors();

            services.AddHttpClient("HttpClient").AddPolicyHandler(x =>
            {
                return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(3, retry)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,  ILogger logger, ApplicationDbContext context)
        {

            if (env.IsProduction())
            {
                try
                {
                    var ctx = context.GetInfrastructure().GetService<IMigrator>();
                    //  ctx.GenerateScript();
                    // seeder.SeedAsync(app).Wait();
                }
                catch (System.Exception exception)
                {
                    logger.Error(exception.Message);
                }
                app.UseCors(x =>
                {
                    x.WithOrigins("http://41.138.191.3", "http://10.30.2.130", "http://41.138.171.45", "http://41.138.191.5");
                    x.AllowAnyMethod();
                    x.AllowAnyHeader();
                });
            }
            else if (env.IsStaging())
            {
                app.UseCors(x =>
                {
                    x.WithOrigins("http://41.138.191.5", "http://localhost:7000", "http://41.138.171.45");
                    x.AllowAnyMethod();
                    x.AllowAnyHeader();
                });
                // context.Database.Migrate();
            }
            else
            {
                app.UseCors(x =>
                {
                    x.WithOrigins("*");
                    x.AllowAnyMethod();
                    x.AllowAnyHeader();
                });
            }
            app.UseDocSwagger();
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseHttpsRedirection();

                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseRouting();

           app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
