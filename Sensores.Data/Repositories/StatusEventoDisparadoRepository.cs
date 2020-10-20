using Domain.Entities;
using Domain.Interfaces;
using Infra.Configuration;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class StatusEventoDisparadoRepository : GenericsRepository<StatusEventoDisparado>, IStatusEventoDisparadoRepository
    {
        public StatusEventoDisparadoRepository(ContextBase context)
           : base(context) { }

    }
}
