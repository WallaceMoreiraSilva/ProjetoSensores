using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ProjetoDDD.Sensores.Application.AutoMapper;
using ProjetoDDD.Sensores.Domain;
using ProjetoDDD.Sensores.Infra.Data.Context;

namespace ProjetoDDD.Sensores.Presentation
{
    public class Startup
    {
        private IServiceCollection _services;
        private Configuracao _configuracao;
        public IConfiguration Configuration;
        private IServiceCollection ServiceCollection { get => _services;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            _configuracao = new Configuracao();
            Configuration.Bind(_configuracao);
            _configuracao.ValidarConfiguracao();
            services.Configure<Configuracao>(Configuration);

            services.AdicionarLog4Net(_configuracao.CaminhoArquivoLog);
            services.AdicionarServicosBaseLog();

            services.AddControllersWithViews();

            services.AddAuthentication("CookieSeriesAuth")
                .AddCookie("CookieSeriesAuth", opt =>
                {
                    opt.Cookie.Name = "SeriesAuthCookie";
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", p => p.RequireRole("SecretRole"));
                opt.AddPolicy("AdvancedUser", p => p.RequireRole("Student"));
            });

            services.AddDbContext<ContextBase>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ProjetoModeloDDD")).EnableSensitiveDataLogging());
            
            services.AddAutoMapper(typeof(AutoMapperSetup));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

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
