using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<EventoDisparado> EventoDisparados { get; set; }
        public DbSet<StatusEventoDisparado> StatusEventoDisparados { get; set; }
        public DbSet<StatusSensor> StatusSensores{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
            base.OnConfiguring(optionsBuilder);
        }

        private string GetStringConectionConfig()
        {
            string strCon = "Data Source=.\\SQLEXPRESS;Initial Catalog=ProjetoModeloDDD;Integrated Security=False;User ID=sa;Password=123456;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            return strCon;
        }
    }
}
