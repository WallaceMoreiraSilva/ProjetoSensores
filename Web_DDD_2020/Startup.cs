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

            services.AddAuthentication("CookieSeriesAuth")
                .AddCookie("CookieSeriesAuth", opt =>
               {
                   opt.Cookie.Name = "SeriesAuthCookie";
               });

            services.AddAuthorization(opt =>
            {
                //Policies (Pol�ticas) => s�o aplica��es de Roles e Claims para determinar contratos de acessos a determinadas �reas de uma aplica��o.
                opt.AddPolicy("Admin", p => p.RequireRole("SecretRole"));
                opt.AddPolicy("AdvancedUser", p => p.RequireRole("Student"));
            });

            services.AddDbContext<ContextBase>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ProjetoModeloDDD")).EnableSensitiveDataLogging());

            NativeInjector.RegisterServices(services);

            services.AddAutoMapper(typeof(AutoMapperSetup));
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

            //tem que ficar antes de UseAuthorization. Voc� s� pode autorizar depois de autenticar
            //Autoriza��o � ter permiss�o para voc� fazer algo que voc� quer => Ex: Acessa a p�gina de administra��o (s� acessa aqui quem tem autoriza��o)
            //Autentica��o � voc� confirmar que voc� � voc�  =>  Ex: quando voc� preenche usuario e senha => voc� esta autenticando
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Sensor}/{action=Index}/{id?}");

                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
