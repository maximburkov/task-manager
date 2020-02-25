using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (!context.HostingEnvironment.IsDevelopment())
                    {
                        string uri = $"https://{Environment.GetEnvironmentVariable("KEY_VAULT_NAME")}.vault.azure.net/";
                        string clientId = Environment.GetEnvironmentVariable("KEY_VAULT_CLIENT_ID");
                        string clientSecret = Environment.GetEnvironmentVariable("KEY_VAULT_CLIENT_SECRET");

                        config.AddAzureKeyVault(uri, clientId, clientSecret, new DefaultKeyVaultSecretManager());
                    }
                })
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
    }
}
