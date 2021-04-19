using Microsoft.Extensions.DependencyInjection;

namespace ProjetoDDD.Sensores.Presentation
{
    public interface IStartup
    {
        void ConfigurarServicos(IServiceCollection services);
    }
}
