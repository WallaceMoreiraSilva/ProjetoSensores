using Domain.Entities;
using Domain.Interfaces;
using Infra.Configuration;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class RegiaoRepository : GenericsRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(ContextBase context)
           : base(context) { }

    }
}
