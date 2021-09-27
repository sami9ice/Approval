using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace NewCustomerActivationProcess
{
    public class Program
    {
        public static void Main(string[] args)
         {
            // CreateHostBuilder(args).Build().Run();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            var services = new ServiceCollection();

            services.AddSerilogLogger(config);
            var logger = services.BuildServiceProvider().GetService<ILogger>();
            try
            {
                logger.Information("Application Starting.");
                CreateHostBuilder(args).Build().Run();
               // CreateWebHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                logger.Fatal(ex, "The Application failed to start.");

            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

    //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //WebHost.CreateDefaultBuilder(args)
    //    .ConfigureLogging((hostingContext, logging) =>
    //    {
    //        logging.AddConsole();
    //        logging.AddDebug();
    //    })
    //    .UseStartup<Startup>();


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
                {
                webBuilder.UseStartup<Startup>();
        });
    }
}
