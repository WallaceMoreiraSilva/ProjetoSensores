//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using ProjetoDDD.Sensores.Infra.Data.Areas.Identity.Data;

//[assembly: HostingStartup(typeof(ProjetoDDD.Sensores.Infra.Data.Areas.Identity.IdentityHostingStartup))]
//namespace ProjetoDDD.Sensores.Infra.Data.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) =>
//            {
//                services.AddDbContext<ContextBase>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("ProjetoSensores")));

//                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                    .AddEntityFrameworkStores<ContextBase>();
//            });
//        }
//    }
//}