using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> option)
            : base(option) { }

        #region DbSets

        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<EventoDisparado> EventoDisparados { get; set; }
        public DbSet<StatusEventoDisparado> StatusEventoDisparados { get; set; }
        public DbSet<StatusSensor> StatusSensores{ get; set; }
        public DbSet<LogAuditoria> LogAuditorias { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
