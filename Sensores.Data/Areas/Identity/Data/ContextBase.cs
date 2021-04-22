using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoDDD.Sensores.Domain.Entities;

namespace ProjetoDDD.Sensores.Infra.Data.Areas.Identity.Data
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options)
            : base(options) { }

        #region DbSets

        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<EventoDisparado> EventoDisparados { get; set; }
        public DbSet<LogAuditoria> LogAuditorias { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);           
        }
    }
}
