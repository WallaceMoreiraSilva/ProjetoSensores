using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceSensor;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppSensor : InterfaceSensorApp
    {
        ISensor _ISensor;

        public AppSensor(ISensor ISensor)
        {
            _ISensor = ISensor;
        }

        public async Task Add(Sensor Objeto)
        {
            await _ISensor.Add(Objeto);
        }

        public async Task Delete(Sensor Objeto)
        {
            await _ISensor.Delete(Objeto);
        }

        public async Task<Sensor> GetEntityById(int Id)
        {
            return await _ISensor.GetEntityById(Id);
        }

        public async Task<List<Sensor>> List()
        {
            return await _ISensor.List();
        }

        public async Task Update(Sensor Objeto)
        {
            await _ISensor.Update(Objeto);
        }
    }
}
