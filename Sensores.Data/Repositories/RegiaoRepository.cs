using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Domain.Interfaces;
using ProjetoDDD.Sensores.Infra.Data.Context;
using ProjetoDDD.Sensores.Infra.Data.Repository.Generics;

namespace ProjetoDDD.Sensores.Infra.Data.Repositories
{
    public class RegiaoRepository : GenericsRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(ContextBase context)
           : base(context) { }

    }
}
