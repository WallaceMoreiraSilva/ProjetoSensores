using Domain.Entities;
using Domain.Interfaces;
using Infra.Configuration;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class SensorRepository : GenericsRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(ContextBase context)
           : base(context) { }

    }
}
