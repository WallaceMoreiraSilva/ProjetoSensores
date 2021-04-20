using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoDDD.Sensores.Application.AutoMapper;
using ProjetoDDD.Sensores.Infra.Data.Context;
using ProjetoDDD.Sensores.Infra.IOC.NativeInjector;

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
            opt.AddPolicy("Admin", p => p.RequireRole("SecretRole"));
            opt.AddPolicy("AdvancedUser", p => p.RequireRole("Student"));
        });

        services.AddDbContext<ContextBase>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ProjetoModeloDDD")).EnableSensitiveDataLogging());

        NativeInjector.RegisterServices(services);

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