using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Api.Core.SolarMeasurements
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogging();

            try
            {
                //CreateWebHostBuilder(args).Build().Run();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Exception: {e}");
                throw;
            }
            finally
            {
                // Close and flush the log.
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        private static void ConfigureLogging()
        {
            Console.WriteLine("Initialising logger...");
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(
                    LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                .WriteTo.RollingFile("/tmp/SolarMonitor/Api.Core.SolarMeasurements.log")
                .CreateLogger();        
        }

//        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//                .UseSerilog()
//                .UseStartup<Startup>();
    }
}
