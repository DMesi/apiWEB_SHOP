using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string LogFolder = Environment.CurrentDirectory + "\\Content\\Log\\log-.txt";

           Log.Logger = new LoggerConfiguration()
            .WriteTo.File(
               path: LogFolder,
               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}]{Message:lj}{NewLine}{Exception}",
               rollingInterval: RollingInterval.Day,
               restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
            ).CreateLogger();

            try
            {
                Log.Information("Aplikacija pocinje sa radom");
                CreateHostBuilder(args).Build().Run();  // ovo je samo po defaultu bilo
            }

            catch (Exception ex)
            {
                Log.Fatal(ex, "Neka greska");
            }

            finally
            {
                Log.CloseAndFlush();
            }

        
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() // dodato za LOG
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
