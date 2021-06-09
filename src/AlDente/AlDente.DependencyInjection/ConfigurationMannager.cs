using AlDente.Contracts.EstadosClientes;
using AlDente.DataAccess.Core;
using AlDente.Services.EstadosClientes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace AlDente.DependencyInjection
{
    public static class ConfigurationMannager
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEstadoClienteService, EstadoClienteService>();

            services.Configure<AppSettings>(options => configuration.GetSection("MySettings").Bind(options));
        }

        public static void Configure(IApplicationBuilder app)
        {

        }
    }
}
