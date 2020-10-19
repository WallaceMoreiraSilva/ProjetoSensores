using SensoresAPP.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace SensoresAPP.SensoresService
{
    public class StatusSensorService : IStatusSensorService
    {
        IStatusSensorRepository _IStatusSensor;

        public StatusSensorService(IStatusSensorRepository IStatusSensor)
        {
            _IStatusSensor = IStatusSensor;
        }

        public async Task Add(StatusSensor Objeto)
        {
            await _IStatusSensor.Add(Objeto);
        }

        public async Task Delete(StatusSensor Objeto)
        {
            await _IStatusSensor.Delete(Objeto);
        }

        public async Task<StatusSensor> GetEntityById(int Id)
        {
            return await _IStatusSensor.GetEntityById(Id);
        }

        public async Task<List<StatusSensor>> List()
        {
            return await _IStatusSensor.List();
        }

        public async Task Update(StatusSensor Objeto)
        {
            await _IStatusSensor.Update(Objeto);
        }
    }
}
