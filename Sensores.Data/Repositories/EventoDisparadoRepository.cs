using Domain.Entities;
using Domain.Interfaces;
using Infra.Configuration;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class EventoDisparadoRepository : GenericsRepository<EventoDisparado>, IEventoDisparadoRepository
    {
        public EventoDisparadoRepository(ContextBase context)
           : base(context) { }

    }
}
