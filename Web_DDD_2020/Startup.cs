using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Sensores.IoC;
using AutoMapper;
using SensoresApp.AutoMapper;

namespace ProjetoDDD
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();    

            services.AddDbContext<ContextBase>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ProjetoModeloDDD")).EnableSensitiveDataLogging());

            NativeInjector.RegisterServices(services);

            services.AddAutoMapper(typeof(AutoMapperSetup));

            
            //#region Services

            //services.AddSingleton<ISensorService, SensorService>();         
            //services.AddSingleton<IRegiaoService, RegiaoService>();           
            //services.AddSingleton<IPaisService, PaisService>();           
            //services.AddSingleton<IEventoDisparadoService, EventoDisparadoService>();           
            //services.AddSingleton<IStatusEventoDisparadoService, StatusEventoDisparadoService>();            
            //services.AddSingleton<IStatusSensorService, StatusSensorService>();

            //#endregion

            //#region Repository

            //services.AddSingleton(typeof(IGenericsRepository<>), typeof(GenericsRepository<>));

            //services.AddSingleton<ISensorRepository, SensorRepository>();
            //services.AddSingleton<IRegiaoRepository, RegiaoRepository>();
            //services.AddSingleton<IPaisRepository, PaisRepository>();
            //services.AddSingleton<IEventoDisparadoRepository, EventoDisparadoRepository>();
            //services.AddSingleton<IStatusEventoDisparadoRepository, StatusEventoDisparadoRepository>();
            //services.AddSingleton<IStatusSensorRepository, StatusSensorRepository>();

            //#endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
