using SensoresAPP.Interfaces;
using Domain.Interfaces;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SensoresAPP.SensoresService;

namespace Sensores.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<ISensorService, SensorService>();    

            #endregion

            #region Repositories

            services.AddScoped<ISensorRepository, SensorRepository>();

            #endregion
        }
    }
}
