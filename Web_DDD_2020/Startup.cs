using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceEventoDisparado;
using Domain.Interfaces.InterfacePais;
using Domain.Interfaces.InterfaceRegiao;
using Domain.Interfaces.InterfaceSensor;
using Domain.Interfaces.InterfaceStatusEventoDisparado;
using Domain.Interfaces.InterfaceStatusSensor;
using Infrastructure.Repository.Generics;
using Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web_DDD_2020
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));

            services.AddSingleton<ISensor, RepositorySensor>();
            services.AddSingleton<InterfaceSensorApp, AppSensor>();

            services.AddSingleton<IRegiao, RepositoryRegiao>();
            services.AddSingleton<InterfaceRegiaoApp, AppRegiao>();

            services.AddSingleton<IPais, RepositoryPais>();
            services.AddSingleton<InterfacePaisApp, AppPais>();

            services.AddSingleton<IEventoDisparado, RepositoryEventoDisparado>();
            services.AddSingleton<InterfaceEventoDisparadoApp, AppEventoDisparado>();

            services.AddSingleton<IStatusEventoDisparado, RepositoryStatusEventoDisparado>();
            services.AddSingleton<InterfaceStatusEventoDisparadoApp, AppStatusEventoDisparado>();

            services.AddSingleton<IStatusSensor, RepositoryStatusSensor>();
            services.AddSingleton<InterfaceStatusSensorApp, AppStatusSensor>();

            services.AddControllersWithViews();           
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
