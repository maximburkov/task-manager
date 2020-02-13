using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((context, config) =>
                //    {
                //        if (context.HostingEnvironment.IsProduction())
                //        {
                //            config.AddAzureKeyVault($"https://taskmanager.vault.azure.net/", "6cbbb013-1931-4f5c-aa9e-c9dcbe717be4", "nfJRq/EV.aN:2c6SRPd.faYtpZIds4Y7");
                //        }
                //    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                 })
                .UseNLog();

        //private static string GetKeyVaultEndpoint() => "https://taskmanager.vault.azure.net";
    }
}
