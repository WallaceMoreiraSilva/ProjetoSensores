using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceStatusSensor;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class StatusSensorService : InterfaceStatusSensorApp
    {
        IStatusSensor _IStatusSensor;

        public StatusSensorService(IStatusSensor IStatusSensor)
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
