using ProjetoDDD.Sensores.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjetoDDD.Sensores.Infra.Data.Context
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> option)
            : base(option) { }

        #region DbSets

        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Pais> Paises { get; set; }          
        public DbSet<Log> Logs { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
