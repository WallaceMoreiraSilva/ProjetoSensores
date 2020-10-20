using Domain.Entities;
using Domain.Interfaces;
using Infra.Configuration;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class PaisRepository : GenericsRepository<Pais>, IPaisRepository
    {
        public PaisRepository(ContextBase context)
           : base(context) { }

    }
}
