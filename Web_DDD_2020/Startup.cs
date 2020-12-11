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
                //Policies (Políticas) => são aplicações de Roles e Claims para determinar contratos de acessos a determinadas áreas de uma aplicação.
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

            //tem que ficar antes de UseAuthorization. Você só pode autorizar depois de autenticar
            //Autorização é ter permissão para você fazer algo que você quer => Ex: Acessa a página de administração (só acessa aqui quem tem autorização)
            //Autenticação é você confirmar que você é você  =>  Ex: quando você preenche usuario e senha => você esta autenticando
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
