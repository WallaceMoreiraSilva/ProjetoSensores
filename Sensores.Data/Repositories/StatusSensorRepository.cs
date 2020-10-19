using Domain.Entities;
using Domain.Interfaces;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class StatusSensorRepository : GenericsRepository<StatusSensor>, IStatusSensorRepository
    {

    }
}
