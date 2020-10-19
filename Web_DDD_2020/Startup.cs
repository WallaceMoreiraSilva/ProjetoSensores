using SensoresAPP.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.Generics;
using Infra.Repositories;
using Infra.Repository.Generics;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sensores.IoC;
using SensoresAPP.SensoresService;

namespace ProjetoDDD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextBase>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ProjetoModeloDDD")).EnableSensitiveDataLogging());
            NativeInjector.RegisterServices(services);

            services.AddSingleton(typeof(IGenericsRepository<>), typeof(GenericsRepository<>));

            services.AddSingleton<ISensorRepository, SensorRepository>();
            services.AddSingleton<ISensorService, SensorService>();

            services.AddSingleton<IRegiaoRepository, RegiaoRepository>();
            services.AddSingleton<IRegiaoService, RegiaoService>();

            services.AddSingleton<IPaisRepository, PaisRepository>();
            services.AddSingleton<IPaisService, PaisService>();

            services.AddSingleton<IEventoDisparadoRepository, EventoDisparadoRepository>();
            services.AddSingleton<IEventoDisparadoService, EventoDisparadoService>();

            services.AddSingleton<IStatusEventoDisparadoRepository, StatusEventoDisparadoRepository>();
            services.AddSingleton<InterfaceStatusEventoDisparadoApp, StatusEventoDisparadoService>();

            services.AddSingleton<IStatusSensorRepository, StatusSensorRepository>();
            services.AddSingleton<InterfaceStatusSensorApp, StatusSensorService>();

            services.AddControllersWithViews();           
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Sensor}/{action=Index}/{id?}");
            });
        }

    }
}
