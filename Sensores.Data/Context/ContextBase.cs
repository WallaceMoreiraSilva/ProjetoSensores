using ProjetoDDD.Sensores.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjetoDDD.Sensores.Infra.Data.Context
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> option)
            : base(option) { }

        #region DbSets

        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<EventoDisparado> EventoDisparados { get; set; }       
        public DbSet<LogAuditoria> LogAuditorias { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
