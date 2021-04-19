using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NGConnector.App.Repositorio.MSSql.Settings
{
    public static class SettingsExtensions
    {
        public static T LoadSettings<T>(this IConfiguration configuration, IServiceCollection service, string sectionName) where T : class
        {
            IConfigurationSection sections = configuration.GetSection(sectionName);
            service.Configure<T>(sections);

            T settings = sections.Get<T>();
            return settings;
        }

        public static T LoadSettings<T>(this IConfiguration configuration, string sectionName) where T : class
        {
            IConfigurationSection sections = configuration.GetSection(sectionName);
            T settings = sections.Get<T>();
            return settings;
        }

    }
}