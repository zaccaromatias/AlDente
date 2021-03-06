using AlDente.Contracts.Beneficios;
using AlDente.Contracts.Clientes;
using AlDente.Contracts.Core;
using AlDente.Contracts.DiasLaborables;
using AlDente.Contracts.Empleados;
using AlDente.Contracts.Mesas;
using AlDente.Contracts.Opiniones;
using AlDente.Contracts.Reservas;
using AlDente.Contracts.Restaurantes;
using AlDente.Contracts.Sanciones;
using AlDente.Contracts.Turnos;
using AlDente.DataAccess.Beneficios;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.DiasLaborables;
using AlDente.DataAccess.Mesas;
using AlDente.DataAccess.Opiniones;
using AlDente.DataAccess.Reservas;
using AlDente.DataAccess.Restaurantes;
using AlDente.DataAccess.Sanciones;
using AlDente.DataAccess.Turnos;
using AlDente.DataAccess.Usuarios;
using AlDente.Services.Beneficios;
using AlDente.Services.Clientes;
using AlDente.Services.Core;
using AlDente.Services.DiasLaborables;
using AlDente.Services.Empleados;
using AlDente.Services.Mesas;
using AlDente.Services.Opiniones;
using AlDente.Services.Reservas;
using AlDente.Services.Restaurantes;
using AlDente.Services.Sanciones;
using AlDente.Services.Turnos;
using AlDente.UI.Web.Blazor.Data;
using AlDente.UI.Web.Blazor.Helpers;
using AlDente.UI.Web.Blazor.Models;
using AlDente.UI.Web.Blazor.Services;
using BlazorBrowserStorage;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using System;


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
            services.AddServerSideBlazor()
                .AddHubOptions(o => { o.MaximumReceiveMessageSize = 102400000; });
            services.AddSingleton<WeatherForecastService>();
            services.AddBlazorBrowserStorage();
            services.AddHttpContextAccessor();
            services.AddScoped<SessionData>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
            });

            AddUIServices(services);
            AddCustomServices(services);
            AddRepositories(services);
            AddValidators(services);
            services.AddAuthorizationCore();
            services.AddSyncfusionBlazor();
        }

        private void AddValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddTransient<IValidator<RegisterModel>, RegisterModelValidator>();
            services.AddTransient<IValidator<MesaDTO>, MesaDTOValidator>();
            services.AddTransient<IValidator<EmpleadoDTO>, EmpleadoDTOValidator>();
            services.AddTransient<IValidator<RestauranteDTO>, RestauranteDTOValidator>();
            services.AddTransient<IValidator<TurnoDTO>, TurnoDTOValidator>();
            services.AddTransient<IValidator<DiaLaboralDTO>, DiaLaboralDTOValidator>();
            services.AddTransient<IValidator<ReservaACancelarDTO>, ReservaACancelarDTOValidator>();
            services.AddTransient<IValidator<OpinionDTO>, OpinionDTOValidator>();
            services.AddTransient<IValidator<ReservaACancelarDTO>, ReservaACancelarDTOValidator>();
            services.AddTransient<IValidator<TipoSancionDTO>, TipoSancionDTOValidator>();
            services.AddTransient<IValidator<TipoBeneficioDTO>, TipoBeneficioDTOValidator>();
            services.AddTransient<IValidator<PoliticaBeneficioDTO>, PoliticaBeneficioDTOValidator>();
            services.AddTransient<IValidator<PoliticaSancionDTO>, PoliticaSancionDTOValidator>();
        }

        private void AddRepositories(IServiceCollection services)
        {

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMesaRepository, MesaRepository>();
            services.AddScoped<ITurnoRepository, TurnoRepository>();
            services.AddScoped<IRestauranteRepository, RestauranteRepository>();
            services.AddScoped<IDiaLaboralRepository, DiaLaboralRepository>();
            services.AddScoped<IReservaMesaRepository, ReservaMesaRepository>();
            services.AddScoped<IOpinionRepository, OpinionRepository>();

            services.AddScoped<ITipoSancionRepository, TipoSancionRepository>();
            services.AddScoped<ITipoBeneficioRepository, TipoBeneficioRepository>();
            services.AddScoped<IPoliticaBeneficioRepository, PoliticaBeneficioRepository>();
            services.AddScoped<IPoliticaSancionRepository, PoliticaSancionRepository>();
            services.AddScoped<IBeneficioRepository, BeneficioRepository>();
            services.AddScoped<ISancionRepository, SancionRepository>();
        }

        private void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.Configure<AppSettings>(options =>
            {
                options.CacheItemExpiration = 0;
                options.CommandTimeout = 0;
                options.ConnectionString = Configuration.GetConnectionString("DefaultDataBase");
                options.RestauranteId = Configuration.GetValue<int>("RestauranteId");
                options.URL = Configuration.GetValue<string>("URL");

            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IMesaService, MesaService>();
            services.AddScoped<ITurnoService, TurnoService>();
            services.AddScoped<IEmpleadoService, EmpleadoService>();
            services.AddScoped<IRestauranteService, RestauranteService>();
            services.AddScoped<IDiaLaboralService, DiaLaboralService>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IOpinionService, OpinionService>();
            services.AddScoped<ITipoSancionService, TipoSancionService>();
            services.AddScoped<ITipoBeneficioService, TipoBeneficioService>();
            services.AddScoped<IPoliticaBeneficioService, PoliticaBeneficioService>();
            services.AddScoped<IPoliticaSancionService, PoliticaSancionService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBeneficioService, BeneficioService>();

            services.AddFluentEmail("no-reply@AlDente.com")
                .AddRazorRenderer()
                .AddSmtpSender("localhost", 25);
        }

        private void AddUIServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationClientService, AuthenticationClientService>();
            services.AddScoped<ClienteAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ClienteAuthenticationStateProvider>());
            services.AddScoped<IToastService, ToastService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            SyncfusionLicenseProvider.RegisterLicense("NTI1MzA5QDMxMzkyZTMzMmUzMFFlSkw5RVo1V2JHcUpNMGF6S0RqNC9NZU0yYkdPcGMwSTM0dG5IOGxXOTA9");
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
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

            });


        }
    }
}
