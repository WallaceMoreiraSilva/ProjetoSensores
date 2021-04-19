using Microsoft.Extensions.DependencyInjection;

namespace ProjetoDDD.Sensores.Infra.Persistencia
{
    public static class Inicializacao
    {
        public static IServiceCollection AdicionarPersistencia(this IServiceCollection services, string connectionString)
        {          
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IRegiaoService, RegiaoService>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IEventoDisparadoService, EventoDisparadoService>();           

            services.AddScoped(typeof(IGenericsRepository<>), typeof(GenericsRepository<>));
            services.AddScoped(typeof(IGenericsLogAuditoriaRepository<>), typeof(GenericsLogAuditoriaRepository<>));
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IRegiaoRepository, RegiaoRepository>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IEventoDisparadoRepository, EventoDisparadoRepository>();  
         
            return services;
        }
    }
}
