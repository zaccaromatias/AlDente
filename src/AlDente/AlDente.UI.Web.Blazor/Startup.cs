using AlDente.Contracts.EstadosClientes;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.EstadosClientes;
using AlDente.Services.EstadosClientes;
using AlDente.UI.Web.Blazor.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AlDente.UI.Web.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            RepoDb.SqlServerBootstrap.Initialize();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEstadoClienteRepository, EstadoClienteRepository>();
            services.AddScoped<IEstadoClienteService, EstadoClienteService>();

            services.Configure<AppSettings>(options =>
            {
                options.CacheItemExpiration = 0;
                options.CommandTimeout = 0;
                options.ConnectionString = Configuration.GetConnectionString("DefaultDataBase");

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
