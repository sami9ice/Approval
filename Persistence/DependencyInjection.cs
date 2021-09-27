using Application.Common.Passwordgenerator;
using Application.Interfaces;
using Application.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Persistence.Uploads;

namespace Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                options.UseOpenIddict();
            });
            services.AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            //services.AddScoped<IGeneratorService, GeneratorService>();
            //services.AddScoped<IGeneratePassword, GeneratePasword>();
            //services.AddScoped<IExcelService, ExcelService>();

        }
    }
}
