using Microsoft.Extensions.DependencyInjection;
using ProjetoDDD.Sensores.Application.Interfaces;
using ProjetoDDD.Sensores.Application.Services;
using ProjetoDDD.Sensores.Domain.Interfaces;
using ProjetoDDD.Sensores.Domain.Interfaces.Generics;
using ProjetoDDD.Sensores.Infra.Data.Repositories;
using ProjetoDDD.Sensores.Infra.Data.Repository.Generics;

namespace ProjetoDDD.Sensores.Infra.IOC.NativeInjector
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services          

            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IRegiaoService, RegiaoService>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IEventoDisparadoService, EventoDisparadoService>();   

            #endregion

            #region Repositories

            services.AddScoped(typeof(IGenericsRepository<>), typeof(GenericsRepository<>));     
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IRegiaoRepository, RegiaoRepository>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IEventoDisparadoRepository, EventoDisparadoRepository>();

            #endregion
            
        }
    }
}