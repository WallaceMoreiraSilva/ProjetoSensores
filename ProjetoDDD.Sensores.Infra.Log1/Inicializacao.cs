using log4net.Config;
using log4net.Core;
using Microsoft.Extensions.DependencyInjection;
using NDD.MicroServico.Base.Logs;
using ProjetoDDD.Sensores.Application.Interfaces;
using SimpleInjector;
using System;
using System.IO;
using System.Reflection;

namespace ProjetoDDD.Sensores.CrossCutting.Log
{
    public static class Inicializacao
    {
        public static IServiceCollection AdicionarLog4Net(this IServiceCollection services, string fileName)
        {
            CriarLogManagerPorArquivoConfiguracao(fileName);
            services.AddTransient((Func<IServiceProvider, IGestorLog>)((IServiceProvider c) => new Log4NetGestorLog()));
            return services;
        }

        public static IServiceCollection AdicionarLog4NetFramework(this IServiceCollection services, string fileName)
        {
            CriarLogManagerPorArquivoConfiguracaoFramework(fileName);
            services.AddTransient((Func<IServiceProvider, IGestorLog>)((IServiceProvider c) => new Log4NetGestorLog()));
            return services;
        }

        private static bool NomeArquivoConfiguracaoIgualNuloouVazio(string fileName)
        {
            return string.IsNullOrEmpty(fileName);
        }

        public static Container AdicionarLog4Net(this Container container, string fileName)
        {
            CriarLogManagerPorArquivoConfiguracao(fileName);
            container.Register<IGestorLog, Log4NetGestorLog>();
            return container;
        }
    }
}
