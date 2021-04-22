using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Domain.Interfaces;
using ProjetoDDD.Sensores.Infra.Data.Areas.Identity.Data;
using ProjetoDDD.Sensores.Infra.Data.Repository.Generics;

namespace ProjetoDDD.Sensores.Infra.Data.Repositories
{
    public class PaisRepository : GenericsRepository<Pais>, IPaisRepository
    {
        public PaisRepository(ContextBase context)
           : base(context) { }

    }
}
