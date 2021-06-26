using AlDente.UI.Web.Blazor.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            var host = builder.Build();

            IServiceProvider serviceProvider = host.Services.GetRequiredService<IServiceProvider>();
            using (var s = serviceProvider.CreateScope())
            {
                IAuthenticationClientService authenticationClientService = s.ServiceProvider.GetRequiredService<IAuthenticationClientService>();
                await authenticationClientService.Initialize();
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
