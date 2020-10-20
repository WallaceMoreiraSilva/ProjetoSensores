using Domain.Entities;
using Domain.Interfaces;
using Infra.Configuration;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class StatusSensorRepository : GenericsRepository<StatusSensor>, IStatusSensorRepository
    {
        public StatusSensorRepository(ContextBase context)
           : base(context) { }

    }
}
