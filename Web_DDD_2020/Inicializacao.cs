using Microsoft.Extensions.DependencyInjection;
using ProjetoDDD.Sensores.Application.Interfaces;
using System;

namespace ProjetoDDD.Sensores.Presentation
{
    public static class Inicializacao
    {
        public static IServiceCollection AdicionarServicosBaseLog(this IServiceCollection services, OpcoesBaseLog opcoes = null)
        {
            services.AddScoped<ILogComIdentificadorUnico, LogComIdentificadorUnicoImpl>((IServiceProvider c) => new LogComIdentificadorUnicoImpl(c.GetService<IGestorLog>(), opcoes));
            return services;
        }
    }
}